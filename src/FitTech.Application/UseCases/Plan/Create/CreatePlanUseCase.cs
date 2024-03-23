using AutoMapper;
using FitTech.Comunication.Requests.Plan;
using FitTech.Comunication.Responses.Plan;
using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Gym;
using FitTech.Domain.Repositories.Plan;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Plan.Create
{
    public class CreatePlanUseCase : ICreatePlanUseCase
    {
        private readonly IGymReadOnlyRepository _gymReadOnlyRepository;
        private readonly IPlanReadOnlyRepository _planReadOnlyRepository;
        private readonly IPlanWriteOnlyRepository _planWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlanUseCase(
            IGymReadOnlyRepository gymReadOnlyRepository,
            IPlanWriteOnlyRepository planWriteOnlyRepository,
            IPlanReadOnlyRepository planReadOnlyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _gymReadOnlyRepository = gymReadOnlyRepository;
            _planReadOnlyRepository = planReadOnlyRepository;
            _planWriteOnlyRepository = planWriteOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseCreatePlanDTO> Execute(RequestCreatePlanDTO request)
        {
            await Validate(request);
            var entity = _mapper.Map<Domain.Entities.Plan>(request);
            var gymEntity = await _gymReadOnlyRepository.GetGymById(request.GymId);

            entity.Gym = gymEntity;

            await _planWriteOnlyRepository.CreatePlan(entity);

            await _unitOfWork.Commit();

            return new ResponseCreatePlanDTO
            {
                Name = entity.Name,
                Price = entity.Price,
                PlanType = entity.PlanType.ToString()
            };
        }

        private async Task Validate(RequestCreatePlanDTO request)
        {
            var validator = new CreatePlanValidator();
            var result = validator.Validate(request);

            var gymIsValid = await _gymReadOnlyRepository.GetGymById(request.GymId);
            if (gymIsValid == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("GymId", ResourceErrorMessages.GYM_NOT_FOUND));
            }

            var planNameInUse = await _planReadOnlyRepository.PlanNameIsInUse(request.Name, request.GymId);
            if (planNameInUse)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("Name", ResourceErrorMessages.PLAN_NAME_IN_USE));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }

        }
    }
}
