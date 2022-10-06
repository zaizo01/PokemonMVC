using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonApp.Migrations
{
    public partial class pokemonmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "PokemonTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "PokemonRegions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_PokemonId",
                table: "PokemonTypes",
                column: "PokemonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonRegions_PokemonId",
                table: "PokemonRegions",
                column: "PokemonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonRegions_Pokemon_PokemonId",
                table: "PokemonRegions",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Pokemon_PokemonId",
                table: "PokemonTypes",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonRegions_Pokemon_PokemonId",
                table: "PokemonRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Pokemon_PokemonId",
                table: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropIndex(
                name: "IX_PokemonTypes_PokemonId",
                table: "PokemonTypes");

            migrationBuilder.DropIndex(
                name: "IX_PokemonRegions_PokemonId",
                table: "PokemonRegions");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "PokemonRegions");
        }
    }
}
