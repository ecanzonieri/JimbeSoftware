using AutoMapper;
using AutoMapper.Mappers;
using Ninject.Modules;
using Ninject;
using CoreEntity = JimbeCore.Domain.Entities;
using DTO = JimbeWFC.DataContracts;

namespace JimbeService.IoC
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {

            Bind<ITypeMapFactory>().To<TypeMapFactory>();
            foreach (var mapper in MapperRegistry.AllMappers())
                Bind<IObjectMapper>().ToConstant(mapper);
            Bind<ConfigurationStore>().ToSelf().InSingletonScope()
                .WithConstructorArgument("mappers",
                                         ctx => ctx.Kernel.GetAll<IObjectMapper>());
            Bind<IConfiguration>().ToMethod(ctx => ctx.Kernel.Get<ConfigurationStore>());
            Bind<IConfigurationProvider>().ToMethod(ctx =>
                                                    ctx.Kernel.Get<ConfigurationStore>());
            Bind<IMappingEngine>().To<MappingEngine>();

            MapConfigure(Kernel.Get<IConfiguration>());
        }

        private static void MapConfigure(IConfiguration configuration)
        {
            //From CoreEntity to DTO
            configuration.CreateMap<CoreEntity.Location, DTO.Location>()
                .ForMember(dest => dest.SensorsList, opt => opt.MapFrom(src => src.SensorsList))
                .ForMember(dest => dest.StatisticsList, opt => opt.MapFrom(src => src.StatisticsList))
                .ForMember(dest => dest.TasksList, opt => opt.MapFrom(src => src.TasksList));
            configuration.CreateMap<CoreEntity.Sensor, DTO.Sensor>()
                .Include<CoreEntity.WiFiConnectedSensor, DTO.WiFiConnectedSensor>()
                .Include<CoreEntity.WiFiSensor, DTO.WiFiSensor>();
            configuration.CreateMap<CoreEntity.Task, DTO.Task>()
                .Include<CoreEntity.StartProcess, DTO.StartProcess>()
                .Include<CoreEntity.MessageInfo,DTO.MessageInfo>();
            configuration.CreateMap<CoreEntity.WiFiNetwork, DTO.WiFiNetwork>();
            configuration.CreateMap<CoreEntity.Statistic, DTO.Statistic>()
                .Include<CoreEntity.LocationStatistic, DTO.LocationStatistic>();
            configuration.CreateMap<CoreEntity.WiFiConnectedSensor, DTO.WiFiConnectedSensor>();
            configuration.CreateMap<CoreEntity.WiFiSensor, DTO.WiFiSensor>();
            configuration.CreateMap<CoreEntity.StartProcess, DTO.StartProcess>();
            configuration.CreateMap<CoreEntity.MessageInfo, DTO.MessageInfo>();
            configuration.CreateMap<CoreEntity.LocationStatistic, DTO.LocationStatistic>();

            //Reverse mapping from DTO to CoreEntity

            configuration.CreateMap<DTO.Location, CoreEntity.Location>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sensors, opt => opt.Ignore())
                .ForMember(dest => dest.Statistics, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());
            configuration.CreateMap<DTO.Sensor, CoreEntity.Sensor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .Include<DTO.WiFiConnectedSensor, CoreEntity.WiFiConnectedSensor>()
                .Include<DTO.WiFiSensor, CoreEntity.WiFiSensor>();
            configuration.CreateMap<DTO.Task, CoreEntity.Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Success, opt=> opt.Ignore())
                .Include<DTO.StartProcess, CoreEntity.StartProcess>()
                .Include<DTO.MessageInfo,CoreEntity.MessageInfo>();
            configuration.CreateMap<DTO.WiFiNetwork, CoreEntity.WiFiNetwork>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sensor, opt => opt.Ignore());
            configuration.CreateMap<DTO.Statistic, CoreEntity.Statistic>()
                .Include<DTO.LocationStatistic, CoreEntity.LocationStatistic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            configuration.CreateMap<DTO.WiFiConnectedSensor, CoreEntity.WiFiConnectedSensor>();
            configuration.CreateMap<DTO.WiFiSensor, CoreEntity.WiFiSensor>();
            configuration.CreateMap<DTO.StartProcess, CoreEntity.StartProcess>();
            configuration.CreateMap<DTO.MessageInfo, CoreEntity.MessageInfo>();
            configuration.CreateMap<DTO.LocationStatistic, CoreEntity.LocationStatistic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore());
        }
    }
}
