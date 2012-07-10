// This is the main DLL file.

#include "stdafx.h"
#include "JimbeWiFi.h"
#include "WifiException.h"
#pragma comment(lib, "wlanapi.lib")

using namespace System::Net::NetworkInformation;
using namespace System::Runtime::InteropServices;

namespace Jimbe {
	namespace JimbeWiFi{

		JimbeWiFi::JimbeWiFi(){
			InitApi(JimbeWiFi::NativeApiVersion::windowsVista);
		}

		JimbeWiFi::JimbeWiFi(JimbeWiFi::NativeApiVersion version){
			InitApi(version);
		}

		void JimbeWiFi::InitApi(JimbeWiFi::NativeApiVersion version)
		{
			hClientHandle=INVALID_HANDLE_VALUE;
			dwNegotiatedVersion=0;
			DWORD dwResult=0;
			pin_ptr<DWORD> pdwNegotiatedVersion=&dwNegotiatedVersion;
			pin_ptr<HANDLE> phClientHandle=&hClientHandle;
			dwResult = WlanOpenHandle((DWORD)version, NULL, pdwNegotiatedVersion, phClientHandle);
			if (dwResult!=ERROR_SUCCESS)
				if (dwResult==ERROR_REMOTE_SESSION_LIMIT_EXCEEDED)
					throw gcnew WifiToManyHandleException("Too many handles have been issued by the server. Please call GC to release the resources");
				else throw gcnew WifiException("Error on open Wlan Api");
		}

		JimbeWiFi::~JimbeWiFi(void)
		{
			this->!JimbeWiFi();
		}

		JimbeWiFi::!JimbeWiFi()
		{
			DWORD dwResult=0;
			if (hClientHandle==INVALID_HANDLE_VALUE) 
				return;
			dwResult=WlanCloseHandle(hClientHandle,NULL);
			if (dwResult!=ERROR_SUCCESS)
				throw gcnew WifiException("Error on close Wlan Api");
		}


		IEnumerable<WifiInterface ^>^ JimbeWiFi::WiFiEnumInterfaces(void)
		{
			LinkedList<WifiInterface ^>^ interfacelist=nullptr;
			DWORD dwResult=0;
			//Variable used for WlanEnumInterfaces
			PWLAN_INTERFACE_INFO_LIST pIfList = NULL;
			PWLAN_INTERFACE_INFO pIfInfo = NULL;
			int i;

			dwResult = WlanEnumInterfaces(hClientHandle, NULL, &pIfList);
			if (dwResult != ERROR_SUCCESS)
				throw gcnew WifiException("Error on get available interfaces");
			interfacelist=gcnew LinkedList<WifiInterface ^>();
			for (i = 0; i < (int) pIfList->dwNumberOfItems; i++) {
				pIfInfo = (WLAN_INTERFACE_INFO *) &pIfList->InterfaceInfo[i];
				interfacelist->AddLast(
					gcnew WifiInterface(
					Marshal::PtrToStringAuto(static_cast<IntPtr>(pIfInfo->strInterfaceDescription)),
					FromGUID(pIfInfo->InterfaceGuid),
					(WifiInterface::WlanInterfaceState) pIfInfo->isState
					));
			}
			WlanFreeMemory(pIfList);
			return interfacelist;
		}

		IEnumerable<WifiNetwork ^>^ JimbeWiFi::WiFiGetAvailableNetworkList(WifiInterface ^ wInterface)
		{			
			//Variable used for WlanGetAvailableNetworkList
			PWLAN_AVAILABLE_NETWORK_LIST pBssList = NULL;
		    PWLAN_AVAILABLE_NETWORK pBssEntry = NULL;
			//Variable used for WlanGetNetworkBssList
			PWLAN_BSS_LIST pWlanBssList=NULL;
			PWLAN_BSS_ENTRY pWlanBssEntry=NULL;
			
			DWORD dwResult=0;		
			
			int i, j;
			
			LinkedList<WifiNetwork^>^ networks=nullptr;
			LinkedList<PhysicalAddress ^>^ maclist=nullptr;
			
			GUID interfaceGUID=ToGUID(wInterface->InterfaceGuid);

			dwResult = WlanGetAvailableNetworkList(hClientHandle,
                                             &interfaceGUID,
                                             0, 
                                             NULL, 
                                             &pBssList);
			if (dwResult!=ERROR_SUCCESS)
				throw gcnew WifiException("Error on get available networks");
			
			networks= gcnew LinkedList<WifiNetwork^>();
			bool duplicate;
			for (i=0; i<(int) pBssList->dwNumberOfItems; i++) {
				pBssEntry = (WLAN_AVAILABLE_NETWORK *) & pBssList->Network[i];
				
				String ^ssid=Marshal::PtrToStringAnsi(static_cast<IntPtr>(pBssEntry->dot11Ssid.ucSSID), static_cast<Int32>(pBssEntry->dot11Ssid.uSSIDLength));
				
				String ^profile= Marshal::PtrToStringAuto(static_cast<IntPtr>(pBssEntry->strProfileName));
				WifiNetwork::BSSType type=(WifiNetwork::BSSType) pBssEntry->dot11BssType;
				ULONG sigPower= pBssEntry->wlanSignalQuality;
				
				duplicate=false;
				for each (WifiNetwork^ net in networks) if (net->Ssid->Equals(ssid)) duplicate=true;  
				if (duplicate) continue;

				if (dwNegotiatedVersion==(int)NativeApiVersion::windowsVista) {
					maclist = gcnew LinkedList<PhysicalAddress^>();
					array<Byte> ^byteArray;

					dwResult=WlanGetNetworkBssList(hClientHandle,
													&interfaceGUID, 
													&pBssEntry->dot11Ssid, 
													pBssEntry->dot11BssType, 
													pBssEntry->bSecurityEnabled, 
													NULL, 
													&pWlanBssList);
					if (dwResult!=ERROR_SUCCESS)
						throw gcnew WifiException("Error on get network bssid");
					
					for (j=0; j<(int)pWlanBssList->dwNumberOfItems; j++) {
						pWlanBssEntry=pWlanBssList->wlanBssEntries+j;
						byteArray  = gcnew array<Byte>(6);
						Marshal::Copy((IntPtr)pWlanBssEntry->dot11Bssid, byteArray, 0, 6 );
						maclist->AddLast(gcnew PhysicalAddress(byteArray));
					}
					WlanFreeMemory(pWlanBssList);
				} else maclist=nullptr;
				networks->AddLast(
					gcnew WifiNetwork(
					ssid,
					profile,
					maclist,
					type,
					sigPower));
			}
			WlanFreeMemory(pBssList);
			return networks;
		}

