using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationName.Databases.MySql.AppDatabase.YahooFinanceSchema.Migrations
{
    public partial class NasdaqCompositeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nasdaq_composite_daily",
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
                    table.PrimaryKey("PK_nasdaq_composite_daily", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "nasdaq_composite_monthly",
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
                    table.PrimaryKey("PK_nasdaq_composite_monthly", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "nasdaq_composite_weekly",
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
                    table.PrimaryKey("PK_nasdaq_composite_weekly", x => x.Date);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nasdaq_composite_daily");

            migrationBuilder.DropTable(
                name: "nasdaq_composite_monthly");

            migrationBuilder.DropTable(
                name: "nasdaq_composite_weekly");
        }
    }
}
