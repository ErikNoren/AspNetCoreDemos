using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreDemos.EntityFrameworkCore.Models
{
    public partial class PizzaTimeContext : DbContext
    {
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<PizzaAndToppings> PizzaAndToppings { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }

        public PizzaTimeContext(DbContextOptions<PizzaTimeContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>(entity =>
            {
                entity
                    .ToTable("Pizza")
                    .Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<PizzaAndToppings>(entity =>
            {
                entity
                    .ToTable("PizzaAndToppings")
                    .HasKey(e => new { e.PizzaId, e.ToppingId })
                    .HasName("PK_PizzaAndToppings");

                entity
                    .HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaAndToppings)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PizzaAndToppings_Pizza");

                entity
                    .HasOne(d => d.Topping)
                    .WithMany(p => p.PizzaAndToppings)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PizzaAndToppings_Topping");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity
                    .ToTable("Topping")
                    .Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });
        }
    }
}