using Xunit;
using ExpenseApi.Controllers;
using AutoMapper;
using ExpenseApi.Repositories;
using ExpenseApi.Models.DTO;
using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using ExpenseApi.Models;
using System.Net;
using static ExpenseAPI.Tests.TestsHelper.TestsHelper;
using System.Linq;

namespace ExpenseAPI.Tests.Models.DTO
{
    public class ExpenseDtoTests
    {
        [Fact]
        public async void ExpenseDto_DateInFuture_NotValidate()
        {
            ExpenseDto expenseDto = new ExpenseDto()
            {
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1),
                Comment = "minimum comment"
            };

            Assert.True(ValidateModel(expenseDto).Any(
                v => v.MemberNames.Contains("Date")
             && v.ErrorMessage!.Contains("Date cannot be in the future.")
            ));
        }
    }
}