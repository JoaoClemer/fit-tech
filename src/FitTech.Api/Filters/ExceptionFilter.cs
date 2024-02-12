using FitTech.Comunication.Responses;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FitTech.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is FitTechException)
            {
                DealWithFitTechException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private static void DealWithFitTechException(ExceptionContext context)
        {
            if(context.Exception is ValidationErrorsException) 
            {
                DealWithValidationErrorsException(context);
            }else if (context.Exception is InvalidLoginException)
            {
                DealWithInvalidLoginException(context);
            }

        }

        private static void DealWithValidationErrorsException(ExceptionContext context)
        {
            var validationErrorException = context.Exception as ValidationErrorsException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ResponseErrorDTO(validationErrorException.ErrorMessages));
        }

        private static void DealWithInvalidLoginException(ExceptionContext context)
        {
            var invalidLoginexception = context.Exception as InvalidLoginException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new ObjectResult(new ResponseErrorDTO(invalidLoginexception.Message));
        }

        private static void ThrowUnknowError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorDTO(ResourceErrorMessages.UNKNOW_ERROR));
        }
    }
}
