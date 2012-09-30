using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using JimbeWFC.ServiceContract;

namespace JimbeApp
{
    public static class ProxyFactory
    {
        private static ChannelFactory<ILocationService> _channelFactory=null;

        public static ILocationService GetProxy()
        {
            if(_channelFactory ==null)
            _channelFactory=new ChannelFactory<ILocationService>(new BasicHttpBinding(), Properties.Settings.Default.Url_service);

            return (_channelFactory.CreateChannel());

        }
    }
}
