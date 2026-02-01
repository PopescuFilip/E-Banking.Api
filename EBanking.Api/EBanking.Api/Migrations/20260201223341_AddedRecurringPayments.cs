using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBanking.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecurringPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderIban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverIban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Recurency = table.Column<int>(type: "int", nullable: false),
                    LastMadePayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDefinitions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDefinitions_UserId",
                table: "PaymentDefinitions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDefinitions");
        }
    }
}
