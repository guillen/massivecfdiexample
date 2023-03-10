using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cfdi.API.Migrations
{
    public partial class AddCfdiTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CfdiHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poliza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inciso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endoso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgenteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicioEmisionPoliza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFinEmisionPoliza = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CfdiHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CfdiHistories");
        }
    }
}
