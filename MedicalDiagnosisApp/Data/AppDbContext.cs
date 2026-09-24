using MedicalDiagnosisApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalDiagnosisApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiseaseSymptom>()
                .HasKey(x => new
                {
                    x.DiseaseId,
                    x.SymptomId
                });

            modelBuilder.Entity<DiseaseSymptom>()
                .HasOne(x => x.Disease)
                .WithMany(x => x.DiseaseSymptoms)
                .HasForeignKey(x => x.DiseaseId);

            modelBuilder.Entity<DiseaseSymptom>()
                .HasOne(x => x.Symptom)
                .WithMany(x => x.DiseaseSymptoms)
                .HasForeignKey(x => x.SymptomId);
        }
    }
}