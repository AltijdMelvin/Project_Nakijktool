using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: STEVEN
Achternaam: LORA
StudentNr: s25083
Klas: i1d

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    //Een constructor wordt gebruikt om de class te `constructen` en dus te initialiseren, met of zonder parameters.
    //

    //Vraag 1B
    //Lijn(lijnNr), Bus(busNr, lijn, vertrektijd, voornaam, achternaam), Halte(naam, rijsduur)
    //

    //Vraag 1C
    //instantie variable kunnen verschrillen van waarde per class en zijn altijd private, voor een property\class variable hoeft dit niet te gelden.
    //
    #endregion

    #region Vraag2
    public class Bus
    {
        private int busNr;
        private int aantalPassagiers;
        private Lijn rijdOpLijn;
        private DateTime vertrektijd;
        private string bestuurderNaam;
        private string _voornaam;
        private string _achternaam;

        public string BestuurderNaam
        {
            get
            {
                return bestuurderNaam;
            }

            set
            {
                bestuurderNaam = value;
            }
        }

        public int BusNr
        {
            get
            {
                return busNr;
            }

            set
            {
                busNr = value;
            }
        }

        public int AantalPassagiers
        {
            get
            {
                return aantalPassagiers;
            }

            set
            {
                aantalPassagiers = value;
            }
        }

        public Lijn RijdOpLijn
        {
            get
            {
                return rijdOpLijn;
            }

            set
            {
                rijdOpLijn = value;
            }
        }

        public DateTime Vertrektijd
        {
            get
            {
                return vertrektijd;
            }

            set
            {
                vertrektijd = value;
            }
        }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam )
        {
            BusNr = busNr;
            Vertrektijd = vertrektijd;
            RijdOpLijn = lijn;
            _voornaam = voornaam;
            _achternaam = achternaam;
            bestuurderNaam = voornaam + " " + achternaam;
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
        private int lijnNr;
        private List<Halte> haltes = new List<Halte>();

        public int LijnNr
        {
            get
            {
                return lijnNr;
            }

            set
            {
                lijnNr = value;
            }
        }

        public List<Halte> Haltes
        {
            get
            {
                return haltes;
            }

            set
            {
                haltes = value;
            }
        }

        public Lijn(int lijnNr)
        {
            LijnNr = lijnNr;
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
            foreach (Halte halte in Haltes)
            {
                t += halte.Reisduur;
            }
            return t;
        }
        #endregion
    }

    public class Halte
    {
        private string naam;
        private TimeSpan reisduur;

        public string Naam
        {
            get
            {
                return naam;
            }

            set
            {
                naam = value;
            }
        }

        public TimeSpan Reisduur
        {
            get
            {
                return reisduur;
            }

            set
            {
                reisduur = value;
            }
        }

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

        private int current;
        private bool empty =true;
        public bool IsEmpty
        {
            get
            {
                return empty;
            }
        }

        public string[] Stack
        {
            get
            {
                return stack;
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            empty = false;
            Stack[current] = halteNaam;
            current++;
        }

        public string Pop()
        {
            string value = "";
            current--;
            value = Stack[current];
            if(current == 0)
            {
                empty = true;
            }
            return value;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
           string str = "";
           for(int i = current; i > 0; i--)
            {
                str += Pop();
                str += ",";
            }
             str = str.Remove(str.Length - 1);
            return str;
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
        private int count;
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
                count = 0;
                HalteLinked next = top;
                while (next != null)
                {
                    count++;
                    next = next.Volgende;
                }
                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromSeconds(0);
            HalteLinked next = top;
            while(next != null)
            {
                t += next.Reisduur;
                next = next.Volgende;
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
