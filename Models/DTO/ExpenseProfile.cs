using System;
using AutoMapper;

namespace ExpenseApi.Models.DTO
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseDtoOut>()
                .ForMember(eo => eo.UserName, opt => opt.MapFrom(e => e.User.GetName()))
                .ForMember(eo => eo.Amount, opt => opt.MapFrom(e => e.Amount.GetAmoutInfo()));

            CreateMap<ExpenseDtoIn, Expense>();

            CreateMap<AmountDtoIn, AmountInContext>();
            CreateMap<UserDto, User>();
        }
    }
}
