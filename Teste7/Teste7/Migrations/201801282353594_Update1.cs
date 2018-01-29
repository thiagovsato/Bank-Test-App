namespace Teste7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Depositoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClienteID = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.Saques",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClienteID = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.Transferencias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientePagadorID = c.Int(nullable: false),
                        ClienteRecebedorID = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.ClientePagadorID)
                .ForeignKey("dbo.Clientes", t => t.ClienteRecebedorID)
                .Index(t => t.ClientePagadorID)
                .Index(t => t.ClienteRecebedorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transferencias", "ClienteRecebedorID", "dbo.Clientes");
            DropForeignKey("dbo.Transferencias", "ClientePagadorID", "dbo.Clientes");
            DropForeignKey("dbo.Saques", "ClienteID", "dbo.Clientes");
            DropForeignKey("dbo.Depositoes", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Transferencias", new[] { "ClienteRecebedorID" });
            DropIndex("dbo.Transferencias", new[] { "ClientePagadorID" });
            DropIndex("dbo.Saques", new[] { "ClienteID" });
            DropIndex("dbo.Depositoes", new[] { "ClienteID" });
            DropTable("dbo.Transferencias");
            DropTable("dbo.Saques");
            DropTable("dbo.Depositoes");
        }
    }
}
