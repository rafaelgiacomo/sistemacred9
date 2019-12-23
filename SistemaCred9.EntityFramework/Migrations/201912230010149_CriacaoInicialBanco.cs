namespace SistemaCred9.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoInicialBanco : DbMigration
    {
        public override void Up()
        {
            Sql("SET TRANSACTION ISOLATION LEVEL READ COMMITTED");

            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 250),
                        NomeUsuario = c.String(nullable: false, maxLength: 50),
                        Senha = c.String(nullable: false, maxLength: 1000),
                        TipoUsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Venda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.VendaStatusHistorico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendaId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Observacao = c.String(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Venda", t => t.VendaId)
                .Index(t => t.VendaId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.UsuarioRole",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.RoleId })
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.RoleId);

            Sql(@"INSERT INTO Role (Id, Descricao) VALUES (1, 'Operador')
                  INSERT INTO Role (Id, Descricao) VALUES (2, 'BackOffice')
                  INSERT INTO Role (Id, Descricao) VALUES (3, 'Coordenador')
                  INSERT INTO Role (Id, Descricao) VALUES (4, 'Administrador')");

        }
        
        public override void Down()
        {
            Sql("SET TRANSACTION ISOLATION LEVEL READ COMMITTED");

            DropForeignKey("dbo.Venda", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.VendaStatusHistorico", "VendaId", "dbo.Venda");
            DropForeignKey("dbo.VendaStatusHistorico", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UsuarioRole", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.UsuarioRole", new[] { "RoleId" });
            DropIndex("dbo.UsuarioRole", new[] { "UsuarioId" });
            DropIndex("dbo.VendaStatusHistorico", new[] { "UsuarioId" });
            DropIndex("dbo.VendaStatusHistorico", new[] { "VendaId" });
            DropIndex("dbo.Venda", new[] { "UsuarioId" });
            DropTable("dbo.UsuarioRole");
            DropTable("dbo.VendaStatusHistorico");
            DropTable("dbo.Venda");
            DropTable("dbo.Usuario");
            DropTable("dbo.Role");

            Sql(@"DELETE FROM Role WHERE Id = 1
                  DELETE FROM Role WHERE Id = 2
                  DELETE FROM Role WHERE Id = 3
                  DELETE FROM Role WHERE Id = 4");
        }
    }
}
