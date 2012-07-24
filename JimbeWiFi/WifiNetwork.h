#pragma once
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Net::NetworkInformation;

namespace Jimbe {
	namespace JimbeWiFi {

		public ref class WifiNetwork
		{
		public:
			enum class BSSType{
				infrastructure=1,
				indipendent=2,
				any=3 
			};
			
			property int SignalQuality{int get();};

			property String^ Ssid{String ^ get();};
			
			property String^ Profile{String ^ get();};
			
			property WifiNetwork::BSSType Type{WifiNetwork::BSSType get();};
			
			property IEnumerable<PhysicalAddress ^> ^ Bssids{IEnumerable<PhysicalAddress ^> ^ get();}; 
		
			WifiNetwork(String^ ssid, String^ profile, IEnumerable<PhysicalAddress ^> ^bssids, WifiNetwork::BSSType type, int signalQuality);
		
		private:
			int _signalQuality;
			String^ _ssid;
			String^ _profile;
			WifiNetwork::BSSType _type;
			IEnumerable<PhysicalAddress ^>^ _bssids;
		};
	}
}
