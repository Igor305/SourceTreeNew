using EducationApp.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EducationApp.DataAccessLayer.AppContext
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<ImagePrintingEdition> ImagePrintingEditions { get; set; }
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
            modelBuilder.Entity<ImagePrintingEdition>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<PrintingEdition>()
                .HasMany(c => c.ImagePrintingEdition)
                .WithOne(e => e.PrintingEdition);

            modelBuilder.Entity<OrderItem> ()
                .HasIndex(u => u.PrintingEditionId)
                .IsUnique(false);
            modelBuilder.Entity<OrderItem>()
                .HasIndex(u => u.OrderId)
                .IsUnique(false);

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
