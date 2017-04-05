namespace WorkFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class indexedcategory : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Categories", "Lbl", unique: true, name: "IndexLblCategory");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", "IndexLblCategory");
        }
    }
}
