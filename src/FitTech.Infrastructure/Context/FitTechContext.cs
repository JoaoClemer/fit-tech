using FitTech.Domain.Entities;
using FitTech.Infrastructure.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FitTech.Infrastructure.Context
{
    public class FitTechContext : DbContext
    {
        public FitTechContext(DbContextOptions options) : base(options) { }

        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Traning> Tranings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Plan> Plans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new ExerciseMap());
            modelBuilder.ApplyConfiguration(new GymMap());
            modelBuilder.ApplyConfiguration(new PlanMap());
            modelBuilder.ApplyConfiguration(new StudentMap());
            modelBuilder.ApplyConfiguration(new TraningMap());
        }

    }
}
