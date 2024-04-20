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

            var studentsActive = allStudentsOfGym.Where(s => s.StudentPlan != null && s.StudentPlan.IsActive).ToList();

            var studentsInative = allStudentsOfGym.Where(s => s.StudentPlan != null && s.StudentPlan.IsActive == false).Count();

            var studentsWithoutAPlan = allStudentsOfGym.Where(s => s.StudentPlan == null).Count();

            var amountOfActivePlans = studentsActive.Select(s => s.StudentPlan.Plan.Price).ToList().Sum();

            var response = new ResponseDashboardDTO
            {
                GymName = loggedUser.Gym.Name,
                Results = new List<ResponseInformationDTO>
                {
                    new ResponseInformationDTO
                    {
                        Title = "Alunos ativos",
                        Value = studentsActive.Count().ToString()
                    },
                    new ResponseInformationDTO
                    {
                        Title = "Alunos inativos",
                        Value = studentsInative.ToString()
                    },
                    new ResponseInformationDTO
                    {
                        Title = "Alunos sem plano",
                        Value = studentsWithoutAPlan.ToString()
                    },
                    new ResponseInformationDTO
                    {
                        Title = "Valor total ativos",
                        Value = amountOfActivePlans.ToString("N3")
                    }
                }
            };

            return response;
        
        }
    }
}
