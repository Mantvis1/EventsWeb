using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Events.Migrations
{
    public partial class _2 : Migration
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
                   password = table.Column<string>(nullable: false),
                   email = table.Column<string>(nullable:true),
                   isBanned = table.Column<bool>(nullable: false, defaultValue: false),
                   isAdmin = table.Column<bool>(nullable: false, defaultValue: false)
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
                    createdBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.id);
                    table.ForeignKey("FK_User", x => x.createdBy, "User", "id", onDelete: ReferentialAction.NoAction);
                }
        );
            migrationBuilder.CreateTable(
                name: "Support",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: false, maxLength: 50),
                    summary = table.Column<string>(nullable: false, maxLength: 1000),
                    writenBy = table.Column<int>(nullable: false),
                    solution = table.Column<string>(nullable: false, maxLength: 200, defaultValue: ""),
                    solvedBy = table.Column<int>(nullable: true, defaultValue: null)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Support", x => x.id);
                    table.ForeignKey("FK_Writer", x => x.writenBy, "User", "id", onDelete: ReferentialAction.NoAction);
                    table.ForeignKey("FK_AdminUser", x => x.solvedBy, "User", "id", onDelete: ReferentialAction.NoAction);
                }
                );
            migrationBuilder.CreateTable(
                name: "UserEvents",
                 columns: table => new
                 {
                     id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                     participan = table.Column<int>(nullable: false),
                     eventId = table.Column<int>(nullable: false)
                 },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_UserEvents", x => x.id);
                      table.ForeignKey("FK_Participan", x => x.participan, "User", "id", onDelete: ReferentialAction.NoAction);
                      table.ForeignKey("FK_EventId", x => x.eventId, "Event", "id", onDelete: ReferentialAction.NoAction);
                  }
                );

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", null, "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Support");

            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "id");
        }
    }
}
