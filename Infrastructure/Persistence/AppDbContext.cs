﻿using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Common.Extensions;
using Infrastructure.Identity;
using Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DatabaseFacade database => Database;
        public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();

        public DbSet<UserGroup> UserGroups => Set<UserGroup>();
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        
         public virtual DbSet<Blacklist> Blacklists { get; set; } = null!;
        public virtual DbSet<ContactPerson> ContactPeople { get; set; } = null!;
        public virtual DbSet<Lookup> Lookups { get; set; } = null!;
        public virtual   DbSet<TabsModel> Tabs{ get; set; } = null!;
        public virtual DbSet<ProjectModel> Projects { get; set; } = null!;
        public virtual DbSet<ComponentModel> Components { get; set; } = null!;
        public virtual DbSet<feildsModel> feilds { get; set; } = null!;
        public virtual DbSet<ButtonModel> buttons { get; set; } = null!;
       

        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
