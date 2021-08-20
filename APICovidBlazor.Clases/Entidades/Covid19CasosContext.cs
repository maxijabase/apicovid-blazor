using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace APICovidBlazor.Clases.Entidades
{
    public partial class Covid19CasosContext : DbContext
    {
        public Covid19CasosContext()
        {
        }

        public Covid19CasosContext(DbContextOptions<Covid19CasosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Casoscovid> Casoscovids { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=..\\Covid19Casos.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Casoscovid>(entity =>
            {
                entity.HasKey(e => e.IdEventoCaso);

                entity.ToTable("casoscovid");

                entity.Property(e => e.IdEventoCaso)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id_evento_caso");

                entity.Property(e => e.Edad)
                    .HasColumnType("integer")
                    .HasColumnName("edad");

                entity.Property(e => e.Fallecido)
                    .HasColumnType("varchar")
                    .HasColumnName("fallecido");

                entity.Property(e => e.FechaApertura)
                    .HasColumnType("text")
                    .HasColumnName("fecha_apertura");

                entity.Property(e => e.ResidenciaProvinciaNombre)
                    .HasColumnType("varchar")
                    .HasColumnName("residencia_provincia_nombre");

                entity.Property(e => e.Sexo)
                    .HasColumnType("varchar")
                    .HasColumnName("sexo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
