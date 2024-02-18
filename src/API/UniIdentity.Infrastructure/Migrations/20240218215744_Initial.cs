using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniIdentity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Realm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccessTokenLifeSpan = table.Column<int>(type: "integer", nullable: false),
                    SsoMaxLifeSpan = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SslRequirement = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    VerifyEmail = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ClientSecret = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Protocol = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    BaseUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RootUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ManagementUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ClientAuthenticationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RegistrationToken = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AccessType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RealmId = table.Column<string>(type: "character varying(100)", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ConsentRequired = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AuthorizationCodeFlowEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ImplicitFlowEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DirectAccessGrantsEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ClientCredentialsGrantEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_Realm_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealmAttribute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(100)", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealmAttribute", x => new { x.Id, x.Name });
                    table.ForeignKey(
                        name: "FK_RealmAttribute_Realm_Id",
                        column: x => x.Id,
                        principalTable: "Realm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scope",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Protocol = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RealmId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scope_Realm_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    NormalizedUsername = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RealmId = table.Column<string>(type: "character varying(100)", nullable: false),
                    IdentityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Realm_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAttribute",
                columns: table => new
                {
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAttribute", x => new { x.UniqueId, x.Name });
                    table.ForeignKey(
                        name: "FK_ClientAttribute_Client_UniqueId",
                        column: x => x.UniqueId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ClientRealmConstraint = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsClientRole = table.Column<bool>(type: "boolean", nullable: false),
                    RealmId = table.Column<string>(type: "character varying(100)", nullable: false),
                    ClientUniqueId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Client_ClientUniqueId",
                        column: x => x.ClientUniqueId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Role_Realm_RealmId",
                        column: x => x.RealmId,
                        principalTable: "Realm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScope",
                columns: table => new
                {
                    ClientUniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScopeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultScope = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScope", x => new { x.ClientUniqueId, x.ScopeId });
                    table.ForeignKey(
                        name: "FK_ClientScope_Client_ClientUniqueId",
                        column: x => x.ClientUniqueId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientScope_Scope_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScopeAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopeAttribute", x => new { x.Id, x.Name });
                    table.ForeignKey(
                        name: "FK_ScopeAttribute_Scope_Id",
                        column: x => x.Id,
                        principalTable: "Scope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SecretData = table.Column<string>(type: "text", nullable: true),
                    CredentialData = table.Column<string>(type: "text", nullable: true),
                    Priority = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credential_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleGraph",
                columns: table => new
                {
                    ParentRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildRoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGraph", x => new { x.ParentRoleId, x.ChildRoleId });
                    table.ForeignKey(
                        name: "FK_RoleGraph_Role_ChildRoleId",
                        column: x => x.ChildRoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleGraph_Role_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientId",
                table: "Client",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_RealmId_ClientId",
                table: "Client",
                columns: new[] { "RealmId", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientScope_ScopeId",
                table: "ClientScope",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_Credential_UserId",
                table: "Credential",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ClientId",
                table: "Role",
                column: "ClientUniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ClientRealmConstraint_Name",
                table: "Role",
                columns: new[] { "ClientRealmConstraint", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_RealmId",
                table: "Role",
                column: "RealmId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGraph_ChildRoleId",
                table: "RoleGraph",
                column: "ChildRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGraph_ParentRoleId",
                table: "RoleGraph",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_RealmId",
                table: "Scope",
                column: "RealmId");

            migrationBuilder.CreateIndex(
                name: "IX_Scope_RealmId_Name",
                table: "Scope",
                columns: new[] { "RealmId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentityId",
                table: "User",
                column: "IdentityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_NormalizedEmail",
                table: "User",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_NormalizedUsername",
                table: "User",
                column: "NormalizedUsername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RealmId",
                table: "User",
                column: "RealmId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAttribute");

            migrationBuilder.DropTable(
                name: "ClientScope");

            migrationBuilder.DropTable(
                name: "Credential");

            migrationBuilder.DropTable(
                name: "RealmAttribute");

            migrationBuilder.DropTable(
                name: "RoleGraph");

            migrationBuilder.DropTable(
                name: "ScopeAttribute");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Scope");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Realm");
        }
    }
}
