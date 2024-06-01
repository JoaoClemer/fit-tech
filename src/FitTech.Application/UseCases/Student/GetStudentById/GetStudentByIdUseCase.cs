using AutoMapper;
using FitTech.Comunication.Responses.Student;
using FitTech.Domain.Repositories.Student;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;

namespace FitTech.Application.UseCases.Student.GetStudentById
{
    public class GetStudentByIdUseCase : IGetStudentByIdUseCase
    {
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetStudentByIdUseCase(IStudentReadOnlyRepository studentReadOnlyRepository, IMapper mapper)
        {
            _studentReadOnlyRepository = studentReadOnlyRepository;
            _mapper = mapper;
        }
        public async Task<ResponseStudentInformationsDTO> Execute(int studentId)
        {
            var student = await _studentReadOnlyRepository.GetStudentById(studentId);

            if (student == null)
                throw new FitTechException(ResourceErrorMessages.STUDENT_NOT_FOUND);

            var response = _mapper.Map<ResponseStudentInformationsDTO>(student);

            return response;
        }
    }
}
