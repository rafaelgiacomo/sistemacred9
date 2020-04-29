using SistemaCred9.Modelo;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class TabelaComissaoMap : EntityTypeConfiguration<TabelaComissao>
    {
        public TabelaComissaoMap()
        {
            ToTable("TabelaComissao");

            HasKey(t => t.Id);

            Property(t => t.Descricao)
                .HasMaxLength(500)
                .IsRequired();

            Property(t => t.ValorComissao)
                .IsRequired();

            HasMany(x => x.Contratos)
                .WithOptional(x => x.TabelaComissao)
                .HasForeignKey(x => x.TabelaComissaoId)
                .WillCascadeOnDelete(false);
        }
    }
}
