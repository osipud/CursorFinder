namespace Server_WCF.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitNewDatabaseStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CursorPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XPos = c.Int(nullable: false),
                        YPos = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        ActionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Events",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "EventDate",
                        new Dictionary<string, object>
                        {
                            { "UniqueAttribute", "SQLite.CodeFirst.UniqueAttribute" },
                        }
                    },
                });
            DropTable("dbo.Users",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "UserLogin",
                        new Dictionary<string, object>
                        {
                            { "UniqueAttribute", "SQLite.CodeFirst.UniqueAttribute" },
                        }
                    },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        UserLogin = c.String(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "UniqueAttribute",
                                    new AnnotationValues(oldValue: null, newValue: "SQLite.CodeFirst.UniqueAttribute")
                                },
                            }),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Long(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "UniqueAttribute",
                                    new AnnotationValues(oldValue: null, newValue: "SQLite.CodeFirst.UniqueAttribute")
                                },
                            }),
                        EventName = c.String(),
                        cX = c.Int(nullable: false),
                        cY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            DropTable("dbo.CursorPositions");
        }
    }
}
