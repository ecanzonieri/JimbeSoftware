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
				throw gcnew WifiException("Error on open Wlan Api");
		}

		JimbeWiFi::~JimbeWiFi(void)
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
			
			PWLAN_AVAILABLE_NETWORK_LIST pBssList = NULL;
		    PWLAN_AVAILABLE_NETWORK pBssEntry = NULL;
			PWLAN_BSS_LIST pWlanBssList=NULL;
			PWLAN_BSS_ENTRY pWlanBssEntry=NULL;
			
			UCHAR ssid[DOT11_SSID_MAX_LENGTH+1];
			DWORD dwResult=0;		
			
			int i, k,j;
			
			LinkedList<WifiNetwork^>^ networklist=nullptr;
			LinkedList<PhysicalAddress ^>^ maclist=nullptr;
			
			GUID interfaceGUID=ToGUID(wInterface->InterfaceGuid);

			dwResult = WlanGetAvailableNetworkList(hClientHandle,
                                             &interfaceGUID,
                                             0, 
                                             NULL, 
                                             &pBssList);
			if (dwResult!=ERROR_SUCCESS)
				throw gcnew WifiException("Error on get available networks");
			
			networklist= gcnew LinkedList<WifiNetwork^>();
			
			for (i=0; i<(int) pBssList->dwNumberOfItems; i++) {
				pBssEntry = (WLAN_AVAILABLE_NETWORK *) & pBssList->Network[i];
				
				if (pBssEntry->dot11Ssid.uSSIDLength==0) ssid[0]='\0';
				else for (k=0; k<(int)(pBssEntry->dot11Ssid.uSSIDLength);k++) ssid[k]=pBssEntry->dot11Ssid.ucSSID[k];
				ssid[k]='\0';
				
				String ^profile= Marshal::PtrToStringAuto(static_cast<IntPtr>(pBssEntry->strProfileName));
				WifiNetwork::BSSType type=(WifiNetwork::BSSType) pBssEntry->dot11BssType;
				ULONG sigPower= pBssEntry->wlanSignalQuality;
				
				if (dwNegotiatedVersion==(int)NativeApiVersion::windowsVista) {
					PhysicalAddress ^addr;
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
					
					for (j=0; j<pWlanBssList->dwNumberOfItems; j++) {
						pWlanBssEntry=pWlanBssList->wlanBssEntries+j;
						byteArray  = gcnew array<Byte>(6);
						Marshal::Copy((IntPtr)pWlanBssEntry->dot11Bssid, byteArray, 0, 6 );
						maclist->AddLast(gcnew PhysicalAddress(byteArray));
					}
					WlanFreeMemory(pWlanBssList);
				} else maclist=nullptr;

				networklist->AddLast(
					gcnew WifiNetwork(
					Marshal::PtrToStringAnsi(static_cast<IntPtr>(ssid)),
					profile,
					maclist,
					type,
					sigPower
					));

			}
			WlanFreeMemory(pBssList);
			return networklist;
		}

		WifiNetwork^ JimbeWiFi::WiFiGetCurrentConnection(WifiInterface ^wInterface){
			return nullptr;
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