namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeArquivoImportacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContratoRelatorioPagamento", "NomeArquivo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContratoRelatorioPagamento", "NomeArquivo");
        }
    }
}
