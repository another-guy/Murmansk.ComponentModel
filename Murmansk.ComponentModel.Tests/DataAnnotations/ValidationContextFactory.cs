using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Murmansk.ComponentModel.Tests.DataAnnotations
{
    public static class ValidationContextFactory
    {
        public static ValidationContext Create()
        {
            return new ValidationContext("", new Dictionary<object, object>());
        }
    }
}
