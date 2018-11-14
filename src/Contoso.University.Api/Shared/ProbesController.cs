using Microsoft.AspNetCore.Mvc;

namespace Contoso.University.Api.Shared
{
    [Route("_")]
    public class ProbesController : Controller
    {
        [HttpGet(nameof(Healthz))]
        public IActionResult Healthz() => Ok();
    }
}
