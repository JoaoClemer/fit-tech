using AutoMapper;
using FitTech.Application.Services.LoggedUser;
using FitTech.Comunication.Requests.Shared;
using FitTech.Comunication.Responses.Shared;
using FitTech.Comunication.Responses.Student;
using FitTech.Domain.Repositories.Student;

namespace FitTech.Application.UseCases.Student.GetAllStudentsOfGym
{
    public class GetAllStudentsOfGymUseCase : IGetAllStudentsOfGymUseCase
    {
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;
        public GetAllStudentsOfGymUseCase(IStudentReadOnlyRepository studentReadOnlyRepository, ILoggedUser loggedUser, IMapper mapper)
        {
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }
        public async Task<ResponseListForTableDTO<ResponseStudentInList>> Execute(RequestFilterDTO filter)
        {
            var loggedUser = await this._loggedUser.GetLoggedUser();

            var allStudentsOfGym = await _studentReadOnlyRepository.GetAllStudentsOfGym(loggedUser.Gym.Id);

            if (filter.OnlyIsActive)
                allStudentsOfGym = allStudentsOfGym.Where(s => s.StudentPlan != null && s.StudentPlan.IsActive.Equals(true)).ToList();
            else if (filter.OnlyIsNotActive)
                allStudentsOfGym = allStudentsOfGym.Where(s => s.StudentPlan.Equals(null) || s.StudentPlan.IsActive.Equals(false)).ToList();

            if (string.IsNullOrEmpty(filter.FilterText))
                allStudentsOfGym = allStudentsOfGym.Where(s => s.Name.ToUpper().Contains(filter.FilterText.ToUpper())).ToList();

            var studentsPerPage = 10;
            var pageCount = (allStudentsOfGym.Count() + studentsPerPage - 1) / studentsPerPage;

            allStudentsOfGym = allStudentsOfGym.Skip((filter.PageNumber - 1) * studentsPerPage).Take(studentsPerPage).ToList();

            var studentList = _mapper.Map<List<ResponseStudentInList>>(allStudentsOfGym);

            var response = new ResponseListForTableDTO<ResponseStudentInList>(studentList, filter.PageNumber, studentsPerPage, pageCount);

            return response;
        }

    }
}
