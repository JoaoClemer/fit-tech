using AutoMapper;
using FitTech.Comunication.Requests.Address;
using FitTech.Comunication.Requests.Employee;
using FitTech.Comunication.Requests.Gym;
using FitTech.Comunication.Requests.Plan;
using FitTech.Comunication.Requests.Student;
using FitTech.Comunication.Responses.Student;
using FitTech.Domain.Entities;

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

            CreateMap<RequestCreatePlanDTO, Domain.Entities.Plan>()
                .ForMember(entity => entity.Gym, config => config.Ignore());

            CreateMap<Student, ResponseStudentInListDTO>()
                .ForMember(dto => dto.Id, config => config.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, config => config.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.Email, config => config.MapFrom(entity => entity.EmailAddress))
                .ForMember(dto => dto.PlanName, config => config.MapFrom(entity => entity.StudentPlan.Plan.Name))
                .ForMember(dto => dto.PlanIsActive, config => config.MapFrom(entity => entity.StudentPlan.IsActive))
                .ForMember(dto => dto.PlanExpirationDate, config => config.MapFrom(entity => entity.StudentPlan.ExpirationDate));

        }
    }
}
