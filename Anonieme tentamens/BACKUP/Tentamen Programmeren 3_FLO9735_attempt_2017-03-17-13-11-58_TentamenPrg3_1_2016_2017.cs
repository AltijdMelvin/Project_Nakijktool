using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: ANYA
Achternaam: FLO
StudentNr: s97357
Klas: i1d

    //commentaar: ik heb de meeste properties nie zoals het schema gedaan
    //omdat ik dat zelf zo duidelijker vind

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    //doormiddel van een constructor kunnen er meerdere instances van objecten gemaakt worden
    //een constructor kan ook eigenschappen meegeven aan het object dat gecreerd wordt

    //Vraag 1B
    //Lijn heeft een constructor 1 parameter
    //Bus heeft een constructor met 5 parameters
    //Halte heeft een constructor met 2 parameters

    //Vraag 1C
    //een instantie variable is onderdeel van een object, een property wordt gebruikt om deze aan te passen 
    //maar alleen van de instance die gemaakt is, daarom zijn instantie variablen altijd private zodat
    //deze niet aangepast worden wanneer er een property aangepast wordt, op deze manier heeft elke instance
    //zijn eigen variablen die aangepast kunnen worden met properties.
    #endregion

    #region Vraag2
    public class Bus
    {
        private int busNr;
        private Lijn lijn;
        private DateTime vertrektijd;
        private int aantalPassagiers;
        private string _voornaam;
        private string _achternaam;


        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this.busNr = busNr;
            this.lijn = lijn;
            this.vertrektijd = vertrektijd;
            _voornaam = voornaam;
            _achternaam = achternaam;
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
                return lijn;
            }
            set
            {
                lijn = value;
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


        public string BestuurderNaam
        {
            get
            {
                return _voornaam + " " + _achternaam;
            }
        }

        public void StapIn()
        {
            aantalPassagiers++;
        }

        public void StapUit()
        {
            aantalPassagiers--;
        }

    }

    public class Lijn
    {
        private int lijnNr;
        private List<Halte> haltes = new List<Halte>();
        public Lijn(int lijnNr)
        {
            this.lijnNr = lijnNr;
        }

        public int LijnNr
        {
            get { return lijnNr; }
            set { lijnNr = value; }
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
        public void AddHalte(Halte halte)
        {
            haltes.Add(halte);
        }

        public TimeSpan ReisTijd()
        {
            TimeSpan totaal = TimeSpan.Zero;

            foreach(Halte h in haltes)
            {
                totaal += h.Reisduur;
            }

            return totaal;
        }
    }

    public class Halte
    {
        private string naam;
        private TimeSpan reisduur;

        public Halte(string naam, TimeSpan reisduur)
        {
            this.naam = naam;
            this.reisduur = reisduur;
        }

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

            TimeSpan reistijd = lijn.ReisTijd();
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
        private int head = -1;
        public bool IsEmpty
        {
            get
            {
                if(stack[0] == null)
                {
                    return true;
                }
                return false;
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            head += 1;
            stack[head] = halteNaam;
        }

        public string Pop()
        {
            string old = stack[head];
            stack[head] = null;
            head -= 1;
            return old;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string returnString = "";
            for(int i = head; i >= 0; i--)
            {
                returnString += Pop();
                if(i != 0)
                {
                    returnString += ",";
                }
            }
            return returnString;
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
                var current = top;
                while(current.Volgende != null)
                {
                    count++;
                    current = current.Volgende;
                }
                count++;
                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.Zero;

            var current = top;
            while(current.Volgende != null)
            {
                t += current.Reisduur;
                current = current.Volgende;
            }
            t += current.Reisduur;
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
