using Microsoft.EntityFrameworkCore.Migrations;

namespace apifmu.Migrations
{
    public partial class primeiraSegunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_forumresposta_cliente_ClienteId",
                table: "forumresposta");

            migrationBuilder.DropForeignKey(
                name: "FK_forumresposta_forum_ForumId",
                table: "forumresposta");

            migrationBuilder.AlterColumn<int>(
                name: "ForumId",
                table: "forumresposta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "forumresposta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_forumresposta_cliente_ClienteId",
                table: "forumresposta",
                column: "ClienteId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_forumresposta_forum_ForumId",
                table: "forumresposta",
                column: "ForumId",
                principalTable: "forum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_forumresposta_cliente_ClienteId",
                table: "forumresposta");

            migrationBuilder.DropForeignKey(
                name: "FK_forumresposta_forum_ForumId",
                table: "forumresposta");

            migrationBuilder.AlterColumn<int>(
                name: "ForumId",
                table: "forumresposta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "forumresposta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_forumresposta_cliente_ClienteId",
                table: "forumresposta",
                column: "ClienteId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_forumresposta_forum_ForumId",
                table: "forumresposta",
                column: "ForumId",
                principalTable: "forum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
