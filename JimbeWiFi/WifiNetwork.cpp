#include "StdAfx.h"
#include "WifiNetwork.h"

namespace Jimbe {
	namespace JimbeWiFi {
		
		WifiNetwork::WifiNetwork(String^ ssid, String^ profile, IEnumerable<PhysicalAddress^> ^bssids, WifiNetwork::BSSType type, int signalQuality)
		{
			this->_signalQuality=signalQuality;
			this->_ssid=ssid;
			this->_profile=profile;
			this->_bssids=bssids;
			this->_type=type;
		}

		int WifiNetwork::SignalQuality::get(){
			return _signalQuality;
		}

		String^ WifiNetwork::Ssid::get(){
			return _ssid;
		}

		String^ WifiNetwork::Profile::get(){
			return _profile;
		}
		
		WifiNetwork::BSSType WifiNetwork::Type::get(){
			return _type;
		}

		IEnumerable<PhysicalAddress ^> ^ WifiNetwork::Bssids::get(){
			return _bssids;
		}
	}
}
