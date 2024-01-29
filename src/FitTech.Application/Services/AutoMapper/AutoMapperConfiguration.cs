using AutoMapper;
using FitTech.Comunication.Requests.Address;
using FitTech.Comunication.Requests.Employee;
using FitTech.Comunication.Requests.Gym;
using FitTech.Comunication.Requests.Student;

namespace FitTech.Application.Services.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<RequestCreateGymDTO, Domain.Entities.Gym>();

            CreateMap<RequestRegisterAddressDTO, Domain.Entities.Address>();

            CreateMap<RequestCreateEmployeeDTO, Domain.Entities.Employee>()
                .ForMember(entity => entity.Gym, config => config.Ignore());

            CreateMap<RequestCreateStudentDTO, Domain.Entities.Student>()
                .ForMember(entity => entity.Gym, config => config.Ignore());
        }
    }
}
