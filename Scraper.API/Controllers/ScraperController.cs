using Microsoft.AspNetCore.Mvc;
using Scraper.API.Common;
using Scraper.Application.Features.ScrapingByOder;

namespace Scraper.API.Controllers
{
    public class ScraperController : ApplicationController
    {
        [HttpPost("path")]
        [ApiVersionNeutral]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateList(
            [FromServices] SetOrderHandler handler,
            //[FromBody] string filePath,
            CancellationToken ct)
        {
            var result = await handler.Handle(ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

    }
}
