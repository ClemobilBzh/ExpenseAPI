using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using static ExpenseAPI.Constants.ErrorMessage;
using ExpenseAPI.Models.DTO;

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
                v => (v.MemberNames.Contains(nameof(AmountDto.CurrencyId))
                && v.ErrorMessage!.Contains(CURRENCY_REQUIRED))
                || (v.MemberNames.Contains(nameof(AmountDto.Amount))
                 && v.ErrorMessage!.Contains("The field Amount must be between 0,01 and 1000000."))
            ));
        }
    }
}
