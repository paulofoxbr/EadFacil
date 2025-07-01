using EadFacil.BC.GestaoAluno.Domain.Entities;
using EadFacil.Core.Communication.Mediator;
using EadFacil.Core.Data;
using EadFacil.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;


namespace EadFacil.BC.GestaoAluno.Data.Context;

public class DbContextAluno : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public DbContextAluno(DbContextOptions<DbContextAluno> options,IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<MatriculaAula> MatriculasAulas { get; set; }
    public DbSet<Certificado> Certificados { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextAluno).Assembly);
    }

    public async Task<bool> Commit()
    {
        var result = await base.SaveChangesAsync();
        var suscess = result > 0;
        if (suscess) await _mediatorHandler.PublicarEventos(this);
        return suscess;
    }
}
