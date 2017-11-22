using Microsoft.EntityFrameworkCore.Migrations;

namespace _01.HospitalDatabase.Migrations
{
    public partial class FixDoctorPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Doctor",
                newName: "Specialty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Doctor",
                newName: "Speciality");
        }
    }
}