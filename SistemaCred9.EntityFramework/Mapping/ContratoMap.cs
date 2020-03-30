using SistemaCred9.Modelo.Panorama;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class ContratoMap : EntityTypeConfiguration<Contrato>
    {
        public ContratoMap()
        {
            ToTable("Contrato");

            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ClienteId)
                .IsRequired();

            Property(t => t.DataLancamento)
                .IsRequired();

            Property(t => t.NumContrato)
                .IsRequired();

            Property(t => t.UsuarioId)
                .IsRequired();

            Property(t => t.NumContrato)
                .HasColumnAnnotation("IX_NUMCONTRATO_UNIQUE", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}
