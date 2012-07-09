#include "StdAfx.h"
#include "WifiInterface.h"

namespace Jimbe {
	namespace JimbeWiFi {

		WifiInterface::WifiInterface(String^ description, Guid interfaceGuid, WlanInterfaceState state) {
			this->_description=description;
			this->_interfaceGuid=interfaceGuid;
			this->_isState=state;
		}

		String^ WifiInterface::Description::get(){
			return _description;
		}		

		WifiInterface::WlanInterfaceState WifiInterface::IsState::get(){
			return _isState;
		}

		Guid WifiInterface::InterfaceGuid::get(){
			return _interfaceGuid;
		}
	}
}
