using System;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public class WiFiNetwork : Entity<Guid>
    {
        public virtual string Ssid { get; set; }

        public virtual int SignalQuality { get; set; }

        public virtual WiFiNetworkSet NetworkSet { get; set; }

        public WiFiNetwork()
        {
        }
        public WiFiNetwork (string ssid, int sigqual)
        {
            Ssid = ssid;
            SignalQuality = sigqual;
        }
        public WiFiNetwork(string ssid, int sigqual, WiFiNetworkSet networkSet) : this(ssid, sigqual)
        {
            NetworkSet = networkSet;
        }

        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            if (ReferenceEquals(this, comparable)) return true;
            var other = comparable as WiFiNetwork;
            if (ReferenceEquals(null, other)) return false;
            return other.SignalQuality == SignalQuality && other.Ssid == Ssid;
        }

        #endregion
    }
}
