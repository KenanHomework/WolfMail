using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace WolfMail.Migrations;

public partial class FinishRelations : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table =>
                new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "MailGroups",
            columns: table =>
                new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_MailGroups", x => x.Id);
                table.ForeignKey(
                    name: "FK_MailGroups_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "MailAddresses",
            columns: table =>
                new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    MailGroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_MailAddresses", x => x.Id);
                table.ForeignKey(
                    name: "FK_MailAddresses_MailGroups_MailGroupId",
                    column: x => x.MailGroupId,
                    principalTable: "MailGroups",
                    principalColumn: "Id"
                );
                table.ForeignKey(
                    name: "FK_MailAddresses_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateIndex(
            name: "IX_MailAddresses_MailGroupId",
            table: "MailAddresses",
            column: "MailGroupId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_MailAddresses_UserId",
            table: "MailAddresses",
            column: "UserId",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "IX_MailGroups_UserId",
            table: "MailGroups",
            column: "UserId"
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "MailAddresses");

        migrationBuilder.DropTable(name: "MailGroups");

        migrationBuilder.DropTable(name: "Users");
    }
}
