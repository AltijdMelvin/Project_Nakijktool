using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text;


//Voornaam: LENNY
//Achternaam: CAREY
//StudentNr: s43401
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
    // De constructor initialiseert de gegeven variabelen in het object die nodig zijn voor de correcte werking van het object.
    //

    //Vraag 1B
    //Lijn (LijnNr : int), Bus(BusNr : int, vertrektijd : DateTime, Voornaam : string, Achternaam : string), Halte(naam : string, reisduur : Timespan)
    //

    //Vraag 1C
    //Properties zijn overal buiten de class ook beschikbaar. Instantie variabelen zijn private en daarom niet van buiten de class te manipuleren.
    //
    #endregion

    #region Vraag2
    public class Bus
    {
        private int _busNr;
        private int _aantalPassagiers;
        private Lijn _rijdOpLijn;
        private DateTime _vertrekTijd;
        private string _bestuurdersNaam;

        private string _voornaam;
        private string _achternaam;

        public int BusNr {
            get {
                return this._busNr;
            }
        }
        public int AantalPassagiers
        {
            get
            {
                return this._aantalPassagiers;
            }
        }
        public Lijn RijdOpLijn
        {
            get
            {
                return this._rijdOpLijn;
            }
        }
        public DateTime Vertrektijd {
            get
            {
                return this._vertrekTijd;
            }
        }
        public string BestuurderNaam
        {
            get
            {
                return this._voornaam + " " + this._achternaam;
            }
        }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this._busNr = busNr;
            this._rijdOpLijn = lijn;
            this._vertrekTijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
        }

        public void StapIn()
        {
            this._aantalPassagiers++;
        }

        public void StapUit()
        {
            this._aantalPassagiers--;
        }
    }

    public class Lijn
    {
        private int _lijnNr;
        private List<Halte> _haltes = new List<Halte>();

        public int LijnNr {
            get
            {
                return this._lijnNr;
            }
        }
        public List<Halte> Haltes {
            get
            {
                return this._haltes;
            }

        }
        
        public Lijn(int lijnNr)
        {
            this._lijnNr = lijnNr;
        }

        public void AddHalte(Halte halte)
        {
            this._haltes.Add(halte); 
        }

        public TimeSpan Reistijd()
        {
            TimeSpan _totalReistijd = TimeSpan.FromSeconds(0);
            
            foreach(Halte halte in _haltes)
            {
                _totalReistijd += halte.Reisduur;
            }

            return _totalReistijd;
        }
    }

    public class Halte
    {
        private string _naam;
        private TimeSpan _reisduur;

        public string Naam
        {
            get
            {
                return this._naam;
            }
        }
        public TimeSpan Reisduur
        {
            get
            {
                return this._reisduur;
            }

        }

        public Halte(string naam, TimeSpan reisduur)
        {
            this._naam = naam;
            this._reisduur = reisduur;
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
        private int index = -1;
        private int count = 0;
        private string[] stack = new string[20];

        public bool IsEmpty
        {
            get
            {
                if(count <= 0)
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
            index++;
            stack[index] = halteNaam;
            count++;
        }

        public string Pop()
        {
            string popped = stack[index];

            stack[index] = null;
            index--;
            count--;

            return popped;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            StringBuilder toReturn = new StringBuilder();

            while(count != 0)
            {
                if (count == 1)
                {
                    toReturn.Append(this.Pop());
                }
                else
                {
                    toReturn.Append(this.Pop() + ",");
                }
            }

            Console.WriteLine("testline");

            return toReturn.ToString();
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
                HalteLinked current = top;
                int count = 0;
                while(current != null)
                {
                    count++;
                    current = current.Volgende;
                }
                
                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            TimeSpan toReturn = TimeSpan.FromSeconds(0);

            while(top != null)
            {
                toReturn += top.Reisduur;
                top = top.Volgende;
            }

            return toReturn;
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
            Console.WriteLine(nhl.Volgende.Naam);
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
