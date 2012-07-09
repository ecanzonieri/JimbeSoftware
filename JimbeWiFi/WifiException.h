#pragma once

using namespace System;
using namespace System::Runtime::Serialization;
namespace Jimbe {
	namespace JimbeWiFi {
		[Serializable]
		public ref class WifiException : public Exception
		{
		public:
			WifiException(void): Exception(){}			
			WifiException(String^ msg) : Exception(msg){}
			WifiException(String^ msg, Exception^ inner) : Exception(msg,inner){}
		protected:
			WifiException(SerializationInfo^ info, StreamingContext context) : Exception(info,context){}
		};
		[Serializable]
		public ref class WifiToManyHandleException : public WifiException
		{
		public:
			WifiToManyHandleException(void): WifiException(){}			
			WifiToManyHandleException(String^ msg) : WifiException(msg){}
			WifiToManyHandleException(String^ msg, WifiException^ inner) : WifiException(msg,inner){}
		protected:
			WifiToManyHandleException(SerializationInfo^ info, StreamingContext context) : WifiException(info,context){}

		};
	}
}
