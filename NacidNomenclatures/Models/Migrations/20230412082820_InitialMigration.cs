using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    version = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "district",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code2 = table.Column<string>(type: "text", nullable: true),
                    secondlevelregioncode = table.Column<string>(type: "text", nullable: true),
                    mainsettlementcode = table.Column<string>(type: "text", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "municipality",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    districtid = table.Column<int>(type: "integer", nullable: false),
                    code2 = table.Column<string>(type: "text", nullable: true),
                    mainsettlementcode = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipality", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipality_district_districtid",
                        column: x => x.districtid,
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "settlement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    municipalityid = table.Column<int>(type: "integer", nullable: false),
                    districtid = table.Column<int>(type: "integer", nullable: false),
                    municipalitycode = table.Column<string>(type: "text", nullable: true),
                    districtcode = table.Column<string>(type: "text", nullable: true),
                    municipalitycode2 = table.Column<string>(type: "text", nullable: true),
                    districtcode2 = table.Column<string>(type: "text", nullable: true),
                    typename = table.Column<string>(type: "text", nullable: true),
                    settlementname = table.Column<string>(type: "text", nullable: true),
                    typecode = table.Column<string>(type: "text", nullable: true),
                    mayoraltycode = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    altitude = table.Column<string>(type: "text", nullable: true),
                    isdistrict = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settlement", x => x.id);
                    table.ForeignKey(
                        name: "FK_settlement_district_districtid",
                        column: x => x.districtid,
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_settlement_municipality_municipalityid",
                        column: x => x.municipalityid,
                        principalTable: "municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "institution",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lotnumber = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: true),
                    uic = table.Column<string>(type: "text", nullable: true),
                    shortname = table.Column<string>(type: "text", nullable: true),
                    shortnamealt = table.Column<string>(type: "text", nullable: true),
                    organizationtype = table.Column<int>(type: "integer", nullable: true),
                    ownershiptype = table.Column<int>(type: "integer", nullable: true),
                    settlementid = table.Column<int>(type: "integer", nullable: true),
                    municipalityid = table.Column<int>(type: "integer", nullable: true),
                    districtid = table.Column<int>(type: "integer", nullable: true),
                    isresearchuniversity = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    namealt = table.Column<string>(type: "text", nullable: true),
                    alias = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    vieworder = table.Column<int>(type: "integer", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    rootid = table.Column<int>(type: "integer", nullable: false),
                    parentid = table.Column<int>(type: "integer", nullable: true),
                    level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institution", x => x.id);
                    table.ForeignKey(
                        name: "FK_institution_district_districtid",
                        column: x => x.districtid,
                        principalTable: "district",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_institution_institution_parentid",
                        column: x => x.parentid,
                        principalTable: "institution",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_institution_institution_rootid",
                        column: x => x.rootid,
                        principalTable: "institution",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_institution_municipality_municipalityid",
                        column: x => x.municipalityid,
                        principalTable: "municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_institution_settlement_settlementid",
                        column: x => x.settlementid,
                        principalTable: "settlement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_institution_districtid",
                table: "institution",
                column: "districtid");

            migrationBuilder.CreateIndex(
                name: "IX_institution_municipalityid",
                table: "institution",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "IX_institution_parentid",
                table: "institution",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "IX_institution_rootid",
                table: "institution",
                column: "rootid");

            migrationBuilder.CreateIndex(
                name: "IX_institution_settlementid",
                table: "institution",
                column: "settlementid");

            migrationBuilder.CreateIndex(
                name: "IX_municipality_districtid",
                table: "municipality",
                column: "districtid");

            migrationBuilder.CreateIndex(
                name: "IX_settlement_districtid",
                table: "settlement",
                column: "districtid");

            migrationBuilder.CreateIndex(
                name: "IX_settlement_municipalityid",
                table: "settlement",
                column: "municipalityid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "institution");

            migrationBuilder.DropTable(
                name: "settlement");

            migrationBuilder.DropTable(
                name: "municipality");

            migrationBuilder.DropTable(
                name: "district");
        }
    }
}
