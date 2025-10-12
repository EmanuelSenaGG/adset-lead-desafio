
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {

        [HttpGet("home")]
        public  async Task<IActionResult> Home()
        {
            return Ok("API ta rodando");
        } 
    }
}
