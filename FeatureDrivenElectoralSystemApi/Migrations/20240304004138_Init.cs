using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FeatureDrivenElectoralSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CharacteristicsId = table.Column<int>(type: "integer", nullable: false),
                    Characteristic = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeatureItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FeatureId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureItem_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characteristics",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Genre" },
                    { 2, "Language" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Characteristic", "CharacteristicId", "CharacteristicsId", "Name" },
                values: new object[,]
                {
                    { 1, 0, null, 1, "Women" },
                    { 2, 0, null, 1, "Man" },
                    { 3, 0, null, 2, "English" },
                    { 4, 0, null, 2, "Ukrainian" },
                    { 5, 0, null, 2, "Korean" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Anna" },
                    { 2, "Bob" },
                    { 3, "July" }
                });

            migrationBuilder.InsertData(
                table: "FeatureItem",
                columns: new[] { "Id", "FeatureId", "ItemId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 3, 1 },
                    { 3, 4, 1 },
                    { 4, 2, 2 },
                    { 5, 5, 2 },
                    { 6, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureItem_FeatureId",
                table: "FeatureItem",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureItem_ItemId",
                table: "FeatureItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_CharacteristicId",
                table: "Features",
                column: "CharacteristicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureItem");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Characteristics");
        }
    }
}
