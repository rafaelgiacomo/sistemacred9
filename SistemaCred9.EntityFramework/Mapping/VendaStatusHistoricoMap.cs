using SistemaCred9.Modelo;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class VendaStatusHistoricoMap : EntityTypeConfiguration<VendaStatusHistorico>
    {
        public VendaStatusHistoricoMap()
        {
            ToTable("VendaStatusHistorico");

            HasKey(t => t.Id);

            Property(t => t.VendaId)
                .IsRequired();

            Property(t => t.StatusId)
                .IsRequired();

            Property(t => t.Data)
                .IsRequired();

            Property(t => t.UsuarioId)
                .IsRequired();

            Property(t => t.Observacao)
                .IsRequired();
        }
    }
}
