using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


//Voornaam: HOLLIE
//Achternaam: GENEVA
//StudentNr: s18127
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
    // Een constructor is er om een klasse te instantieren. Als je een nieuwe klasse aanroept "new Foo()"
    // roep je eigenlijk de constructor aan, de constructor bouwt je klasse op. Aan een constructor kan je
    // ook argumenten meegeven, deze argumenten kan je dan bijvoorbeeld toekennen aan instantievariabelen.
    // Een voorbeeld hiervoor zou kunenn zijn als je data op wilt slaan in de klasse, heirdoor kan je de 
    // data direct meegeven in de constructor. Maar in een constructor kun je ook andere dingen doen, zoals
    // andere klassen initialiseren of rekenen. Een constructor heeft altijd dezelfde naam als de klasse en
    // geen return type.

    //Vraag 1B
    // In het diagram:
    //
    // Halte(naam : string, reisduur: TimeSpan)
    // Lijn(lijnNr : int)
    // Bus(busNr : int, lijn : Lijn, vertrektijd : DateTime, voornaam : string, achternaam : string)
    //
    // In C#:
    //
    // public Halte(string naam, TimeSpan reisduur) { } 
    // public Lijn(int lijnNr) { }
    // public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam) { }

    //Vraag 1C
    // Een property is public en kan daarom berijkt worden vanaf buiten de klasse, een instantie variabele
    // is een variabele die privé is en kan daarom niet bereikt worden van buiten de klasse. Vaak refereerd
    // een property naar een instantie variabele doormiddel van een get {} of door een lambda expressie. De
    // reden dat het op deze manier gedaan wordt is om er voor te zorgen dat de interne methodes van een klasse
    // gewijzigd kunnen worden zonder dat er iets veranderd aan de returntype. Dus de naam van de property 
    // blijft hetzelfde. Het verschil is dus dat een property wel vanaf buiten de klasse bereikt kan worden
    // en een instantie variabele niet.
    #endregion

    #region Vraag2
    public class Bus
    {
        private string _voornaam;
        private string _achternaam;

        public int BusNr { get; private set; }
        public int AantalPassagiers { get; private set; }
        public Lijn RijdOpLijn { get; private set; }
        public DateTime Vertrektijd { get; private set; }
        public string BestuurderNaam { get { return _voornaam + " " + _achternaam; } }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            BusNr = busNr;
            RijdOpLijn = lijn;
            Vertrektijd = vertrektijd;
            _voornaam = voornaam;
            _achternaam = achternaam;
        }

        public void StapIn()
        {
            AantalPassagiers++;
        }

        public void StapUit()
        {
            AantalPassagiers--;
        }
    }

    public class Lijn
    {
        public int LijnNr { get; private set; }
        public List<Halte> Haltes { get; private set; }

        public Lijn(int lijnNr)
        {
            LijnNr = lijnNr;
            Haltes = new List<Halte>();
        }

        public void AddHalte(Halte halte)
        {
            Haltes.Add(halte);
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromSeconds(0);
            foreach (Halte h in Haltes)
                t += h.Reisduur;
            return t;
        }
        #endregion
    }

    public class Halte
    {
        public string Naam { get; private set; }
        public TimeSpan Reisduur { get; private set; }

        public Halte(string naam, TimeSpan reisduur)
        {
            Naam = naam;
            Reisduur = reisduur;
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
        private string[] stack = new string[20];
        private int index = 0;

        public bool IsEmpty
        {
            get
            {
                if (index <= 0)
                    return true;
                return false;
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            stack[index] = halteNaam;
            index++;
        }

        public string Pop()
        {
            index--;
            string temp = stack[index];
            stack[index] = null;
            return temp;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string reversed = "";
            int currentindex = index;
            for (int i = 0; i < currentindex; i++)
            {
                if (i != 0)
                    reversed += ",";
                reversed += Pop();
            }
            return reversed;
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
                int total = 0;
                HalteLinked current = top;
                while (current != null)
                {
                    total++;
                    current = current.Volgende;
                }
                return total;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromSeconds(0);
            HalteLinked current = top;
            while (current != null)
            {
                t += current.Reisduur;
                current = current.Volgende;
            }
            return t;
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
