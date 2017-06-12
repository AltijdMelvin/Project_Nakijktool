using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


//Voornaam: ELAINA
//Achternaam: HARVEY
//StudentNr: s79418
//Klas: i1c

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    //
    // Een constructor is een soort class (Methode) met dezelfde naam als de class. Deze wordt uitgevoerd wanneer de class aangeroepen wordt.

    //Vraag 1B
    //
    // Lijn, Bus, Halte

    //Vraag 1C
    //
    // Een instance variable is een private property van een class en is alleen in de huidige class te gebruiken.
    // Een property is een public property van een class en is vanuit andere classes aan te roepen.
    // Een voorbeeld van een property is bijvoorbeeld in een class piraat de property naam. Vervolgens kan je in de class bemanning Piraat.naam aanroepen omdat het een public property is. Dit kan met een instance variable niet.
 
    #endregion

    #region Vraag2
    public class Bus
    {
        private string _voornaam;
        private string _achternaam;
        public int BusNr { get; set; }
        public int AantalPassagiers { get; set; }
        public Lijn RijdOpLijn { get; set; }
        public DateTime Vertrektijd { get; set; }
        public string BestuurderNaam { get; set; }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this.BusNr = busNr;
            this.RijdOpLijn = lijn;
            this.Vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
            this.BestuurderNaam = _voornaam + " " + _achternaam;
        }

        public void StapIn()
        {
            AantalPassagiers = AantalPassagiers + 1;
        }

        public void StapUit()
        {
            AantalPassagiers = AantalPassagiers - 1;
        }
    }

    public class Lijn
    {
        public int LijnNr { get; set; }
        public List<Halte> Haltes = new List<Halte>();

        public Lijn(int lijnNr)
        {
            this.LijnNr = lijnNr;
        }

        public void AddHalte(Halte halte)
        {
            this.Haltes.Add(halte);
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan reistijd = TimeSpan.Zero;
            foreach (Halte halte in Haltes)
            {
                reistijd = reistijd + halte.Reisduur;
            }
            return reistijd;
        }
        #endregion
    }

    public class Halte
    {
        public string Naam { get; }
        public TimeSpan Reisduur { get; }

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
        public bool IsEmpty
        {
            get
            {
                if (halteStack.Count == 0)
                {
                    return true;
                }
                else { return false; }
                
            }
        }

        Stack<string> halteStack = new Stack<string>(20);
        Stack<string> halteStackReversed = new Stack<string>(20);

        public HalteStack()
        {
            
        }

        public void Push(string halteNaam)
        {
            halteStack.Push(halteNaam);
            halteStackReversed.Push(halteNaam);
        }

        public string Pop()
        {
            return halteStack.Pop();
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            //while (halteStack.Count != 0)
            //{
            //    halteStackReversed.Push(halteStack.Pop());
            //}
            List<string> reversedList = new List<string>();
            
            while (halteStackReversed.Count != 0)
            {
                reversedList.Add(halteStackReversed.Pop());
            }
            string reversedString = "";
            foreach (string s in reversedList)
            {
                reversedString = reversedString + s + ",";
            }
            
            return reversedString;
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
            Assert.AreEqual("Bushalte NHL Hogeschool,Bushalte Stenden Hogeschool,Bushalte Wissesdwinger,Bushalte Harmonie,Bushalte Zaailand,"
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
                throw new NotImplementedException();
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            throw new NotImplementedException();
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
