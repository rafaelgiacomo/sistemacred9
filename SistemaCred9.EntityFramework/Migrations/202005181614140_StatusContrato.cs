namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusContrato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContratoRelatorio", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContratoRelatorio", "Status");
        }
    }
}
