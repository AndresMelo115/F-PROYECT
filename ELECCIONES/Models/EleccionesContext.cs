using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ELECCIONES.Models
{
    public partial class EleccionesContext : IdentityDbContext
    {
        public EleccionesContext()
        {
        }

        public EleccionesContext(DbContextOptions<EleccionesContext> options)
            : base(options)
        {
        }

       
        public virtual DbSet<Candidatos> Candidatos { get; set; }
        public virtual DbSet<Ciudadanos> Ciudadanos { get; set; }
        public virtual DbSet<Elecciones> Elecciones { get; set; }
        public virtual DbSet<Partidos> Partidos { get; set; }
        public virtual DbSet<PuestoElecto> PuestoElecto { get; set; }
        public virtual DbSet<Resultado> Resultado { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SANDY02;Database=Elecciones;persist security info=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Candidatos>(entity =>
            {
                entity.HasKey(e => e.IdCandidatos)
                    .HasName("PK__Candidat__FFAB99880628D312");

                entity.Property(e => e.IdCandidatos).HasColumnName("id_Candidatos");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FotoPerfil)
                    .HasColumnName("Foto_Perfil")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PartidoPertenece).HasColumnName("Partido_Pertenece");

                entity.Property(e => e.PuestoAspira).HasColumnName("Puesto_Aspira");

                entity.HasOne(d => d.PartidoPerteneceNavigation)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.PartidoPertenece)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartidosPe");

                entity.HasOne(d => d.PuestoAspiraNavigation)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.PuestoAspira)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PuestoAs");
            });

            modelBuilder.Entity<Ciudadanos>(entity =>
            {
                entity.HasKey(e => e.IdCiudadanos)
                    .HasName("PK__Ciudadan__A7ACDF178EAA5AA0");

                entity.Property(e => e.IdCiudadanos).HasColumnName("id_Ciudadanos");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Elecciones>(entity =>
            {
                entity.HasKey(e => e.IdElecciones)
                    .HasName("PK__Eleccion__A4B502946D8F97B4");

                entity.Property(e => e.IdElecciones).HasColumnName("id_Elecciones");

                entity.Property(e => e.FechaRealizacion)
                    .IsRequired()
                    .HasColumnName("Fecha_Realizacion")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidatos).HasColumnName("idCandidatos");

                entity.Property(e => e.IdCiudadanos).HasColumnName("idCiudadanos");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCandidatosNavigation)
                    .WithMany(p => p.Elecciones)
                    .HasForeignKey(d => d.IdCandidatos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidatos");

                entity.HasOne(d => d.IdCiudadanosNavigation)
                    .WithMany(p => p.Elecciones)
                    .HasForeignKey(d => d.IdCiudadanos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ciudadanos");
            });

            modelBuilder.Entity<Partidos>(entity =>
            {
                entity.HasKey(e => e.IdPartidos)
                    .HasName("PK__Partidos__189159DD5031CFB1");

                entity.Property(e => e.IdPartidos).HasColumnName("id_Partidos");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogoPartido)
                    .HasColumnName("Logo_Partido")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PuestoElecto>(entity =>
            {
                entity.HasKey(e => e.IdPuestoE)
                    .HasName("PK__Puesto_E__46878D972E299F5C");

                entity.ToTable("Puesto_Electo");

                entity.Property(e => e.IdPuestoE).HasColumnName("id_PuestoE");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resultado>(entity =>
            {
                entity.HasKey(e => e.IdResultado)
                    .HasName("PK__Resultad__F64F43EE2FB5B041");

                entity.Property(e => e.IdResultado).HasColumnName("id_Resultado");

                entity.Property(e => e.IdElecciones).HasColumnName("idElecciones");

                entity.Property(e => e.ResultadoTotal).HasColumnName("Resultado_Total");

                entity.HasOne(d => d.IdEleccionesNavigation)
                    .WithMany(p => p.Resultado)
                    .HasForeignKey(d => d.IdElecciones)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Elecciones");
            });
        }
    }
}
