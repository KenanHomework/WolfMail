using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WolfMail.Migrations
{
    public partial class UpdateDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses");

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses");

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
