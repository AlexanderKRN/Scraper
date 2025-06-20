using Microsoft.AspNetCore.Mvc;
using Scraper.API.Common;
using Scraper.Application.Features.ScrapingByOder;
using Scraper.Infrastructure.Queries.Notices;

namespace Scraper.API.Controllers;

/// <summary>
/// Контроллер приложения
/// </summary>
public class ScraperController : ApplicationController
{
    /// <summary>
    /// Загрузка списка ссылок из файла и сохранение в виде ордера
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("order")]
    [ApiVersionNeutral]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromForm] SetOrderHandlerRequest request,
        [FromServices] SetOrderHandler handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// Выгрузка собранных данных по Id ордера
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("notices")]
    [ApiVersionNeutral]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        [FromServices] GetNoticesQuery handler,
        [FromQuery] GetNoticesRequest request, 
        CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
