using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using JimbeWCF.DataContracts;
using JimbeWCF.ServiceContract;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {

            var wsDualHttpBinding = new WSDualHttpBinding();
            wsDualHttpBinding.ClientBaseAddress = new Uri("http://localhost:9091/JimbeClient");

            var _channelFactory = new ChannelFactory<ILocationService>(wsDualHttpBinding, "http://localhost:9090/Jimbe");

            ILocationService proxy = _channelFactory.CreateChannel();
            
            IList<Task> tasks= new List<Task>();

            var locations = proxy.GetLocations();


            if (locations==null) Console.WriteLine("No Locations");
            else
                foreach (var location1 in locations)
                {
                    printLocation(location1);
                }

//            tasks.Add(new StartProcess()
//                {
//                    ProcessName = "http://www.google.it",
//                    Type = Task.TaskType.Spot
//                });
            tasks.Add(new MessageInfo()
                {
                    Message = "Delayed 10s",
                    Type = Task.TaskType.Delayed,
                    Delay = new TimeSpan(0,0,10)
                });
//            tasks.Add(new MessageInfo()
//                {
//                    Message = "Periodic 5s",
//                    Type = Task.TaskType.Periodic,
//                    Delay = new TimeSpan(0, 0, 5)
//                });

            var location = new Location()
                {
                    Description = "location1",
                    Name = "location1",
                    TasksList = tasks
                };

            Console.WriteLine(location.Name+" "+location.Description+" Insert new location");

            Console.ReadLine();
            proxy.InsertLocation(location);

            Console.WriteLine("Read location");
            Console.ReadLine();
            var current = proxy.GetCurrentLocation();
            if (current!=null)
            {
                printLocation(current);
            }
            Console.ReadLine();
            Console.WriteLine("Inserted second location");
            location = new Location()
                {
                    Name = "location2",
                    Description = "location2"
                };
            proxy.InsertLocation(location);
            Console.ReadLine();
            current=proxy.GetCurrentLocation();
            printLocation(current);
            Console.WriteLine("Press enter to delete location");
            Console.ReadLine();
            proxy.DeleteLocation(current);

            locations = proxy.GetLocations();
            if (locations == null) Console.WriteLine("No Locations");
            else
                foreach (var location1 in locations)
                {
                    printLocation(location1);
                }
        }

        private static void printLocation(Location location)
        {
            Console.WriteLine("Location: " + location.Name + " " + location.Description);
            foreach (var statistic in location.StatisticsList)
            {
                Console.WriteLine("\tStatistic: " + statistic.Start + " " + statistic.End);
            }
            Console.WriteLine("-------------------");
            foreach (var task in location.TasksList)
            {
                Console.WriteLine("\tTask: " + task.GetType() + " " + task.Type);
            }
        }
    }
}
