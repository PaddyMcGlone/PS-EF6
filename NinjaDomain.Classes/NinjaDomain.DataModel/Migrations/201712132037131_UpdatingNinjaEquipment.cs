namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingNinjaEquipment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas");
            DropIndex("dbo.NinjaEquipments", new[] { "Ninja_Id" });
            AlterColumn("dbo.NinjaEquipments", "Ninja_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.NinjaEquipments", "Ninja_Id");
            AddForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas");
            DropIndex("dbo.NinjaEquipments", new[] { "Ninja_Id" });
            AlterColumn("dbo.NinjaEquipments", "Ninja_Id", c => c.Int());
            CreateIndex("dbo.NinjaEquipments", "Ninja_Id");
            AddForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas", "Id");
        }
    }
}
