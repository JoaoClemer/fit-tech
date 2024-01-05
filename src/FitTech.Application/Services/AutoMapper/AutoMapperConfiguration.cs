using AutoMapper;
using FitTech.Comunication.Requests.Address;
using FitTech.Comunication.Requests.Gym;

namespace FitTech.Application.Services.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<RequestCreateGymDTO, Domain.Entities.Gym>();

            CreateMap<RequestRegisterAddressDTO, Domain.Entities.Address>();
        }
    }
}
