// JimbeWiFi.h

#include "WifiInterface.h"
#include "WifiNetwork.h"
#include "IWifiApi.h"

#pragma once

using namespace System;
using namespace System::Collections::Generic;


namespace Jimbe {
	namespace JimbeWiFi {
		public ref class JimbeWiFi : public IWifiApi
		{
		public:
			enum class NativeApiVersion{
				windowsXp=1,
				windowsVista=2
			};
			
			JimbeWiFi(void);
			JimbeWiFi(JimbeWiFi::NativeApiVersion version);


			virtual	IEnumerable<WifiInterface ^> ^WiFiEnumInterfaces(void);
			virtual IEnumerable<WifiNetwork ^> ^WiFiGetAvailableNetworkList(WifiInterface ^ wInterface);
			virtual WifiNetwork^ WiFiGetCurrentConnection(WifiInterface ^wInterface);

			static Guid JimbeWiFi::FromGUID( _GUID& guid );
			static _GUID JimbeWiFi::ToGUID( Guid& guid );
		
		protected:
			!JimbeWiFi();
			~JimbeWiFi(void);
		
		private:
			HANDLE hClientHandle;
			DWORD  dwNegotiatedVersion;
			JimbeWiFi::NativeApiVersion _required;
			void InitApi(JimbeWiFi::NativeApiVersion version);
		};
	}
}
