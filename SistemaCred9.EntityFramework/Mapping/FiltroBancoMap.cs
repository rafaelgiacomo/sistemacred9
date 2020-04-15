using SistemaCred9.Modelo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class FiltroBancoMap : EntityTypeConfiguration<FiltroBanco>
    {
        public FiltroBancoMap()
        {
            ToTable("FiltroBanco");

            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Banco)
                .IsRequired();

            Property(t => t.Coeficiente)
                .IsRequired();

            Property(t => t.MinLiquido)
                .IsOptional();

            Property(t => t.MaxParcelasEmAberto)
                .IsRequired();

            Property(t => t.MinParcelasEmAberto)
                .IsOptional();

            Property(t => t.MinParcela)
                .IsOptional();

            Property(t => t.Prazo)
                .IsOptional();
        }
    }
}
