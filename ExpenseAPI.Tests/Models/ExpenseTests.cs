using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using ExpenseApi.Models;

using static ExpenseAPI.Tests.TestsHelper.TestsHelper;

using Xunit;

namespace ExpenseAPI.Tests.Models
{
    public class ExpenseTests
    {
        [Fact]
        public void Expense_UserMissing_NotValidate()
        {
            Expense expense = new Expense() { };

            List<ValidationResult> validationResults = ValidateModel(expense);

            Assert.Contains(validationResults,
                v => v.ErrorMessage == "User must be registered before its use."
            );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("User")
            ));
        }

        [Fact]
        public void Expense_InconsistentUserCurrency_NotValidate()
        {
            User user = new User()
            {
                Currency = new Currency() { Id = 1 },
                Expenses = new List<Expense>()
            };
            Expense expense = new Expense()
            {
                User = user,
                AmountDetails = new AmountDetails()
                { Currency = new Currency() { Id = 2 } }
            };

            List<ValidationResult> validationResults = ValidateModel(expense);

            Assert.Contains(validationResults,
                v => v.ErrorMessage == "Expense currency is inconsistent with user currency."
            );
            Assert.True(validationResults.Any(
                v => v.MemberNames.Contains("AmountDetails")
            ));
        }

        [Fact]
        public void Expense_DoubleExpense_NotValidate()
        {
            User user = new User()
            {
                Currency = new Currency() { Id = 1 },
                Expenses = new List<Expense>()
            };
            Expense registeredExpense = new Expense()
            {
                Id = 1,
                User = user,
                AmountDetails = new AmountDetails()
                {
                    Currency = new Currency() { Id = 1 },
                    Amount = 12.34F
                }
            };
            Expense newExpense = new Expense()
            {
                User = user,
                AmountDetails = new AmountDetails()
                {
                    Currency = new Currency() { Id = 1 },
                    Amount = 12.34F
                }
            };

            user.Expenses.Add(registeredExpense);

            List<ValidationResult> validationResults = ValidateModel(newExpense);

            Assert.Contains(validationResults, v =>
               v.ErrorMessage == "Expense has been already registered."
            );
        }

        [Fact]
        public void Expense_Validate()
        {
            User user = new User()
            {
                Currency = new Currency() { Id = 1 },
                Expenses = new List<Expense>()
            };

            Expense newExpense = new Expense()
            {
                User = user,
                AmountDetails = new AmountDetails()
                {
                    Currency = new Currency() { Id = 1 },
                    Amount = 12.34F
                }
            };

            List<ValidationResult> validationResults = ValidateModel(newExpense);

            Assert.Empty(validationResults);
        }
    }
}
