using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class PagamentoMap : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("pagamento");

            builder.Property(i => i.Id)
            .HasColumnName("id");   

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Tipo)
            .HasColumnName("tipo")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired();

           
        }
    }
}