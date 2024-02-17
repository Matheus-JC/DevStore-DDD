using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStore.Sales.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedVouchers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Vouchers",
            columns: ["Id", "Code", "DiscountType", "DiscountValue", "Quantity", "CreationDate", "UsageDate", "ValidityDate", "Active", "Used"],
            values: new object[,]
            {
                { new Guid("54c6420e-1027-43b5-8343-65cffbb410aa"), "PROMO-10-PERCENT", 0, 10.00m, 50, DateTime.UtcNow, null, DateTime.UtcNow.AddYears(3), true, false },
                { new Guid("f2bb8f3b-66d2-4eff-bbce-8af7dceaf67b"), "PROMO-15-FIXED", 1, 15.00m, 0, DateTime.UtcNow, null, DateTime.UtcNow.AddYears(3), true, false },
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: new Guid("54c6420e-1027-43b5-8343-65cffbb410aa"));

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: new Guid("f2bb8f3b-66d2-4eff-bbce-8af7dceaf67b"));
        }
    }
}
