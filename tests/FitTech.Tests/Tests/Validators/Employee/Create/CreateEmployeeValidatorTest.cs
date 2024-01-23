using FitTech.Application.UseCases.Employee.Create;
using FitTech.Application.UseCases.Gym.Create;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.Validators.Employee.Create
{
    public class CreateEmployeeValidatorTest
    {
        [Fact]
        public void Valid_Success()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Valid_Error_Empty_Name()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_NAME));

        }

        [Fact]
        public void Valid_Error_Empty_Cpf()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Cpf = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_CPF));

        }

        [Fact]
        public void Valid_Error_Empty_Password()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PASSWORD));

        }

        [Fact]
        public void Valid_Error_Empty_Salary()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Salary = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_SALARY));

        }

        [Fact]
        public void Valid_Error_Empty_Employee_Type()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.EmployeeType = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_EMPLOYEE_TYPE));

        }

        [Fact]
        public void Valid_Error_Empty_GymId()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.GymId = 0;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_GYM_ID));

        }

        [Fact]
        public void Valid_Error_Empty_Email()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.EmailAddress = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_EMAIL));

        }

        [Fact]
        public void Valid_Error_Empty_Phone_Number()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.PhoneNumber = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_PHONE_NUMBER));

        }

        [Fact]
        public void Valid_Error_Empty_Address()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_ADDRESS));

        }

        [Fact]
        public void Valid_Error_Invalid_Email()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.EmailAddress = "fitemail.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));

        }

        [Fact]
        public void Valid_Error_Invalid_Phone_Number()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.PhoneNumber = "1112345";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PHONE_NUMBER));

        }

        [Fact]
        public void Valid_Error_Invalid_Cpf()
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Cpf = "123123123";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_CPF));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Valid_Error_Invalid_Password(int passwordLength)
        {
            var validator = new CreateEmployeeValidator();

            var request = RequestCreateEmployeeBuilder.Build(passwordLength);
            request.Address = RequestRegisterAddressBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));

        }
    }

}
