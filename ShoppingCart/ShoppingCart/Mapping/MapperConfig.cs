using AutoMapper;

namespace habahabamall.Mapping
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainToModelMappingProfile>();
                config.AddProfile<ModelToDomainMappingProfile>();
                // config.ValidateInlineMaps = false;
            });
        }
    }
}