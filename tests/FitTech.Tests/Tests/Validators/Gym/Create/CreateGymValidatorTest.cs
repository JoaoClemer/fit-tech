using FitTech.Application.UseCases.Gym.Create;
using FitTech.Comunication.Requests.Gym;
using Xunit;
using FluentAssertions;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;

namespace FitTech.Tests.Tests.Validators.Gym.Create
{
    public class CreateGymValidatorTest
    {
        [Fact]
        public void Valid_Success()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Valid_Error_Empty_Name()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_NAME));

        }

        [Fact]
        public void Valid_Error_Empty_Email()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.EmailAddress = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_EMAIL));

        }

        [Fact]
        public void Valid_Error_Empty_Phone_Number()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.PhoneNumber = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PHONE_NUMBER));

        }

        [Fact]
        public void Valid_Error_Empty_Address()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_ADDRESS));

        }

        [Fact]
        public void Valid_Error_Invalid_Email()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.EmailAddress = "fitemail.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));

        }

        [Fact]
        public void Valid_Error_Invalid_Phone_Number()
        {
            var validator = new CreateGymValidator();

            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.PhoneNumber = "1112345";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PHONE_NUMBER));

        }
    }
}
