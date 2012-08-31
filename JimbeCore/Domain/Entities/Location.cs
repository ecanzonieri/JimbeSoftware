using System;
using System.Collections.Generic;
using System.Linq;
using JimbeCore.Domain.Interfaces;
using JimbeCore.Domain.Model;
using TracerX;

namespace JimbeCore.Domain.Entities
{
    public class Location : Entity<Guid>, ILocation
    {

        private static Logger logger = Logger.GetLogger("Location");

        public Location(string name, string description, IList<Sensor> sensors, IList<Task> tasks)
        {
            SensorsList = sensors;
            TasksList = tasks;
            Description = description;
            Name = name;
        }

        public Location(string name, IList<Sensor> sensors, IList<Task> tasks )
        {
            SensorsList = sensors;
            TasksList = tasks;
            Name = name;
            Description = "No description available for this location";
        }

        public Location (string name)
        {
            Name = name;
        }

        public Location()
        {
            SensorsList = new List<Sensor>();
            TasksList = new List<Task>();
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
            get { return Tasks; }
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
            else logger.Warn("Sensors list is empty");
            return sumWeight;
        }

        public virtual double GetLocationAffinity(ILocation location)
        {

            IEnumerable<ISensor> sensorsource, sensors;

            if (Sensors.Count() > location.Sensors.Count())
            {
                sensors = Sensors;
                sensorsource = location.Sensors;
            }
            else
            {
                sensors = location.Sensors;
                sensorsource = Sensors;
            }

            double distance = 0.0;

            double maxweigthsum = Math.Max(GetWeigthSum(), location.GetWeigthSum());

            foreach (var sensor in sensors)
            {
                bool match = false;
                double singledistance = 0.0;

                foreach (
                    var sensor1 in
                        sensorsource.Where(
                            sensor1 =>
                            (sensor1.GetType() == sensor.GetType() && sensor1.GetDistance(sensor) > singledistance)))
                {
                    match = true;
                    singledistance = sensor1.GetDistance(sensor);
                }
                if (match)
                    distance += sensor.Weigth*singledistance;
            }

            logger.Info("Sensor distance = ", distance, " Max Weigth = ", maxweigthsum);
            return distance/maxweigthsum;
        }



        #endregion

        #region Overrides of Entity<Guid>

        public override bool EqualsBusiness(IBusinessComparable comparable)
        {
            var other = comparable as Location;
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name.Equals(other.Name);
        }

        #endregion
    }
}