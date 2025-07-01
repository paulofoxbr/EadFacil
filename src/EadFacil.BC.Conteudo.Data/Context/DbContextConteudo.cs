using EadFacil.BC.Conteudo.Domain.Entities;
using EadFacil.Core.Communication.Mediator;
using EadFacil.Core.Data;
using EadFacil.Core.Data.Extensions;
using EadFacil.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace EadFacil.BC.Conteudo.Data.Context;
public class DbContextConteudo : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public DbContextConteudo(DbContextOptions<DbContextConteudo> options,IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aula> Aulas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Event>();    
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextConteudo).Assembly);
    }
    public async Task<bool> Commit()
    {
        var result = await base.SaveChangesAsync();
        var suscess = result > 0;
        if (suscess) await _mediatorHandler.PublicarEventos(this);
        return suscess;
    }
    
}