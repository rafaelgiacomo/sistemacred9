namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControleAtivoContrato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContratoRelatorio", "Ativo", c => c.Boolean(nullable: false));
            Sql("UPDATE ContratoRelatorio SET Ativo = 1");
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContratoRelatorio", "Ativo");
        }
    }
}
