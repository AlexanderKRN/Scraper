using Microsoft.AspNetCore.Mvc;
using Scraper.Domain.Common;

namespace Scraper.API.Common
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApplicationControllerl : ControllerBase
    {
        protected new IActionResult Ok(object? result = null)
        {
            var envelope = Envelope.Ok(result);

            return base.Ok(envelope);
        }

        protected IActionResult BadRequest(Error? error)
        {
            var errorInfo = new ErrorInfo(error);
            var envelope = Envelope.Error(errorInfo);

            return base.BadRequest(envelope);
        }
    }
}
