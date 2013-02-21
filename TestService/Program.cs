using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using JimbeWFC.DataContracts;
using JimbeWFC.ServiceContract;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {

            var _channelFactory = new ChannelFactory<ILocationService>(new BasicHttpBinding(), "http://localhost:9090/Jimbe");

            ILocationService proxy = _channelFactory.CreateChannel();
            
            IList<Task> tasks= new List<Task>();
           
            Console.WriteLine("Try to read current location");
            Console.ReadLine();

            var locations = proxy.GetLocations();


            if (locations==null) Console.WriteLine("No Locations");
            else
                foreach (var location1 in locations)
                {
                    Console.WriteLine("Location " + location1.Name +" "+ location1.Description);
                    foreach (var statistic in location1.StatisticsList)
                    {
                        Console.WriteLine("\tStatistic: " + statistic.Start+ " "+ statistic.End);
                    }
                    Console.WriteLine("-------------------");
                    foreach (var task in location1.TasksList)
                    {
                        Console.WriteLine("\tTask: "+task.GetType()+ " "+task.Type);
                    }
                }

            tasks.Add(new StartProcess()
                {
                    ProcessName = "http://www.google.it",
                    Type = Task.TaskType.Spot
                });
            tasks.Add(new MessageInfo()
                {
                    Message = "Delayed 10s",
                    Type = Task.TaskType.Delayed,
                    Delay = new TimeSpan(0,0,10)
                });
            tasks.Add(new MessageInfo()
                {
                    Message = "Periodic 5s",
                    Type = Task.TaskType.Periodic,
                    Delay = new TimeSpan(0, 0, 5)
                });

            var location = new Location()
                {
                    Description = "AAA",
                    Name = "AAA",
                    TasksList = tasks
                };

            Console.WriteLine(location.Name+" "+location.Description+" Insert new location");

            Console.ReadLine();
            proxy.InsertLocation(location);

            Console.WriteLine("Read location");
            Console.ReadLine();
            var current = proxy.GetCurrentLocation();
            if (current!=null) {
                Console.WriteLine("Current: "+current.Name+" "+current.Description);
                foreach (var statistic in current.StatisticsList)
                {
                    Console.WriteLine("\tStatistic: " + statistic.Start+ " "+ statistic.End);
                }
                Console.WriteLine("-------------------");
                foreach (var task in current.TasksList)
                {
                    Console.WriteLine("\tTask: "+task.GetType()+ " "+task.Type);
                }
            }
            Console.ReadLine();
        }
    }
}
