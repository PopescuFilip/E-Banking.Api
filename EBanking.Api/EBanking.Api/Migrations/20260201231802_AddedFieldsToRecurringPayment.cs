using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBanking.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToRecurringPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Recurency",
                table: "PaymentDefinitions",
                newName: "Recurrency");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "PaymentDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverAccountName",
                table: "PaymentDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "PaymentDefinitions");

            migrationBuilder.DropColumn(
                name: "ReceiverAccountName",
                table: "PaymentDefinitions");

            migrationBuilder.RenameColumn(
                name: "Recurrency",
                table: "PaymentDefinitions",
                newName: "Recurency");
        }
    }
}
