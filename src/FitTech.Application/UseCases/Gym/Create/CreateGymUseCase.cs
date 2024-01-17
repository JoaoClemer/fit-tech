using AutoMapper;
using FitTech.Comunication.Requests.Gym;
using FitTech.Comunication.Responses;
using FitTech.Comunication.Responses.Gym;
using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Gym;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Gym.Create
{
    public class CreateGymUseCase : ICreateGymUseCase
    {
        private readonly IGymWriteOnlyRepository _writeOnlyRepository;
        private readonly IGymReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateGymUseCase(IGymWriteOnlyRepository writeOnlyRepository, IGymReadOnlyRepository readOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseCreateGymDTO> Execute(RequestCreateGymDTO request)
        {
            await Validate(request);

            var entity = _mapper.Map<Domain.Entities.Gym>(request);
            entity.Address.State.ToUpper();

            await _writeOnlyRepository.CreateGym(entity);

            await _unitOfWork.Commit();

            return new ResponseCreateGymDTO
            {
                EmailAddress = entity.EmailAddress,
                Name = entity.Name
            };
        }

        private async Task Validate(RequestCreateGymDTO request)
        {
            var validator = new CreateGymValidator();

            var result = validator.Validate(request);

            var emailInUSe = await _readOnlyRepository.GetGymByEmail(request.EmailAddress);
            if (emailInUSe != null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("EmailAddress",ResourceErrorMessages.GYM_EMAIL_IN_USE));
            }

            var nameInUse = await _readOnlyRepository.GetGymByName(request.Name);
            if (nameInUse != null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("Name", ResourceErrorMessages.GYM_NAME_IN_USE));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }

    }
}
