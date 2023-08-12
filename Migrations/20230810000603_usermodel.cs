using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASPAPI.Migrations
{
    /// <inheritdoc />
    public partial class usermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    HitPoints = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    Intelligence = table.Column<int>(type: "integer", nullable: false),
                    Roles = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    TaxCode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Refreshtoken",
                columns: table => new
                {
                    userid = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    tokenid = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    refreshtoken = table.Column<string>(type: "character varying(9999999)", unicode: false, maxLength: 9999999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Refreshtoken_pkey", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    role = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Refreshtoken");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
