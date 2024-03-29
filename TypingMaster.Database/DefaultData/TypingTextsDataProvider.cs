﻿using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.DefaultData;

public class TypingTextsDataProvider
{
    public IEnumerable<TypingTextEntity> TypingTexts { get; } = GetTypingTexts();

    private static IEnumerable<TypingTextEntity> GetTypingTexts()
    {
        return new List<TypingTextEntity>
        {
            new() {Id = 1, CultureId = 1, DifficultyLevelId = 1, Text = "Przysłowiowy kot."},
            new() {Id = 2, CultureId = 1, DifficultyLevelId = 1, Text = "Miłość jest ślepa."},
            new() {Id = 3, CultureId = 1, DifficultyLevelId = 1, Text = "Szczęśliwe zakończenie."},
            new() {Id = 4, CultureId = 1, DifficultyLevelId = 1, Text = "Skok w dal."},
            new() {Id = 5, CultureId = 1, DifficultyLevelId = 1, Text = "Cisza przed burzą."},
            new() {Id = 6, CultureId = 1, DifficultyLevelId = 2, Text = "Nie bądź bierny, działaj samodzielnie."},
            new() {Id = 7, CultureId = 1, DifficultyLevelId = 2, Text = "Zawsze powtarzaj pozytywne myśli."},
            new() {Id = 8, CultureId = 1, DifficultyLevelId = 2, Text = "Szanuj ludzi, którzy cię otaczają."},
            new() {Id = 9, CultureId = 1, DifficultyLevelId = 2, Text = "Zdrowie to nasz największy skarb."},
            new() {Id = 10, CultureId = 1, DifficultyLevelId = 2, Text = "Warto poświęcić czas na rozwój osobisty."},
            new()
            {
                Id = 11, CultureId = 1, DifficultyLevelId = 3,
                Text = "Szukaj motywacji w każdym dniu, aby realizować swoje cele."
            },
            new()
            {
                Id = 12, CultureId = 1, DifficultyLevelId = 3,
                Text = "Nie bój się prosić o pomoc, to oznaka siły, a nie słabości."
            },
            new()
            {
                Id = 13, CultureId = 1, DifficultyLevelId = 3,
                Text = "Słuchaj uważnie i bądź otwarty na różne punkty widzenia."
            },
            new()
            {
                Id = 14, CultureId = 1, DifficultyLevelId = 3,
                Text = "Wszyscy popełniamy błędy, ale to nasze doświadczenia uczą nas mądrości."
            },
            new()
            {
                Id = 15, CultureId = 1, DifficultyLevelId = 3,
                Text = "Nie pozwól, aby przeszłość definiowała twoją przyszłość."
            },
            new()
            {
                Id = 16, CultureId = 1, DifficultyLevelId = 4,
                Text = "Czemuż nie ma jeszcze pizzy? Czekam już godzinę i nic. Może jednak zamówić coś innego?"
            },
            new()
            {
                Id = 17, CultureId = 1, DifficultyLevelId = 4,
                Text =
                    "Właśnie skończyłem pisać swoją nową powieść. Przeczytaj ją, jeśli lubisz literaturę science fiction"
            },
            new()
            {
                Id = 18, CultureId = 1, DifficultyLevelId = 4,
                Text = "Mam dobre wieści! Otrzymałem dzisiaj awans w pracy. Oto nagroda za ciężką pracę i poświęcenie."
            },
            new()
            {
                Id = 19, CultureId = 1, DifficultyLevelId = 4,
                Text =
                    "Czuję się tak zmęczony, że chciałbym tylko leżeć w łóżku i odpocząć przez resztę dnia. A może warto zrobić sobie krótką drzemkę?"
            },
            new()
            {
                Id = 20, CultureId = 1, DifficultyLevelId = 4,
                Text =
                    "Zaczynam kurs programowania od podstaw i jestem pod wrażeniem, jak dużo można się nauczyć w tak krótkim czasie. To prawdziwie fascynujące."
            },
            new()
            {
                Id = 21, CultureId = 1, DifficultyLevelId = 5,
                Text =
                    "Cześć! Jak się masz? Dziś jest piękny dzień, pełen słońca i pozytywnej energii. Co u Ciebie słychać? Mam nadzieję, że wszystko idzie po Twojej myśli i że masz wspaniały dzień!"
            },
            new()
            {
                Id = 22, CultureId = 1, DifficultyLevelId = 5,
                Text =
                    "W dzisiejszych czasach ludzie coraz częściej szukają sposobów na poprawę swojego zdrowia i samopoczucia. Od jogi po zdrowe odżywianie, możliwości jest wiele!"
            },
            new()
            {
                Id = 23, CultureId = 1, DifficultyLevelId = 5,
                Text =
                    "Czasem warto zrobić sobie przerwę od codzienności i pozwolić sobie na chwilę relaksu. Może to być kąpiel z olejkami eterycznymi lub leniwe popołudnie z książką w ręku."
            },
            new()
            {
                Id = 24, CultureId = 1, DifficultyLevelId = 5,
                Text =
                    "Kreatywność to nie tylko talent, ale także umiejętność rozwijania swoich pomysłów i poszukiwania nowych rozwiązań. Każdy może się jej nauczyć!"
            },
            new()
            {
                Id = 25, CultureId = 1, DifficultyLevelId = 5,
                Text =
                    "Nauka nowego języka to świetny sposób na rozwijanie swoich umiejętności i poznawanie nowych kultur. W dzisiejszych czasach jest to łatwiejsze niż kiedykolwiek wcześniej, dzięki szerokiemu dostępowi do materiałów edukacyjnych i narzędzi online."
            },

            new() {Id = 26, CultureId = 2, DifficultyLevelId = 1, Text = "A proverbial cat."},
            new() {Id = 27, CultureId = 2, DifficultyLevelId = 1, Text = "Love is blind."},
            new() {Id = 28, CultureId = 2, DifficultyLevelId = 1, Text = "Happy ending."},
            new() {Id = 29, CultureId = 2, DifficultyLevelId = 1, Text = "Long jump."},
            new() {Id = 30, CultureId = 2, DifficultyLevelId = 1, Text = "Calm before the storm."},
            new() {Id = 31, CultureId = 2, DifficultyLevelId = 2, Text = "Don't be passive, take independent action."},
            new() {Id = 32, CultureId = 2, DifficultyLevelId = 2, Text = "Always repeat positive thoughts."},
            new() {Id = 33, CultureId = 2, DifficultyLevelId = 2, Text = "Respect the people around you."},
            new() {Id = 34, CultureId = 2, DifficultyLevelId = 2, Text = "Health is our greatest treasure."},
            new()
            {
                Id = 35, CultureId = 2, DifficultyLevelId = 2,
                Text = "It's worth dedicating time to personal development."
            },
            new()
            {
                Id = 36, CultureId = 2, DifficultyLevelId = 3, Text = "Seek motivation every day to achieve your goals."
            },
            new()
            {
                Id = 37, CultureId = 2, DifficultyLevelId = 3,
                Text = "Don't be afraid to ask for help; it's a sign of strength, not weakness."
            },
            new()
            {
                Id = 38, CultureId = 2, DifficultyLevelId = 3,
                Text = "Listen carefully and be open to different points of view."
            },
            new()
            {
                Id = 39, CultureId = 2, DifficultyLevelId = 3,
                Text = "We all make mistakes, but our experiences teach us wisdom."
            },
            new() {Id = 40, CultureId = 2, DifficultyLevelId = 3, Text = "Don't let the past define your future."},
            new()
            {
                Id = 41, CultureId = 2, DifficultyLevelId = 4,
                Text =
                    "Why isn't the pizza here yet? I've been waiting for an hour and nothing. Maybe I should order something else?"
            },
            new()
            {
                Id = 42, CultureId = 2, DifficultyLevelId = 4,
                Text = "I just finished writing my new novel. Read it if you like science fiction literature."
            },
            new()
            {
                Id = 43, CultureId = 2, DifficultyLevelId = 4,
                Text =
                    "I have good news! I received a promotion at work today. Here's the reward for hard work and dedication."
            },
            new()
            {
                Id = 44, CultureId = 2, DifficultyLevelId = 4,
                Text =
                    "I feel so tired that I would just like to lie in bed and rest for the rest of the day. Or maybe it's worth taking a short nap?"
            },
            new()
            {
                Id = 45, CultureId = 2, DifficultyLevelId = 4,
                Text =
                    "I'm starting a programming course from scratch, and I'm impressed by how much you can learn in such a short time. It's truly fascinating."
            },
            new()
            {
                Id = 46, CultureId = 2, DifficultyLevelId = 5,
                Text =
                    "Hello! How are you? Today is a beautiful day, full of sunshine and positive energy. What's up with you? I hope everything is going well for you, and you have a wonderful day!"
            },
            new()
            {
                Id = 47, CultureId = 2, DifficultyLevelId = 5,
                Text =
                    "In today's world, people are increasingly looking for ways to improve their health and well-being. From yoga to healthy eating, there are many possibilities!"
            },
            new()
            {
                Id = 48, CultureId = 2, DifficultyLevelId = 5,
                Text =
                    "Sometimes it's worth taking a break from the routine and allowing yourself a moment of relaxation. It could be a bath with essential oils or a lazy afternoon with a book in hand."
            },
            new()
            {
                Id = 49, CultureId = 2, DifficultyLevelId = 5,
                Text =
                    "Creativity is not only a talent but also the ability to develop your ideas and seek new solutions. Everyone can learn it!"
            },
            new()
            {
                Id = 50, CultureId = 2, DifficultyLevelId = 5,
                Text =
                    "Learning a new language is a great way to develop your skills and explore new cultures. Nowadays, it's easier than ever, thanks to broad access to educational materials and online tools."
            },

            new() {Id = 51, CultureId = 3, DifficultyLevelId = 1, Text = "Ein sprichwörtlicher Kater."},
            new() {Id = 52, CultureId = 3, DifficultyLevelId = 1, Text = "Liebe macht blind."},
            new() {Id = 53, CultureId = 3, DifficultyLevelId = 1, Text = "Happy End."},
            new() {Id = 54, CultureId = 3, DifficultyLevelId = 1, Text = "Weitsprung."},
            new() {Id = 55, CultureId = 3, DifficultyLevelId = 1, Text = "Ruhe vor dem Sturm."},
            new()
            {
                Id = 56, CultureId = 3, DifficultyLevelId = 2,
                Text = "Sei nicht passiv, ergreife eigenständige Maßnahmen."
            },
            new() {Id = 57, CultureId = 3, DifficultyLevelId = 2, Text = "Wiederhole immer positive Gedanken."},
            new() {Id = 58, CultureId = 3, DifficultyLevelId = 2, Text = "Respektiere die Menschen um dich herum."},
            new() {Id = 59, CultureId = 3, DifficultyLevelId = 2, Text = "Gesundheit ist unser größter Schatz."},
            new()
            {
                Id = 60, CultureId = 3, DifficultyLevelId = 2,
                Text = "Es lohnt sich, Zeit für die persönliche Entwicklung zu opfern."
            },
            new()
            {
                Id = 61, CultureId = 3, DifficultyLevelId = 3,
                Text = "Suche jeden Tag Motivation, um deine Ziele zu erreichen."
            },
            new()
            {
                Id = 62, CultureId = 3, DifficultyLevelId = 3,
                Text = "Hab keine Angst, um Hilfe zu bitten; es ist ein Zeichen von Stärke, nicht Schwäche."
            },
            new()
            {
                Id = 63, CultureId = 3, DifficultyLevelId = 3,
                Text = "Höre aufmerksam zu und sei offen für verschiedene Standpunkte."
            },
            new()
            {
                Id = 64, CultureId = 3, DifficultyLevelId = 3,
                Text = "Wir alle machen Fehler, aber unsere Erfahrungen lehren uns Weisheit."
            },
            new()
            {
                Id = 65, CultureId = 3, DifficultyLevelId = 3,
                Text = "Lass nicht zu, dass die Vergangenheit deine Zukunft definiert."
            },
            new()
            {
                Id = 66, CultureId = 3, DifficultyLevelId = 4,
                Text =
                    "Warum ist die Pizza noch nicht da? Ich warte schon seit einer Stunde und nichts. Vielleicht sollte ich etwas anderes bestellen?"
            },
            new()
            {
                Id = 67, CultureId = 3, DifficultyLevelId = 4,
                Text = "Ich habe gerade meinen neuen Roman beendet. Lies ihn, wenn du Science-Fiction-Literatur magst."
            },
            new()
            {
                Id = 68, CultureId = 3, DifficultyLevelId = 4,
                Text =
                    "Ich habe gute Nachrichten! Ich habe heute eine Beförderung bei der Arbeit erhalten. Hier ist die Belohnung für harte Arbeit und Engagement."
            },
            new()
            {
                Id = 69, CultureId = 3, DifficultyLevelId = 4,
                Text =
                    "Ich fühle mich so müde, dass ich einfach nur im Bett liegen und den Rest des Tages ausruhen möchte. Oder vielleicht lohnt es sich, ein kurzes Nickerchen zu machen?"
            },
            new()
            {
                Id = 70, CultureId = 3, DifficultyLevelId = 4,
                Text =
                    "Ich fange einen Programmierkurs von Grund auf an, und ich bin beeindruckt, wie viel man in so kurzer Zeit lernen kann. Es ist wirklich faszinierend."
            },
            new()
            {
                Id = 71, CultureId = 3, DifficultyLevelId = 5,
                Text =
                    "Hallo! Wie geht es dir? Heute ist ein schöner Tag, voller Sonnenschein und positiver Energie. Was gibt's Neues bei dir? Ich hoffe, alles läuft gut für dich, und du hast einen wundervollen Tag!"
            },
            new()
            {
                Id = 72, CultureId = 3, DifficultyLevelId = 5,
                Text =
                    "In der heutigen Welt suchen die Menschen immer häufiger nach Möglichkeiten, ihre Gesundheit und ihr Wohlbefinden zu verbessern. Von Yoga bis zu gesunder Ernährung gibt es viele Möglichkeiten!"
            },
            new()
            {
                Id = 73, CultureId = 3, DifficultyLevelId = 5,
                Text =
                    "Manchmal lohnt es sich, eine Pause von der Routine zu machen und sich einen Moment der Entspannung zu gönnen. Es könnte ein Bad mit ätherischen Ölen oder ein fauler Nachmittag mit einem Buch in der Hand sein."
            },
            new()
            {
                Id = 74, CultureId = 3, DifficultyLevelId = 5,
                Text =
                    "Kreativität ist nicht nur ein Talent, sondern auch die Fähigkeit, deine Ideen zu entwickeln und nach neuen Lösungen zu suchen. Das kann jeder lernen!"
            },
            new()
            {
                Id = 75, CultureId = 3, DifficultyLevelId = 5,
                Text =
                    "Eine neue Sprache zu lernen ist eine großartige Möglichkeit, deine Fähigkeiten zu entwickeln und neue Kulturen zu erkunden. Heutzutage ist das dank breitem Zugang zu Bildungsmaterialien und Online-Tools einfacher denn je."
            },

            new() {Id = 76, CultureId = 4, DifficultyLevelId = 1, Text = "Un gato proverbial."},
            new() {Id = 77, CultureId = 4, DifficultyLevelId = 1, Text = "El amor es ciego."},
            new() {Id = 78, CultureId = 4, DifficultyLevelId = 1, Text = "Final feliz."},
            new() {Id = 79, CultureId = 4, DifficultyLevelId = 1, Text = "Salto largo."},
            new() {Id = 80, CultureId = 4, DifficultyLevelId = 1, Text = "Calma antes de la tormenta."},
            new()
            {
                Id = 81, CultureId = 4, DifficultyLevelId = 2, Text = "No seas pasivo, toma medidas independientes."
            },
            new() {Id = 82, CultureId = 4, DifficultyLevelId = 2, Text = "Siempre repite pensamientos positivos."},
            new() {Id = 83, CultureId = 4, DifficultyLevelId = 2, Text = "Respeta a las personas que te rodean."},
            new() {Id = 84, CultureId = 4, DifficultyLevelId = 2, Text = "La salud es nuestro tesoro más grande."},
            new()
            {
                Id = 85, CultureId = 4, DifficultyLevelId = 2,
                Text = "Vale la pena dedicar tiempo al desarrollo personal."
            },
            new()
            {
                Id = 86, CultureId = 4, DifficultyLevelId = 3,
                Text = "Busca motivación todos los días para alcanzar tus objetivos."
            },
            new()
            {
                Id = 87, CultureId = 4, DifficultyLevelId = 3,
                Text = "No tengas miedo de pedir ayuda; es una señal de fortaleza, no debilidad."
            },
            new()
            {
                Id = 88, CultureId = 4, DifficultyLevelId = 3,
                Text = "Escucha con atención y mantente abierto a diferentes puntos de vista."
            },
            new()
            {
                Id = 89, CultureId = 4, DifficultyLevelId = 3,
                Text = "Todos cometemos errores, pero nuestras experiencias nos enseñan sabiduría."
            },
            new() {Id = 90, CultureId = 4, DifficultyLevelId = 3, Text = "No dejes que el pasado defina tu futuro."},
            new()
            {
                Id = 91, CultureId = 4, DifficultyLevelId = 4,
                Text =
                    "¿Por qué aún no ha llegado la pizza? Llevo esperando una hora y nada. ¿Quizás debería pedir algo más?"
            },
            new()
            {
                Id = 92, CultureId = 4, DifficultyLevelId = 4,
                Text =
                    "Acabo de terminar de escribir mi nueva novela. Léela si te gusta la literatura de ciencia ficción."
            },
            new()
            {
                Id = 93, CultureId = 4, DifficultyLevelId = 4,
                Text =
                    "¡Tengo buenas noticias! Hoy recibí un ascenso en el trabajo. Aquí está la recompensa por el trabajo duro y la dedicación."
            },
            new()
            {
                Id = 94, CultureId = 4, DifficultyLevelId = 4,
                Text =
                    "Me siento tan cansado que solo me gustaría estar tumbado en la cama y descansar el resto del día. ¿O tal vez vale la pena echarse una siesta corta?"
            },
            new()
            {
                Id = 95, CultureId = 4, DifficultyLevelId = 4,
                Text =
                    "Estoy empezando un curso de programación desde cero, y estoy impresionado por la cantidad que se puede aprender en tan poco tiempo. Es realmente fascinante."
            },
            new()
            {
                Id = 96, CultureId = 4, DifficultyLevelId = 5,
                Text =
                    "¡Hola! ¿Cómo estás? Hoy es un día hermoso, lleno de sol y energía positiva. ¿Qué hay de nuevo contigo? Espero que todo te vaya bien y que tengas un día maravilloso."
            },
            new()
            {
                Id = 97, CultureId = 4, DifficultyLevelId = 5,
                Text =
                    "En el mundo actual, la gente busca cada vez más formas de mejorar su salud y bienestar. ¡Desde el yoga hasta la alimentación saludable, hay muchas posibilidades!"
            },
            new()
            {
                Id = 98, CultureId = 4, DifficultyLevelId = 5,
                Text =
                    "A veces vale la pena tomarse un descanso de la rutina y permitirse un momento de relajación. Podría ser un baño con aceites esenciales o una tarde tranquila con un libro en la mano."
            },
            new()
            {
                Id = 99, CultureId = 4, DifficultyLevelId = 5,
                Text =
                    "La creatividad no es solo un talento, sino también la capacidad de desarrollar tus ideas y buscar nuevas soluciones. ¡Cualquiera puede aprenderlo!"
            },
            new()
            {
                Id = 100, CultureId = 4, DifficultyLevelId = 5,
                Text =
                    "Aprender un nuevo idioma es una excelente manera de desarrollar tus habilidades y explorar nuevas culturas. Hoy en día, es más fácil que nunca, gracias al amplio acceso a materiales educativos y herramientas en línea."
            },

            new() {Id = 101, CultureId = 5, DifficultyLevelId = 1, Text = "Un chat proverbial."},
            new() {Id = 102, CultureId = 5, DifficultyLevelId = 1, Text = "L'amour est aveugle."},
            new() {Id = 103, CultureId = 5, DifficultyLevelId = 1, Text = "Fin heureuse."},
            new() {Id = 104, CultureId = 5, DifficultyLevelId = 1, Text = "Saut en longueur."},
            new() {Id = 105, CultureId = 5, DifficultyLevelId = 1, Text = "Calme avant la tempête."},
            new()
            {
                Id = 106, CultureId = 5, DifficultyLevelId = 2,
                Text = "Ne soyez pas passif, prenez des mesures indépendantes."
            },
            new() {Id = 107, CultureId = 5, DifficultyLevelId = 2, Text = "Répétez toujours des pensées positives."},
            new()
            {
                Id = 108, CultureId = 5, DifficultyLevelId = 2, Text = "Respectez les personnes qui vous entourent."
            },
            new() {Id = 109, CultureId = 5, DifficultyLevelId = 2, Text = "La santé est notre plus grand trésor."},
            new()
            {
                Id = 110, CultureId = 5, DifficultyLevelId = 2,
                Text = "Il vaut la peine de consacrer du temps au développement personnel."
            },
            new()
            {
                Id = 111, CultureId = 5, DifficultyLevelId = 3,
                Text = "Cherchez la motivation chaque jour pour atteindre vos objectifs."
            },
            new()
            {
                Id = 112, CultureId = 5, DifficultyLevelId = 3,
                Text = "N'ayez pas peur de demander de l'aide; c'est un signe de force, pas de faiblesse."
            },
            new()
            {
                Id = 113, CultureId = 5, DifficultyLevelId = 3,
                Text = "Écoutez attentivement et soyez ouvert à différents points de vue."
            },
            new()
            {
                Id = 114, CultureId = 5, DifficultyLevelId = 3,
                Text = "Nous faisons tous des erreurs, mais nos expériences nous enseignent la sagesse."
            },
            new()
            {
                Id = 115, CultureId = 5, DifficultyLevelId = 3, Text = "Ne laissez pas le passé définir votre avenir."
            },
            new()
            {
                Id = 116, CultureId = 5, DifficultyLevelId = 4,
                Text =
                    "Pourquoi la pizza n'est-elle pas encore là? J'attends depuis une heure et rien. Peut-être devrais-je commander autre chose?"
            },
            new()
            {
                Id = 117, CultureId = 5, DifficultyLevelId = 4,
                Text =
                    "Je viens de finir d'écrire mon nouveau roman. Lisez-le si vous aimez la littérature de science-fiction."
            },
            new()
            {
                Id = 118, CultureId = 5, DifficultyLevelId = 4,
                Text =
                    "J'ai de bonnes nouvelles! J'ai reçu une promotion au travail aujourd'hui. Voici la récompense pour le travail acharné et le dévouement."
            },
            new()
            {
                Id = 119, CultureId = 5, DifficultyLevelId = 4,
                Text =
                    "Je me sens tellement fatigué que j'aimerais juste m'allonger dans le lit et me reposer pour le reste de la journée. Ou peut-être vaut-il la peine de faire une courte sieste?"
            },
            new()
            {
                Id = 120, CultureId = 5, DifficultyLevelId = 4,
                Text =
                    "Je commence un cours de programmation à partir de zéro, et je suis impressionné par la quantité que l'on peut apprendre en si peu de temps. C'est vraiment fascinant."
            },
            new()
            {
                Id = 121, CultureId = 5, DifficultyLevelId = 5,
                Text =
                    "Salut! Comment ça va? Aujourd'hui est une belle journée, pleine de soleil et d'énergie positive. Quoi de neuf chez toi? J'espère que tout se passe bien pour toi, et que tu passes une journée merveilleuse!"
            },
            new()
            {
                Id = 122, CultureId = 5, DifficultyLevelId = 5,
                Text =
                    "Dans le monde d'aujourd'hui, les gens cherchent de plus en plus des moyens d'améliorer leur santé et leur bien-être. Du yoga à une alimentation saine, il existe de nombreuses possibilités!"
            },
            new()
            {
                Id = 123, CultureId = 5, DifficultyLevelId = 5,
                Text =
                    "Parfois, il vaut la peine de faire une pause dans la routine et de s'offrir un moment de détente. Ça pourrait être un bain avec des huiles essentielles ou un après-midi tranquille avec un livre à la main."
            },
            new()
            {
                Id = 124, CultureId = 5, DifficultyLevelId = 5,
                Text =
                    "La créativité n'est pas seulement un talent, mais aussi la capacité de développer ses idées et de chercher de nouvelles solutions. Tout le monde peut l'apprendre!"
            },
            new()
            {
                Id = 125, CultureId = 5, DifficultyLevelId = 5,
                Text =
                    "Apprendre une nouvelle langue est un excellent moyen de développer ses compétences et d'explorer de nouvelles cultures. De nos jours, c'est plus facile que jamais, grâce à un large accès à des supports éducatifs et à des outils en ligne."
            },

            new() {Id = 126, CultureId = 6, DifficultyLevelId = 1, Text = "一只谚语中的猫。"},
            new() {Id = 127, CultureId = 6, DifficultyLevelId = 1, Text = "爱情是盲目的。"},
            new() {Id = 128, CultureId = 6, DifficultyLevelId = 1, Text = "美好的结局。"},
            new() {Id = 129, CultureId = 6, DifficultyLevelId = 1, Text = "跳远。"},
            new() {Id = 130, CultureId = 6, DifficultyLevelId = 1, Text = "风暴前的宁静。"},
            new() {Id = 131, CultureId = 6, DifficultyLevelId = 2, Text = "不要被动，采取独立行动。"},
            new() {Id = 132, CultureId = 6, DifficultyLevelId = 2, Text = "始终重复积极的思维。"},
            new() {Id = 133, CultureId = 6, DifficultyLevelId = 2, Text = "尊重你周围的人。"},
            new() {Id = 134, CultureId = 6, DifficultyLevelId = 2, Text = "健康是我们最大的财富。"},
            new() {Id = 135, CultureId = 6, DifficultyLevelId = 2, Text = "值得花时间发展个人能力。"},
            new() {Id = 136, CultureId = 6, DifficultyLevelId = 3, Text = "每天寻找动力，实现你的目标。"},
            new() {Id = 137, CultureId = 6, DifficultyLevelId = 3, Text = "不要害怕寻求帮助；这是一种力量的体现，而不是软弱。"},
            new() {Id = 138, CultureId = 6, DifficultyLevelId = 3, Text = "仔细倾听，对不同的观点保持开放态度。"},
            new() {Id = 139, CultureId = 6, DifficultyLevelId = 3, Text = "我们都会犯错，但我们的经验使我们变得更加智慧。"},
            new() {Id = 140, CultureId = 6, DifficultyLevelId = 3, Text = "不要让过去定义你的未来。"},
            new() {Id = 141, CultureId = 6, DifficultyLevelId = 4, Text = "为什么披萨还没到？我等了一个小时了，什么都没有。也许我应该点别的东西？"},
            new() {Id = 142, CultureId = 6, DifficultyLevelId = 4, Text = "我刚刚写完了我的新小说。如果你喜欢科幻文学，就来读一读吧。"},
            new() {Id = 143, CultureId = 6, DifficultyLevelId = 4, Text = "我有个好消息！今天我在工作中晋升了。这是对努力工作和奉献的奖励。"},
            new() {Id = 144, CultureId = 6, DifficultyLevelId = 4, Text = "我感到很累，我只想躺在床上休息一天的剩下时间。或者也许值得小睡一会儿？"},
            new()
            {
                Id = 145, CultureId = 6, DifficultyLevelId = 4, Text = "我开始一门从零开始的编程课程，我对在如此短的时间内能学到这么多感到印象深刻。这真是令人着迷。"
            },
            new()
            {
                Id = 146, CultureId = 6, DifficultyLevelId = 5,
                Text = "你好！你好吗？今天是美好的一天，充满阳光和正能量。你怎么样？我希望一切都很好，你度过了美好的一天！"
            },
            new()
            {
                Id = 147, CultureId = 6, DifficultyLevelId = 5, Text = "在今天的世界里，人们越来越多地寻找改善健康和幸福的方式。从瑜伽到健康饮食，有许多可能性！"
            },
            new()
            {
                Id = 148, CultureId = 6, DifficultyLevelId = 5,
                Text = "有时候值得从例行公事中休息一下，给自己一点放松的时刻。这可能是用精油泡个澡，或者端坐一下，手里拿着一本书度过悠闲的下午。"
            },
            new() {Id = 149, CultureId = 6, DifficultyLevelId = 5, Text = "创造力不仅是一种天赋，也是发展思想并寻找新解决方案的能力。每个人都可以学会！"},
            new()
            {
                Id = 150, CultureId = 6, DifficultyLevelId = 5,
                Text = "学习一门新语言是发展技能和探索新文化的好方法。如今，由于广泛获取教育材料和在线工具的便利，这变得比以往任何时候都更容易。"
            }
        };
    }
}