		WifiNetwork^ JimbeWiFi::WiFiGetCurrentConnection(WifiInterface ^wInterface){
			DWORD dwResult = 0;

			// variables used for WlanQueryInterfaces for opcode = wlan_intf_opcode_current_connection
			PWLAN_CONNECTION_ATTRIBUTES pConnectInfo = NULL;
			DWORD connectInfoSize = sizeof(WLAN_CONNECTION_ATTRIBUTES);
			WLAN_OPCODE_VALUE_TYPE opCode = wlan_opcode_value_type_invalid;
	
			GUID interfaceGUID=ToGUID(wInterface->InterfaceGuid);
			
			if (wInterface->IsState != WifiInterface::WlanInterfaceState::connected) 
				throw gcnew WifiException("Interface is not currently connected to a network");
			else {
                dwResult = WlanQueryInterface(hClientHandle,
												&interfaceGUID,
												wlan_intf_opcode_current_connection,
												NULL,
												&connectInfoSize,
												(PVOID *) &pConnectInfo,
												&opCode);
				if (dwResult!=ERROR_SUCCESS)
					switch (dwResult) {
					case ERROR_ACCESS_DENIED:	throw gcnew WifiException("Not have sufficient permissions");
					case ERROR_INVALID_STATE:	throw gcnew WifiException("Interface is not currently connected to a network");
					default:					throw gcnew WifiException("Error in WlanQueryInterface");				
				}

				String ^profile=Marshal::PtrToStringAuto(static_cast<IntPtr>(pConnectInfo->strProfileName));
				String ^ssid=Marshal::PtrToStringAnsi(static_cast<IntPtr>(pConnectInfo->wlanAssociationAttributes.dot11Ssid.ucSSID), static_cast<Int32>(pConnectInfo->wlanAssociationAttributes.dot11Ssid.uSSIDLength));
				
				int sigPower=pConnectInfo->wlanAssociationAttributes.wlanSignalQuality;
				WifiNetwork::BSSType type=(WifiNetwork::BSSType)pConnectInfo->wlanAssociationAttributes.dot11BssType;
				
				array<byte> ^ byteArray  = gcnew array<Byte>(6);
				Marshal::Copy((IntPtr)pConnectInfo->wlanAssociationAttributes.dot11Bssid, byteArray, 0, 6 );
				LinkedList<PhysicalAddress^>^ maclist=gcnew LinkedList<PhysicalAddress^>();
				maclist->AddLast(gcnew PhysicalAddress(byteArray));
				
				return gcnew WifiNetwork(ssid,profile,maclist,type,sigPower);

			} 
		}

		 Guid JimbeWiFi::FromGUID( _GUID& guid ) {
			return Guid( guid.Data1, guid.Data2, guid.Data3, 
                guid.Data4[ 0 ], guid.Data4[ 1 ], 
                guid.Data4[ 2 ], guid.Data4[ 3 ], 
                guid.Data4[ 4 ], guid.Data4[ 5 ], 
                guid.Data4[ 6 ], guid.Data4[ 7 ] );
		}

		_GUID JimbeWiFi::ToGUID( Guid& guid ) {
			array<Byte>^ guidData = guid.ToByteArray();
			pin_ptr<Byte> data = &(guidData[ 0 ]);

			return *(_GUID *)data;
		}

	}
}