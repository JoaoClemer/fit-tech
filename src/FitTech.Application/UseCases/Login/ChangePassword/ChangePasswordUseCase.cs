using FitTech.Application.Services.Cryptography;
using FitTech.Application.Services.LoggedUser;
using FitTech.Comunication.Requests.Login;
using FitTech.Domain.Entities;
using FitTech.Domain.Enum;
using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Employee;
using FitTech.Domain.Repositories.Student;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Login.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IEmployeeUpdateOnlyRepository _employeeUpdateOnlyRepository;
        private readonly IStudentUpdateOnlyRepository _studentUpdateOnlyRepository;
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly IEmployeeReadOnlyRepository _employeeReadOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly IUnitOfWork _unitOfWork;
        public ChangePasswordUseCase(
            IEmployeeUpdateOnlyRepository employeeUpdateOnlyRepository,
            IStudentUpdateOnlyRepository studentUpdateOnlyRepository,
            IStudentReadOnlyRepository studentReadOnlyRepository,
            IEmployeeReadOnlyRepository employeeReadOnlyRepository,
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork,
            PasswordEncryptor passwordEncryptor)
        {
            _employeeUpdateOnlyRepository = employeeUpdateOnlyRepository;
            _studentUpdateOnlyRepository = studentUpdateOnlyRepository;
            _employeeReadOnlyRepository = employeeReadOnlyRepository;
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _loggedUser = loggedUser;
            _passwordEncryptor = passwordEncryptor;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(RequestChangePasswordDTO request)
        {
            var loggedUser = await _loggedUser.GetLoggedUser();

            Valid(request, loggedUser);

            if(loggedUser.GetUserType().Equals(UserType.Student))
            {
                var studentUser = await _studentReadOnlyRepository.GetStudentById(loggedUser.Id);
                studentUser.Password = _passwordEncryptor.Encrypt(request.NewPassword);

                _studentUpdateOnlyRepository.Update(studentUser);

            }else
            {
                var employeeUser = await _employeeReadOnlyRepository.GetEmployeeById(loggedUser.Id);
                employeeUser.Password = _passwordEncryptor.Encrypt(request.NewPassword);

                _employeeUpdateOnlyRepository.Update(employeeUser);
            }

            await _unitOfWork.Commit();

        }

        private void Valid(RequestChangePasswordDTO request, User user)
        {
            var validator = new ChangePasswordValidator();

            var result = validator.Validate(request);

            var currentPasswordCript = _passwordEncryptor.Encrypt(request.CurrentPassword);

            if(!user.Password.Equals(currentPasswordCript))
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("currentPassword", ResourceErrorMessages.INVALID_CURRENT_PASSWORD));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }
    }
}
