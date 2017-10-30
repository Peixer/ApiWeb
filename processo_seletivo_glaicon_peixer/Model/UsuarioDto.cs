using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using processo_seletivo_glaicon_peixer.Validator;

namespace processo_seletivo_glaicon_peixer.Model
{
    public class UsuarioDto : IValidatableObject
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new UsuarioDtoValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}