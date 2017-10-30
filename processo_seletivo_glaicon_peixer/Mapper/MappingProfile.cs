using AutoMapper;
using processo_seletivo_glaicon_peixer.Model;

namespace processo_seletivo_glaicon_peixer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Usuario, UsuarioSimpleDto>();
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}
