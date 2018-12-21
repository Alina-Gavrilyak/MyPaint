namespace WebApiServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boards", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Boards", new[] { "Room_Id" });
            RenameColumn(table: "dbo.Boards", name: "Room_Id", newName: "RoomId");
            AlterColumn("dbo.Boards", "RoomId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Boards", "RoomId");
            AddForeignKey("dbo.Boards", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boards", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Boards", new[] { "RoomId" });
            AlterColumn("dbo.Boards", "RoomId", c => c.Guid());
            RenameColumn(table: "dbo.Boards", name: "RoomId", newName: "Room_Id");
            CreateIndex("dbo.Boards", "Room_Id");
            AddForeignKey("dbo.Boards", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
