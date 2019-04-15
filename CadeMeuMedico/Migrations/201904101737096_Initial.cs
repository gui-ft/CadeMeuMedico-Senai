namespace CadeMeuMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.CidadeID);
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        MedicoID = c.Int(nullable: false),
                        DataAdd = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(),
                        CRM = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                        DataAtual = c.String(),
                        CidadeID = c.Int(nullable: false),
                        EspecialidadeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicoID, t.DataAdd })
                .ForeignKey("dbo.Cidade", t => t.CidadeID, cascadeDelete: true)
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeID, cascadeDelete: true)
                .Index(t => t.CidadeID)
                .Index(t => t.EspecialidadeID);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadeID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medico", "EspecialidadeID", "dbo.Especialidade");
            DropForeignKey("dbo.Medico", "CidadeID", "dbo.Cidade");
            DropIndex("dbo.Medico", new[] { "EspecialidadeID" });
            DropIndex("dbo.Medico", new[] { "CidadeID" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Medico");
            DropTable("dbo.Cidade");
        }
    }
}
