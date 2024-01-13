﻿using AutoMapper;
using FitTech.Comunication.Requests.Employee;
using FitTech.Comunication.Responses.Employee;
using FitTech.Domain.Repositories;
using FitTech.Domain.Repositories.Employee;
using FitTech.Domain.Repositories.Gym;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Employee.Create
{
    public class CreateEmployeeUseCase : ICreateEmployeeUseCase
    {
        private readonly IEmployeeWriteOnlyRepository _writeOnlyRepository;
        private readonly IEmployeeReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfwork;
        private readonly IGymReadOnlyRepository _gymReadOnlyRepository;

        public CreateEmployeeUseCase(
            IEmployeeWriteOnlyRepository writeOnlyRepository, 
            IEmployeeReadOnlyRepository readOnlyRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfwork,
            IGymReadOnlyRepository gymReadOnlyRepository)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _unitOfwork = unitOfwork;
            _gymReadOnlyRepository = gymReadOnlyRepository;
            
        }
        public async Task<ResponseCreateEmployeeDTO> Execute(RequestCreateEmployeeDTO request)
        {
            await Validate(request);

            var entity = _mapper.Map<Domain.Entities.Employee>(request);
            var gymEntity = await _gymReadOnlyRepository.GetGymById(request.GymId);

            entity.Gym = gymEntity;
            entity.Password = "cript";

            await _writeOnlyRepository.CreateEmployee(entity);

            await _unitOfwork.Commit();

            return new ResponseCreateEmployeeDTO
            {
                EmailAddress = entity.EmailAddress,
                GymName = entity.Gym.Name,
                Token = ""
            };

        }

        private async Task Validate(RequestCreateEmployeeDTO request)
        {
            var validator = new CreateEmployeeValidator();

            var result = validator.Validate(request);

            var gymIsValid = await _gymReadOnlyRepository.GetGymById(request.GymId);
            if(gymIsValid == null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("GymId", ResourceErrorMessages.GYM_NOT_FOUND));
            }

            var emailInUse = await _readOnlyRepository.GetEmployeeByEmail(request.EmailAddress);
            if (emailInUse != null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("EmailAddress", ResourceErrorMessages.EMPLOYEE_EMAIL_IN_USE));
            }

            var cpfInUse = await _readOnlyRepository.GetEmployeeByCPF(request.Cpf);
            if (cpfInUse != null)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("Cpf", ResourceErrorMessages.EMPLOYEE_CPF_IN_USE));
            }

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsException(errorMessages);
            }
        }
    }
}
