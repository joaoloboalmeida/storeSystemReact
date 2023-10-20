using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSystem.Models;
using StoreSystem.Services.Clientes;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> ObterClientesAsync()
        {
            try
            {
                var clientes = await _clienteService.ObterClientesAsync();
                return Ok(clientes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "01X01 - Erro ao obter clientes");
            }
        }

        [HttpGet("{id:int}", Name = "ObterClientePorIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Cliente>> ObterClientePorIdAsync(int id)
        {
            try
            {
                var cliente = await _clienteService.ObterClientePorIdAsync(id);
                
                if(cliente == null)
                    return NotFound("Nenhum cliente foi encontrado");

                return Ok(cliente);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "01X02 - Erro ao obter cliente");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CriarClienteAsync(Cliente cliente)
        {
            try
            {
                var cpfExistente = await _clienteService.VerificarSeCpfJaExisteAsync(cliente.Cpf);
                if (!cpfExistente)
                {
                    await _clienteService.CriarClienteAsync(cliente);
                    return CreatedAtRoute(nameof(ObterClientePorIdAsync), new { id = cliente.Id }, cliente);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "01X03 - Não foi possível criar o cliente");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "01X04 - Erro ao criar cliente");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AtualizarClienteAsync(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if(cliente.Id == id)
                {
                    var cpfExistente = await _clienteService.VerificarSeCpfJaExisteAsync(cliente.Cpf, cliente.Id);
                    if (!cpfExistente)
                    {
                        await _clienteService.AtualizarClienteAsync(cliente);
                        return Ok($"O cliente {cliente.Nome} foi atualizado com sucesso!");
                    }
                }

                return StatusCode(StatusCodes.Status400BadRequest, "01X05 - Não foi possível atualizar o cliente");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "01X06 - Erro ao atualizar cliente");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ExcluirClienteAsync(int id)
        {
            try
            {
                    var cliente = await _clienteService.ObterClientePorIdAsync(id);
                    if(cliente != null)
                    {
                        await _clienteService.ExcluirClienteAsync(cliente);
                        return Ok($"O cliente {cliente.Nome} foi excluído com sucesso!");
                    }

                return NotFound("01X07 - Cliente não encontrado");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "01X08 - Erro ao atualizar cliente");
            }
        }
    }
}
