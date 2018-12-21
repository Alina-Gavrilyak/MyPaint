namespace WebApiServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Strokes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BoardId = c.Guid(nullable: false),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId);
            
            DropColumn("dbo.Boards", "Strokes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Boards", "Strokes", c => c.Binary());
            DropForeignKey("dbo.Strokes", "BoardId", "dbo.Boards");
            DropIndex("dbo.Strokes", new[] { "BoardId" });
            DropTable("dbo.Strokes");
        }
    }
}
