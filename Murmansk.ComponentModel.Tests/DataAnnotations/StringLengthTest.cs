using System;
using System.ComponentModel.DataAnnotations;
using Murmansk.ComponentModel.DataAnnotations;
using Xunit;
using StringLengthAttribute = Murmansk.ComponentModel.DataAnnotations.StringLengthAttribute;

namespace Murmansk.ComponentModel.Tests.DataAnnotations
{
    public class StringLengthTest
    {
        private IValidationAttribute sut;

        [Theory]
        [InlineData(2, 1)]
        [InlineData(2, 0)]
        [InlineData(100, 99)]
        [InlineData(100, 50)]
        public void CanNotBeCreatedWithIncorrectRange(int minLength, int maxLength)
        {
            // Arrange
            // Act
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
            {
                sut = new StringLengthAttribute(minLength, maxLength);
            });

            // Assert
            Assert.Equal($"Min length ({minLength}) must be not greater than max length ({maxLength}).", exception.Message);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(-1, 1)]
        [InlineData(-1, -1)]
        public void CanNotBeCreatedWithNegativeLengths(int minLength, int maxLength)
        {
            // Arrange
            // Act
            var exception = Assert.ThrowsAny<ArgumentException>(() =>
            {
                sut = new StringLengthAttribute(minLength, maxLength);
            });

            // Assert
            Assert.Equal($"Range [{minLength}:{maxLength}] contains negative lengths.", exception.Message);
        }

        [Theory]
        [InlineData(0, 1, "")]
        [InlineData(0, 1, "a")]
        [InlineData(0, 2, "")]
        [InlineData(0, 2, "a")]
        [InlineData(0, 2, "ab")]
        [InlineData(1, 2, "a")]
        [InlineData(1, 1, "a")]
        [InlineData(2, 2, "ab")]
        public void CorrectlyReturnsValidResults(int minLength, int maxLength, object objectToValidate)
        {
            // Arrange
            sut = new StringLengthAttribute(minLength, maxLength);

            // Act
            var validationResult = sut.IsValid(objectToValidate, null);

            // Assert
            Assert.Equal(ValidationResult.Success, validationResult);
        }

        [Theory]
        [InlineData(0, 1, "ab")]
        [InlineData(0, 2, "abc")]
        [InlineData(1, 2, "")]
        [InlineData(1, 2, "abc")]
        [InlineData(2, 2, "")]
        [InlineData(2, 2, "a")]
        [InlineData(2, 2, "abc")]
        public void CorrectlyReturnsInValidResults(int minLength, int maxLength, object objectToValidate)
        {
            // Arrange
            sut = new StringLengthAttribute(minLength, maxLength);

            // Act
            var validationResult = sut.IsValid(objectToValidate, ValidationContextFactory.Create());

            // Assert
            Assert.NotEqual(ValidationResult.Success, validationResult);

            Assert.Contains("The field", validationResult.ErrorMessage);
            Assert.Contains("is invalid.", validationResult.ErrorMessage);
        }
    }
}
