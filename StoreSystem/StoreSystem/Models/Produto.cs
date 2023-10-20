using System.Text.Json.Serialization;

namespace StoreSystem.Models
{
    public class Produto
    {
        public Produto(string nome, decimal valor) 
        {
            Nome = nome;
            Valor = valor;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public int? ClienteId { get; set; }

        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }
    }
}
