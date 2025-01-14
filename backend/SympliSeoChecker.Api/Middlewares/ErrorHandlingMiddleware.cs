using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using SympliSeoChecker.Common.Enums;
using SympliSeoChecker.Common.Utilities;
using SympliSeoChecker.Domain.Models.Responses;
using SympliSeoChecker.Domain.Models.Validators;
using System.Net;

namespace SympliSeoChecker.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception.GetType() == typeof(ValidationException))
            {
                var errors = BuildFluentValidationError(((ValidationException)exception).Errors);
                var result = JsonConvert.SerializeObject(errors);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(result);
            }
            else
            {
                var error = new ErrorResponseModel
                {
                    Code = (long)ErrorCode.Invalid,
                    Message = exception.Message,
                };
                var result = JsonConvert.SerializeObject(error);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(result);
            }
        }

        private ErrorResponseModel BuildFluentValidationError(IEnumerable<ValidationFailure> errors)
        {
            var errorModels = errors.Select(error =>
                new ValidateErrorModel
                {
                    ErrorCode = Enum.TryParse<ErrorCode>(error.ErrorCode, out var errorCode) ? errorCode : ErrorCode.Invalid,
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName
                }).ToList();

            return new ErrorResponseModel
            {
                Code = (long)ErrorCode.Invalid,
                Message = CommonUtility.GetErrorMessage(ErrorCode.Invalid),
                Details = errorModels
            };
        }
    }
}
