using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class InstrutorMap : IEntityTypeConfiguration<Instrutor>
    {
        public void Configure(EntityTypeBuilder<Instrutor> builder)
        {
            builder.ToTable("instrutor");

            builder.Property(i => i.Id)
            .HasColumnName("id");   

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
            .HasColumnName("nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(i => i.Phone)
            .HasColumnName("phone")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(11)
            .IsRequired();
        }
    }
}