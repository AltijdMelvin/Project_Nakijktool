using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: TYREE
Achternaam: ROBENA
StudentNr: s15033
Klas: i1b

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    //Een object van een klasse aanmaken die (meestal) public is, waardoor in andere classes een object van deze class kan worden aangemaakt.
    //

    //Vraag 1B
    //
    //Lijn(lijnNr: int), Bus(busNr: int, vertrektijd : DateTime, voornaam : string, achternaam : string), Halte(naam :string, reisduur : TimeSpan)
    //

    //Vraag 1C
    // Een instantie variable kan alleen binnen de class waarin deze staat worden gebruikt. 
    // Een property kan door de get ook van buiten de class worden bekeken en mits er een set is ook worden aangepast.    
    #endregion

    #region Vraag2
    public class Bus
    {
        int busNr;
        int Aantalpassagiers = 0;
        Lijn rijdoplijn;
        DateTime vertrektijd;
        string Bestuurdernaam;
        string _voornaam;
        string _achternaam;

        public int BusNr { get { return busNr; } }
        public int AantalPassagiers { get { return Aantalpassagiers; } }
        public DateTime Vertrektijd { get { return vertrektijd; } }
        public Lijn RijdOpLijn {  get { return rijdoplijn; } }
        public string BestuurderNaam {  get { return Bestuurdernaam; } }
        public string voornaam {  get { return _voornaam; } }
        public string achternaam {  get { return _achternaam; } }
        

        public Bus(int BusNr, Lijn rijdoplijn, DateTime vertrektijd, string _voornaam, string _achternaam)
        {
            busNr = BusNr;
            this.rijdoplijn = rijdoplijn;
            this.vertrektijd = vertrektijd;
            this._voornaam = _voornaam;
            this._achternaam = _achternaam;
            Bestuurdernaam = _voornaam + " " + _achternaam;
        }

        public void StapIn()
        {
            Aantalpassagiers++;
        }

        public void StapUit()
        {
            if (AantalPassagiers > 0)
            {
                Aantalpassagiers--;
            }
        }
    }

    public class Lijn
    {
        int lijnNr;
        List<Halte> haltes = new List<Halte>();
        public List<Halte> Haltes {  get { return haltes; } }
        public int LijnNr { get { return lijnNr; } }

        public Lijn(int lijnNr)
        {
            this.lijnNr = lijnNr;
        }
        public void AddHalte(Halte halte)
        {
            haltes.Add(halte);
        }



        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan totaal = TimeSpan.FromSeconds(0);

            foreach (Halte h in haltes)
            {
                totaal += h.Reisduur;
            }

            return totaal;
        }
        #endregion
    }

    public class Halte
    {
        string naam;
        TimeSpan reisduur;

        public string Naam { get { return naam; } }
        public TimeSpan Reisduur {  get { return reisduur; } }

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
        string[] halteStack = new string[20];
        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < halteStack.Length; i++)
                {
                    if (halteStack[i] != null)
                    {
                        return false;
                    }
                   
                }

                return true;
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            int index = 0;

            while (halteStack[index] != null)
            {
                index++;
            }

            halteStack[index] = halteNaam; 
        }

        public string Pop()
        {
            int index = 1;

            while (halteStack[index] != null)
            {
                index++;
            }

            string returnPop = halteStack[index - 1];
            halteStack[index - 1] = null;
            return returnPop;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string returnString = "";

            for (int i = halteStack.Length - 1; i > 0; i--)
            {
                if (halteStack[i] != null)
                {
                    returnString += Pop() + ",";
                }
            }
            returnString += halteStack[0]; //zodat er niet nog een komma achteraan de returnstring komt
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
                HalteLinked h = top;
                
                while (h != null)
                {
                    count++;
                    h = h.Volgende;
                }

                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromSeconds(0);
            HalteLinked h = top; 

            while (h != null)
            {
                t += h.Reisduur;
                h = h.Volgende;
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
