﻿// <auto-generated />
using System;
using System.Collections.Generic;
using LiftLog.Api.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LiftLog.Api.Migrations.RateLimit
{
    [DbContext(typeof(RateLimitContext))]
    [Migration("20240110005744_InitialRateLimitMigration")]
    partial class InitialRateLimitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LiftLog.Api.Models.RateLimitConsumption", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<List<DateTimeOffset>>("Requests")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone[]");

                    b.HasKey("Key");

                    b.ToTable("RateLimitConsumptions");
                });
#pragma warning restore 612, 618
        }
    }
}
