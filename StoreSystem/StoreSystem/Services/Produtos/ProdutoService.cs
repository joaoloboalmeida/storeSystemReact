using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Models;
using StoreSystem.ViewModels;

namespace StoreSystem.Services.Produtos
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosAsync()
        {
            try
            {
                return await _context.Produtos.AsNoTracking().ToListAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<Produto> ObterProdutoPorIdAsync(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                return produto;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task CriarProdutoAsync(Produto produto)
        {
            try
            {
                var prod = new Produto(produto.Nome, produto.Valor)
                {
                    ClienteId = produto.ClienteId
                };

                _context.Produtos.Add(prod);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AtualizarProdutoAsync(Produto produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task ExcluirProdutoAsync(Produto produto)
        {
            try
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task VincularProdutosAoClienteAsync(VinculacaoProdutosViewModel viewModel)
        {
            try
            {
                foreach(var idProduto in viewModel.Ids)
                {
                    var prod = await ObterProdutoPorIdAsync(idProduto);
                    if(prod != null)
                    {
                        prod.ClienteId = viewModel.clienteId;
                        _context.Produtos.Update(prod);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
