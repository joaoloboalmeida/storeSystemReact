using StoreSystem.Models;

namespace StoreSystem.Services.Clientes
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterClientesAsync();
        Task<Cliente> ObterClientePorIdAsync(int id);
        Task CriarClienteAsync(Cliente cliente);
        Task AtualizarClienteAsync(Cliente cliente);
        Task ExcluirClienteAsync(Cliente cliente);
        Task<bool> VerificarSeCpfJaExisteAsync(string cpf, int id = 0);
    }
}
