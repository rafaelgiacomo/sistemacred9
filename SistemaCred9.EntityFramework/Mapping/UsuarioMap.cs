using SistemaCred9.Modelo;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");

            HasKey(t => t.Id);

            Property(t => t.Email)
                .HasMaxLength(250)
                .IsRequired();

            Property(t => t.Nome)
                .HasMaxLength(150)
                .IsRequired();

            Property(t => t.NomeUsuario)
                .HasMaxLength(50)
                .IsRequired();

            Property(t => t.Senha)
                .HasMaxLength(20)
                .IsRequired();

            Property(t => t.TipoUsuarioId)
                .IsRequired();

            HasMany(r => r.Roles)
                .WithMany(r => r.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("UsuarioId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("UsuarioRole");
                });
        }
    }
}
