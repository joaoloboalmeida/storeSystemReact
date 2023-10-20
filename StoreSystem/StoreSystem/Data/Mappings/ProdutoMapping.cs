using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreSystem.Models;

namespace StoreSystem.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.HasIndex(x => x.Nome, "IX_Nome_Produto").IsUnique();

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnName("Valor")
                .HasColumnType("DECIMAL")
                .HasPrecision(18, 2)
                .HasDefaultValue(0m);

            builder.HasOne(x => x.Cliente)
                .WithMany(p => p.Produtos)
                .HasConstraintName("FK_Produto_Cliente")
                .IsRequired(false)
                .HasForeignKey(cp => cp.ClienteId);
        }
    }
}
