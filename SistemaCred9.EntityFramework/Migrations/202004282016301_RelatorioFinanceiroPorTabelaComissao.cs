namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatorioFinanceiroPorTabelaComissao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContratoRelatorio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contrato = c.Int(nullable: false),
                        Cpf = c.String(maxLength: 20),
                        NomeCliente = c.String(),
                        NomeAssessor = c.String(),
                        Tabela = c.String(),
                        Banco = c.String(),
                        BancoCredor = c.String(),
                        PercentualComissao = c.Single(),
                        ValorCalculo = c.Single(),
                        ValorEmprestimo = c.Single(),
                        TarefaExecucaoStatus = c.String(),
                        DataLancamento = c.DateTime(),
                        TabelaComissaoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TabelaComissao", t => t.TabelaComissaoId)
                .Index(t => t.TabelaComissaoId);
            
            CreateTable(
                "dbo.TabelaComissao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 500),
                        ValorComissao = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContratoRelatorio", "TabelaComissaoId", "dbo.TabelaComissao");
            DropIndex("dbo.ContratoRelatorio", new[] { "TabelaComissaoId" });
            DropTable("dbo.TabelaComissao");
            DropTable("dbo.ContratoRelatorio");
        }
    }
}
