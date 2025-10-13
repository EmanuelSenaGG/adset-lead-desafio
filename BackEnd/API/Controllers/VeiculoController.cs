
using API.Dtos;
using API.Dtos.Veiculo;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _service;

        public VeiculoController(IVeiculoService service)
        {
            _service = service;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarVeiculo([FromForm] CadastrarVeiculoDto veiculo)
        {

            CadastrarVeiculoDto veiculoInserido = await _service.CadastrarVeiculoAsync(veiculo);
            return CreatedAtAction(nameof(ObterVeiculoPeloID), new { id = veiculoInserido.Id }, veiculoInserido);

        }


        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarVeiculo([FromBody] AtualizarVeiculoDto veiculo)
        {

            AtualizarVeiculoDto veiculoInserido = await _service.AtualizarVeiculoAsync(veiculo);
            return CreatedAtAction(nameof(ObterVeiculoPeloID), new { id = veiculoInserido.Id }, veiculoInserido);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterVeiculoPeloID(int id)
        {
            VeiculoDto veiculo = await _service.ObterPorIdAsync(id);
            return Ok(veiculo);

        }

        [HttpGet("opcionais")]
        public async Task<IActionResult> ListarOpcionais()
        {
            List<OpcionalDto> opcionais = await _service.ListarOpcionaisAsync();
            return Ok(opcionais);

        }

        [HttpGet("informacoes")]
        public async Task<IActionResult> ObterInformacoes()
        {
            InformacoesVeiculosDto informacoes = await _service.ObterInformacoesAsync();
            return Ok(informacoes);

        }


        [HttpGet("")]
        public async Task<IActionResult> ListarVeiculos()
        {
            List<VeiculoDto> opcionais = await _service.ListarVeiculosAsync();
            return Ok(opcionais);

        }


    }
}
