namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataCpc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContratoRelatorio", "DataCpc", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContratoRelatorio", "DataCpc");
        }
    }
}
