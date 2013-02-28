﻿using System;
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
            Mapper.CreateMap<Location, JimbeWFC.DataContracts.Location>()
                .ForMember(dest => dest.SensorsList, opt => opt.MapFrom(src => src.SensorsList))
                .ForMember(dest => dest.StatisticsList, opt => opt.MapFrom(src => src.StatisticsList))
                .ForMember(dest => dest.TasksList, opt => opt.MapFrom(src => src.TasksList));
            Mapper.CreateMap<Sensor, JimbeWFC.DataContracts.Sensor>()
                .Include<WiFiConnectedSensor, JimbeWFC.DataContracts.WiFiConnectedSensor>()
                .Include<WiFiSensor, JimbeWFC.DataContracts.WiFiSensor>();
            Mapper.CreateMap<Task, JimbeWFC.DataContracts.Task>()
                .Include<StartProcess, JimbeWFC.DataContracts.StartProcess>()
                .Include<MessageInfo,JimbeWFC.DataContracts.MessageInfo>();
            Mapper.CreateMap<WiFiNetwork, JimbeWFC.DataContracts.WiFiNetwork>();
            Mapper.CreateMap<Statistic, JimbeWFC.DataContracts.Statistic>()
                .Include<LocationStatistic, JimbeWFC.DataContracts.LocationStatistic>();
            Mapper.CreateMap<WiFiConnectedSensor, JimbeWFC.DataContracts.WiFiConnectedSensor>();
            Mapper.CreateMap<WiFiSensor, JimbeWFC.DataContracts.WiFiSensor>();
            Mapper.CreateMap<WiFiNetworkSet, JimbeWFC.DataContracts.WiFiNetworkSet>();
            Mapper.CreateMap<StartProcess, JimbeWFC.DataContracts.StartProcess>();
            Mapper.CreateMap<MessageInfo, JimbeWFC.DataContracts.MessageInfo>();
            Mapper.CreateMap<LocationStatistic, JimbeWFC.DataContracts.LocationStatistic>();

            //Reverse mapping from DTO to CoreEntity

            Mapper.CreateMap<JimbeWFC.DataContracts.Location, Location>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sensors, opt => opt.Ignore())
                .ForMember(dest => dest.Statistics, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWFC.DataContracts.Sensor, Sensor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .Include<JimbeWFC.DataContracts.WiFiConnectedSensor, WiFiConnectedSensor>()
                .Include<JimbeWFC.DataContracts.WiFiSensor, WiFiSensor>();
            Mapper.CreateMap<JimbeWFC.DataContracts.Task, Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Success, opt=> opt.Ignore())
                .Include<JimbeWFC.DataContracts.StartProcess, StartProcess>()
                .Include<JimbeWFC.DataContracts.MessageInfo,MessageInfo>();
            Mapper.CreateMap<JimbeWFC.DataContracts.WiFiNetwork, WiFiNetwork>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NetworkSet, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWFC.DataContracts.Statistic, Statistic>()
                .Include<JimbeWFC.DataContracts.LocationStatistic, LocationStatistic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWFC.DataContracts.WiFiConnectedSensor, WiFiConnectedSensor>();
            Mapper.CreateMap<JimbeWFC.DataContracts.WiFiSensor, WiFiSensor>();
            Mapper.CreateMap<JimbeWFC.DataContracts.StartProcess, StartProcess>();
            Mapper.CreateMap<JimbeWFC.DataContracts.MessageInfo, MessageInfo>();
            Mapper.CreateMap<JimbeWFC.DataContracts.WiFiNetworkSet, WiFiNetworkSet>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.Sensor, opt => opt.Ignore());
            Mapper.CreateMap<JimbeWFC.DataContracts.LocationStatistic, LocationStatistic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
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
            var dtolocation = new JimbeWFC.DataContracts.Location()
            {
                Description = "pippo",
                Name = "pippo",
                StatisticsList = null,
                TasksList = TaskHelper.GetDtoTasks()
            };
            var corelocation = engine.Map<JimbeWFC.DataContracts.Location, Location>(dtolocation);
            Assert.AreEqual(corelocation.Name, dtolocation.Name);
        }

        [TestMethod]
        public void TaskMapTest()
        {
            Mapper.Reset();
            var kernel = new StandardKernel((new AutoMapperModule()));
            var engine = kernel.Get<IMappingEngine>();
            JimbeWFC.DataContracts.Task dtotask = new JimbeWFC.DataContracts.StartProcess()
                {
                    ProcessName = "pippo",
                    Arguments = "foo",
                    Delay = new TimeSpan(0, 0, 1),
                    Type = JimbeWFC.DataContracts.Task.TaskType.Spot
                };

            var coretask = engine.Map<JimbeWFC.DataContracts.Task, Task>(dtotask);
            if (coretask.GetType()==typeof(StartProcess))
            {
                var sp=(StartProcess) coretask;
                var dtosp = (JimbeWFC.DataContracts.StartProcess) dtotask;
                Assert.AreEqual(dtosp.ProcessName,sp.ProcessName);
            } else Assert.Fail("Mapping failed");
        }
    }
}
