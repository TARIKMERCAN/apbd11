using Apbd11.Models;

namespace Apbd11.Context;

using Microsoft.EntityFrameworkCore;

public class AnimalClinicContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalTypes> AnimalTypes { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AnimalClinic;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Employee)
            .WithMany(e => e.Visits)
            .HasForeignKey(v => v.EmployeeId);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Animal)
            .WithMany(a => a.Visits)
            .HasForeignKey(v => v.AnimalId);

        modelBuilder.Entity<Animal>()
            .HasOne(a => a.AnimalTypes)
            .WithMany(at => at.Animals)
            .HasForeignKey(a => a.AnimalTypesId);
    }
}