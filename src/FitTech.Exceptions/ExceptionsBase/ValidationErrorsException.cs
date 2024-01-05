namespace FitTech.Exceptions.ExceptionsBase
{
    public class ValidationErrorsException : FitTechException
    {
        public List<string> ErrorMessages { get; set; }

        public ValidationErrorsException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
