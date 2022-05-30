using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Tests.TestsHelper
{
    public static class TestsHelper
    {
        public static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
