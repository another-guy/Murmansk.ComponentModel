using System.ComponentModel.DataAnnotations;

namespace Murmansk.ComponentModel.DataAnnotations
{
    public abstract class ValidationAttributeBase : ValidationAttribute, IValidationAttribute
    {
        ValidationResult IValidationAttribute.IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}
