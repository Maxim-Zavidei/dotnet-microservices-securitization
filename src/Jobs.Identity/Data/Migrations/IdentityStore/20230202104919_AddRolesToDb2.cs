using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jobs.Identity.Data.Migrations.IdentityStore
{
    /// <inheritdoc />
    public partial class AddRolesToDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0f3813f-c3c6-4966-a0d5-a9c90f5384ff", null, "Admin", "ADMIN" },
                    { "c6256701-096b-4efd-b6be-20a540702bc0", null, "Guest", "GUEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0f3813f-c3c6-4966-a0d5-a9c90f5384ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6256701-096b-4efd-b6be-20a540702bc0");
        }
    }
}
