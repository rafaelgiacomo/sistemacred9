namespace SistemaCred9.EntityFramework.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class TarefaExecucao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrato", "StatusTarefaExecucaoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Contrato", "NumContrato", c => c.Int(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_NUMCONTRATO_UNIQUE",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { IsUnique: True }")
                    },
                }));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contrato", "NumContrato", c => c.Int(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "IX_NUMCONTRATO_UNIQUE",
                        new AnnotationValues(oldValue: "IndexAnnotation: { IsUnique: True }", newValue: null)
                    },
                }));
            DropColumn("dbo.Contrato", "StatusTarefaExecucaoId");
        }
    }
}
