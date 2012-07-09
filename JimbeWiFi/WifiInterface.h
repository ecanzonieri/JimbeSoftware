#pragma once

using namespace System;

namespace Jimbe {
	namespace JimbeWiFi {

		public ref class WifiInterface
		{

		public:
			
			enum class WlanInterfaceState 
			{
				notReady,
				connected,
				formed,
				disconnecting,
				disconnected,
				associating,
				discovering,
				authenticating 
			};
			
			property String^ Description {String^ get();};
			
			property Guid InterfaceGuid {Guid get();};
			
			property WifiInterface::WlanInterfaceState IsState {WifiInterface::WlanInterfaceState get();};
		
		internal:
			WifiInterface(String ^description, Guid interfaceGuid, WifiInterface::WlanInterfaceState state);
		
		private:
			String^ _description;
			Guid _interfaceGuid;
			WifiInterface::WlanInterfaceState _isState;
		};
	}		
}

