using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataValidationAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.UniqueConstraint("AK_Role_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TokenExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Email", x => x.Email);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonProvidedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Data_User_PersonProvidedId",
                        column: x => x.PersonProvidedId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataCheck",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCheck", x => new { x.UserId, x.DataId });
                    table.ForeignKey(
                        name: "FK_DataCheck_Data_DataId",
                        column: x => x.DataId,
                        principalTable: "Data",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataCheck_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("37491307-9159-465b-a902-855c7c315341"), "User" },
                    { new Guid("61631b6b-22d3-4a61-83d7-288ed59be881"), "Manager" },
                    { new Guid("7f7d960c-dfc3-4379-b1ec-6112e827862f"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "IsActive", "PasswordHash", "PasswordSalt", "RefreshToken", "RoleId", "TokenExpirationDate" },
                values: new object[,]
                {
                    { new Guid("2efa388c-584b-4e8d-9c5d-29fa600cfac9"), "3", true, "mVJOzj048UABroLpqaiOCXC7ov4rY/bHqr8zYznk+2I=", new byte[] { 34, 54, 150, 248, 77, 23, 108, 37, 149, 29, 207, 94, 119, 12, 110, 183 }, null, new Guid("61631b6b-22d3-4a61-83d7-288ed59be881"), null },
                    { new Guid("35434031-0853-472c-8c87-3b7831e0fd17"), "1", true, "yE3UT3m6W4KlkNLJdTAM4UQYIZuS7QIU/6kWcAjU/mc=", new byte[] { 118, 75, 18, 36, 222, 48, 190, 38, 185, 49, 119, 151, 113, 34, 164, 228 }, null, new Guid("37491307-9159-465b-a902-855c7c315341"), null },
                    { new Guid("adca4721-3a4c-44ca-80a6-de74abc7450e"), "5", true, "4PDjB8dRo5n/m9N7vnYR8tM/PdyB0M7wV+dHNRAD3YQ=", new byte[] { 51, 48, 107, 15, 165, 157, 219, 223, 108, 49, 81, 49, 152, 219, 5, 159 }, null, new Guid("7f7d960c-dfc3-4379-b1ec-6112e827862f"), null },
                    { new Guid("ead9e0c0-395b-4489-a82b-416562905957"), "4", true, "Kfqg0txtZSqNkmeQbOosmGadf/IIaB2z3WaeMr3C1o0=", new byte[] { 205, 233, 1, 141, 87, 98, 77, 54, 135, 152, 169, 87, 148, 116, 188, 201 }, null, new Guid("61631b6b-22d3-4a61-83d7-288ed59be881"), null },
                    { new Guid("f5080b8b-9b17-497b-8fe3-9470978b0ab1"), "6", true, "Pmuehs/dEiTauUtjQXqPWSjr4XombKHPuqgZBW1JYhM=", new byte[] { 128, 183, 146, 79, 41, 2, 134, 246, 132, 144, 191, 89, 214, 199, 85, 70 }, null, new Guid("7f7d960c-dfc3-4379-b1ec-6112e827862f"), null },
                    { new Guid("f725550b-b2b0-4509-85bd-556535471756"), "2", true, "KjK/J9gOLsJ5Qi8MQlpn7+G5YcU1ZnQtuZI1X+TTzy0=", new byte[] { 137, 110, 127, 108, 129, 137, 155, 42, 178, 110, 230, 77, 29, 222, 131, 149 }, null, new Guid("37491307-9159-465b-a902-855c7c315341"), null }
                });

            migrationBuilder.InsertData(
                table: "Data",
                columns: new[] { "Id", "Date", "Information", "PersonProvidedId" },
                values: new object[,]
                {
                    { new Guid("0cc8cf83-0dde-4fa7-a140-0c95a47dc723"), new DateTime(23, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 6", new Guid("f725550b-b2b0-4509-85bd-556535471756") },
                    { new Guid("70baeb6b-e3f3-4c3c-a6d0-22c1aca1df4f"), new DateTime(23, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 2", new Guid("35434031-0853-472c-8c87-3b7831e0fd17") },
                    { new Guid("9e0bac5b-2c58-43a7-b548-99a4a94728fc"), new DateTime(23, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 3", new Guid("35434031-0853-472c-8c87-3b7831e0fd17") },
                    { new Guid("9e943e08-112b-46e1-b56a-70f27c625617"), new DateTime(23, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 8", new Guid("f725550b-b2b0-4509-85bd-556535471756") },
                    { new Guid("c0267569-6cb2-4009-8425-96af1ab376eb"), new DateTime(23, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 4", new Guid("35434031-0853-472c-8c87-3b7831e0fd17") },
                    { new Guid("c43cc9e3-530c-446b-9acf-644d08941f79"), new DateTime(23, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 1", new Guid("35434031-0853-472c-8c87-3b7831e0fd17") },
                    { new Guid("c9ef087d-059d-446f-a7e3-639c4f3523ee"), new DateTime(23, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 5", new Guid("f725550b-b2b0-4509-85bd-556535471756") },
                    { new Guid("eeb851e3-61cc-410e-b572-12828858db5e"), new DateTime(23, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Информация 7", new Guid("f725550b-b2b0-4509-85bd-556535471756") }
                });

            migrationBuilder.InsertData(
                table: "DataCheck",
                columns: new[] { "DataId", "UserId", "Valid" },
                values: new object[,]
                {
                    { new Guid("0cc8cf83-0dde-4fa7-a140-0c95a47dc723"), new Guid("2efa388c-584b-4e8d-9c5d-29fa600cfac9"), false },
                    { new Guid("9e0bac5b-2c58-43a7-b548-99a4a94728fc"), new Guid("2efa388c-584b-4e8d-9c5d-29fa600cfac9"), null },
                    { new Guid("c43cc9e3-530c-446b-9acf-644d08941f79"), new Guid("2efa388c-584b-4e8d-9c5d-29fa600cfac9"), true },
                    { new Guid("70baeb6b-e3f3-4c3c-a6d0-22c1aca1df4f"), new Guid("ead9e0c0-395b-4489-a82b-416562905957"), false },
                    { new Guid("c9ef087d-059d-446f-a7e3-639c4f3523ee"), new Guid("ead9e0c0-395b-4489-a82b-416562905957"), true },
                    { new Guid("eeb851e3-61cc-410e-b572-12828858db5e"), new Guid("ead9e0c0-395b-4489-a82b-416562905957"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Data_PersonProvidedId",
                table: "Data",
                column: "PersonProvidedId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCheck_DataId",
                table: "DataCheck",
                column: "DataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataCheck");

            migrationBuilder.DropTable(
                name: "Data");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
