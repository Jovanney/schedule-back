using AutoMapper;
using ScheduleApp.Application.DTOs;
using ScheduleApp.Domain.Entities;

namespace ScheduleApp.Infrastructure.Profiles;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<CreateContactDto, Contact>();
        CreateMap<UpdateContactDto, Contact>();
    }
}
