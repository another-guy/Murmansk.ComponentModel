using System;
using System.ComponentModel.DataAnnotations;

namespace Murmansk.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class StringLength : ValidationAttribute
    {
        private readonly int minLength;
        private readonly int maxLength;

        public StringLength(int minLength, int maxLength)
        {
            if (minLength > maxLength)
            {
                throw new ArgumentException($"Min length ({minLength}) must be not greater than max length ({maxLength}).");
            }

            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var actualLength = value.ToString().Length;
            if (actualLength >= minLength && actualLength <= maxLength)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"{validationContext.DisplayName} length must be in range [{minLength}:{maxLength}]. Value '{value}' has length={actualLength}.");
        }
    }
}
