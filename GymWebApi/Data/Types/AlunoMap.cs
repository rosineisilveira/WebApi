using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("aluno");

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