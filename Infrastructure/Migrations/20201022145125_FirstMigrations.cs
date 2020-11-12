using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FirstMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Currency = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    CreditId = table.Column<Guid>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.CreditId);
                    table.ForeignKey(
                        name: "FK_Credits_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Debit",
                columns: table => new
                {
                    DebitId = table.Column<Guid>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debit", x => x.DebitId);
                    table.ForeignKey(
                        name: "FK_Debit_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Currency", "CustomerId" },
                values: new object[] { new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"), "BRL", new Guid("4c510cfe-5d61-4a46-a3d9-c4313426621f") });

            migrationBuilder.InsertData(
                table: "Credits",
                columns: new[] { "CreditId", "AccountId", "Currency", "TransactionDate", "Value" },
                values: new object[] { new Guid("7bf066ba-379a-4e72-a59b-9755fda432ce"), new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"), "BRL", new DateTime(2020, 10, 22, 14, 51, 25, 470, DateTimeKind.Utc).AddTicks(6059), 400m });

            migrationBuilder.InsertData(
                table: "Debit",
                columns: new[] { "DebitId", "AccountId", "Currency", "TransactionDate", "Value" },
                values: new object[] { new Guid("31ade963-bd69-4afb-9df7-611ae2cfa651"), new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"), "BRL", new DateTime(2020, 10, 22, 14, 51, 25, 470, DateTimeKind.Utc).AddTicks(7447), 50m });

            migrationBuilder.CreateIndex(
                name: "IX_Credits_AccountId",
                table: "Credits",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Debit_AccountId",
                table: "Debit",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Debit");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
