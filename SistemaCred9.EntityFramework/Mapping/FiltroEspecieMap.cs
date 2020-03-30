using SistemaCred9.Modelo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class FiltroEspecieMap : EntityTypeConfiguration<FiltroEspecie>
    {
        public FiltroEspecieMap()
        {
            ToTable("FiltroEspecie");

            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Descricao)
                .IsRequired();

            Property(t => t.CodEspecie)
                .IsRequired();
        }
    }
}
