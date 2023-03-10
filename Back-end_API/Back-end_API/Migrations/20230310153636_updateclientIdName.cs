using Microsoft.EntityFrameworkCore.Migrations;

namespace Back_end_API.Migrations
{
    public partial class updateclientIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Clients",
                newName: "ClientID");

            migrationBuilder.AddColumn<int>(
                name: "ClientID",
                table: "Sinistres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sinistres_ClientID",
                table: "Sinistres",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sinistres_Clients_ClientID",
                table: "Sinistres",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sinistres_Clients_ClientID",
                table: "Sinistres");

            migrationBuilder.DropIndex(
                name: "IX_Sinistres_ClientID",
                table: "Sinistres");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Sinistres");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "Clients",
                newName: "ID");
        }
    }
}
