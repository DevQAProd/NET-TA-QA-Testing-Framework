using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationName.Databases.MySql.AppDatabase.YahooFinanceSchema.Migrations
{
    public partial class DaxPerformanceIndexTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dax_performance_index_daily",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    AdjClose = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dax_performance_index_daily", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dax_performance_index_monthly",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    AdjClose = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dax_performance_index_monthly", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dax_performance_index_weekly",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    AdjClose = table.Column<decimal>(type: "decimal(19,9)", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dax_performance_index_weekly", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dax_performance_index_daily");

            migrationBuilder.DropTable(
                name: "dax_performance_index_monthly");

            migrationBuilder.DropTable(
                name: "dax_performance_index_weekly");
        }
    }
}
