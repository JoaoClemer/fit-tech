namespace FitTech.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : FitTechException
    {
        public InvalidLoginException() : base(ResourceErrorMessages.INVALID_LOGIN)
        {
            
        }
    }
}
