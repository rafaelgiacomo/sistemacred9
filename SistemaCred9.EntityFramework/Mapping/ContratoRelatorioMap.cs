using SistemaCred9.Modelo;
using System.Data.Entity.ModelConfiguration;

namespace SistemaCred9.EntityFramework.Mapping
{
    public class ContratoRelatorioMap : EntityTypeConfiguration<ContratoRelatorio>
    {
        public ContratoRelatorioMap()
        {
            ToTable("ContratoRelatorio");

            HasKey(t => t.Id);

            Property(t => t.Contrato)
                .IsRequired();

            Property(t => t.Cpf)
                .HasMaxLength(20)
                .IsOptional();

            Property(t => t.NomeCliente)
                .IsOptional();

            Property(t => t.NomeAssessor)
                .IsOptional();

            Property(t => t.Tabela)
                .IsOptional();

            Property(t => t.Banco)
                .IsOptional();

            Property(t => t.BancoCredor)
                .IsOptional();

            Property(t => t.PercentualComissao)
                .IsOptional();

            Property(t => t.ValorCalculo)
                .IsOptional();

            Property(t => t.ValorEmprestimo)
                .IsOptional();

            Property(t => t.TarefaExecucaoStatus)
                .IsOptional();

            Property(t => t.DataLancamento)
                .IsOptional(); 
        }
    }
}
