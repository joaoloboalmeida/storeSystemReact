using Microsoft.EntityFrameworkCore;
using StoreSystem.Data;
using StoreSystem.Models;

namespace StoreSystem.Services.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ObterClientesAsync() 
        {
            try
            {
                var clientes = await _context.Clientes.AsNoTracking().ToListAsync();
                foreach(var cliente in clientes)
                {
                    var produtos = await _context.Produtos.Where(x => x.ClienteId == cliente.Id).ToListAsync();
                    cliente.Produtos = produtos;
                }

                return clientes;
            }
            catch 
            {
                throw new Exception();
            }
        }
        

        public async Task<Cliente> ObterClientePorIdAsync(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                return cliente;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task CriarClienteAsync(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AtualizarClienteAsync(Cliente cliente)
        {
            try
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
            
        }

        public async Task ExcluirClienteAsync(Cliente cliente)
        {
            try
            {
               _context.Clientes.Remove(cliente);
               await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<bool> VerificarSeCpfJaExisteAsync(string cpf, int id = 0)
        {
            var cpfExistente = id == 0 ? await _context.Clientes.AnyAsync(x => x.Cpf == cpf) : await _context.Clientes.AnyAsync(x => x.Cpf == cpf && x.Id != id);
            if (cpfExistente)
            {
                return true;
            }

            return false;
        }
    }
}
