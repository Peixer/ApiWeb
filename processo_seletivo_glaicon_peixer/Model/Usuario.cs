using System;
using processo_seletivo_glaicon_peixer.Data;

namespace processo_seletivo_glaicon_peixer.Model
{
    public class Usuario : IEntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public Guid Guid { get; set; }
    }
}