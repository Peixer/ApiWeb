using processo_seletivo_glaicon_peixer.Model;

namespace processo_seletivo_glaicon_peixer.Data
{
    public class UsuarioRepository : EntityBaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext context) : base(context)
        {
        }
    }
}