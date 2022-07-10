using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class MatriculaMap : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
             builder.ToTable("matricula");

            builder.Property(i => i.Id)
            .HasColumnName("id");   

            builder.HasKey(i => i.Id);

            builder.Property(i => i.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasColumnType("SMALLDATETIME")
                .IsRequired();

            builder.Property(i => i.Status)
                .HasColumnName("status")
                .HasDefaultValue(true)
                .IsRequired();

            //Relacionamentos

            builder.HasOne(i => i.Aluno)
                .WithOne(i => i.Matricula)
                .HasConstraintName("FK_Matricula_Aluno")
                .HasForeignKey<Matricula>(i => i.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(i => i.Pagamento)
                .WithMany(i => i.Matriculas)
                .HasConstraintName("FK_Matricula_Pagamento")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Plano)
                .WithMany(x => x.Matriculas)
                .HasConstraintName("FK_Matricula_plano")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(i => i.Treinos)
                .WithMany(i => i.Matriculas)
                .UsingEntity<Dictionary<string, object>>(
                    "matricula_treino",
                    treino => treino
                        .HasOne<Treino>()
                        .WithMany()
                        .HasForeignKey("treino_id")
                        .HasConstraintName("FK_matricula_treino_treino_id")
                        .OnDelete(DeleteBehavior.Restrict),
                    matricula => matricula
                        .HasOne<Matricula>()
                        .WithMany()
                        .HasForeignKey("matricula_id")
                        .HasConstraintName("FK_matricula_treino_matricula_id")
                        .OnDelete(DeleteBehavior.Restrict));
        }
    }
}