using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GYMIND.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTwoNewColumnsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "gyms",
                columns: table => new
                {
                    GymId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gyms", x => x.GymId);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    LocationID = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.roleid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    passwordhash = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: true),
                    dateofbirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    membershipid = table.Column<Guid>(type: "uuid", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    biography = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    profilepictureurl = table.Column<string>(type: "text", nullable: true),
                    haschangedname = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    medicalconditions = table.Column<string>(type: "text", nullable: true),
                    emergencycontact = table.Column<string>(type: "text", nullable: true),
                    refreshtoken = table.Column<string>(type: "text", nullable: true),
                    refreshtokenexpiry = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "gymbranches",
                columns: table => new
                {
                    GymBranchID = table.Column<Guid>(type: "uuid", nullable: false),
                    GymID = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OperatingHours = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    ServiceDescription = table.Column<string>(type: "text", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gymbranches", x => x.GymBranchID);
                    table.ForeignKey(
                        name: "FK_gymbranches_gyms_GymID",
                        column: x => x.GymID,
                        principalTable: "gyms",
                        principalColumn: "GymId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gymbranches_locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                columns: table => new
                {
                    MembershipID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    GymID = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMember = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    JoinedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberships", x => x.MembershipID);
                    table.ForeignKey(
                        name: "FK_memberships_gyms_GymID",
                        column: x => x.GymID,
                        principalTable: "gyms",
                        principalColumn: "GymId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_memberships_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "systemadminactions",
                columns: table => new
                {
                    systemadminactionid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TargetEntity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TargetID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Outcome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_systemadminactions", x => x.systemadminactionid);
                    table.ForeignKey(
                        name: "FK_systemadminactions_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userrole",
                columns: table => new
                {
                    userroleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    roleid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => x.userroleid);
                    table.ForeignKey(
                        name: "FK_userrole_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "roleid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userrole_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "announcements",
                columns: table => new
                {
                    announcementid = table.Column<Guid>(type: "uuid", nullable: false),
                    GymBranchID = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_announcements", x => x.announcementid);
                    table.ForeignKey(
                        name: "FK_announcements_gymbranches_GymBranchID",
                        column: x => x.GymBranchID,
                        principalTable: "gymbranches",
                        principalColumn: "GymBranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gymadminactions",
                columns: table => new
                {
                    gymadminactionid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    GymBranchID = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TargetEntity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TargetID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Outcome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gymadminactions", x => x.gymadminactionid);
                    table.ForeignKey(
                        name: "FK_gymadminactions_gymbranches_GymBranchID",
                        column: x => x.GymBranchID,
                        principalTable: "gymbranches",
                        principalColumn: "GymBranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gymadminactions_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gymsessions",
                columns: table => new
                {
                    GymSessionID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    GymBranchID = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SessionDuration = table.Column<int>(type: "integer", nullable: true),
                    CheckInLat = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    CheckInLong = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    IsVerifiedLocation = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gymsessions", x => x.GymSessionID);
                    table.ForeignKey(
                        name: "FK_gymsessions_gymbranches_GymBranchID",
                        column: x => x.GymBranchID,
                        principalTable: "gymbranches",
                        principalColumn: "GymBranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gymsessions_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    notificationid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: true),
                    GymBranchID = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.notificationid);
                    table.ForeignKey(
                        name: "FK_notifications_gymbranches_GymBranchID",
                        column: x => x.GymBranchID,
                        principalTable: "gymbranches",
                        principalColumn: "GymBranchID");
                    table.ForeignKey(
                        name: "FK_notifications_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "traffictrack",
                columns: table => new
                {
                    TrafficTrackID = table.Column<Guid>(type: "uuid", nullable: false),
                    GymBranchID = table.Column<Guid>(type: "uuid", nullable: false),
                    TrafficTimestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CapacityPercentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    EntryCount = table.Column<int>(type: "integer", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traffictrack", x => x.TrafficTrackID);
                    table.ForeignKey(
                        name: "FK_traffictrack_gymbranches_GymBranchID",
                        column: x => x.GymBranchID,
                        principalTable: "gymbranches",
                        principalColumn: "GymBranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usernotification",
                columns: table => new
                {
                    usernotificationid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    notificationid = table.Column<Guid>(type: "uuid", nullable: false),
                    readstatus = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    readat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usernotification", x => x.usernotificationid);
                    table.ForeignKey(
                        name: "FK_usernotification_notifications_notificationid",
                        column: x => x.notificationid,
                        principalTable: "notifications",
                        principalColumn: "notificationid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usernotification_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_announcements_GymBranchID",
                table: "announcements",
                column: "GymBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_gymadminactions_GymBranchID",
                table: "gymadminactions",
                column: "GymBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_gymadminactions_UserID",
                table: "gymadminactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_gymbranches_GymID",
                table: "gymbranches",
                column: "GymID");

            migrationBuilder.CreateIndex(
                name: "IX_gymbranches_LocationID",
                table: "gymbranches",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_gymsessions_GymBranchID",
                table: "gymsessions",
                column: "GymBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_gymsessions_UserID",
                table: "gymsessions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_memberships_GymID",
                table: "memberships",
                column: "GymID");

            migrationBuilder.CreateIndex(
                name: "IX_memberships_UserID",
                table: "memberships",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_GymBranchID",
                table: "notifications",
                column: "GymBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserID",
                table: "notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_systemadminactions_UserID",
                table: "systemadminactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_traffictrack_GymBranchID",
                table: "traffictrack",
                column: "GymBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_usernotification_notificationid",
                table: "usernotification",
                column: "notificationid");

            migrationBuilder.CreateIndex(
                name: "IX_usernotification_userid_notificationid",
                table: "usernotification",
                columns: new[] { "userid", "notificationid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userrole_roleid",
                table: "userrole",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_userrole_userid_roleid",
                table: "userrole",
                columns: new[] { "userid", "roleid" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "announcements");

            migrationBuilder.DropTable(
                name: "gymadminactions");

            migrationBuilder.DropTable(
                name: "gymsessions");

            migrationBuilder.DropTable(
                name: "memberships");

            migrationBuilder.DropTable(
                name: "systemadminactions");

            migrationBuilder.DropTable(
                name: "traffictrack");

            migrationBuilder.DropTable(
                name: "usernotification");

            migrationBuilder.DropTable(
                name: "userrole");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "gymbranches");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "gyms");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
