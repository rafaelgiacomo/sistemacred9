namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContratoPanoramaUnico : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContratoRelatorio", new[] { "Contrato" });
            CreateIndex("dbo.ContratoRelatorio", "Contrato", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContratoRelatorio", new[] { "Contrato" });
            CreateIndex("dbo.ContratoRelatorio", "Contrato");
        }
    }
}
