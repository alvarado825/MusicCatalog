using FluentValidation;
using System.Net;
using MusicCatalog.Application.Exceptions;
using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Enums;
using MusicCatalog.Api.Dto;

namespace MusicCatalog.Api.Extensions
{
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static ErrorDetails CreateErrorDetail(Exception exception, IHostEnvironment environment, int statusCode, string traceId)
        {           
            return new ErrorDetails
            {
                Title = GetTitle(exception),
                Detail = GetExceptionDetails(exception, environment, statusCode),
                Code = GetExceptionCode(exception),
                Status = statusCode,
                Extensions = GetExtensions(exception,traceId)
            };
        }

        public static string GetTitle(Exception exception)
        {
            return exception switch
            {
                NotFoundException => "Resource not found",
                AlreadyExistsException => "Resource already exists",
                InputValidationException => "Input error",
                ValidationException => "Validation Error",
                DomainException => "Domain error",
                DomainRuleViolationException => "Domain rule error",
                BusinessRuleException => "Business rule error",
                TrackInactiveException => "Track Inactive",
                _ => "Intern error."
            };
        }

        public static int GetStatusCode(Exception exception)
        {
            return exception switch
            {               
                NotFoundException => (int)HttpStatusCode.NotFound,
                AlreadyExistsException => (int)HttpStatusCode.Conflict,
                InputValidationException => (int)HttpStatusCode.UnprocessableEntity,
                ValidationException => (int)HttpStatusCode.UnprocessableEntity,
                DomainException => (int)HttpStatusCode.UnprocessableEntity,
                DomainRuleViolationException => (int)HttpStatusCode.Conflict,
                BusinessRuleException => (int)HttpStatusCode.Conflict,
                TrackInactiveException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }

        public static string GetExceptionDetails(Exception exception, IHostEnvironment environment, int statusCode)
        {
            string detail = "";

            if(statusCode == 500)
                if (environment.IsDevelopment())
                    detail = exception.ToString();
                else
                    detail = "An unexpected error occurred";          
            else
                detail = exception.Message;

            return detail;
        }

        public static Dictionary<string, object?> GetExtensions(Exception exception, string traceId)
        {
            Dictionary<string, object?> extensions = new Dictionary<string, object?>();

            extensions.Add("traceId", traceId);

            if(exception is ValidationException ex)
            {
                extensions.Add("errors", ex.Errors.Select(x => x.ErrorMessage));
            }

            return extensions;
        }

        public static string GetExceptionCode(Exception exception)
        {
            if(exception is BaseException ex)
            {
                return ex.Code.ToString();
            }

            return ErrorCode.Unknown.ToString();
        }
    }
}