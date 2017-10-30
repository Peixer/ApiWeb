using FluentValidation;
using processo_seletivo_glaicon_peixer.Model;

namespace processo_seletivo_glaicon_peixer.Validator
{
    public class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidator()
        {
            RuleFor(s => s.Nome).NotEmpty().WithMessage("Nome incorreto");
            RuleFor(s => s.Email).NotEmpty().EmailAddress().WithMessage("Email incorreto");
        }
    }
}