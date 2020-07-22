namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataImportacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContratoRelatorio", "DataImportacao", c => c.DateTime());
            AddColumn("dbo.ContratoRelatorioPagamento", "DataImportacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContratoRelatorioPagamento", "DataImportacao");
            DropColumn("dbo.ContratoRelatorio", "DataImportacao");
        }
    }
}
