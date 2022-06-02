using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using static ExpenseAPI.Constants.ErrorMessage;
using static ExpenseAPI.Constants.ValidationRules;
using ExpenseAPI.Models.DTO;

using static ExpenseAPI.Tests.TestsHelper.TestsHelper;

using Xunit;

namespace ExpenseAPI.Tests.Models.DTO
{
    public class ExpenseDtoTests
    {
        [Fact]
        public void ExpenseDto_DateInFuture_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto()
            {
                Date = DateTime.Today.AddDays(1),
                Comment = "minimum comment",
                Nature = ExpenseAPI.Models.Enum.ExpenseNature.Restaurant,
                UserId = 1
            };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.Contains(validationResults,
                v => v.ErrorMessage == DATE_IN_FUTURE
            );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains(nameof(ExpenseDto.Date))
            ));
        }

        [Fact]
        public void ExpenseDto_DateTooOld_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto()
            {
                Date = DateTime.Today
                        .AddMonths(-MAX_RETURN_IN_PAST_IN_MONTHS)
                        .AddDays(-1),
                Comment = "minimum comment",
                Nature = ExpenseAPI.Models.Enum.ExpenseNature.Restaurant,
                UserId = 1
            };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.Contains(validationResults,
                v => v.ErrorMessage == String.Format(DATE_TOO_OLD, MAX_RETURN_IN_PAST_IN_MONTHS)
            );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains(nameof(ExpenseDto.Date))
            ));
        }

        [Fact]
        public void ExpenseDto_MissingComment_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto() { };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.Contains(validationResults,
                            v => v.ErrorMessage == "The Comment field is required."
                        );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains(nameof(expenseDto.Comment))
            ));
        }

        [Fact]
        public void ExpenseDto_MissingNature_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto() { };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.Contains(validationResults,
                            v => v.ErrorMessage == NATURE_ID_REQUIRED
                        );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains(nameof(expenseDto.Nature))
            ));
        }

        [Fact]
        public void ExpenseDto_MissingUserId_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto() { };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.Contains(validationResults,
                v => v.ErrorMessage == USER_ID_REQUIRED
            );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains(nameof(expenseDto.UserId))
            ));
        }

        [Fact]
        public void ExpenseDto_Validate()
        {
            ExpenseDto expenseDto = new ExpenseDto()
            {
                Date = DateTime.Today.AddDays(-1),
                Comment = "minimum comment",
                Nature = ExpenseAPI.Models.Enum.ExpenseNature.Restaurant,
                UserId = 1
            };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(!validationResults.Any());
        }
    }
}