using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Add_relasi_dan_kardilanitas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bokings_employee_guid",
                table: "tb_tr_bokings",
                column: "employee_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_bokings_room_guid",
                table: "tb_tr_bokings",
                column: "room_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations",
                column: "university_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_account_guid",
                table: "tb_m_account_roles",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_role_guid",
                table: "tb_m_account_roles",
                column: "role_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_account_guid",
                table: "tb_m_account_roles",
                column: "account_guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_roles_tb_m_roles_role_guid",
                table: "tb_m_account_roles",
                column: "role_guid",
                principalTable: "tb_m_roles",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations",
                column: "guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_educations_tb_m_universities_university_guid",
                table: "tb_m_educations",
                column: "university_guid",
                principalTable: "tb_m_universities",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employees_tb_m_accounts_guid",
                table: "tb_m_employees",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bokings_tb_m_employees_employee_guid",
                table: "tb_tr_bokings",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bokings_tb_m_rooms_room_guid",
                table: "tb_tr_bokings",
                column: "room_guid",
                principalTable: "tb_m_rooms",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_roles_tb_m_accounts_account_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_roles_tb_m_roles_role_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_employees_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_educations_tb_m_universities_university_guid",
                table: "tb_m_educations");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employees_tb_m_accounts_guid",
                table: "tb_m_employees");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bokings_tb_m_employees_employee_guid",
                table: "tb_tr_bokings");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bokings_tb_m_rooms_room_guid",
                table: "tb_tr_bokings");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_bokings_employee_guid",
                table: "tb_tr_bokings");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_bokings_room_guid",
                table: "tb_tr_bokings");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_educations_university_guid",
                table: "tb_m_educations");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_account_roles_account_guid",
                table: "tb_m_account_roles");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_account_roles_role_guid",
                table: "tb_m_account_roles");
        }
    }
}
