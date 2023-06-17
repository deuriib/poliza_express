using System.Net;
using FluentValidation.Results;
using PolizaExpress.Infrastructure.Middlewares;

namespace PolizaExpress.Application.Extensions;

public static class ValidationExtensions
{
    public static ErrorResponse? ToErrorResponse(this List<ValidationFailure> failures)
    {
        return failures.Select(f => new ErrorResponse
        {
            Message = f.ErrorMessage,
            Code = (int)HttpStatusCode.BadRequest
        }).FirstOrDefault();
    }
}