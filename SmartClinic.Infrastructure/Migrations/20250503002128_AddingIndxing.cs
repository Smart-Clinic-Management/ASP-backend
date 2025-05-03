using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndxing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_IsActive",
                table: "Patients",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DayOfWeek",
                table: "DoctorSchedules",
                column: "DayOfWeek");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_StartTime",
                table: "DoctorSchedules",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_IsActive",
                table: "Doctors",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IsActive",
                table: "AspNetUsers",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StartTime",
                table: "Appointments",
                column: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_IsActive",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_DayOfWeek",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_StartTime",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_IsActive",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StartTime",
                table: "Appointments");
        }
    }
}
