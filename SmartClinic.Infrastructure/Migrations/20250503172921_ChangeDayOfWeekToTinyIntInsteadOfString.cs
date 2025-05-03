using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDayOfWeekToTinyIntInsteadOfString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "DayOfWeek",
                table: "DoctorSchedules",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DayOfWeek",
                table: "DoctorSchedules",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }
    }
}
