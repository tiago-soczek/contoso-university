using Microsoft.AspNetCore.Mvc;

namespace Contoso.University.Api.Shared.Api
{
    [Route("_")]
    public class ProbesController : Controller
    {
        [HttpGet("healthz")]
        public IActionResult Healthz()
        {
            return Ok();
        }
    }
}
