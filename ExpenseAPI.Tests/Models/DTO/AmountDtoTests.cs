using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using ExpenseApi.Models.DTO;

using static ExpenseAPI.Tests.TestsHelper.TestsHelper;

using Xunit;

namespace ExpenseAPI.Tests.Models.DTO
{
    public class AmountDtoTests
    {
        [Fact]
        public void ExpenseDto_MissingObject_NotValidate()
        {
            AmountDto amountDto = new AmountDto();

            List<ValidationResult> validationResults = ValidateModel(amountDto);

            Assert.True(validationResults.Any(
                v => (v.MemberNames.Contains("CurrencyId")
                && v.ErrorMessage!.Contains("CurrencyId must be informed"))
                || (v.MemberNames.Contains("Amount")
                 && v.ErrorMessage!.Contains("The field Amount must be between 0,01 and 1000000."))
            ));
        }
    }
}
