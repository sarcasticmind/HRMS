using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HR_System.Models
{
    public partial class HR_SystemContext : DbContext
    {
        public HR_SystemContext()
        {
        }

        public HR_SystemContext(DbContextOptions<HR_SystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OfficialHoliday> OfficialHolidays { get; set; }
        public virtual DbSet<PenalityExtra> PenalityExtras { get; set; }
        public virtual DbSet<WeekEnd> WeekEnds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=HR_System;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AttendingTime).HasColumnType("time(5)");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.EmpId).HasColumnName("Emp_id");

                entity.Property(e => e.LeavingTime).HasColumnType("time(5)");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK_Attendence_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.AttendingTime).HasColumnType("time(5)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfContract).HasColumnType("date");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LeavingTime).HasColumnType("time(5)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsFixedLength(true);

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.Salary).HasColumnType("money");
            });

            modelBuilder.Entity<OfficialHoliday>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PenalityExtra>(entity =>
            {
                entity.HasKey(e => e.Penalityhour);

                entity.ToTable("penality-extra");

                entity.Property(e => e.Penalityhour).HasColumnName("penalityhour");

                entity.Property(e => e.Extrahour).HasColumnName("extrahour");
            });

            modelBuilder.Entity<WeekEnd>(entity =>
            {
                entity.HasKey(e => e.Weekend1);

                entity.ToTable("WeekEnd");

                entity.Property(e => e.Weekend1)
                    .HasMaxLength(15)
                    .HasColumnName("weekend1");

                entity.Property(e => e.Weekend2)
                    .HasMaxLength(15)
                    .HasColumnName("weekend2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
