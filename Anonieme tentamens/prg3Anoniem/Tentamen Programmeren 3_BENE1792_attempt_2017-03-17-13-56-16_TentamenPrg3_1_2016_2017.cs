using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: CHER
Achternaam: BENEDICT
StudentNr: s17927
Klas: i1a

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    // Het doel van een constructer is dat bij het maken van een object van een clas dat je waarden moet mee geven
    // Hierbij kun je denken aan bijvoorbeeld een naam of een andere variable. Het is ook handig om hier een counter bij te houden
    // Stel we maken een object van dieren dan kunnen we in de constructor zeggen amountofanimals++; hiermee houden we bij hoeveel dieren er zijn aangemaakt.

    //Vraag 1B
    // Bij de Lijn class word de constructor Lijn() constructor aangemaakt met de parameter linNr.
    // Bij de Bus class word de constructor Bus() aangemaakt met de parameters: busNr, lijn, vetrektijd, datetime, voornaam en achternaam
    // Bij de Halte class word de constructor Halte() aangemaakt met de parameters: naam en reisduur.
    // Een constructor kun je herkennen aan de naam, de naam van de constructor is altijd hetzelfde als de class.

    //Vraag 1C
    // Een instantie variable kun je gewoon defineren meestal word dit op private gezet. Bij een property
    // maken we gebruik van getters en setters. Met een getter kun je de informatie van een variable ophalen
    // Met een set kunnen we de private instantie variable benaderen en hier regels aan toe dienen met bijvoorbeeld if statements dit zorgt voor veiligheid.
    #endregion

    #region Vraag2
    public class Bus
    {
        // Hier staan de instantie variable van de Bus class
        private string _voornaam;
        private string _achternaam;

        // Hier staan de properties van de Bus class
        public int BusNR
        {
            set;
            get;
        }

        public int AantalPassagiers
        {
            set;
            get;
        }

        public Lijn RijdOpLijn
        {
            set;
            get;
        }

        public DateTime Vetrektijd
        {
            set;
            get;
        }

        public string BestuurNaam
        {
            set{
                _voornaam = _voornaam;
                _achternaam = _achternaam;
            }
            get { 
                return _voornaam + _achternaam;

            }
        }

        // Hier staat de constructor voor de Bus class
        public Bus(int busNr, Lijn lijn, DateTime vetrektijd, string voornaam, string achternaam)
        {
            this.BusNR = busNr;
            this.RijdOpLijn = lijn;
            this.Vetrektijd = vetrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
        }

        // Hier staan de methodes voor de Bus class
        public void StapIn()
        {

        }

        public void StapUit()
        {

        }

    }

    public class Lijn
    {
        // Hier staan de properties van de Lijn class
        public int LijnNr
        {
            set;
            get;
        }

        public List<Halte> Haltes
        {
            set;
            get;
        }

        // Hier staat de constructor voor de Lijn class
        public Lijn(int lijnr)
        {
            this.LijnNr = lijnr;
        }

        // Hier staan de methoden voor de Lijn class
        public void AddHalte(Halte halte)
        {
          
            Haltes.Add(halte);
        }
        
        public TimeSpan Reistijd
        {
            set;
            get;
        }

    }

    public class Halte
    {

        // Hier komen de properties te staan voor de class Halte 
        public string Naam
        {
            set;
            get;
        }

        public TimeSpan Reisduur
        {
            set;
            get;
        }

        // Hier staat de constructor van de class Halte
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
            Bus bus = new Bus(10, lijn, vertrek, "Joris ", "Lops");

            lijn.AddHalte(new Halte("Bushalte NHL Hogeschool", TimeSpan.Zero));
            lijn.AddHalte(new Halte("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1)));
            lijn.AddHalte(new Halte("Bushalte Wissesdwinger", TimeSpan.FromMinutes(2)));
            lijn.AddHalte(new Halte("Bushalte Harmonie", TimeSpan.FromMinutes(3)));
            lijn.AddHalte(new Halte("Bushalte Zaailand", TimeSpan.FromMinutes(1)));
            lijn.AddHalte(new Halte("Bushalte Busstation", TimeSpan.FromMinutes(5)));


            Assert.AreEqual("Joris Lops", bus.BestuurNaam);
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

            Assert.AreEqual(10, bus.BusNR);

            Assert.AreEqual(612, bus.RijdOpLijn.LijnNr);

            Assert.AreEqual(new DateTime(2016, 09, 12, 14, 26, 0), bus.Vetrektijd);
            
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
            /*
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
            */
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
                throw new NotImplementedException();
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            throw new NotImplementedException();
        }

        public string Pop()
        {
            throw new NotImplementedException();
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            throw new NotImplementedException();
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
