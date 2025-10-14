using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorController : ControllerBase
    {
        private readonly ICorService _service;
        public CorController(ICorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCores()
        {
            List<string> cores = await _service.ObterCoresAsync();
            return Ok(cores);
        }
    }
}
