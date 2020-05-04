namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndiceNaColunaContrato : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ContratoRelatorio", "Contrato");
            CreateIndex("dbo.ContratoRelatorioPagamento", "Contrato");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContratoRelatorioPagamento", new[] { "Contrato" });
            DropIndex("dbo.ContratoRelatorio", new[] { "Contrato" });
        }
    }
}
