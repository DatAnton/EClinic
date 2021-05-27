using System;
using EClinic.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EClinic.Data
{
    public class ClinicContext : IdentityDbContext<User>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorType> DoctorTypes { get; set; }
        public DbSet<MedicalCard> MedicalCards { get; set; }
        public DbSet<Meetting> Meettings { get; set; }
        public DbSet<MeettingStatus> MeettingStatuses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }

        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DoctorType>(e =>
            {
                e.ToTable("DoctorTypes", "meta");
                e.HasData(new DoctorType() { Id = 1, Name = "Лікар педіатр" },
                new DoctorType() { Id = 2, Name = "Сімейний лікар" },
                new DoctorType() { Id = 3, Name = "ЛОР" },
                new DoctorType() { Id = 4, Name = "Хірург" },
                new DoctorType() { Id = 5, Name = "Дерматолог" },
                new DoctorType() { Id = 6, Name = "Стоматолог" });
            });

            builder.Entity<MeettingStatus>(e =>
            {
                e.ToTable("MeettingStatus", "meta");
                e.HasData(new MeettingStatus() { Id = 1, Name = "Заплановано" },
                new MeettingStatus() { Id = 2, Name = "В процесі" },
                new MeettingStatus() { Id = 3, Name = "Відмінено" },
                new MeettingStatus() { Id = 4, Name = "Проведено" });
            });


            base.OnModelCreating(builder);
        }
    }
}
