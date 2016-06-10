using System;
using System.ComponentModel.DataAnnotations;

namespace Murmansk.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class StringLengthAttribute : ValidationAttributeBase
    {
        private readonly int minLength;
        private readonly int maxLength;

        public StringLengthAttribute(int minLength, int maxLength)
        {
            if (minLength < 0 || maxLength < 0)
            {
                throw new ArgumentException($"Range [{minLength}:{maxLength}] contains negative lengths.");
            }

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

            return new ValidationResult(GetErrorMessage(value, validationContext, actualLength));
        }

        private string GetErrorMessage(object value, ValidationContext validationContext, int actualLength)
        {
            var displayName = validationContext != null ? validationContext.DisplayName : "???";
            return $"{displayName} length must be in range [{minLength}:{maxLength}]. Value '{value}' has length={actualLength}.";
        }
    }
}
