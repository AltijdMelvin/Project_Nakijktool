using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


//Voornaam: PING
//Achternaam: SHAKITA
//StudentNr: s58186
//Klas: i1a

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    // Een constructor zorgt ervoor dat als een instantie van het object aangemaakt wordt dat er ook gelijk een aantal variabelen ge-set worden.
    // De variablen die ge-set worden zijn de variablen die in de constructor staan, of te wel die je moet invullen als je een object aanmaakt.
    // Dit zorgt ervoor dat bepaalde variablen maar 1 keer ge-set kunnen worden, namelijk alleen als een instantie van de classe wordt aangemaakt.

    //Vraag 1B
    // Lijn(lijnNr), Bus(busNr, lijn, vertrektijd, voornaam, achternaam), Halte(naam, reisduur)
    //

    //Vraag 1C
    // Een property is een link tussen een gebruiker en een variable. Je kan het read-only, set-only of beide tegelijk maken (get en set).
    // Wat een property doet is ervoor zorgen dat bepaalde instantie variablen alleen bereikbaar zijn door middel van zichzelf. 
    // De programmeur heeft als het ware een deel van de bereikaarheid van de variable afgeschermd.
    // Oftewel, als een property read-only is is het dus niet mogelijk om de waarde van het variable te setten (behalve in de constructor)
    //
    // Instantie variablen zijn de persoonlijke variablen van een object. Hier wordt de data in opgeslagen en zo kan het object dus weer verwijzen naar bepaalde waardes.
    // Dat is de reden dat je niet wilt dat iedereen bij die variablen kan en ze aanpassen, en daarom gebruik je properties om er bij te komen.

    #endregion

    #region Vraag2
    public class Bus
    {
        private int busNr;
        private Lijn lijn;
        private DateTime vertrektijd;
        private int aantalPassagiers;

        public int BusNr
        {
            get
            {
                return busNr;
            }
        }

        public int AantalPassagiers
        {
            get
            {
                return aantalPassagiers;
            }
        }

        public Lijn RijdOpLijn
        {
            get
            {
                return lijn;
            }
        }

        public DateTime Vertrektijd
        {
            get
            {
                return vertrektijd;
            }
        }

        public string BestuurderNaam
        {
            get
            {
                return _voornaam + " " + _achternaam;
            }
        }

        private string _voornaam, _achternaam;

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this.busNr = busNr;
            this.lijn = lijn;
            this.vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
            aantalPassagiers = 0;
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
        private List<Halte> haltes;

        public int LijnNr
        {
            get
            {
                return lijnNr;
            }
        }

        public List<Halte> Haltes
        {
            get
            {
                return haltes;
            }
        }

        public Lijn(int lijnNr)
        {
            this.lijnNr = lijnNr; 
            haltes = new List<Halte>();
        }

        public void AddHalte(Halte halte)
        {
            Haltes.Add(halte);
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan totaleReisduur = TimeSpan.FromSeconds(0);

            foreach(Halte halte in Haltes)
            {
                totaleReisduur += halte.Reisduur;
            }

            return totaleReisduur;
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
        }
        public TimeSpan Reisduur
        {
            get
            {
                return reisduur;
            }
        }

        public Halte(string naam, TimeSpan reisduur)
        {
            this.naam = naam; 
            this.reisduur = reisduur;
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
        private string[] stack;
        private int top;

        public bool IsEmpty
        {
            get
            {
                foreach (string s in stack)
                {
                    if(s != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public HalteStack()
        {
            this.stack = new string[20]; //ik vind this leuk om aan te roepen
            this.top = -1;
        }

        public void Push(string halteNaam)
        {
            top++;
            stack[top] = halteNaam;
        }

        public string Pop()
        {
            string halteToReturn = stack[top];
            stack[top] = null;
            top--;
            return halteToReturn;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string printedHaltes = "";

            while (top >= 1)
            {
                printedHaltes += Pop() + ",";
            }
            printedHaltes += Pop();

            return printedHaltes;
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
                int totalNodes = 0;
                HalteLinked tempTop = top;

                while(tempTop != null)
                {
                    totalNodes++;
                    tempTop = tempTop.Volgende;
                }

                return totalNodes;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            TimeSpan totaleReistijd = TimeSpan.FromSeconds(0);
            HalteLinked tempTop = top;

            while(tempTop != null)
            {
                totaleReistijd += tempTop.Reisduur;
                tempTop = tempTop.Volgende;
            }

            return totaleReistijd;
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
