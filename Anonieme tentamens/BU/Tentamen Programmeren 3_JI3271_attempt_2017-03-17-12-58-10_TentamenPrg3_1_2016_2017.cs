using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// 17-3-2017 // 12:56
//Voornaam: LAWANDA
//Achternaam: JI
//StudentNr: s32712
//Klas: i1f

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    // Een klasse constructor wordt aangeroepen bij object initialisatie
    // Zodra je een klasse initialiseert wordt de constructor aangeroepen.
    // Je gebruikt de constructor om instantie variabelen te instantieren en andere data te initialiseren
    //

    //Vraag 1B
    // Constructor van klasse 'Lijn': Lijn(lijnNr : int)
    // Constructor van klasse 'Halte': Halte(naam : string, reisduur : TimeSpan)
    // Constructor van klasse 'Bus': Bus(busNr : int, lijn : Lijn, vertrektijd : DateTime, voornaam : string, achternaam : string)
    //

    //Vraag 1C
    // Een property bevat een get en set accessor, een instantie variable heeft standaard de ingebouwde get en set accessors waarvan de accessability wordt overgenomen van de variable publicity modifier.
    //
    #endregion

    #region Vraag2
    public class Bus
    {
        public int BusNr { get; protected set; }
        public int AantalPassagiers { get; protected set; }
        public Lijn RijdOpLijn;
        public DateTime Vertrektijd;
        // Je kan ook public maken, en dan een protected set modifier voor de property
        // (tenzij je de bestuurder aanpasbaar wilt stellen)
        private string _voornaam;
        private string _achternaam;
        public string BestuurderNaam { get; protected set; }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this.BusNr = busNr;
            this.AantalPassagiers = 0;
            this.RijdOpLijn = lijn;
            this.Vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
            this.BestuurderNaam = $"{voornaam} {achternaam}";
        }

        // Even wat C#6 actie
        // -- en ++ is slomer (scheelt bijna niks in zulke gevallen, maar op grote schaal wel)
        public void StapIn()
            => AantalPassagiers += 1;
        public void StapUit()
            => AantalPassagiers -= 1;
    }

    public class Lijn
    {
        public int LijnNr;
        public List<Halte> Haltes;

        public Lijn(int lijnNr)
        {
            this.LijnNr = lijnNr;
            this.Haltes = new List<Halte>();
        }

        public void AddHalte(Halte halte)
        {
            if (!Haltes.Contains(halte))
                Haltes.Add(halte);
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            long total = Haltes.Sum(halte => halte.Reisduur.Ticks);
            return new TimeSpan(total);
        }
        #endregion
    }

    public class Halte
    {
        public string Naam;
        public TimeSpan Reisduur;

        public Halte(string naam, TimeSpan reisduur)
        {
            this.Naam = naam;
            this.Reisduur = reisduur;
        }
    }

    public class TestCasesVraag2
    {
        [Test]
        public void TestVraag2()
        {
            DateTime vertrek = new DateTime(2016, 09, 12, 14, 26, 0);

            Lijn lijn = new Lijn(612);
            Bus bus = new Bus(10, lijn, vertrek, "Joris", "Lops");

            lijn.AddHalte(new Halte("Bushalte NHL Hogeschool", TimeSpan.Zero));
            lijn.AddHalte(new Halte("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1)));
            lijn.AddHalte(new Halte("Bushalte Wissesdwinger", TimeSpan.FromMinutes(2)));
            lijn.AddHalte(new Halte("Bushalte Harmonie", TimeSpan.FromMinutes(3)));
            lijn.AddHalte(new Halte("Bushalte Zaailand", TimeSpan.FromMinutes(1)));
            lijn.AddHalte(new Halte("Bushalte Busstation", TimeSpan.FromMinutes(5)));


            Assert.AreEqual("Joris Lops", bus.BestuurderNaam);
            Assert.AreEqual(612, bus.RijdOpLijn.LijnNr);
            Assert.AreEqual("Bushalte Wissesdwinger", bus.RijdOpLijn.Haltes[2].Naam);
            Assert.AreEqual(TimeSpan.FromMinutes(2), bus.RijdOpLijn.Haltes[2].Reisduur);

            Assert.AreEqual(0, bus.AantalPassagiers);
            bus.StapIn();
            bus.StapIn();
            Assert.AreEqual(2, bus.AantalPassagiers);
            bus.StapUit();
            Assert.AreEqual(1, bus.AantalPassagiers);
            bus.StapUit();
            Assert.AreEqual(0, bus.AantalPassagiers);

            Assert.AreEqual(6, bus.RijdOpLijn.Haltes.Count);

            Assert.AreEqual(10, bus.BusNr);

            Assert.AreEqual(612, bus.RijdOpLijn.LijnNr);

            Assert.AreEqual(new DateTime(2016, 09, 12, 14, 26, 0), bus.Vertrektijd);

        }
    }
    #endregion

    #region Vraag3
    //Vraag 3, zie class Lijn in Vraag 2

    public class TestCasesVraag3
    {
        [Test]
        public void TestVraag3()
        {

            DateTime vertrek = new DateTime(2016, 09, 12, 14, 26, 0);

            Lijn lijn = new Lijn(612);
            Bus bus = new Bus(10, lijn, vertrek, "Joris", "Lops");

            lijn.AddHalte(new Halte("Bushalte NHL Hogeschool", TimeSpan.Zero));
            lijn.AddHalte(new Halte("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1)));
            lijn.AddHalte(new Halte("Bushalte Wissesdwinger", TimeSpan.FromMinutes(2)));
            lijn.AddHalte(new Halte("Bushalte Harmonie", TimeSpan.FromMinutes(3)));
            lijn.AddHalte(new Halte("Bushalte Zaailand", TimeSpan.FromMinutes(1)));
            lijn.AddHalte(new Halte("Bushalte Busstation", TimeSpan.FromMinutes(5)));

            TimeSpan reistijd = lijn.Reistijd();
            Assert.AreEqual(TimeSpan.FromMinutes(12), reistijd);

            DateTime aankomst = vertrek + reistijd;
            Assert.AreEqual(new DateTime(2016, 09, 12, 14, 38, 0), aankomst);

        }
    }
    #endregion

    #region Vraag4
    public class HalteStack
    {
        public string[] halteNamen { get; protected set; }
        private int total = 0;
        public bool IsEmpty
        {
            get
            {
                return halteNamen.All(string.IsNullOrEmpty) && total == 0;
            }
        }

        public HalteStack()
        {
            halteNamen = new string[20];
        }

        public void Push(string halteNaam)
        {
            halteNamen[total] = halteNaam;
            total += 1;
        }

        public string Pop()
        {
            if (total == 0)
                return string.Empty;

            string naam = halteNamen[total - 1];

            if (string.IsNullOrEmpty(naam))
                return string.Empty;

            halteNamen[total - 1] = null;
            total -= 1;
            return naam;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            List<string> names = new List<string>();
            for (int i = halteNamen.Length; i != 0; i--)
            {
                if (string.IsNullOrEmpty(halteNamen[i - 1]))
                    continue;
                names.Add(this.Pop());
            }
            return string.Join(",", names);
        }
    }

    public class TestCasesVraag4
    {
        [Test]
        public void Test_Vraag4A()
        {
            HalteStack hs = new HalteStack();
            Assert.AreEqual(true, hs.IsEmpty);
            hs.Push("Bushalte Zaailand");
            Assert.AreEqual(false, hs.IsEmpty);
            Assert.AreEqual("Bushalte Zaailand", hs.Pop());
            Assert.AreEqual(true, hs.IsEmpty);

            hs.Push("Bushalte Zaailand");
            hs.Push("Bushalte Harmonie");
            hs.Push("Bushalte Wissesdwinger");

            Assert.AreEqual(false, hs.IsEmpty);
            Assert.AreEqual("Bushalte Wissesdwinger", hs.Pop());

            Assert.AreEqual(false, hs.IsEmpty);
            Assert.AreEqual("Bushalte Harmonie", hs.Pop());

            Assert.AreEqual(false, hs.IsEmpty);
            Assert.AreEqual("Bushalte Zaailand", hs.Pop());
            Assert.AreEqual(true, hs.IsEmpty);
        }

        [Test]
        public void Test_Vraag4B()
        {
            HalteStack hs = new HalteStack();
            hs.Push("Bushalte Zaailand");
            hs.Push("Bushalte Harmonie");
            hs.Push("Bushalte Wissesdwinger");
            hs.Push("Bushalte Stenden Hogeschool");
            hs.Push("Bushalte NHL Hogeschool");

            string reversed = hs.PrintHaltesReversed();
            Assert.AreEqual("Bushalte NHL Hogeschool,Bushalte Stenden Hogeschool,Bushalte Wissesdwinger,Bushalte Harmonie,Bushalte Zaailand"
                , reversed);
        }
    }
    #endregion

    #region Vraag5
    public class HalteLinked
    {
        public string Naam { get; }
        public TimeSpan Reisduur { get; set; }
        public HalteLinked Volgende { get; set; }

        public HalteLinked(string naam, TimeSpan reisduur)
        {
            Naam = naam;
            Reisduur = reisduur;
        }
    }

    public class LijnLinked
    {
        private HalteLinked top = null;
        public int LijnNr { get; set; }

        public LijnLinked(int lijnNr, HalteLinked halte)
        {
            LijnNr = lijnNr;
            top = halte;
        }

        //Vraag 5A
        public int Count
        {
            get
            {
                int count = 0;
                HalteLinked needsNull = top;
                while (needsNull != null)
                {
                    count += 1;
                    needsNull = needsNull.Volgende;
                }
                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            long ticks = 0;
            HalteLinked needsNull = top;
            while (needsNull != null)
            {
                ticks += needsNull.Reisduur.Ticks;
                needsNull = needsNull.Volgende;
            }
            return new TimeSpan(ticks);
        }
    }

    public class TestCasesVraag5
    {
        [Test]
        public void Test_Vraag5A()
        {
            HalteLinked nhl = new HalteLinked("Bushalte NHL Hogeschool", TimeSpan.Zero);

            LijnLinked lijnLinked = new LijnLinked(12, nhl);
            Assert.AreEqual(1, lijnLinked.Count);

            HalteLinked stenden = new HalteLinked("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1));
            nhl.Volgende = stenden;
            Assert.AreEqual(2, lijnLinked.Count);

            HalteLinked harmonie = new HalteLinked("Bushalte Harmonie", TimeSpan.FromMinutes(3));
            stenden.Volgende = harmonie;
            Assert.AreEqual(3, lijnLinked.Count);

            HalteLinked zaailand = new HalteLinked("Bushalte Zaailand", TimeSpan.FromMinutes(1));
            harmonie.Volgende = zaailand;
            Assert.AreEqual(4, lijnLinked.Count);

            HalteLinked busstation = new HalteLinked("Bushalte Busstation", TimeSpan.FromMinutes(5));
            zaailand.Volgende = busstation;
            Assert.AreEqual(5, lijnLinked.Count);
        }

        [Test]
        public void Test_Vraag5B()
        {
            HalteLinked nhl = new HalteLinked("Bushalte NHL Hogeschool", TimeSpan.Zero);
            HalteLinked stenden = new HalteLinked("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1));
            HalteLinked harmonie = new HalteLinked("Bushalte Harmonie", TimeSpan.FromMinutes(3));
            HalteLinked zaailand = new HalteLinked("Bushalte Zaailand", TimeSpan.FromMinutes(1));
            HalteLinked busstation = new HalteLinked("Bushalte Busstation", TimeSpan.FromMinutes(5));

            nhl.Volgende = stenden;
            stenden.Volgende = harmonie;
            harmonie.Volgende = zaailand;
            zaailand.Volgende = busstation;

            LijnLinked lijnLinked = new LijnLinked(12, nhl);

            Assert.AreEqual(TimeSpan.FromMinutes(10), lijnLinked.Reistijd());
        }
    }
    #endregion
}