using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;

namespace JimbeCore.Domain.Entities
{
    public class Location : Entity<Guid>, ILocation
    {
        private const double Threshold = 0.9;

        public Location(string name, string description, IList<Sensor> sensors, IList<Task> tasks)
        {
            SensorsList = sensors;
            TasksList = tasks;
            Description = description;
            Name = name;
            StatisticsList= new List<Statistic>();
        }

        public Location(string name, IList<Sensor> sensors, IList<Task> tasks )
        {
            SensorsList = sensors;
            TasksList = tasks;
            Name = name;
            Description = "No description available for this location";
            StatisticsList= new List<Statistic>();
        }

        public Location (IList<Sensor> sensors )
        {
            SensorsList = sensors;
            TasksList= new List<Task>();
            StatisticsList= new List<Statistic>();
        }

        public Location()
        {
            SensorsList = new List<Sensor>();
            TasksList = new List<Task>();
            StatisticsList= new List<Statistic>();
        }

        #region ILocation members

        public virtual IList<Sensor> SensorsList { get; set; }

        public virtual IList<Task> TasksList { get; set; }

        public virtual IEnumerable<ISensor> Sensors
        {
            get { return SensorsList; }
            set { }
        }

        public virtual IEnumerable<ITask> Tasks
        {
            get { return TasksList; }
            set { }
        } 
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<Statistic> StatisticsList { get; set; }

        public virtual IEnumerable<IStatistic> Statistics { 
            get { return StatisticsList; }
            set { }
        } 

        public virtual void AddSensors(Sensor sensor)
        {
            SensorsList.Add(sensor);
            sensor.Location = this;
        }

        public virtual void AddTask (Task task)
        {
            task.Location = this;
            TasksList.Add(task);
        }

        public virtual double GetWeigthSum()
        {
            double sumWeight = 0.0;
            if (Sensors != null && Sensors.Any())
                sumWeight += Sensors.Sum(sensor => sensor.Weigth);
            return sumWeight;
        }

        public virtual double GetLocationAffinity(ILocation location)
        {

            double distance = 0.0;

            double maxweigthsum = Math.Max(GetWeigthSum(), location.GetWeigthSum());

            foreach (var sensor in location.Sensors)
            {
                var distances= (from ss in Sensors where ss.GetType() == sensor.GetType() select ss.GetDistance(sensor));
                if (distances.Any())
                    distance += sensor.Weigth*distances.Max();
            }

            return distance/maxweigthsum;
        }

        public virtual void UpdateLocationSensors(ILocation location)
        {
            
            foreach (var othersensor in location.Sensors)
            {
                var query = from sensor in Sensors
                                     where othersensor.GetType() == sensor.GetType()
                                     select new {Sensor = sensor, Distance = sensor.GetDistance(othersensor)};
                if (query.Any() && query.Max(x => x.Distance)<Threshold)
                {
                    var sensor = (from q in query where q.Distance >= query.Max(x => x.Distance) select q.Sensor).FirstOrDefault();
                    sensor.UpdateSensorDataset(othersensor);
                }
            }
        }

        #endregion

        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            var other = comparable as Location;
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (Name.Equals(other.Name) && Description.Equals(other.Description));
        }

        #endregion

        public virtual void RemoveSensorsInfo(ILocation location)
        {
            foreach (var othersensor in location.Sensors)
            {
                var query = from sensor in Sensors
                            where othersensor.GetType() == sensor.GetType()
                            select new {Sensor = sensor, Distance = sensor.GetDistance(othersensor)};
                if (query.Any() && query.Max(x => x.Distance) >= Threshold)
                {
                    var sensor =
                        (from q in query where q.Distance >= query.Max(x => x.Distance) select q.Sensor).FirstOrDefault();
                    if (sensor!=null) sensor.RemoveInfo(othersensor);
                }
            }
        }
    }
}