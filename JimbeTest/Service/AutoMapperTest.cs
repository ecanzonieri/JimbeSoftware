using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using AutoMapper;
using JimbeService.IoC;
using CoreEntity = JimbeCore.Domain.Entities;
using DTO = JimbeWFC.DataContracts;

namespace JimbeTest.Service
{
    [TestClass]
    public class AutoMapperTest
    {

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            

            //From CoreEntity to DTO
            Mapper.CreateMap<CoreEntity.Location, DTO.Location>();
            Mapper.CreateMap<CoreEntity.Sensor, DTO.Sensor>()
                .Include<CoreEntity.WiFiConnectedSensor, DTO.WiFiConnectedSensor>()
                .Include<CoreEntity.WiFiSensor, DTO.WiFiSensor>();
            Mapper.CreateMap<CoreEntity.Task, DTO.Task>()
                .Include<CoreEntity.StartProcess, DTO.StartProcess>();
            Mapper.CreateMap<CoreEntity.WiFiNetwork, DTO.WiFiNetwork>();
            Mapper.CreateMap<CoreEntity.Statistic, DTO.Statistic>()
                .Include<CoreEntity.LocationStatistic, DTO.LocationStatistic>();
            Mapper.CreateMap<CoreEntity.WiFiConnectedSensor, DTO.WiFiConnectedSensor>();
            Mapper.CreateMap<CoreEntity.WiFiSensor, DTO.WiFiSensor>();
            Mapper.CreateMap<CoreEntity.StartProcess, DTO.StartProcess>();
            Mapper.CreateMap<CoreEntity.LocationStatistic, DTO.LocationStatistic>();

            //Reverse mapping from DTO to CoreEntity

            Mapper.CreateMap<DTO.Location, CoreEntity.Location>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sensors, opt => opt.Ignore())
                .ForMember(dest => dest.Statistics, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());
            Mapper.CreateMap<DTO.Sensor, CoreEntity.Sensor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .Include<DTO.WiFiConnectedSensor, CoreEntity.WiFiConnectedSensor>()
                .Include<DTO.WiFiSensor, CoreEntity.WiFiSensor>();
            Mapper.CreateMap<DTO.Task, CoreEntity.Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Success, opt=> opt.Ignore())
                .Include<DTO.StartProcess, CoreEntity.StartProcess>();
            Mapper.CreateMap<DTO.WiFiNetwork, CoreEntity.WiFiNetwork>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sensor, opt => opt.Ignore());
            Mapper.CreateMap<DTO.Statistic, CoreEntity.Statistic>()
                .Include<DTO.LocationStatistic, CoreEntity.LocationStatistic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            Mapper.CreateMap<DTO.WiFiConnectedSensor, CoreEntity.WiFiConnectedSensor>();
            Mapper.CreateMap<DTO.WiFiSensor, CoreEntity.WiFiSensor>();
            Mapper.CreateMap<DTO.StartProcess, CoreEntity.StartProcess>();
            Mapper.CreateMap<DTO.LocationStatistic, CoreEntity.LocationStatistic>()
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
            var dtolocation = new DTO.Location()
            {
                Description = "pippo",
                Name = "pippo",
                StatisticsList = null,
                TasksList = null
            };
            var corelocation = engine.Map<DTO.Location, CoreEntity.Location>(dtolocation);
            Assert.AreEqual(corelocation.Name, dtolocation.Name);
        }
    }
}
