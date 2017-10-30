namespace processo_seletivo_glaicon_peixer.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });
        }

    }
}