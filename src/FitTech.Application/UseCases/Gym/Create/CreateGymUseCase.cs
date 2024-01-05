using AutoMapper;
using FitTech.Comunication.Requests.Gym;
using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Gym;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Gym.Create
{
    public class CreateGymUseCase : ICreateGymUseCase
    {
        private readonly IGymWriteOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateGymUseCase(IGymWriteOnlyRepository readOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestCreateGymDTO request)
        {
            Validate(request);

            var entity = _mapper.Map<Domain.Entities.Gym>(request);
            entity.Address.State.ToUpper();

            await _readOnlyRepository.CreateGym(entity);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestCreateGymDTO request)
        {
            var validator = new CreateGymValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }

    }
}
