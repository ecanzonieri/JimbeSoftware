using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using JimbeWCF.ServiceContract;

namespace JimbeApp
{
    public static class ProxyFactory
    {
        private static ChannelFactory<ILocationService> _channelFactory=null;

        public static ILocationService GetProxy(string myuri)
        {
            if (_channelFactory == null)
            {
                var wsDualHttpBinding = new WSDualHttpBinding();
                wsDualHttpBinding.ClientBaseAddress = new Uri(myuri);

                _channelFactory = new ChannelFactory<ILocationService>(wsDualHttpBinding,
                                                                       Properties.Settings.Default.Url_service);
            }
            return (_channelFactory.CreateChannel());

        }
    }
}
