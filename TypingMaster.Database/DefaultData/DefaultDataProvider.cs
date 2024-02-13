using System.Collections.Generic;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.DefaultData;

internal static class DefaultDataProvider
{
    internal static IEnumerable<TypingLevelEntity> GetTypingLevels() =>
        new List<TypingLevelEntity>
        {
            new() {Id = 1, DifficultyLevel = 1, Name = "Minimalistyczny", DifficultyCoefficient = 0.8},
            new() {Id = 2, DifficultyLevel = 2, Name = "Krótki", DifficultyCoefficient = 0.9},
            new() {Id = 3, DifficultyLevel = 3, Name = "Standardowy", DifficultyCoefficient = 1.0},
            new() {Id = 4, DifficultyLevel = 4, Name = "Długi", DifficultyCoefficient = 1.1},
            new() {Id = 5, DifficultyLevel = 5, Name = "Bardzo długi", DifficultyCoefficient = 1.2},
        };

    internal static IEnumerable<TypingTextEntity> GetTypingTexts() =>
        new List<TypingTextEntity>
        {
            
            new() {Id = 1, DifficultyLevelId = 1, Text = "Przysłowiowy kot." },
            new() {Id = 2, DifficultyLevelId = 1, Text = "Miłość jest ślepa." },
            new() {Id = 3, DifficultyLevelId = 1, Text = "Szczęśliwe zakończenie." },
            new() {Id = 4, DifficultyLevelId = 1, Text = "Skok w dal." },
            new() {Id = 5, DifficultyLevelId = 1, Text = "Cisza przed burzą." },
            new() {Id = 6, DifficultyLevelId = 2, Text = "Nie bądź bierny, działaj samodzielnie." },
            new() {Id = 7, DifficultyLevelId = 2, Text = "Zawsze powtarzaj pozytywne myśli." },
            new() {Id = 8, DifficultyLevelId = 2, Text = "Szanuj ludzi, którzy cię otaczają." },
            new() {Id = 9, DifficultyLevelId = 2, Text = "Zdrowie to nasz największy skarb." },
            new() {Id = 10, DifficultyLevelId = 2, Text = "Warto poświęcić czas na rozwój osobisty." },
            new() {Id = 11, DifficultyLevelId = 3, Text = "Szukaj motywacji w każdym dniu, aby realizować swoje cele." },
            new() {Id = 12, DifficultyLevelId = 3, Text = "Nie bój się prosić o pomoc, to oznaka siły, a nie słabości." },
            new() {Id = 13, DifficultyLevelId = 3, Text = "Słuchaj uważnie i bądź otwarty na różne punkty widzenia." },
            new() {Id = 14, DifficultyLevelId = 3, Text = "Wszyscy popełniamy błędy, ale to nasze doświadczenia uczą nas mądrości." },
            new() {Id = 15, DifficultyLevelId = 3, Text = "Nie pozwól, aby przeszłość definiowała twoją przyszłość." },
            new() {Id = 16, DifficultyLevelId = 4, Text = "Czemuż nie ma jeszcze pizzy? Czekam już godzinę i nic. Może jednak zamówić coś innego?" },
            new() {Id = 17, DifficultyLevelId = 4, Text = "Właśnie skończyłem pisać swoją nową powieść. Przeczytaj ją, jeśli lubisz literaturę science fiction" },
            new() {Id = 18, DifficultyLevelId = 4, Text = "Mam dobre wieści! Otrzymałem dzisiaj awans w pracy. Oto nagroda za ciężką pracę i poświęcenie." },
            new() {Id = 19, DifficultyLevelId = 4, Text = "Czuję się tak zmęczony, że chciałbym tylko leżeć w łóżku i odpocząć przez resztę dnia. A może warto zrobić sobie krótką drzemkę?" },
            new() {Id = 20, DifficultyLevelId = 4, Text = "Zaczynam kurs programowania od podstaw i jestem pod wrażeniem, jak dużo można się nauczyć w tak krótkim czasie. To prawdziwie fascynujące." },
            new() {Id = 21, DifficultyLevelId = 5, Text = "Cześć! Jak się masz? Dziś jest piękny dzień, pełen słońca i pozytywnej energii. Co u Ciebie słychać? Mam nadzieję, że wszystko idzie po Twojej myśli i że masz wspaniały dzień!" },
            new() {Id = 22, DifficultyLevelId = 5, Text = "W dzisiejszych czasach ludzie coraz częściej szukają sposobów na poprawę swojego zdrowia i samopoczucia. Od jogi po zdrowe odżywianie, możliwości jest wiele!" },
            new() {Id = 23, DifficultyLevelId = 5, Text = "Czasem warto zrobić sobie przerwę od codzienności i pozwolić sobie na chwilę relaksu. Może to być kąpiel z olejkami eterycznymi lub leniwe popołudnie z książką w ręku." },
            new() {Id = 24, DifficultyLevelId = 5, Text = "Kreatywność to nie tylko talent, ale także umiejętność rozwijania swoich pomysłów i poszukiwania nowych rozwiązań. Każdy może się jej nauczyć!" },
            new() {Id = 25, DifficultyLevelId = 5, Text = "Nauka nowego języka to świetny sposób na rozwijanie swoich umiejętności i poznawanie nowych kultur. W dzisiejszych czasach jest to łatwiejsze niż kiedykolwiek wcześniej, dzięki szerokiemu dostępowi do materiałów edukacyjnych i narzędzi online." },
        };
}