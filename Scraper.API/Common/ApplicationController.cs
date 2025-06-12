using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Scraper.API.Common
{
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
