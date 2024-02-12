using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBankingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "a1", null, "ApplicationRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "au1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83eef4ce-bcd0-45b0-b560-feba85606bf5", "AQAAAAIAAYagAAAAEFH45Bs/onhiUIe7cgZCu+nQq4bvBp/q1b7m0J6VOVcPpx6wAcIYUiysXYWIc/QrCw==", "14e01f6e-1702-4b63-a1d9-48148fb31cab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "au1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c108f3de-b7f1-4e6c-a9f0-098d3415fd03", "AQAAAAIAAYagAAAAEHWeJo6+Eo1ciMjKFltX1sscWEo6AUAYHTmsRrhryRxClh50c4J79MRgMhLjaLi0Vg==", "9c5215e6-d99e-44ff-a7e5-0e04c7bb0ed1" });
        }
    }
}
