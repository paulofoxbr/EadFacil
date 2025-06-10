using EadFacil.BC.Conteudo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EadFacil.BC.Conteudo.Data.Mappings;

public class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Cursos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasColumnName("Titulo")
            .HasMaxLength(200);
        
        builder.Property(c => c.ConteudoProgramatico.Ementa)
            .IsRequired()
            .HasColumnName("Ementa")
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000);
        
        builder.Property(c => c.ConteudoProgramatico.Materiais)
            .HasColumnName("Materiais")
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000);


        builder.HasMany(c => c.Aulas)
            .WithOne(a => a.Curso)
            .HasForeignKey(a => a.CursoId);
    }
}