namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContratoRelatorioPagamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContratoRelatorioPagamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contrato = c.Int(nullable: false),
                        Cpf = c.String(maxLength: 20),
                        NomeCliente = c.String(),
                        Produto = c.String(),
                        Tabela = c.String(),
                        Banco = c.String(),
                        PercentualComissao = c.Single(),
                        ValorComissao = c.Single(),
                        ValorEmprestimo = c.Single(),
                        DataComissao = c.DateTime(),
                        TipoPlanilha = c.Int(),
                        TabelaComissaoId = c.Int(),
                        ContratoRelatorioId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContratoRelatorio", t => t.ContratoRelatorioId)
                .Index(t => t.ContratoRelatorioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContratoRelatorioPagamento", "ContratoRelatorioId", "dbo.ContratoRelatorio");
            DropIndex("dbo.ContratoRelatorioPagamento", new[] { "ContratoRelatorioId" });
            DropTable("dbo.ContratoRelatorioPagamento");
        }
    }
}
