using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scraper.Domain.Common;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace Scraper.API.Common;

/// <summary>
/// Переопределение результата обработки от библиотеки FluentValidation
/// </summary>
public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    /// <summary>
    /// Результат обработки данных запроса
    /// </summary>
    /// <param name="context"> Контекст </param>
    /// <param name="validationProblemDetails"> Описание ошибок </param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
            throw new Exception("ValidationProblemDetails is null");

        List<ErrorInfo> errorInfos = [];
        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var errors = validationErrors
                .Select(Error.Deserialize)
                .Select(e => new ErrorInfo(e, invalidField));

            errorInfos.AddRange(errors);
        }

        var envelope = Envelope.Error(errorInfos.ToArray());

        return new BadRequestObjectResult(envelope);
    }
}
