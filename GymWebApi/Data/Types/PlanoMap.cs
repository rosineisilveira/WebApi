using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class PlanoMap : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.ToTable("plano");

            builder.Property(i => i.Id)
            .HasColumnName("id");   

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
            .HasColumnName("nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(i => i.Valor)
            .HasColumnName("valor")
            .HasColumnType("DECIMAL")
            .IsRequired();
        }
    }
}