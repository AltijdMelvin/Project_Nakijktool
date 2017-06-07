using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


//Voornaam: BETTINA
//Achternaam: SHEBA
//StudentNr: s39601
//Klas: i1e

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    // Een constructor maakt stuk code dat je kan her-gebruiken zoals "New Test()".
    // of je geeft het nieuwe waards zoals: "Halte("Leeuwarden", 10:11".

    //Vraag 1B
    // In Lijn zit Constructor Lijn(lijnNr : int), In Halte zit Halte(naam : string, reisduur : TimeSpan) en bij bus
    // zit Bus(busNr : int, lijn : Lijn, vertrektijd : DateTime, voornaam : string, achternaam : string)

    //Vraag 1C
    // property is altijd public en instantie variable altijd private.
    //
    #endregion

    #region Vraag2
    public class Bus
    {
        int busnr;
        int aantalpassagiers;
        Lijn rijdoplijn;
        DateTime vertrektijd;
        string bestuurdernaam;
        private string _voornaam;
        private string _achternaam;

        public int BusNr
        {
            get { return this.busnr; }
            set { this.busnr = value; }
        }

        public int AantalPassagiers
        {
            get { return this.aantalpassagiers; }
            set { this.aantalpassagiers = value; }
        }

        public Lijn RijdOpLijn
        {
            get { return this.rijdoplijn; }
            set { this.rijdoplijn = value; }
        }

        public DateTime Vertrektijd
        {
            get { return this.vertrektijd; }
            set { this.vertrektijd = value; }
        }

        public string BestuurderNaam
        {
            get { return _voornaam + " " + _achternaam; }
            set { bestuurdernaam = value; }
        }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this.busnr = busNr;
            this.RijdOpLijn = lijn;
            this.vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
        }

        public void StapIn()
        {
            aantalpassagiers++;
        }

        public void StapUit()
        {
            aantalpassagiers--;
        }
    }

    public class Lijn
    {
        int lijnnr;
        List<Halte> haltes = new List<Halte>();

        public int LijnNr
        {
            get { return this.lijnnr; }
            set { this.lijnnr = value; }
        }

        public List<Halte> Haltes
        {
            get { return this.haltes; }
            set { this.haltes = value; }
        }

        public Lijn(int lijnNr)
        {
            this.LijnNr = lijnNr;
        }

        public void AddHalte(Halte halte)
        {
            haltes.Add(halte);
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromMinutes(0);
            for (int i = 0; i < haltes.Count; i++)
            {
                t += haltes[i].Reisduur;
            }
            return t;
        }
        #endregion
    }

    public class Halte
    {
        string naam = string.Empty;
        TimeSpan reisduur;

        public string Naam
        {
            get { return this.naam; }
            set { this.naam = value; }
        }

        public TimeSpan Reisduur
        {
            get { return this.reisduur; }
            set { this.reisduur = value; }
        }

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
        int Counter = 0;
        string[] stack = new string[20];

        public bool IsEmpty
        {
            get
            {
                if(stack[0] == null) { return true; }
                else { return false; }
            }
        }

        public HalteStack()
        {

        }

        public void Push(string halteNaam)
        {
            stack[Counter] = halteNaam;
            Counter++;
        }

        public string Pop()
        {
            string temp = stack[Counter - 1];
            stack[Counter - 1] = null;
            Counter--;
            return temp;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            List<string> temp = new List<string>();
            foreach(string s in stack)
            {
                if(s == null) { break; }
                else { temp.Add(s); }
            }
            string[] Temp = temp.ToArray();
            Array.Reverse(Temp);
            string result = string.Join(",", Temp);
            return result;
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
