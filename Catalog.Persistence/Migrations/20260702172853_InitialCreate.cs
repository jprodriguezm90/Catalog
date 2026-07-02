using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    InStore = table.Column<int>(type: "int", nullable: false),
                    Online = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("254d8bb7-fd25-4c80-ab2f-24acbcab94c8"), "Adidas is a global German company that makes shoes, clothes, and sports gear.", "Adidas" },
                    { new Guid("6ca12909-23a9-4b62-a519-53e8e803e387"), "Nike is the world's leading sports brand, designing and selling athletic footwear, apparel, and equipment.", "Nike" },
                    { new Guid("878bc0af-6692-43f2-a131-32e5d6a22532"), "GymShark is a fitness apparel, manufacturer & online retailer based in the United Kingdom.", "GymShark" },
                    { new Guid("b56a84d8-f6d7-4831-bc43-eb6b087f63b9"), "Diadora is an Italian sportswear and footwear company founded in 1948 in Caerano di San Marco, Italy.", "Diadora" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2abd90b8-845a-45f3-8081-9a49198105fb"), "Pants are a type of clothing worn on the lower body, covering the legs and typically extending from the waist to the ankles. They come in various styles, such as jeans, trousers, leggings, and shorts, and can be made from different materials like denim, cotton, or synthetic fabrics.", "Pants" },
                    { new Guid("79fbd40c-96b0-4daa-a53b-0664d3a626d0"), "Shoes are a type of footwear designed to protect and provide comfort to the feet while walking, running, or engaging in various activities. They come in various styles, such as sneakers, boots, sandals, and dress shoes, and can be made from different materials like leather, canvas, or synthetic fabrics.", "Shoes" },
                    { new Guid("a0530e8f-c985-461d-b5ca-cce60adebe9e"), "Shirts are a type of clothing worn on the upper body, typically made of fabric and designed to cover the torso and arms. They come in various styles, such as dress shirts, casual shirts, and t-shirts, and can be made from different materials like cotton, linen, or synthetic fabrics.", "Shirts" },
                    { new Guid("e9ef65ef-a066-427d-88b0-7c7845b9c693"), "Caps are a type of headwear that typically features a rounded crown and a visor or brim at the front. They are designed to provide shade and protection from the sun, as well as to serve as a fashion accessory. Caps come in various styles, such as baseball caps, snapbacks, and fitted caps, and can be made from different materials like cotton, wool, or synthetic fabrics.", "Caps" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0064db26-87a7-4da2-aa13-c72096303b10"), new Guid("b56a84d8-f6d7-4831-bc43-eb6b087f63b9"), new Guid("2abd90b8-845a-45f3-8081-9a49198105fb"), "Cottom Sweat Jogger", 39.99m },
                    { new Guid("097c365a-f057-4de1-aaaf-829516c25467"), new Guid("6ca12909-23a9-4b62-a519-53e8e803e387"), new Guid("79fbd40c-96b0-4daa-a53b-0664d3a626d0"), "Pegasus Nike Runners", 99.99m },
                    { new Guid("4db2c7d5-fc02-4bc4-a245-9f0a5396083a"), new Guid("254d8bb7-fd25-4c80-ab2f-24acbcab94c8"), new Guid("2abd90b8-845a-45f3-8081-9a49198105fb"), "Dry Sweat Jogger", 59.99m },
                    { new Guid("59842595-1129-439e-a84f-dff86c261a3c"), new Guid("878bc0af-6692-43f2-a131-32e5d6a22532"), new Guid("2abd90b8-845a-45f3-8081-9a49198105fb"), "Cottom Sweat Jogger", 39.99m },
                    { new Guid("8a8038f9-9df9-4a7b-a76e-414ae1312827"), new Guid("6ca12909-23a9-4b62-a519-53e8e803e387"), new Guid("e9ef65ef-a066-427d-88b0-7c7845b9c693"), "Baseball Cap Nike", 19.99m },
                    { new Guid("99199924-e42c-4dcf-a2ea-dc482bc3c7be"), new Guid("254d8bb7-fd25-4c80-ab2f-24acbcab94c8"), new Guid("a0530e8f-c985-461d-b5ca-cce60adebe9e"), "Dry Sweat Shirt", 49.99m },
                    { new Guid("a4aa9101-5802-4ec9-a556-468a784dcff6"), new Guid("b56a84d8-f6d7-4831-bc43-eb6b087f63b9"), new Guid("a0530e8f-c985-461d-b5ca-cce60adebe9e"), "Cottom Sweat Shirt", 29.99m },
                    { new Guid("cf198219-da46-4264-b5ed-7565a56d26fe"), new Guid("878bc0af-6692-43f2-a131-32e5d6a22532"), new Guid("a0530e8f-c985-461d-b5ca-cce60adebe9e"), "Cottom Sweat Shirt", 29.99m }
                });

            migrationBuilder.InsertData(
                table: "ProductStocks",
                columns: new[] { "Id", "InStore", "Online", "ProductId", "Size" },
                values: new object[,]
                {
                    { new Guid("03a001e7-168d-43a5-a599-a07b1ad3a692"), 10, 50, new Guid("cf198219-da46-4264-b5ed-7565a56d26fe"), "L" },
                    { new Guid("0f62f318-c85f-4b9a-8580-a07321c57d85"), 10, 50, new Guid("a4aa9101-5802-4ec9-a556-468a784dcff6"), "L" },
                    { new Guid("15fad713-5f5b-4c79-90e4-cc0d64eacf4b"), 10, 50, new Guid("99199924-e42c-4dcf-a2ea-dc482bc3c7be"), "M" },
                    { new Guid("2b068228-d46c-4cbb-bc37-db70f1b284c9"), 10, 50, new Guid("cf198219-da46-4264-b5ed-7565a56d26fe"), "M" },
                    { new Guid("3546c25a-03c4-4fd1-93dd-faef228a6035"), 10, 50, new Guid("99199924-e42c-4dcf-a2ea-dc482bc3c7be"), "L" },
                    { new Guid("3aedd23a-4e5e-46c3-9c0e-110fb4392d55"), 10, 50, new Guid("0064db26-87a7-4da2-aa13-c72096303b10"), "M" },
                    { new Guid("57904706-5617-43f7-93b1-1693c11cba99"), 10, 50, new Guid("cf198219-da46-4264-b5ed-7565a56d26fe"), "S" },
                    { new Guid("5c787691-f278-4fd7-be10-5afc6bda1292"), 10, 50, new Guid("0064db26-87a7-4da2-aa13-c72096303b10"), "S" },
                    { new Guid("666eb407-dc0c-4db7-ba61-f63b013cef82"), 10, 50, new Guid("4db2c7d5-fc02-4bc4-a245-9f0a5396083a"), "S" },
                    { new Guid("6bc34f4b-6603-47b4-9d7a-1e1091ba9d0e"), 10, 50, new Guid("59842595-1129-439e-a84f-dff86c261a3c"), "S" },
                    { new Guid("7eec7336-179e-44a1-a3ca-809c2d988aca"), 10, 50, new Guid("097c365a-f057-4de1-aaaf-829516c25467"), "10" },
                    { new Guid("8cb80b90-a919-4ee6-b0f1-426c7b0ef189"), 10, 50, new Guid("59842595-1129-439e-a84f-dff86c261a3c"), "M" },
                    { new Guid("932aa8ef-59a6-47da-ad4c-367c2db2f5a2"), 10, 50, new Guid("99199924-e42c-4dcf-a2ea-dc482bc3c7be"), "S" },
                    { new Guid("93d82da8-8cf7-496f-85ed-8b8515607bae"), 10, 50, new Guid("0064db26-87a7-4da2-aa13-c72096303b10"), "L" },
                    { new Guid("c453bafc-dc2e-4b7e-a94c-f234b91c57f7"), 10, 50, new Guid("a4aa9101-5802-4ec9-a556-468a784dcff6"), "M" },
                    { new Guid("dc8dfa43-4cf6-4106-9b42-de66c0cda92e"), 10, 50, new Guid("4db2c7d5-fc02-4bc4-a245-9f0a5396083a"), "M" },
                    { new Guid("e0bd8859-5ea7-46d8-b95a-bc4f0df8b411"), 10, 50, new Guid("a4aa9101-5802-4ec9-a556-468a784dcff6"), "S" },
                    { new Guid("e537f5f6-db4e-46a5-8595-8644b13265bf"), 10, 50, new Guid("59842595-1129-439e-a84f-dff86c261a3c"), "L" },
                    { new Guid("eb58cdd8-0672-473a-b403-b695a2527d8d"), 10, 50, new Guid("4db2c7d5-fc02-4bc4-a245-9f0a5396083a"), "L" },
                    { new Guid("efdf260f-ef62-4e98-accc-bde0474acca8"), 10, 50, new Guid("097c365a-f057-4de1-aaaf-829516c25467"), "6" },
                    { new Guid("f379d3ea-9b35-42b7-b659-e78b3f1b0e96"), 10, 50, new Guid("8a8038f9-9df9-4a7b-a76e-414ae1312827"), "U" },
                    { new Guid("fc8aff80-adf7-4232-97de-6309dd767352"), 10, 50, new Guid("097c365a-f057-4de1-aaaf-829516c25467"), "9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductStocks",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
