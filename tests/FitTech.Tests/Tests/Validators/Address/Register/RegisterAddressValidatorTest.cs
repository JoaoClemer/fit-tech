using FitTech.Application.UseCases.Address;
using FitTech.Application.UseCases.Gym.Create;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.Validators.Address.Register
{
    public class RegisterAddressValidatorTest
    {
        [Fact]
        public void Valid_Success()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Valid_Error_Empty_Country()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.Country = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_COUNTRY));

        }

        [Fact]
        public void Valid_Error_Empty_Street()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.Street = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_STREET));

        }

        [Fact]
        public void Valid_Error_Empty_PostalCode()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.PostalCode = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_POSTAL_CODE));

        }

        [Fact]
        public void Valid_Error_Empty_State()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.State = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_STATE));

        }

        [Fact]
        public void Valid_Error_Empty_City()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.City = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_CITY));

        }

        [Fact]
        public void Valid_Empty_Number()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.Number = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_NUMBER));

        }

        [Fact]
        public void Valid_Error_Invalid_Format_PostalCode()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.PostalCode = "43222000";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_POSTAL_CODE));

        }

        [Fact]
        public void Valid_Error_Invalid_Format_State()
        {
            var validator = new RegisterAddressValidator();

            var request = RequestRegisterAddressBuilder.Build();
            request.State = "São paulo";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_STATE));

        }
    }
}
