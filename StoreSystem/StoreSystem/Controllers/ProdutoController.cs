using Microsoft.AspNetCore.Mvc;
using StoreSystem.Models;
using StoreSystem.Services.Clientes;
using StoreSystem.Services.Produtos;
using StoreSystem.ViewModels;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _produtoService;
        private IClienteService _clienteService;

        public ProdutoController(IProdutoService produtoService, IClienteService clienteService)
        {
            _produtoService = produtoService;
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Produto>>> ObterProdutosAsync()
        {
            try
            {
                var produtos = await _produtoService.ObterProdutosAsync();
                return Ok(produtos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "02X01 - Erro ao obter produtos");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProdutoPorIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> ObterProdutoPorIdAsync(int id)
        {
            try
            {
                var produto = await _produtoService.ObterProdutoPorIdAsync(id);
                if (produto == null)
                    return NotFound("Produto não encontrado");

                return Ok(produto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "02X02 - Erro ao obter produto");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CriarProdutoAsync(Produto produto)
        {
            try
            {
                if(produto.ClienteId != null)
                {
                    var cliente = await _clienteService.ObterClientePorIdAsync((int)produto.ClienteId);
                    if (cliente != null)
                    {
                        await _produtoService.CriarProdutoAsync(produto);
                    }
                }
                else
                {
                    await _produtoService.CriarProdutoAsync(produto);
                }

                return CreatedAtRoute(nameof(ObterProdutoPorIdAsync), new { id = produto.Id }, produto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "02X04 - Erro ao criar produto");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AtualizarProdutoAsync(int id, [FromBody] Produto produto)
        {
            try
            {
                if(produto.Id == id)
                {
                    await _produtoService.AtualizarProdutoAsync(produto);
                    return Ok($"O produto {produto.Nome} foi atualizado com sucesso!");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "02X05 - Não foi possível atualizar o produto");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "02X06 - Erro ao criar produto");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ExcluirProdutoAsync(int id)
        {
            try
            {
                var produto = await _produtoService.ObterProdutoPorIdAsync(id);
                if(produto != null)
                {
                    await _produtoService.ExcluirProdutoAsync(produto);
                    return Ok($"O produto {produto.Nome} foi excluído com sucesso!");
                }

                return NotFound("02X07 - Produto não encontrado");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "02X08 - Erro ao atualizar produto");
            }
        }

        [HttpPost("vinculacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> VincularProdutosAoClienteAsync([FromBody] VinculacaoProdutosViewModel viewModel)
        {
            try
            {
                if(viewModel != null && viewModel.Ids.Any())
                {
                    await _produtoService.VincularProdutosAoClienteAsync(viewModel);
                    return Ok($"Vinculação feita com sucesso");
                }

                return BadRequest("02X09 - Erro");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "02X10 - Erro na vincução");
            }
        }
    }
}
