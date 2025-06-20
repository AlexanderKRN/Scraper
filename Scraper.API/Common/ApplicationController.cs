using Microsoft.AspNetCore.Mvc;
using Scraper.Domain.Common;

namespace Scraper.API.Common
{
    /// <summary>
    /// Дополнительный базовый контроллер
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApplicationController : ControllerBase
    {
        /// <summary>
        /// Переопределение IActionResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected new IActionResult Ok(object? result = null)
        {
            var envelope = Envelope.Ok(result);

            return base.Ok(envelope);
        }

        /// <summary>
        /// Переопределение IActionResult
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected IActionResult BadRequest(Error? error)
        {
            var errorInfo = new ErrorInfo(error);
            var envelope = Envelope.Error(errorInfo);

            return base.BadRequest(envelope);
        }
    }
}
