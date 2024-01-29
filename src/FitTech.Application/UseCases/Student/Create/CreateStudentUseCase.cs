using FitTech.Comunication.Requests.Student;
using FitTech.Comunication.Responses;
using FitTech.Domain.Repositories.Gym;
using FitTech.Domain.Repositories.Student;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Exceptions;
using FitTech.Application.Services.CPFValidator;
using AutoMapper;
using FitTech.Application.Services.Cryptography;
using FitTech.Domain.Repositories;
using FitTech.Application.Services.Token;

namespace FitTech.Application.UseCases.Student.Create
{
    public class CreateStudentUseCase : ICreateStudentUseCase
    {
        private readonly IStudentReadOnlyRepository _readOnlyRepository;
        private readonly IStudentWriteOnlyRepository _writeOnlyRepository;
        private readonly IGymReadOnlyRepository _gymReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly TokenController _tokenController;

        public CreateStudentUseCase(
            IStudentReadOnlyRepository readOnlyRepository, 
            IStudentWriteOnlyRepository writeOnlyRepository, 
            IGymReadOnlyRepository gymReadOnlyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            PasswordEncryptor passwordEncryptor,
            TokenController tokenController)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _gymReadOnlyRepository = gymReadOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordEncryptor = passwordEncryptor;
            _tokenController = tokenController;
        }

        public async Task<ResponseCreateStudentDTO> Execute(RequestCreateStudentDTO request)
        {
            await Validate(request);

            var entity = _mapper.Map<Domain.Entities.Student>(request);
            var gymEntity = await _gymReadOnlyRepository.GetGymById(request.GymId);

            entity.Gym = gymEntity;
            entity.Password = _passwordEncryptor.Encrypt(request.Password);

            await _writeOnlyRepository.CreateStudent(entity);

            await _unitOfWork.Commit();

            var token = _tokenController.GenerateToken(entity.EmailAddress);

            return new ResponseCreateStudentDTO
            {
                EmailAddress = entity.EmailAddress,
                GymName = entity.Gym.Name,
                Token = token
            };
        }

        private async Task Validate(RequestCreateStudentDTO request)
        {
            var validator = new CreateStudentValidator();

            var result = validator.Validate(request);

            var gymIsValid = await _gymReadOnlyRepository.GetGymById(request.GymId);
            if (gymIsValid == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("GymId", ResourceErrorMessages.GYM_NOT_FOUND));
            }

            var emailInUse = await _readOnlyRepository.GetStudentByEmail(request.EmailAddress);
            if (emailInUse != null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("EmailAddress", ResourceErrorMessages.STUDENT_EMAIL_IN_USE));
            }

            var cpfInUse = await _readOnlyRepository.GetStudentByCPF(request.Cpf);
            if (cpfInUse != null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("Cpf", ResourceErrorMessages.STUDENT_CPF_IN_USE));
            }

            var CPFValidator = new CPFValidator();

            var cpfIsValid = CPFValidator.ValidateCPF(request.Cpf);
            if (!cpfIsValid)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("Cpf", ResourceErrorMessages.INVALID_CPF));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }
    }
}
