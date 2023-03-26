using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class EMSDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            string con = "Server=DESKTOP-DP6EMGM;Database=EMSDB;Integrated Security=true;";
            optionsBuilder.UseSqlServer(con).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne<Department>(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DeptId)
                .OnDelete(DeleteBehavior.Cascade);

            var adminDepartment = new Department { Id = 1, Name = "Admin" };

            modelBuilder.Entity<Department>()
                .HasData(adminDepartment);

            modelBuilder.Entity<Employee>()
                .HasData(new Employee
                {
                    Id = 1,
                    FullName = "John Doe",
                    EmailAdress = "john@email.com",
                    PhoneNumber = "09920098321",
                    DOB = new DateTime(1999, 2, 12),
                    DeptId = adminDepartment.Id
                });

            base.OnModelCreating(modelBuilder); //configure stuff for the database
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Department { get; set; } = null!;
    }
}
