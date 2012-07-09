#pragma once

#include "WifiInterface.h"
#include "WifiNetwork.h"

namespace Jimbe {
	namespace JimbeWiFi {
		public interface class IWifiApi
		{
			public:
				IEnumerable<WifiInterface ^> ^WiFiEnumInterfaces(void);
				IEnumerable<WifiNetwork ^> ^WiFiGetAvailableNetworkList(WifiInterface ^ wInterface);
				WifiNetwork^ WiFiGetCurrentConnection(WifiInterface ^wInterface);
		};
	}
}

