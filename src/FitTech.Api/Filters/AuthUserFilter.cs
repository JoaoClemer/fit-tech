using FitTech.Application.Services.Token;
using FitTech.Comunication.Responses;
using FitTech.Domain.Repositories.Employee;
using FitTech.Domain.Repositories.Student;
using FitTech.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace FitTech.Api.Filters
{
    public class AuthUserFilter : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly IEmployeeReadOnlyRepository _employeeReadOnlyRepository;

        public AuthUserFilter(
            TokenController tokenController, 
            IStudentReadOnlyRepository studentReadOnlyRepository,
            IEmployeeReadOnlyRepository employeeReadOnlyRepository)
        {
            _tokenController = tokenController;
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _employeeReadOnlyRepository = employeeReadOnlyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenInRequest(context);

                var user = _tokenController.RecoverUser(token);
                var studentUser = await _studentReadOnlyRepository.GetStudentByEmail(user.userEmail);
                var employeeUser = await _employeeReadOnlyRepository.GetEmployeeByEmail(user.userEmail);

                if (studentUser is null && employeeUser is null)
                {
                    throw new SystemException();
                }
            }
            catch(SecurityTokenExpiredException ex)
            {
                ExpiredToken(context);
            }
            catch
            {
                UserWithoutPermission(context);
            }
            
        }
    
        private string TokenInRequest(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if(string.IsNullOrWhiteSpace(authorization))
            {
                throw new SystemException();
            }

            var token = authorization["Bearer".Length..].Trim();

            return token;
        }

        private void ExpiredToken(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorDTO(ResourceErrorMessages.EXPIRED_TOKEN));
        }

        private void UserWithoutPermission(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorDTO(ResourceErrorMessages.USER_WITHOUT_PERMISSION));
        }

    }

    
}
