using AutoMapper;
using Phone.Application.DTOs;
using Phone.Domain.Entities;

namespace Phone.Application.Mappings;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        // Saída
        CreateMap<Contact, ContactDto>();
        CreateMap<Address, AddressDto>();

        // Entrada
        CreateMap<CreateAddressDto, Address>();
        CreateMap<CreateContactDto, Contact>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CriadoEm, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.AtualizadoEm, o => o.MapFrom(_ => DateTime.UtcNow));
    }
}
