using SistemaCred9.Modelo;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class VendaMap : EntityTypeConfiguration<Venda>
    {
        public VendaMap()
        {
            ToTable("Venda");

            HasKey(t => t.Id);

            Property(t => t.ClienteId)
                .IsRequired();

            Property(t => t.StatusId)
                .IsRequired();

            Property(t => t.Data)
                .IsRequired();

            Property(t => t.UsuarioId)
                .IsRequired();

            HasRequired(t => t.Usuario)
                .WithMany(v => v.Vendas)
                .HasForeignKey(x => x.UsuarioId)
                .WillCascadeOnDelete(false);

            HasMany(t => t.Historico)
                .WithRequired(t => t.Venda)
                .HasForeignKey(t => t.VendaId)
                .WillCascadeOnDelete(false);
        }
    }
}
