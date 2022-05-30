using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using ExpenseApi.Models.DTO;

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
                Nature = ExpenseApi.Models.Enum.ExpenseNature.Restaurant,
                UserId = 1
            };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("Date")
             && v.ErrorMessage!.Contains("Date cannot be in the future.")
            ));
        }

        [Fact]
        public void ExpenseDto_DateTooOld_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto()
            {
                Date = DateTime.Today
                        .AddMonths(-3)
                        .AddDays(-1),
                Comment = "minimum comment",
                Nature = ExpenseApi.Models.Enum.ExpenseNature.Restaurant,
                UserId = 1
            };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("Date")
             && v.ErrorMessage!.Contains("Date cannot be older than 3 months.")
            ));
        }

        [Fact]
        public void ExpenseDto_MissingComment_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto() { };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("Comment")
             && v.ErrorMessage!.Contains("The Comment field is required.")
            ));
        }

        [Fact]
        public void ExpenseDto_MissingNature_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto() { };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("Nature")
             && v.ErrorMessage!.Contains("Nature must be informed")
            ));
        }

        [Fact]
        public void ExpenseDto_MissingUserId_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto() { };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("UserId")
             && v.ErrorMessage!.Contains("UserId must be informed")
            ));
        }

        [Fact]
        public void ExpenseDto_Validate()
        {
            ExpenseDto expenseDto = new ExpenseDto()
            {
                Date = DateTime.Today.AddDays(-1),
                Comment = "minimum comment",
                Nature = ExpenseApi.Models.Enum.ExpenseNature.Restaurant,
                UserId = 1
            };

            List<ValidationResult> validationResults = ValidateModel(expenseDto);

            Assert.True(!validationResults.Any());
        }
    }
}