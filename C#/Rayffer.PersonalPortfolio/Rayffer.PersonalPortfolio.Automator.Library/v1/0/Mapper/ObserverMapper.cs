using AutoMapper;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Mapper
{
    public class ObserverMapper : IObserverMapper
    {
        private IMapper mapper = null;
        private MapperConfiguration mapperConfiguration = null;

        public ObserverMapper()
        {
            ConfigureMappers();
        }

        private void ConfigureMappers()
        {
            mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AllowNullCollections = true;

                DeclareActionMappings(configuration);
            });

            mapper = mapperConfiguration.CreateMapper();
        }

        private void DeclareActionMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ActionInformation, UploadItem>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => System.IO.Path.GetFileName(src.SourceFilePath)))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => new FileInfo(src.SourceFilePath).Directory.FullName))
                .ForMember(dest => dest.FileBytes, opt => opt.Ignore())
                .ForMember(dest => dest.UploadTime, opt => opt.Ignore());
        }

        public void AssertConfigurationIsValid()
        {
            mapperConfiguration.AssertConfigurationIsValid();
        }

        public T Map<T>(object source)
        {
            if (source == null)
            {
                return default(T);
            }

            return mapper.Map<T>(source);
        }

        public T2 Map<T1, T2>(T1 source, T2 destination)
        {
            if (source == null)
                return destination;

            return mapper.Map<T1, T2>(source, destination);
        }
    }
}