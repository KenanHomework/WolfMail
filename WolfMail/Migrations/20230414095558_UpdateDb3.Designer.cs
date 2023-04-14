﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WolfMail.Data;

#nullable disable

namespace WolfMail.Migrations
{
    [DbContext(typeof(WolfMailContext))]
    [Migration("20230414095558_UpdateDb3")]
    partial class UpdateDb3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.15");

            modelBuilder.Entity("WolfMail.Models.DataModels.MailGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MailGroups");
                });

            modelBuilder.Entity("WolfMail.Models.DataModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WolfMail.Models.DataModels.WolfMailAddress", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MailGroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("MailGroupId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("MailAddresses");
                });

            modelBuilder.Entity("WolfMail.Models.DataModels.MailGroup", b =>
                {
                    b.HasOne("WolfMail.Models.DataModels.User", "User")
                        .WithMany("MailGroups")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WolfMail.Models.DataModels.WolfMailAddress", b =>
                {
                    b.HasOne("WolfMail.Models.DataModels.MailGroup", null)
                        .WithMany("Mails")
                        .HasForeignKey("MailGroupId");

                    b.HasOne("WolfMail.Models.DataModels.User", "User")
                        .WithOne("MailAddress")
                        .HasForeignKey("WolfMail.Models.DataModels.WolfMailAddress", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("WolfMail.Models.DataModels.MailGroup", b =>
                {
                    b.Navigation("Mails");
                });

            modelBuilder.Entity("WolfMail.Models.DataModels.User", b =>
                {
                    b.Navigation("MailAddress")
                        .IsRequired();

                    b.Navigation("MailGroups");
                });
#pragma warning restore 612, 618
        }
    }
}