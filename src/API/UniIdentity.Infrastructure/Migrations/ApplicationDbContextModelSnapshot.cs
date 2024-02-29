﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniIdentity.Infrastructure.Data;

#nullable disable

namespace UniIdentity.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniIdentity.Domain.Clients.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AccessType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("AuthorizationCodeFlowEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("BaseUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ClientAuthenticationType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("ClientCredentialsGrantEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("ClientKey")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ClientSecret")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("ConsentRequired")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("DirectAccessGrantsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("Enabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("ImplicitFlowEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("ManagementUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Protocol")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("RealmId")
                        .IsRequired()
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RegistrationToken")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("RootUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("ClientKey")
                        .HasDatabaseName("IX_Client_ClientId");

                    b.HasIndex("RealmId", "ClientKey")
                        .IsUnique()
                        .HasDatabaseName("IX_Client_RealmId_ClientId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("UniIdentity.Domain.Clients.ClientAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.HasKey("Id", "Name");

                    b.ToTable("ClientAttribute");
                });

            modelBuilder.Entity("UniIdentity.Domain.Clients.ClientScope", b =>
                {
                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScopeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("DefaultScope")
                        .HasColumnType("boolean");

                    b.HasKey("ClientId", "ScopeId");

                    b.HasIndex("ScopeId");

                    b.ToTable("ClientScope");
                });

            modelBuilder.Entity("UniIdentity.Domain.Configs.Config", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("ProviderId")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("RealmId")
                        .IsRequired()
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("RealmId", "Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Config_RealmId_Name");

                    b.ToTable("Config");
                });

            modelBuilder.Entity("UniIdentity.Domain.Configs.ConfigAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConfigId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigId");

                    b.ToTable("ConfigAttribute");
                });

            modelBuilder.Entity("UniIdentity.Domain.Credentials.Credential", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CredentialData")
                        .HasColumnType("text");

                    b.Property<short>("Priority")
                        .HasColumnType("smallint");

                    b.Property<string>("SecretData")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_Credential_UserId");

                    b.ToTable("Credential");

                    b.HasDiscriminator<string>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("UniIdentity.Domain.Realms.Realm", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("AccessTokenLifeSpan")
                        .HasColumnType("integer");

                    b.Property<bool>("Enabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SslRequirement")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("SsoMaxLifeSpan")
                        .HasColumnType("integer");

                    b.Property<bool>("VerifyEmail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Realms.RealmAttribute", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.HasKey("Id", "Name");

                    b.ToTable("RealmAttribute");
                });

            modelBuilder.Entity("UniIdentity.Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientRealmConstraint")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsClientRole")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RealmId")
                        .IsRequired()
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .HasDatabaseName("IX_Role_ClientId");

                    b.HasIndex("RealmId")
                        .HasDatabaseName("IX_Role_RealmId");

                    b.HasIndex("ClientRealmConstraint", "Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Role_ClientRealmConstraint_Name");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("UniIdentity.Domain.Roles.RoleGraph", b =>
                {
                    b.Property<Guid>("ParentRoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChildRoleId")
                        .HasColumnType("uuid");

                    b.HasKey("ParentRoleId", "ChildRoleId");

                    b.HasIndex("ChildRoleId")
                        .HasDatabaseName("IX_RoleGraph_ChildRoleId");

                    b.HasIndex("ParentRoleId")
                        .HasDatabaseName("IX_RoleGraph_ParentRoleId");

                    b.ToTable("RoleGraph");
                });

            modelBuilder.Entity("UniIdentity.Domain.Scopes.Scope", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Protocol")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("RealmId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("RealmId")
                        .HasDatabaseName("IX_Scope_RealmId");

                    b.HasIndex("RealmId", "Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Scope_RealmId_Name");

                    b.ToTable("Scope");
                });

            modelBuilder.Entity("UniIdentity.Domain.Scopes.ScopeAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.HasKey("Id", "Name");

                    b.ToTable("ScopeAttribute");
                });

            modelBuilder.Entity("UniIdentity.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("EmailVerified")
                        .HasColumnType("boolean");

                    b.Property<Guid>("IdentityId")
                        .HasColumnType("uuid");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("NormalizedUsername")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("RealmId")
                        .IsRequired()
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("IX_User_IdentityId");

                    b.HasIndex("NormalizedEmail")
                        .IsUnique()
                        .HasDatabaseName("IX_User_NormalizedEmail");

                    b.HasIndex("NormalizedUsername")
                        .IsUnique()
                        .HasDatabaseName("IX_User_NormalizedUsername");

                    b.HasIndex("RealmId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("UniIdentity.Domain.Users.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("UniIdentity.Domain.Credentials.PasswordCredential", b =>
                {
                    b.HasBaseType("UniIdentity.Domain.Credentials.Credential");

                    b.HasDiscriminator().HasValue("password");
                });

            modelBuilder.Entity("UniIdentity.Domain.Clients.Client", b =>
                {
                    b.HasOne("UniIdentity.Domain.Realms.Realm", "Realm")
                        .WithMany("Clients")
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Clients.ClientAttribute", b =>
                {
                    b.HasOne("UniIdentity.Domain.Clients.Client", "Client")
                        .WithMany("ClientAttributes")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("UniIdentity.Domain.Clients.ClientScope", b =>
                {
                    b.HasOne("UniIdentity.Domain.Clients.Client", "Client")
                        .WithMany("ClientScopes")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniIdentity.Domain.Scopes.Scope", "Scope")
                        .WithMany("ClientScopes")
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("UniIdentity.Domain.Configs.Config", b =>
                {
                    b.HasOne("UniIdentity.Domain.Realms.Realm", "Realm")
                        .WithMany()
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Configs.ConfigAttribute", b =>
                {
                    b.HasOne("UniIdentity.Domain.Configs.Config", "Config")
                        .WithMany("ConfigAttributes")
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Config");
                });

            modelBuilder.Entity("UniIdentity.Domain.Credentials.Credential", b =>
                {
                    b.HasOne("UniIdentity.Domain.Users.User", "User")
                        .WithMany("Credentials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UniIdentity.Domain.Realms.RealmAttribute", b =>
                {
                    b.HasOne("UniIdentity.Domain.Realms.Realm", "Realm")
                        .WithMany("RealmAttributes")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Roles.Role", b =>
                {
                    b.HasOne("UniIdentity.Domain.Clients.Client", "Client")
                        .WithMany("Roles")
                        .HasForeignKey("ClientId");

                    b.HasOne("UniIdentity.Domain.Realms.Realm", "Realm")
                        .WithMany("Roles")
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Roles.RoleGraph", b =>
                {
                    b.HasOne("UniIdentity.Domain.Roles.Role", "ChildRole")
                        .WithMany("ChildRoles")
                        .HasForeignKey("ChildRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniIdentity.Domain.Roles.Role", "ParentRole")
                        .WithMany("ParentRoles")
                        .HasForeignKey("ParentRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildRole");

                    b.Navigation("ParentRole");
                });

            modelBuilder.Entity("UniIdentity.Domain.Scopes.Scope", b =>
                {
                    b.HasOne("UniIdentity.Domain.Realms.Realm", "Realm")
                        .WithMany()
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Scopes.ScopeAttribute", b =>
                {
                    b.HasOne("UniIdentity.Domain.Scopes.Scope", "Scope")
                        .WithMany("ScopeAttributes")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("UniIdentity.Domain.Users.User", b =>
                {
                    b.HasOne("UniIdentity.Domain.Realms.Realm", "Realm")
                        .WithMany("Users")
                        .HasForeignKey("RealmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Realm");
                });

            modelBuilder.Entity("UniIdentity.Domain.Users.UserRole", b =>
                {
                    b.HasOne("UniIdentity.Domain.Roles.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniIdentity.Domain.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UniIdentity.Domain.Clients.Client", b =>
                {
                    b.Navigation("ClientAttributes");

                    b.Navigation("ClientScopes");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("UniIdentity.Domain.Configs.Config", b =>
                {
                    b.Navigation("ConfigAttributes");
                });

            modelBuilder.Entity("UniIdentity.Domain.Realms.Realm", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("RealmAttributes");

                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("UniIdentity.Domain.Roles.Role", b =>
                {
                    b.Navigation("ChildRoles");

                    b.Navigation("ParentRoles");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("UniIdentity.Domain.Scopes.Scope", b =>
                {
                    b.Navigation("ClientScopes");

                    b.Navigation("ScopeAttributes");
                });

            modelBuilder.Entity("UniIdentity.Domain.Users.User", b =>
                {
                    b.Navigation("Credentials");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
