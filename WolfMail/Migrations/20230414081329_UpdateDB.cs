using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WolfMail.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_MailGroups_Users_UserId",
                table: "MailGroups"
            );

            migrationBuilder
                .AlterColumn<string>(
                    name: "Id",
                    table: "Users",
                    type: "TEXT",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "INTEGER"
                )
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MailGroups",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER"
            );

            migrationBuilder
                .AlterColumn<string>(
                    name: "Id",
                    table: "MailGroups",
                    type: "TEXT",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "INTEGER"
                )
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MailAddresses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER"
            );

            migrationBuilder.AlterColumn<string>(
                name: "MailGroupId",
                table: "MailAddresses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true
            );

            migrationBuilder
                .AlterColumn<string>(
                    name: "Id",
                    table: "MailAddresses",
                    type: "TEXT",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "INTEGER"
                )
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_MailAddresses_Email",
                table: "MailAddresses",
                column: "Email",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_MailGroups_Users_UserId",
                table: "MailGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_MailGroups_Users_UserId",
                table: "MailGroups"
            );

            migrationBuilder.DropIndex(name: "IX_MailAddresses_Email", table: "MailAddresses");

            migrationBuilder
                .AlterColumn<int>(
                    name: "Id",
                    table: "Users",
                    type: "INTEGER",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "TEXT"
                )
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MailGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true
            );

            migrationBuilder
                .AlterColumn<int>(
                    name: "Id",
                    table: "MailGroups",
                    type: "INTEGER",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "TEXT"
                )
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MailAddresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<int>(
                name: "MailGroupId",
                table: "MailAddresses",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true
            );

            migrationBuilder
                .AlterColumn<int>(
                    name: "Id",
                    table: "MailAddresses",
                    type: "INTEGER",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "TEXT"
                )
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_MailAddresses_Users_UserId",
                table: "MailAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_MailGroups_Users_UserId",
                table: "MailGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
