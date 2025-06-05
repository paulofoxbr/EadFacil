using EadFacil.BC.Conteudo.Domain.Entities;
using EadFacil.BC.Conteudo.Domain.ValueObjects;

namespace EadFacil.BC.Conteudo.Domain.Tests;

public class CursoTests
{
    [Fact]
    public void AdicionarNovoCurso_DeveCriarCursoComConteudoProgramatico()
    {
        // Arrange
        var conteudoProgramatico = new ConteudoProgramatico("Introdução ao Curso", "Descrição do curso");
        var curso = new Curso("Curso de Testes", conteudoProgramatico);

        // Act
        var isValid = curso.IsValid();

        // Assert
        Assert.True(isValid);
        Assert.Equal("Curso de Testes", curso.Titulo);
        Assert.Equal(conteudoProgramatico, curso.conteudoProgramatico);
    }
    
    [Fact]
    public void AdicionarNovoCurso_IdentificadorDeveSerGerado()
    {
        // Arrange
        var conteudoProgramatico = new ConteudoProgramatico("Introdução ao Curso", "Descrição do curso");
        var curso = new Curso("Curso de Testes", conteudoProgramatico);

        // Act
        var isValid = curso.IsValid();

        // Assert
        Assert.True(isValid);
        Assert.NotEqual(Guid.Empty, curso.Id);
    }
    
}