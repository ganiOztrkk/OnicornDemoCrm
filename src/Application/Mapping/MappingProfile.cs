using Application.Features.Announcement.GetAll;
using Application.Features.Customer.Create;
using Application.Features.Customer.GetAll;
using Application.Features.Customer.GetById;
using Application.Features.Customer.Update;
using Application.Features.Sale.Create;
using Application.Features.Sale.GetAll;
using Application.Features.Sale.GetById;
using Application.Features.Sale.Update;
using Application.Features.Ticket.GetAll;
using Application.Features.User.Create;
using Application.Features.User.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, GetAllCustomersQueryResponse>().ReverseMap();
        CreateMap<Customer, GetByCustomerIdQueryResponse>().ReverseMap();
        CreateMap<CreateCustomerCommandRequest, Customer>().ReverseMap();
        CreateMap<UpdateCustomerCommandRequest, Customer>().ReverseMap();
        
        CreateMap<Sale, GetAllSaleQueryResponse>().ReverseMap();
        CreateMap<Sale, GetBySaleIdQueryResponse>().ReverseMap();
        CreateMap<CreateSaleCommandRequest, Sale>().ReverseMap();
        CreateMap<UpdateSaleCommandRequest, Sale>().ReverseMap();
        
        CreateMap<Ticket, GetAllTicketsQueryResponse>().ReverseMap();
        
        CreateMap<CreateUserCommandRequest, AppUser>().ReverseMap();
        CreateMap<UpdateUserCommandRequest, AppUser>().ReverseMap();
        
        CreateMap<Announcement, GetAllAnnouncementsQueryResponse>().ReverseMap();

    }
}