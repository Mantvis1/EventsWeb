using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Events.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable:false),
                    isBanned = table.Column<bool>(nullable:false, defaultValue:false),
                    isAdmin = table.Column<bool>(nullable:false, defaultValue:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: false, maxLength: 50),
                    summary = table.Column<string>(nullable: false, maxLength: 500),
                    createdBy = table.Column<int>(nullable:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.id);
                    table.ForeignKey("FK_User", x => x.createdBy,"User","id",onDelete:ReferentialAction.NoAction);
                }
        );
            migrationBuilder.CreateTable(
                name:"Support",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: false, maxLength: 50),
                    summary = table.Column<string>(nullable: false, maxLength: 1000),
                    writenBy = table.Column<int>(nullable: false),
                    solution = table.Column<string>(nullable: false, maxLength: 200),
                    solvedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Support", x => x.id);
                    table.ForeignKey("FK_Writer", x => x.writenBy, "User", "id", onDelete: ReferentialAction.NoAction);
                    table.ForeignKey("FK_AdminUser", x => x.solvedBy, "User", "id", onDelete: ReferentialAction.NoAction);
                }
                );
            migrationBuilder.CreateTable(
                name:"UserEvents",
                 columns: table => new
                 {
                     id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                     participan = table.Column<int>(nullable:false),
                     eventId = table.Column<int>(nullable:false)
                 },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_UserEvents", x => x.id);
                      table.ForeignKey("FK_Participan", x => x.participan, "User", "id", onDelete: ReferentialAction.NoAction);
                      table.ForeignKey("FK_EventId", x => x.eventId, "Event", "id", onDelete: ReferentialAction.NoAction);
                  }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
            migrationBuilder.DropTable(
                name: "Event");
            migrationBuilder.DropTable(
                name: "Support");
            migrationBuilder.DropTable(
               name: "UserEvents");
        }
    }
}
