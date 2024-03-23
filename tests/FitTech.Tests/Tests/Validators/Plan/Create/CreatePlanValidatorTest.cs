using FitTech.Application.UseCases.Plan.Create;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.Validators.Plan.Create
{
    public class CreatePlanValidatorTest
    {
        [Fact]
        public void Valid_Success()
        {
            var validator = new CreatePlanValidator();

            var request = RequestCreatePlanBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Valid_Error_Empty_Name() 
        {
            var validator = new CreatePlanValidator();

            var request = RequestCreatePlanBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_NAME));
        }

        [Fact]
        public void Valid_Error_Empty_Price()
        {
            var validator = new CreatePlanValidator();

            var request = RequestCreatePlanBuilder.Build();
            request.Price = decimal.Zero;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PRICE));
        }

        [Fact]
        public void Valid_Error_Empty_Gym_Id()
        {
            var validator = new CreatePlanValidator();

            var request = RequestCreatePlanBuilder.Build();
            request.GymId = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_GYM_ID));
        }

        [Fact]
        public void Valid_Error_Empty_Plan_Type()
        {
            var validator = new CreatePlanValidator();

            var request = RequestCreatePlanBuilder.Build();
            request.PlanType = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PLAN_TYPE));
        }

    }
}
