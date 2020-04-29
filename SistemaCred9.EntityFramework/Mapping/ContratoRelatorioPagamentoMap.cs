using SistemaCred9.Modelo;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class ContratoRelatorioPagamentoMap : EntityTypeConfiguration<ContratoRelatorioPagamento>
    {
        public ContratoRelatorioPagamentoMap()
        {
            ToTable("ContratoRelatorioPagamento");

            HasKey(t => t.Id);

            Property(t => t.Contrato)
                .IsRequired();

            Property(t => t.Cpf)
                .HasMaxLength(20)
                .IsOptional();

            Property(t => t.NomeCliente)
                .IsOptional();

            Property(t => t.Produto)
                .IsOptional();

            Property(t => t.Tabela)
                .IsOptional();

            Property(t => t.Banco)
                .IsOptional();

            Property(t => t.PercentualComissao)
                .IsOptional();

            Property(t => t.ValorEmprestimo)
                .IsOptional();

            Property(t => t.ValorComissao)
                .IsOptional();

            Property(t => t.DataComissao)
                .IsOptional();

            Property(t => t.TipoPlanilha)
                .IsOptional();

            Property(t => t.ContratoRelatorioId)
                .IsOptional();
        }
    }
}
