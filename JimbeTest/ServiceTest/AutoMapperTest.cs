using System;
using JimbeCore.Domain.Entities;
using JimbeTest.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using AutoMapper;
using JimbeService.IoC;
using Location = JimbeCore.Domain.Entities.Location;
using LocationStatistic = JimbeCore.Domain.Entities.LocationStatistic;
using Sensor = JimbeCore.Domain.Entities.Sensor;
using StartProcess = JimbeCore.Domain.Entities.StartProcess;
using Statistic = JimbeCore.Domain.Entities.Statistic;
using Task = JimbeCore.Domain.Entities.Task;
using WiFiConnectedSensor = JimbeCore.Domain.Entities.WiFiConnectedSensor;
using WiFiNetwork = JimbeCore.Domain.Entities.WiFiNetwork;
using WiFiSensor = JimbeCore.Domain.Entities.WiFiSensor;

namespace JimbeTest.ServiceTest
{
    [TestClass]
    public class AutoMapperTest
    {

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            

            //From CoreEntity to DTO
            Mapper.CreateMap<Location, JimbeWCF.DataContracts.Location>()
                .ForMember(dest => dest.SensorsList, opt => opt.MapFrom(src => src.SensorsList))
                .ForMember(dest => dest.StatisticsList, opt => opt.MapFrom(src => src.StatisticsList))
                .ForMember(dest => dest.TasksList, opt => opt.MapFrom(src => src.TasksList));
            Mapper.CreateMap<Sensor, JimbeWCF.DataContracts.Sensor>()
                .Include<WiFiConnectedSensor, JimbeWCF.DataContracts.WiFiConnectedSensor>()
                .Include<WiFiSensor, JimbeWCF.DataContracts.WiFiSensor>();
            Mapper.CreateMap<Task, JimbeWCF.DataContracts.Task>()
                .Include<StartProcess, JimbeWCF.DataContracts.StartProcess>()
                .Include<MessageInfo,JimbeWCF.DataContracts.MessageInfo>();
            Mapper.CreateMap<WiFiNetwork, JimbeWCF.DataContracts.WiFiNetwork>();
            Mapper.CreateMap<Statistic, JimbeWCF.DataContracts.Statistic>()
                .Include<LocationStatistic, JimbeWCF.DataContracts.LocationStatistic>();
            Mapper.CreateMap<WiFiConnectedSensor, JimbeWCF.DataContracts.WiFiConnectedSensor>();
            Mapper.CreateMap<WiFiSensor, JimbeWCF.DataContracts.WiFiSensor>();
            Mapper.CreateMap<WiFiNetworkSet, JimbeWCF.DataContracts.WiFiNetworkSet>();
            Mapper.CreateMap<StartProcess, JimbeWCF.DataContracts.StartProcess>();
            Mapper.CreateMap<MessageInfo, JimbeWCF.DataContracts.MessageInfo>();
            Mapper.CreateMap<LocationStatistic, JimbeWCF.DataContracts.LocationStatistic>();

            //Reverse mapping from DTO to CoreEntity

            Mapper.CreateMap<JimbeWCF.DataContracts.Location, Location>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sensors, opt => opt.Ignore())
                .ForMember(dest => dest.Statistics, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWCF.DataContracts.Sensor, Sensor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .Include<JimbeWCF.DataContracts.WiFiConnectedSensor, WiFiConnectedSensor>()
                .Include<JimbeWCF.DataContracts.WiFiSensor, WiFiSensor>();
            Mapper.CreateMap<JimbeWCF.DataContracts.Task, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Success, opt=> opt.Ignore())
                .Include<JimbeWCF.DataContracts.StartProcess, StartProcess>()
                .Include<JimbeWCF.DataContracts.MessageInfo,MessageInfo>();
            Mapper.CreateMap<JimbeWCF.DataContracts.WiFiNetwork, WiFiNetwork>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NetworkSet, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWCF.DataContracts.Statistic, Statistic>()
                .Include<JimbeWCF.DataContracts.LocationStatistic, LocationStatistic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWCF.DataContracts.WiFiConnectedSensor, WiFiConnectedSensor>();
            Mapper.CreateMap<JimbeWCF.DataContracts.WiFiSensor, WiFiSensor>();
            Mapper.CreateMap<JimbeWCF.DataContracts.StartProcess, StartProcess>();
            Mapper.CreateMap<JimbeWCF.DataContracts.MessageInfo, MessageInfo>();
            Mapper.CreateMap<JimbeWCF.DataContracts.WiFiNetworkSet, WiFiNetworkSet>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.Sensor, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWCF.DataContracts.LocationStatistic, LocationStatistic>()
                .ForMember(dest => dest.Location, opt => opt.Ignore());
        }

        [TestMethod]
        public void AutoMapperTest1()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void LocationMapTest()
        {
            Mapper.Reset();
            var kernel = new StandardKernel(new AutoMapperModule());
            var engine = kernel.Get<IMappingEngine>();
            var dtolocation = new JimbeWCF.DataContracts.Location()
            {
                Description = "pippo",
                Name = "pippo",
                StatisticsList = null,
                TasksList = TaskHelper.GetDtoTasks()
            };
            var corelocation = engine.Map<JimbeWCF.DataContracts.Location, Location>(dtolocation);
            Assert.AreEqual(corelocation.Name, dtolocation.Name);
        }

        [TestMethod]
        public void TaskMapTest()
        {
            Mapper.Reset();
            var kernel = new StandardKernel((new AutoMapperModule()));
            var engine = kernel.Get<IMappingEngine>();
            JimbeWCF.DataContracts.Task dtotask = new JimbeWCF.DataContracts.StartProcess()
                {
                    ProcessName = "pippo",
                    Arguments = "foo",
                    Delay = new TimeSpan(0, 0, 1),
                    Type = JimbeWCF.DataContracts.Task.TaskType.Spot
                };

            var coretask = engine.Map<JimbeWCF.DataContracts.Task, Task>(dtotask);
            if (coretask.GetType()==typeof(StartProcess))
            {
                var sp=(StartProcess) coretask;
                var dtosp = (JimbeWCF.DataContracts.StartProcess) dtotask;
                Assert.AreEqual(dtosp.ProcessName,sp.ProcessName);
            } else Assert.Fail("Mapping failed");
        }
    }
}
