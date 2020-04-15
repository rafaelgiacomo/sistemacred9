using SistemaCred9.Modelo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class FiltroMap : EntityTypeConfiguration<Filtro>
    {
        public FiltroMap()
        {
            ToTable("Filtro");

            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Ativo)
                .IsRequired();

            Property(t => t.Descricao)
                .IsRequired();

            Property(t => t.CodAgrupamento)
                .IsOptional();

            Property(t => t.LimiteRegistros)
                .IsRequired();

            Property(t => t.MinDataNascimento)
                .IsOptional();

            HasMany(r => r.ListaFiltroBanco)
                .WithMany(r => r.ListaFiltro)
                .Map(cs =>
                {
                    cs.MapLeftKey("FiltroId");
                    cs.MapRightKey("FiltroBancoId");
                    cs.ToTable("FiltroFiltroBanco");
                });

            HasMany(r => r.ListaFiltroEspecie)
                .WithMany(r => r.ListaFiltro)
                .Map(cs =>
                {
                    cs.MapLeftKey("FiltroId");
                    cs.MapRightKey("FiltroEspecieId");
                    cs.ToTable("FiltroFiltroEspecie");
                });
        }
    }
}
