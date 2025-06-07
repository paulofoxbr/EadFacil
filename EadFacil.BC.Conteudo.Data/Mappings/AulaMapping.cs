using EadFacil.BC.Conteudo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EadFacil.BC.Conteudo.Data.Mappings;

public class AulaMapping : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Aulas");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Titulo)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(a => a.Conteudo)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

         builder.HasOne(a => a.Curso)
             .WithMany(c => c.Aulas)
             .HasForeignKey(a => a.CursoId);
    }
    
}