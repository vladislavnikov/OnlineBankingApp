using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBankingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "AspNetRoles",
               keyColumn: "Id",
               keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "au1", 0, "ad4ee1e6-4c0b-4d2b-aa21-38ec8b59a065", "admin@mail.com", false, "Admin", "User", false, null, "admin@mail.com", "adminuser", "AQAAAAIAAYagAAAAEAiwC9690UVjWaKxLGk2grLfLu17iM27phLZBoeSv3AoyLOe6tOm74see9ZxeeabgQ==", null, false, "a8a97be9-870b-42c4-9fc3-f57b35d6e6a1", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "au1", 0, "ad4ee1e6-4c0b-4d2b-aa21-38ec8b59a065", "admin@mail.com", false, "Admin", "User", false, null, "admin@mail.com", "adminuser", "AQAAAAIAAYagAAAAEAiwC9690UVjWaKxLGk2grLfLu17iM27phLZBoeSv3AoyLOe6tOm74see9ZxeeabgQ==", null, false, "a8a97be9-870b-42c4-9fc3-f57b35d6e6a1", false, null });
        }
    }
}
