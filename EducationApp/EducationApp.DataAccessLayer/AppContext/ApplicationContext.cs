using EducationApp.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EducationApp.DataAccessLayer.AppContext
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IServiceProvider serviceProvider, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Author>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PrintingEdition>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<UserInRole>()
                .HasKey(bc => new { bc.UserId, bc.RoleId });
            modelBuilder.Entity<UserInRole>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserInRoly)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserInRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.UserInRoly)
                .HasForeignKey(bc => bc.RoleId);

            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasKey(bc => new { bc.AutorId, bc.PrintingEditionId });
            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasOne(bc => bc.Autor)
                .WithMany(b => b.AutorInPrintingEdition)
                .HasForeignKey(bc => bc.AutorId);
            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasOne(bc => bc.PrintingEdition)
                .WithMany(c => c.AutorInPrintingEdition)
                .HasForeignKey(bc => bc.PrintingEditionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
