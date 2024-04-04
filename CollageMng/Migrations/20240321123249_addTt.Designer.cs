﻿// <auto-generated />
using CollageMng.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollageMng.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240321123249_addTt")]
    partial class addTt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CollageMng.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("CollageMng.Models.Icmarks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("S1")
                        .HasColumnType("int");

                    b.Property<int>("S2")
                        .HasColumnType("int");

                    b.Property<int>("S3")
                        .HasColumnType("int");

                    b.Property<int>("S4")
                        .HasColumnType("int");

                    b.Property<int>("S5")
                        .HasColumnType("int");

                    b.Property<int>("Stuno")
                        .HasColumnType("int");

                    b.Property<int>("sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Icmarks");
                });

            modelBuilder.Entity("CollageMng.Models.ImbaCource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("code1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ImbaCource");
                });

            modelBuilder.Entity("CollageMng.Models.ImcaCource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("code1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ImcaCource");
                });

            modelBuilder.Entity("CollageMng.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Srno")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Login_Data");
                });

            modelBuilder.Entity("CollageMng.Models.Mammarks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("S1")
                        .HasColumnType("int");

                    b.Property<int>("S2")
                        .HasColumnType("int");

                    b.Property<int>("S3")
                        .HasColumnType("int");

                    b.Property<int>("S4")
                        .HasColumnType("int");

                    b.Property<int>("S5")
                        .HasColumnType("int");

                    b.Property<int>("S6")
                        .HasColumnType("int");

                    b.Property<int>("S7")
                        .HasColumnType("int");

                    b.Property<int>("Stuno")
                        .HasColumnType("int");

                    b.Property<int>("sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Mammarks");
                });

            modelBuilder.Entity("CollageMng.Models.MbaCource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("code1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MbaCource");
                });

            modelBuilder.Entity("CollageMng.Models.McaCource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("code1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("McaCources");
                });

            modelBuilder.Entity("CollageMng.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Msg2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sem")
                        .HasColumnType("int");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("CollageMng.Models.Syllabus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sem")
                        .HasColumnType("int");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Syllabus");
                });

            modelBuilder.Entity("CollageMng.Models.Timetable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Timetable");
                });

            modelBuilder.Entity("CollageMng.Models.Tt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lec5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tt");
                });
#pragma warning restore 612, 618
        }
    }
}
