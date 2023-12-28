using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevStore.Catalog.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductsAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("25feafe7-1cab-4e8c-9337-7b109b22b728"), 100, "shirt" },
                    { new Guid("ddd23b4f-754b-49c3-a2c8-759e10692f22"), 101, "mug" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "CategoryId", "Description", "Image", "Name", "Price", "Stock", "Width", "Height", "Depth" },
                values: new object[,]
                {
                    { new Guid("1cdba9cc-6c60-44b5-b4a4-9ac5fb13966b"), true, new Guid("25feafe7-1cab-4e8c-9337-7b109b22b728"), "100% cotton t-shirt, resistant to washing and high temperatures.", "shirt1.jpg", "Software Developer T-Shirt", 100.00m, 8, 5, 5, 5 },
                    { new Guid("3f04ec21-8d57-46b0-a8e2-98f248ed499e"), true, new Guid("ddd23b4f-754b-49c3-a2c8-759e10692f22"), "Porcelain mug with thermal printing.", "mug3.jpg", "Turn Coffee in Code Mug", 20.00m, 5, 12, 8, 5 },
                    { new Guid("40ebe85c-19b9-4f34-a491-5d2a184fce3e"), true, new Guid("ddd23b4f-754b-49c3-a2c8-759e10692f22"), "Porcelain mug with thermal printing.", "mug1.jpg", "Star Bugs Coffee Mug", 20.00m, 5, 12, 8, 5  },
                    { new Guid("572b7fd8-d89c-4452-833c-fd99c35a10c3"), true, new Guid("25feafe7-1cab-4e8c-9337-7b109b22b728"), "100% cotton t-shirt, resistant to washing and high temperatures.", "shirt4.jpg", "Debugar Black T-Shirt", 110.00m, 5, 5, 5, 5  },
                    { new Guid("8058ea48-d033-48b8-be46-aa7f2655768b"), true, new Guid("ddd23b4f-754b-49c3-a2c8-759e10692f22"), "Porcelain mug with thermal printing.", "mug2.jpg", "Programmer Code Mug", 15.00m, 8, 12, 8, 5  },
                    { new Guid("85b1dc9e-8481-4891-86a8-a53571c4cffe"), true, new Guid("25feafe7-1cab-4e8c-9337-7b109b22b728"), "100% cotton t-shirt, resistant to washing and high temperatures.", "shirt2.jpg", "Black Code Life T-Shirt", 90.00m, 3, 5, 5, 5  },
                    { new Guid("9b33b73a-83e9-4a63-aef9-ed30f3e2ccbf"), true, new Guid("ddd23b4f-754b-49c3-a2c8-759e10692f22"), "Porcelain mug with thermal printing.", "mug4.jpg", "No Coffee No Code Mug", 10.00m, 23, 12, 8, 5  },
                    { new Guid("c6d432bc-846e-430f-9f22-441bbcd295c5"), true, new Guid("25feafe7-1cab-4e8c-9337-7b109b22b728"), "100% cotton t-shirt, resistant to washing and high temperatures.", "shirt3.jpg", "Gray Code Life T-Shirt", 80.00m, 15, 5, 5, 5  }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1cdba9cc-6c60-44b5-b4a4-9ac5fb13966b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3f04ec21-8d57-46b0-a8e2-98f248ed499e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("40ebe85c-19b9-4f34-a491-5d2a184fce3e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("572b7fd8-d89c-4452-833c-fd99c35a10c3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8058ea48-d033-48b8-be46-aa7f2655768b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("85b1dc9e-8481-4891-86a8-a53571c4cffe"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9b33b73a-83e9-4a63-aef9-ed30f3e2ccbf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c6d432bc-846e-430f-9f22-441bbcd295c5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("25feafe7-1cab-4e8c-9337-7b109b22b728"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ddd23b4f-754b-49c3-a2c8-759e10692f22"));
        }
    }
}
