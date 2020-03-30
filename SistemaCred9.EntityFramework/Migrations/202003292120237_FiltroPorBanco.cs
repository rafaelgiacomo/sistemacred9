namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FiltroPorBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filtro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ativo = c.Boolean(nullable: false),
                        Descricao = c.String(nullable: false),
                        CodAgrupamento = c.Int(),
                        LimiteRegistros = c.Long(nullable: false),
                        MinDataNascimento = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FiltroBanco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Banco = c.String(nullable: false),
                        MinLiquido = c.Single(),
                        MinParcela = c.Int(),
                        Prazo = c.Int(),
                        MinParcelasEmAberto = c.Int(),
                        MaxParcelasEmAberto = c.Int(nullable: false),
                        Coeficiente = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FiltroEspecie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        CodEspecie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FiltroFiltroBanco",
                c => new
                    {
                        FiltroId = c.Int(nullable: false),
                        FiltroBancoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FiltroId, t.FiltroBancoId })
                .ForeignKey("dbo.Filtro", t => t.FiltroId, cascadeDelete: true)
                .ForeignKey("dbo.FiltroBanco", t => t.FiltroBancoId, cascadeDelete: true)
                .Index(t => t.FiltroId)
                .Index(t => t.FiltroBancoId);
            
            CreateTable(
                "dbo.FiltroFiltroEspecie",
                c => new
                    {
                        FiltroId = c.Int(nullable: false),
                        FiltroEspecieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FiltroId, t.FiltroEspecieId })
                .ForeignKey("dbo.Filtro", t => t.FiltroId, cascadeDelete: true)
                .ForeignKey("dbo.FiltroEspecie", t => t.FiltroEspecieId, cascadeDelete: true)
                .Index(t => t.FiltroId)
                .Index(t => t.FiltroEspecieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FiltroFiltroEspecie", "FiltroEspecieId", "dbo.FiltroEspecie");
            DropForeignKey("dbo.FiltroFiltroEspecie", "FiltroId", "dbo.Filtro");
            DropForeignKey("dbo.FiltroFiltroBanco", "FiltroBancoId", "dbo.FiltroBanco");
            DropForeignKey("dbo.FiltroFiltroBanco", "FiltroId", "dbo.Filtro");
            DropIndex("dbo.FiltroFiltroEspecie", new[] { "FiltroEspecieId" });
            DropIndex("dbo.FiltroFiltroEspecie", new[] { "FiltroId" });
            DropIndex("dbo.FiltroFiltroBanco", new[] { "FiltroBancoId" });
            DropIndex("dbo.FiltroFiltroBanco", new[] { "FiltroId" });
            DropTable("dbo.FiltroFiltroEspecie");
            DropTable("dbo.FiltroFiltroBanco");
            DropTable("dbo.FiltroEspecie");
            DropTable("dbo.FiltroBanco");
            DropTable("dbo.Filtro");
        }
    }
}
