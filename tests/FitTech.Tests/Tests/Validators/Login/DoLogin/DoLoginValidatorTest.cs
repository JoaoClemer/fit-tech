using FitTech.Application.UseCases.Gym.Create;
using FitTech.Application.UseCases.Login.DoLogin;
using FitTech.Application.UseCases.Student.Create;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.Validators.Login.DoLogin
{
    public class DoLoginValidatorTest
    {
        [Fact]
        public void Valid_Success()
        {
            var validator = new DoLoginValidator();

            var request = RequestDoLoginBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Valid_Error_Empty_Email()
        {
            var validator = new DoLoginValidator();

            var request = RequestDoLoginBuilder.Build();
            request.EmailAddress = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_EMAIL));

        }

        [Fact]
        public void Valid_Error_Empty_Password()
        {
            var validator = new DoLoginValidator();

            var request = RequestDoLoginBuilder.Build();
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PASSWORD));

        }

        [Fact]
        public void Valid_Error_Empty_User_Type()
        {
            var validator = new DoLoginValidator();

            var request = RequestDoLoginBuilder.Build();
            request.UserType = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_USER_TYPE));

        }

        public void Valid_Error_Invalid_Email()
        {
            var validator = new DoLoginValidator();

            var request = RequestDoLoginBuilder.Build();
            request.EmailAddress = "invalid.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Valid_Error_Invalid_Password(int passwordLength)
        {
            var validator = new DoLoginValidator();

            var request = RequestDoLoginBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));

        }

    }
}
