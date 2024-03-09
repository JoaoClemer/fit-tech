using FitTech.Application.Services.Cryptography;
using FitTech.Application.Services.Token;
using FitTech.Comunication.Enum;
using FitTech.Comunication.Requests.Login;
using FitTech.Comunication.Responses.Login;
using FitTech.Domain.Entities;
using FitTech.Domain.Enum;
using FitTech.Domain.Repositories.Employee;
using FitTech.Domain.Repositories.Student;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly IEmployeeReadOnlyRepository _employeeReadOnlyRepository;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly TokenController _tokenController;

        public DoLoginUseCase(
            IStudentReadOnlyRepository studentReadOnlyRepository,
            IEmployeeReadOnlyRepository employeeReadOnlyRepository,
            PasswordEncryptor passwordEncryptor,
            TokenController tokenController)
        {
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _employeeReadOnlyRepository = employeeReadOnlyRepository;
            _passwordEncryptor = passwordEncryptor;
            _tokenController = tokenController;
            
        }
        public async Task<ResponseDoLoginDTO> Execute(RequestDoLoginDTO request)
        {
            Validate(request);

            var encryptPassword = _passwordEncryptor.Encrypt(request.Password);
            
            if(request.UserType.Equals(UserTypeDTO.Employee))
            {
                var employeeUser = await _employeeReadOnlyRepository.Login(request.EmailAddress, encryptPassword);

                if(employeeUser == null)
                {
                    throw new InvalidLoginException();
                }

                var employeeUserToken = _tokenController.GenerateToken(employeeUser.EmailAddress, UserType.Employee.ToString());

                return new ResponseDoLoginDTO
                {
                    Name = employeeUser.Name,
                    Token = employeeUserToken
                };
            }

            var studentUser = await _studentReadOnlyRepository.Login(request.EmailAddress, encryptPassword);

            if(studentUser == null)
            {
                throw new InvalidLoginException();
            }

            var studentUserToken = _tokenController.GenerateToken(studentUser.EmailAddress, UserType.Student.ToString());

            return new ResponseDoLoginDTO
            {
                Name = studentUser.Name,
                Token = studentUserToken
            };
        }

        private void Validate(RequestDoLoginDTO request)
        {
            var validator = new DoLoginValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }
    }
}
