namespace ChampionshipOfBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Battles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        AmountRounds = c.Int(nullable: false),
                        IdWinner = c.Int(nullable: false),
                        IdLoser = c.Int(nullable: false),
                        RefereePoints = c.Int(nullable: false),
                        Loser_Id = c.Int(),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boxers", t => t.Loser_Id)
                .ForeignKey("dbo.Boxers", t => t.Winner_Id)
                .Index(t => t.Loser_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Boxers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Battles", "Winner_Id", "dbo.Boxers");
            DropForeignKey("dbo.Battles", "Loser_Id", "dbo.Boxers");
            DropIndex("dbo.Battles", new[] { "Winner_Id" });
            DropIndex("dbo.Battles", new[] { "Loser_Id" });
            DropTable("dbo.Boxers");
            DropTable("dbo.Battles");
        }
    }
}
