namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContratoExportado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContratoRelatorio", "Exportado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContratoRelatorio", "Exportado");
        }
    }
}
