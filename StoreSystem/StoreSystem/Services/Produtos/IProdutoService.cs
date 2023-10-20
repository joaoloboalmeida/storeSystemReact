using StoreSystem.Models;
using StoreSystem.ViewModels;

namespace StoreSystem.Services.Produtos
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterProdutosAsync();
        Task<Produto> ObterProdutoPorIdAsync(int id);
        Task CriarProdutoAsync(Produto produto);
        Task AtualizarProdutoAsync(Produto produto);
        Task ExcluirProdutoAsync(Produto produto);
        Task VincularProdutosAoClienteAsync(VinculacaoProdutosViewModel viewModel);
    }
}
