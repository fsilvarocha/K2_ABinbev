using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Infrastructure;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companys { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        var sucess = await base.SaveChangesAsync() > 0;

        if (sucess) await _mediator.PublicEvents(this);

        return sucess;
    }
}

public static class MediatorExtension
{
    public static async Task PublicEvents<T>(this IMediator mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}