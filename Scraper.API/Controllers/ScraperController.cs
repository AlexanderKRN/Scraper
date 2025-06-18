using Microsoft.AspNetCore.Mvc;
using Scraper.API.Common;
using Scraper.Application.Features.ScrapingByOder;

namespace Scraper.API.Controllers
{
    public class ScraperController : ApplicationController
    {
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

    }
}
