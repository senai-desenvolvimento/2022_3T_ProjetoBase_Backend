using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Patrimonio.Domains;

#nullable disable

namespace Patrimonio.Contexts
{
    public partial class PatrimonioContext : DbContext
    {
        public PatrimonioContext()
        {

        }

        public PatrimonioContext(DbContextOptions<PatrimonioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Equipamento> Equipamentos { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Comentario>(entity =>
            {

                entity.ToTable("comentarios");

                entity.Property(e => e.Comentario1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("datetime")
                    .HasColumnName("data_cadastro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdEquipamentos).HasColumnName("idEquipamentos");

                entity.Property(e => e.IdPerfils).HasColumnName("idPerfils");

                entity.HasOne(d => d.IdEquipamentosNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEquipamentos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comentari__idEqu__2C3393D0");

                entity.HasOne(d => d.IdPerfilsNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPerfils)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comentari__idPer__2D27B809");
            });

            modelBuilder.Entity<Equipamento>(entity =>
            {
                entity.ToTable("equipamentos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .HasColumnName("ativo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("datetime")
                    .HasColumnName("data_cadastro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.Imagem)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("imagem");

                entity.Property(e => e.NomePatrimonio)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nomePatrimonio");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("perfils");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Perfil1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("perfil");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdPerfils).HasColumnName("idPerfils");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.HasOne(d => d.IdPerfilsNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPerfils)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuarios__idPerf__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
