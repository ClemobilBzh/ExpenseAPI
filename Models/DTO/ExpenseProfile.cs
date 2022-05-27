using System;
using AutoMapper;

namespace ExpenseApi.Models.DTO
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseDto>()
                .ForMember(eo => eo.UserName, opt => opt.MapFrom(e => e.User.GetName()))
                .ForMember(eo => eo.AmountDisplay, opt => opt.MapFrom(e => e.Amount.GetAmoutInfo()))
                .ForMember(eo => eo.Amount, opt => opt.Ignore());

            CreateMap<ExpenseDto, Expense>();

            CreateMap<AmountDtoIn, AmountDetails>();
            CreateMap<UserDto, User>();
        }
    }
}
