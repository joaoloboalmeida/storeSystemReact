using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreSystem.Models;

namespace StoreSystem.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(11);

            builder.HasIndex(x => x.Cpf, "IX_CpfCliente").IsUnique();
        }
    }
}
