using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apifmu.Migrations
{
    public partial class doguinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animal_ongs_OngId",
                table: "animal");

            migrationBuilder.DropForeignKey(
                name: "FK_ongs_endereco_EnderecoId",
                table: "ongs");

            migrationBuilder.DropForeignKey(
                name: "FK_usuario_endereco_EnderecoId",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "IX_ongs_EnderecoId",
                table: "ongs");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "ongs");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "usuario",
                newName: "OngsId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_EnderecoId",
                table: "usuario",
                newName: "IX_usuario_OngsId");

            migrationBuilder.RenameColumn(
                name: "Alvara",
                table: "ongs",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "animal",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "Raca",
                table: "animal",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "Porte",
                table: "animal",
                newName: "Personalidade");

            migrationBuilder.RenameColumn(
                name: "OngId",
                table: "animal",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "animal",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "Idade",
                table: "animal",
                newName: "OngsId");

            migrationBuilder.RenameColumn(
                name: "Especie",
                table: "animal",
                newName: "Breed");

            migrationBuilder.RenameIndex(
                name: "IX_animal_OngId",
                table: "animal",
                newName: "IX_animal_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "usuario",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "usuario",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "usuario",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "Lat",
                table: "usuario",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Long",
                table: "usuario",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Quintal",
                table: "usuario",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renda",
                table: "usuario",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Residencia",
                table: "usuario",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TamanhoResidencia",
                table: "usuario",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Tela",
                table: "usuario",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cnpj",
                table: "ongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Lat",
                table: "ongs",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Long",
                table: "ongs",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "adoption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OngsId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    situation = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisapprovalReason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adoption_animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_adoption_ongs_OngsId",
                        column: x => x.OngsId,
                        principalTable: "ongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_adoption_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Cpf",
                table: "usuario",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Email",
                table: "usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ongs_Cnpj",
                table: "ongs",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_animal_OngsId",
                table: "animal",
                column: "OngsId");

            migrationBuilder.CreateIndex(
                name: "IX_adoption_AnimalId",
                table: "adoption",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_adoption_OngsId",
                table: "adoption",
                column: "OngsId");

            migrationBuilder.CreateIndex(
                name: "IX_adoption_UsuarioId",
                table: "adoption",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_animal_ongs_OngsId",
                table: "animal",
                column: "OngsId",
                principalTable: "ongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_animal_usuario_UsuarioId",
                table: "animal",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_ongs_OngsId",
                table: "usuario",
                column: "OngsId",
                principalTable: "ongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animal_ongs_OngsId",
                table: "animal");

            migrationBuilder.DropForeignKey(
                name: "FK_animal_usuario_UsuarioId",
                table: "animal");

            migrationBuilder.DropForeignKey(
                name: "FK_usuario_ongs_OngsId",
                table: "usuario");

            migrationBuilder.DropTable(
                name: "adoption");

            migrationBuilder.DropIndex(
                name: "IX_usuario_Cpf",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "IX_usuario_Email",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "IX_ongs_Cnpj",
                table: "ongs");

            migrationBuilder.DropIndex(
                name: "IX_animal_OngsId",
                table: "animal");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Quintal",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Renda",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Residencia",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "TamanhoResidencia",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Tela",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "ongs");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "ongs");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "ongs");

            migrationBuilder.RenameColumn(
                name: "OngsId",
                table: "usuario",
                newName: "EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_OngsId",
                table: "usuario",
                newName: "IX_usuario_EnderecoId");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "ongs",
                newName: "Alvara");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "animal",
                newName: "OngId");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "animal",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "animal",
                newName: "Raca");

            migrationBuilder.RenameColumn(
                name: "Personalidade",
                table: "animal",
                newName: "Porte");

            migrationBuilder.RenameColumn(
                name: "OngsId",
                table: "animal",
                newName: "Idade");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "animal",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "animal",
                newName: "Especie");

            migrationBuilder.RenameIndex(
                name: "IX_animal_UsuarioId",
                table: "animal",
                newName: "IX_animal_OngId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "usuario",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "usuario",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "ongs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ongs_EnderecoId",
                table: "ongs",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_animal_ongs_OngId",
                table: "animal",
                column: "OngId",
                principalTable: "ongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ongs_endereco_EnderecoId",
                table: "ongs",
                column: "EnderecoId",
                principalTable: "endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_endereco_EnderecoId",
                table: "usuario",
                column: "EnderecoId",
                principalTable: "endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
