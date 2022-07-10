using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class ExercicioMap : IEntityTypeConfiguration<Exercicio>
    {
        public void Configure(EntityTypeBuilder<Exercicio> builder)
        {
             builder.ToTable("exercicio");

            builder.Property(i => i.Id)
            .HasColumnName("id");   

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
            .HasColumnName("nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(i => i.Repeticao)
            .HasColumnName("repeticao")
            .HasColumnType("INTEGER")
            .HasMaxLength(11)
            .IsRequired();

            builder.Property(i => i.Series)
            .HasColumnName("series")
            .HasColumnType("INTEGER")
            .HasMaxLength(11)
            .IsRequired();
        }
    }
}