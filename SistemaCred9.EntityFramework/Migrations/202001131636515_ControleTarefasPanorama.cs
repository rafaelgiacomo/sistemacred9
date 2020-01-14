namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControleTarefasPanorama : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contrato",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        NumContrato = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        DataLancamento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contrato", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Contrato", new[] { "UsuarioId" });
            DropTable("dbo.Contrato");
        }
    }
}
