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
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

            modelBuilder.Entity("UniIdentity.Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("IX_Role_NormalizedName");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8426249a-a917-45e8-b8bb-43a551a884ed"),
                            Name = "DefaultUser",
                            NormalizedName = "DEFAULTUSER"
                        },
                        new
                        {
                            Id = new Guid("35c029cb-f156-4787-9d24-d63951956e3e"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
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

            modelBuilder.Entity("UniIdentity.Domain.Credentials.Credential", b =>
                {
                    b.HasOne("UniIdentity.Domain.Users.User", "User")
                        .WithMany("Credentials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("UniIdentity.Domain.Realms.Realm", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("UniIdentity.Domain.Roles.Role", b =>
                {
                    b.Navigation("UserRoles");
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
