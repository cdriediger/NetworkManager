using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetworkManager.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    nativeVlan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Switches",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ipv4 = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    model = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    modelName = table.Column<string>(nullable: true),
                    lastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Switches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SwitchModels",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    driver = table.Column<string>(nullable: true),
                    portCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwitchModels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Vlans",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    vlanId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    vlan = table.Column<int>(nullable: false),
                    profileid = table.Column<int>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    lastUpdate = table.Column<DateTime>(nullable: false),
                    lldpRemoteDeviceName = table.Column<string>(nullable: true),
                    isUplink = table.Column<bool>(nullable: false),
                    switchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ports_Profiles_profileid",
                        column: x => x.profileid,
                        principalTable: "Profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ports_Switches_switchId",
                        column: x => x.switchId,
                        principalTable: "Switches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaggedVlans",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    vlanId = table.Column<int>(nullable: false),
                    portId = table.Column<int>(nullable: true),
                    profileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaggedVlans", x => x.id);
                    table.ForeignKey(
                        name: "FK_TaggedVlans_Ports_portId",
                        column: x => x.portId,
                        principalTable: "Ports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaggedVlans_Profiles_profileId",
                        column: x => x.profileId,
                        principalTable: "Profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ports_profileid",
                table: "Ports",
                column: "profileid");

            migrationBuilder.CreateIndex(
                name: "IX_Ports_switchId",
                table: "Ports",
                column: "switchId");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedVlans_portId",
                table: "TaggedVlans",
                column: "portId");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedVlans_profileId",
                table: "TaggedVlans",
                column: "profileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "SwitchModels");

            migrationBuilder.DropTable(
                name: "TaggedVlans");

            migrationBuilder.DropTable(
                name: "Vlans");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Switches");
        }
    }
}
