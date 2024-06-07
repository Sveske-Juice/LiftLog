﻿// <auto-generated />
using System;
using LiftLog.Api.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LiftLog.Api.Migrations
{
    [DbContext(typeof(UserDataContext))]
    [Migration("20240606085611_RequiredRsaPublicKey")]
    partial class RequiredRsaPublicKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence<int>("user_lookup_sequence");

            modelBuilder.Entity("LiftLog.Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<byte[]>("EncryptedCurrentPlan")
                        .HasColumnType("bytea")
                        .HasColumnName("encrypted_current_plan");

                    b.Property<byte[]>("EncryptedName")
                        .HasColumnType("bytea")
                        .HasColumnName("encrypted_name");

                    b.Property<byte[]>("EncryptedProfilePicture")
                        .HasColumnType("bytea")
                        .HasColumnName("encrypted_profile_picture");

                    b.Property<byte[]>("EncryptionIV")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("encryption_iv");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<DateTimeOffset>("LastAccessed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_accessed");

                    b.Property<byte[]>("RsaPublicKey")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("rsa_public_key");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.Property<int>("UserLookup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_lookup")
                        .HasDefaultValueSql("nextval('user_lookup_sequence')");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("UserLookup")
                        .IsUnique()
                        .HasDatabaseName("ix_users_user_lookup");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserEvent", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<byte[]>("EncryptedEvent")
                        .IsRequired()
                        .HasMaxLength(5120)
                        .HasColumnType("bytea")
                        .HasColumnName("encrypted_event");

                    b.Property<byte[]>("EncryptionIV")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("encryption_iv");

                    b.Property<DateTimeOffset>("Expiry")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiry");

                    b.Property<DateTimeOffset>("LastAccessed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_accessed");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_user_events");

                    b.HasIndex("Expiry")
                        .HasDatabaseName("ix_user_events_expiry");

                    b.ToTable("user_events", (string)null);
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserEventFilter", b =>
                {
                    b.Property<DateTimeOffset>("Since")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("since");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.ToTable("tmp_stub_table", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserFollowSecret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_user_follow_secrets");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_follow_secrets_user_id");

                    b.ToTable("user_follow_secrets", (string)null);
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserInboxItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<byte[][]>("EncryptedMessage")
                        .IsRequired()
                        .HasColumnType("bytea[]")
                        .HasColumnName("encrypted_message");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_inbox_items");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_inbox_items_user_id");

                    b.ToTable("user_inbox_items", (string)null);
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserEvent", b =>
                {
                    b.HasOne("LiftLog.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_events_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserFollowSecret", b =>
                {
                    b.HasOne("LiftLog.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_follow_secrets_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LiftLog.Api.Models.UserInboxItem", b =>
                {
                    b.HasOne("LiftLog.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_inbox_items_users_user_id");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}