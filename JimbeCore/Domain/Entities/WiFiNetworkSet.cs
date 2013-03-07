using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public class WiFiNetworkSet : Entity<Guid>
    {
        public virtual IList<WiFiNetwork> Networks { get; set; }

        public virtual ISensor Sensor { get; set; }

        public WiFiNetworkSet()
        {
          Networks = new List<WiFiNetwork>();
        }

        public WiFiNetworkSet(IList<WiFiNetwork> networks)
        {
            Networks = networks;
        }

        public WiFiNetworkSet(IList<WiFiNetwork> networks, ISensor sensor):this(networks)
        {
            Sensor = sensor;
        }

        public virtual double GetDistance(WiFiNetworkSet wiFiNetworkSet)
        {
            var query = from mynet in Networks
                        join net in wiFiNetworkSet.Networks
                            on mynet.Ssid equals net.Ssid
                        select new {Quality = net.SignalQuality, MyQuality = mynet.SignalQuality};
            if (query.Any())
            {
                var proximity = query.Sum(@group => Math.Pow((@group.MyQuality - @group.Quality)/100.0, 2));
                int mincount = Math.Min(Networks.Count(), wiFiNetworkSet.Networks.Count());
                return (1.0 - Math.Sqrt(proximity)/mincount)*query.Count()/mincount;
            }
            return 0.0;
        }

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            var other = comparable as WiFiNetworkSet;
            if (other == null) return false;
            if (other.Networks.Count != Networks.Count) return false;
            var compared = from wiFiNetwork in Networks
                           where other.Networks.Any(net => net.EqualsBusiness(wiFiNetwork))
                           select wiFiNetwork;
            if (compared.Count() == Networks.Count()) return true;
            return false;
        }
    }
}
