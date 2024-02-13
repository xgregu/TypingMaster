﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypingMaster.Database;

#nullable disable

namespace TypingMaster.Database.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingLevelEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("DifficultyCoefficient")
                        .HasColumnType("REAL");

                    b.Property<uint>("DifficultyLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TypingLevels");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyCoefficient = 0.80000000000000004,
                            DifficultyLevel = 1u,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Minimalistyczny"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyCoefficient = 0.90000000000000002,
                            DifficultyLevel = 2u,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Krótki"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyCoefficient = 1.0,
                            DifficultyLevel = 3u,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Standardowy"
                        },
                        new
                        {
                            Id = 4L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyCoefficient = 1.1000000000000001,
                            DifficultyLevel = 4u,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Długi"
                        },
                        new
                        {
                            Id = 5L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyCoefficient = 1.2,
                            DifficultyLevel = 5u,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "Bardzo długi"
                        });
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTestEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExecutorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<long>("StatisticsId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TextId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StatisticsId")
                        .IsUnique();

                    b.HasIndex("TextId");

                    b.ToTable("TypingTests");
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTestStatisticsEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ClickPerMinute")
                        .HasColumnType("REAL");

                    b.Property<long>("CompletionTimeSecond")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("EffectivenessPercentage")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("MistakesClicks")
                        .HasColumnType("INTEGER");

                    b.Property<long>("OverallRating")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TotalClicks")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TypingTestStatistics");
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTextEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("DifficultyLevelId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyLevelId");

                    b.ToTable("TypingTexts");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 1L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Przysłowiowy kot."
                        },
                        new
                        {
                            Id = 2L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 1L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Miłość jest ślepa."
                        },
                        new
                        {
                            Id = 3L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 1L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Szczęśliwe zakończenie."
                        },
                        new
                        {
                            Id = 4L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 1L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Skok w dal."
                        },
                        new
                        {
                            Id = 5L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 1L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Cisza przed burzą."
                        },
                        new
                        {
                            Id = 6L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 2L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nie bądź bierny, działaj samodzielnie."
                        },
                        new
                        {
                            Id = 7L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 2L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Zawsze powtarzaj pozytywne myśli."
                        },
                        new
                        {
                            Id = 8L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 2L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Szanuj ludzi, którzy cię otaczają."
                        },
                        new
                        {
                            Id = 9L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 2L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Zdrowie to nasz największy skarb."
                        },
                        new
                        {
                            Id = 10L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 2L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Warto poświęcić czas na rozwój osobisty."
                        },
                        new
                        {
                            Id = 11L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 3L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Szukaj motywacji w każdym dniu, aby realizować swoje cele."
                        },
                        new
                        {
                            Id = 12L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 3L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nie bój się prosić o pomoc, to oznaka siły, a nie słabości."
                        },
                        new
                        {
                            Id = 13L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 3L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Słuchaj uważnie i bądź otwarty na różne punkty widzenia."
                        },
                        new
                        {
                            Id = 14L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 3L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Wszyscy popełniamy błędy, ale to nasze doświadczenia uczą nas mądrości."
                        },
                        new
                        {
                            Id = 15L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 3L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nie pozwól, aby przeszłość definiowała twoją przyszłość."
                        },
                        new
                        {
                            Id = 16L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 4L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Czemuż nie ma jeszcze pizzy? Czekam już godzinę i nic. Może jednak zamówić coś innego?"
                        },
                        new
                        {
                            Id = 17L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 4L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Właśnie skończyłem pisać swoją nową powieść. Przeczytaj ją, jeśli lubisz literaturę science fiction"
                        },
                        new
                        {
                            Id = 18L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 4L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Mam dobre wieści! Otrzymałem dzisiaj awans w pracy. Oto nagroda za ciężką pracę i poświęcenie."
                        },
                        new
                        {
                            Id = 19L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 4L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Czuję się tak zmęczony, że chciałbym tylko leżeć w łóżku i odpocząć przez resztę dnia. A może warto zrobić sobie krótką drzemkę?"
                        },
                        new
                        {
                            Id = 20L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 4L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Zaczynam kurs programowania od podstaw i jestem pod wrażeniem, jak dużo można się nauczyć w tak krótkim czasie. To prawdziwie fascynujące."
                        },
                        new
                        {
                            Id = 21L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 5L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Cześć! Jak się masz? Dziś jest piękny dzień, pełen słońca i pozytywnej energii. Co u Ciebie słychać? Mam nadzieję, że wszystko idzie po Twojej myśli i że masz wspaniały dzień!"
                        },
                        new
                        {
                            Id = 22L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 5L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "W dzisiejszych czasach ludzie coraz częściej szukają sposobów na poprawę swojego zdrowia i samopoczucia. Od jogi po zdrowe odżywianie, możliwości jest wiele!"
                        },
                        new
                        {
                            Id = 23L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 5L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Czasem warto zrobić sobie przerwę od codzienności i pozwolić sobie na chwilę relaksu. Może to być kąpiel z olejkami eterycznymi lub leniwe popołudnie z książką w ręku."
                        },
                        new
                        {
                            Id = 24L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 5L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Kreatywność to nie tylko talent, ale także umiejętność rozwijania swoich pomysłów i poszukiwania nowych rozwiązań. Każdy może się jej nauczyć!"
                        },
                        new
                        {
                            Id = 25L,
                            CreatedDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DifficultyLevelId = 5L,
                            LastChangeDate = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nauka nowego języka to świetny sposób na rozwijanie swoich umiejętności i poznawanie nowych kultur. W dzisiejszych czasach jest to łatwiejsze niż kiedykolwiek wcześniej, dzięki szerokiemu dostępowi do materiałów edukacyjnych i narzędzi online."
                        });
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTestEntity", b =>
                {
                    b.HasOne("TypingMaster.Domain.Entities.TypingTestStatisticsEntity", "Statistics")
                        .WithOne("TypingTest")
                        .HasForeignKey("TypingMaster.Domain.Entities.TypingTestEntity", "StatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypingMaster.Domain.Entities.TypingTextEntity", "Text")
                        .WithMany("Tests")
                        .HasForeignKey("TextId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Statistics");

                    b.Navigation("Text");
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTextEntity", b =>
                {
                    b.HasOne("TypingMaster.Domain.Entities.TypingLevelEntity", "DifficultyLevel")
                        .WithMany("TypingTexts")
                        .HasForeignKey("DifficultyLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DifficultyLevel");
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingLevelEntity", b =>
                {
                    b.Navigation("TypingTexts");
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTestStatisticsEntity", b =>
                {
                    b.Navigation("TypingTest")
                        .IsRequired();
                });

            modelBuilder.Entity("TypingMaster.Domain.Entities.TypingTextEntity", b =>
                {
                    b.Navigation("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}
