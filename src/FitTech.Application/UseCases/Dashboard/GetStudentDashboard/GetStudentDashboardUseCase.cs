using FitTech.Application.Services.LoggedUser;
using FitTech.Comunication.Responses.Shared;
using FitTech.Domain.Repositories.Student;

namespace FitTech.Application.UseCases.Dashboard.GetStudentDashboard
{
    public class GetStudentDashboardUseCase : IGetStudentDashboardUseCase
    {
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly ILoggedUser _loggedUser;

        public GetStudentDashboardUseCase(IStudentReadOnlyRepository studentReadOnlyRepository, ILoggedUser loggedUser)
        {
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseDashboardDTO> Execute()
        {
            var loggedUser = await this._loggedUser.GetLoggedUser();

            var allStudentsOfGym = await _studentReadOnlyRepository.GetAllStudentsOfGym(loggedUser.Gym.Id);

            var studentsActive = allStudentsOfGym.Where(s => s.StudentPlan != null && s.StudentPlan.IsActive).Count();

            var studentsInative = allStudentsOfGym.Where(s => s.StudentPlan != null && s.StudentPlan.IsActive == false).Count();

            var studentsWithoutAPlan = allStudentsOfGym.Where(s => s.StudentPlan == null).Count();

            var response = new ResponseDashboardDTO
            {
                GymName = loggedUser.Gym.Name,
                Results = new List<ResponseInformationDTO>
                {
                    new ResponseInformationDTO
                    {
                        Title = "Active students",
                        Value = studentsActive.ToString()
                    },
                    new ResponseInformationDTO
                    {
                        Title = "Inactive  students",
                        Value = studentsInative.ToString()
                    },
                    new ResponseInformationDTO
                    {
                        Title = "Students without a plan",
                        Value = studentsWithoutAPlan.ToString()
                    },
                }
            };

            return response;
        
        }
    }
}
