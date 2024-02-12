using FitTech.Comunication.Requests.Student;
using FitTech.Domain.Repositories.Gym;
using FitTech.Domain.Repositories.Student;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Exceptions;
using FitTech.Application.Services.CPFValidator;
using AutoMapper;
using FitTech.Application.Services.Cryptography;
using FitTech.Domain.Repositories;
using FitTech.Application.Services.Token;
using FitTech.Domain.Enum;
using FitTech.Domain.Entities;
using FitTech.Comunication.Responses.Student;

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

            entity.RegistrationNumber = await CreateUniqueRegisterNumber();
            entity.Gym = gymEntity;
            entity.Password = _passwordEncryptor.Encrypt(request.Password);

            entity.Plan = new Plan
            {
                ExpirationDate = DateTime.UtcNow,
                IsActive = false,
                Name = PlanType.NoPlan.ToString(),
                PlanType = PlanType.NoPlan,
                Price = 0
            };

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

        private async Task<int> CreateUniqueRegisterNumber()
        {
            var registerNumber = GenerateUniqueRegisterNumber();

            var registerNumberIsUnique = !await _readOnlyRepository.IsRegisterNumberUnique(registerNumber);

            while (!registerNumberIsUnique)
                registerNumber = GenerateUniqueRegisterNumber();

            return registerNumber;
        }

        private int GenerateUniqueRegisterNumber()
        {
            var random = new Random();

            return random.Next(1, 999999999);
        }
    }
}
