using FitTech.Comunication.Requests.Plan;
using FitTech.Exceptions;
using FluentValidation;

namespace FitTech.Application.UseCases.Plan.Create
{
    public class CreatePlanValidator : AbstractValidator<RequestCreatePlanDTO>
    {
        public CreatePlanValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NAME);

            RuleFor(r => r.Price).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PRICE);

            RuleFor(r => r.GymId).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_GYM_ID);

            RuleFor(r => r.PlanType).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PLAN_TYPE);
        }
    }
}
