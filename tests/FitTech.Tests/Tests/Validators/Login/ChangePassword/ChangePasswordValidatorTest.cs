using FitTech.Application.UseCases.Login.ChangePassword;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.Validators.Login.ChangePassword
{
    public class ChangePasswordValidatorTest
    {
        [Fact]
        public void Valid_Success()
        {
            var validator = new ChangePasswordValidator();

            var request = RequestChangePasswordBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public void Valid_Error_Empty_NewPassword()
        {
            var validator = new ChangePasswordValidator();

            var request = RequestChangePasswordBuilder.Build();
            request.NewPassword = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PASSWORD));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Valid_Error_Invalid_NewPassword(int passwordLength)
        {
            var validator = new ChangePasswordValidator();

            var request = RequestChangePasswordBuilder.Build(passwordLength);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));

        }
    }
}
