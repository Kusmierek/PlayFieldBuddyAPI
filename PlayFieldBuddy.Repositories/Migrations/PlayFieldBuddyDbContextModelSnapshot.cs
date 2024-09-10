﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PlayFieldBuddy.Repositories;

#nullable disable

namespace PlayFieldBuddy.Repositories.Migrations
{
    [DbContext(typeof(PlayFieldBuddyDbContext))]
    partial class PlayFieldBuddyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameUser", b =>
                {
                    b.Property<Guid>("JoinedGamesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("JoinedGamesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GameUser");
                });

            modelBuilder.Entity("PlayFieldBuddy.Domain.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PitchId")
                        .HasColumnType("uuid");

                    b.Property<int>("PlayersLimit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PitchId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PlayFieldBuddy.Domain.Models.Pitch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PitchType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Pitches");
                });

            modelBuilder.Entity("PlayFieldBuddy.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameUser", b =>
                {
                    b.HasOne("PlayFieldBuddy.Domain.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("JoinedGamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlayFieldBuddy.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlayFieldBuddy.Domain.Models.Game", b =>
                {
                    b.HasOne("PlayFieldBuddy.Domain.Models.User", "Owner")
                        .WithMany("OwnedGames")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlayFieldBuddy.Domain.Models.Pitch", "Pitch")
                        .WithMany("Games")
                        .HasForeignKey("PitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Pitch");
                });

            modelBuilder.Entity("PlayFieldBuddy.Domain.Models.Pitch", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("PlayFieldBuddy.Domain.Models.User", b =>
                {
                    b.Navigation("OwnedGames");
                });
#pragma warning restore 612, 618
        }
    }
}
