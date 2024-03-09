using FitTech.Application.Services.Token;
using FitTech.Domain.Entities;
using FitTech.Domain.Enum;
using FitTech.Domain.Repositories.Employee;
using FitTech.Domain.Repositories.Student;
using Microsoft.AspNetCore.Http;

namespace FitTech.Application.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly TokenController _tokenController;
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly IEmployeeReadOnlyRepository _employeeReadOnlyRepository;
        public LoggedUser(
            IHttpContextAccessor httpContextAccessor,
            TokenController tokenController,
            IStudentReadOnlyRepository studentReadOnlyRepository,
            IEmployeeReadOnlyRepository employeeReadOnlyRepository)
        {
            _contextAccessor = httpContextAccessor;
            _tokenController = tokenController;
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _employeeReadOnlyRepository = employeeReadOnlyRepository;
        }
        public async Task<User> GetLoggedUser()
        {
            var authorization = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorization["Bearer".Length..].Trim();

            var user = _tokenController.RecoverUser(token);

            if(user.userType.Equals(UserType.Student.ToString()))
            {
                var studentUser = await _studentReadOnlyRepository.GetStudentByEmail(user.userEmail);
                return studentUser;
            }

            var employeeUser = await _employeeReadOnlyRepository.GetEmployeeByEmail(user.userEmail);
            return employeeUser;
        }
    }
}
