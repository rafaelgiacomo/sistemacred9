namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removendo_FlagExportado : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE ContratoRelatorio SET Status = 1 where Exportado = 0");
            Sql("UPDATE ContratoRelatorio SET Status = 3 where Exportado = 1");
            DropColumn("dbo.ContratoRelatorio", "Exportado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContratoRelatorio", "Exportado", c => c.Boolean(nullable: false));
            Sql("UPDATE ContratoRelatorio SET Exportado = 0 where Status = 1");
            Sql("UPDATE ContratoRelatorio SET Exportado = 1 where Status = 3");
        }
    }
}
