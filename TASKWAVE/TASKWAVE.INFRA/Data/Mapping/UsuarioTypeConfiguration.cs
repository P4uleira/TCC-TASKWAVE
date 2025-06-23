using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.INFRA.Data.Mapping
{
    internal class UsuarioTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.ToTable("TB_USUARIO");
            entity.HasKey(e => e.IdUsuario);
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.NomeUsuario).HasColumnName("NOME_USUARIO");
            entity.Property(e => e.EmailUsuario).HasColumnName("EMAIL_USUARIO");
            entity.Property(e => e.SenhaUsuario).HasColumnName("SENHA_USUARIO");
            entity.Property(e => e.DataCriacaoUsuario).HasColumnName("DATA_CRIACAO_USUARIO");

            entity.HasMany(e => e.Acessos)
              .WithMany(a => a.Usuarios)
              .UsingEntity<Dictionary<string, object>>(
                  "TB_ACESSO_USUARIO",
                  l => l.HasOne<Acesso>()
                        .WithMany()
                        .HasForeignKey("ACESSO_ID")
                        .HasConstraintName("FK_ACESSO_ID"),
                  r => r.HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("USUARIO_ID")
                        .HasConstraintName("FK_USUARIO_ID"),
                  j =>
                  {
                      j.HasKey("ACESSO_ID", "USUARIO_ID");
                      j.ToTable("TB_ACESSO_USUARIO");
                  }
              );
        }
    }
}
