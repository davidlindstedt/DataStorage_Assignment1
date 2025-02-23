using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
     name: "Projects",
     columns: table => new
     {
         Id = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
         StartDate = table.Column<DateTime>(type: "date", nullable: false),
         EndDate = table.Column<DateTime>(type: "date", nullable: false),
         CustomerId = table.Column<int>(type: "int", nullable: false),
         StatusId = table.Column<int>(type: "int", nullable: false),
         UserId = table.Column<int>(type: "int", nullable: false),
         ProductId = table.Column<int>(type: "int", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Projects", x => new { x.Id, x.CustomerId });
         table.ForeignKey(
             name: "FK_Projects_Customers_CustomerId",
             column: x => x.CustomerId,
             principalTable: "Customers",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey(
             name: "FK_Projects_Users_UserId",
             column: x => x.UserId,
             principalTable: "Users",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey(
             name: "FK_Projects_Products_ProductId",
             column: x => x.ProductId,
             principalTable: "Products",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey(
             name: "FK_Projects_StatusTypes_StatusId",
             column: x => x.StatusId,
             principalTable: "StatusTypes",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
     });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
    name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "StatusTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
