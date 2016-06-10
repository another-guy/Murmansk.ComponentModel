using System.ComponentModel.DataAnnotations;

namespace Murmansk.ComponentModel.DataAnnotations
{
    public interface IValidationAttribute
    {
        ValidationResult IsValid(object value, ValidationContext validationContext);
    }
}
