using Microsoft.EntityFrameworkCore;
using SQLBlazor.Shared;

namespace SQLBlazor.Server.Models
{
    public partial class TestingContext : DbContext
    {
        public TestingContext()
        {

        }

        public TestingContext(DbContextOptions<TestingContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmpEmailAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMP_EMAIL_ADDRESS");

                entity.Property(e => e.EmpFirstname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMP_FIRSTNAME");

                entity.Property(e => e.EmpSecondname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMP_SECONDNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
