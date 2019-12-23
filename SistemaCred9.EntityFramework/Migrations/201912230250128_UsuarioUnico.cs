namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioUnico : DbMigration
    {
        public override void Up()
        {
            Sql("SET TRANSACTION ISOLATION LEVEL READ COMMITTED");

            CreateIndex("dbo.Usuario", "Email", unique: true);
            CreateIndex("dbo.Usuario", "NomeUsuario", unique: true);

            Sql(@"INSERT INTO Usuario (Nome, Email, NomeUsuario, Senha, TipoUsuarioId) VALUES 
                    ('Rafael Rodrigues Giacomo', 'rafaelgiacomo.rrg@gmail.com', 'rafaelgiacomo', 
                     '035190A7DB34D75531CCC0A733FBCD42E50B086E0A3E5EBF3B58FED06370F7739E7D07A30D8FBEEB418CDA7B703C735401641F837825F69655AED4A26A95C35D', 4)");
        }
        
        public override void Down()
        {
            Sql("SET TRANSACTION ISOLATION LEVEL READ COMMITTED");

            DropIndex("dbo.Usuario", new[] { "NomeUsuario" });
            DropIndex("dbo.Usuario", new[] { "Email" });

            Sql(@"DELETE FROM Usuario WHERE NomeUsuario = 'rafaelgiacomo' and Email = 'rafaelgiacomo.rrg@gmail.com'");
        }
    }
}
