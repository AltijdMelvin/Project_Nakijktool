using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: KAYLENE
Achternaam: DARCY
StudentNr: s19479
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
    // Een constructor wordt uitgevoerd wanneer er een nieuwe instantie van de klasse aangemaakt wordt.
    // Dit is handig voor een initialisatie van de klasse, bijvoorbeeld voor de instantievariabelen.

    //Vraag 1B
    // Lijn, Bus & Halte
    //

    //Vraag 1C
    // Een instantievariabele is prive, en mag nooit van buitenaf direct aangepast worden.
    // Een property is een manier om de waardes van een instantie variabele gecontroleerd te kunnen opvragen of aan te passen.
    // Op deze manier kun je je programma beveiligen, en onverwacht gedrag voorkomen.
    #endregion

    #region Vraag2
    public class Bus
    {
        private int _busNr;
        private int _aantalPassagiers;
        private Lijn _rijdOpLijn;
        private DateTime _vertrektijd;
        private string _bestuurderNaam;
        private string _voornaam;
        private string _achternaam;

        public int BusNr { get { return _busNr; } }
        public int AantalPassagiers { get { return _aantalPassagiers; } }
        public Lijn RijdOpLijn { get { return _rijdOpLijn; } }
        public DateTime Vertrektijd { get { return _vertrektijd; } }
        public string BestuurderNaam { get { return _bestuurderNaam ; } }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this._busNr = busNr;
            this._rijdOpLijn = lijn;
            this._vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
            this._bestuurderNaam = _voornaam + " " + _achternaam;
        }

        public void StapIn()
        {
            _aantalPassagiers++;
        }

        public void StapUit()
        {
            _aantalPassagiers--;
        }
    }

    public class Lijn
    {
        private int _lijnNr;
        private List<Halte> _haltes;

        public int LijnNr { get { return _lijnNr; } }
        public List<Halte> Haltes { get { return _haltes; } }

        public Lijn(int lijnNr)
        {
            this._lijnNr = lijnNr;
            _haltes = new List<Halte>();
        }

        public void AddHalte(Halte halte)
        {
            _haltes.Add(halte);
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromSeconds(0);

            foreach(Halte element in _haltes)
            {
                t += element.Reisduur;
            }

            return t;
        }
        #endregion
    }

    public class Halte
    {
        private string _naam;
        private TimeSpan _reisduur;

        public string Naam { get { return _naam; } }
        public TimeSpan Reisduur { get { return _reisduur; } }

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
        private string[] _array;
        private int _count;
        private int _index;

        public bool IsEmpty
        {
            get
            {
                return _count == 0;
            }
        }

        public HalteStack()
        {
            _array = new string[20];
            _count = 0;
            _index = -1;
        }

        public void Push(string halteNaam)
        {
            _index++;
            _array[_index] = halteNaam;
            _count++;
        }

        public string Pop()
        {
            string result = _array[_index];
            _array[_index] = null;
            _index--;
            _count--;

            return result;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string temp1;
            string result = "";

            for (int i = _index; i >= 0 ; i--)
            {
                temp1 = _array[i];
                result = result + temp1;

                if (i != 0)
                {
                    result = result + ",";
                }
            }

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
                int count = 0;
                HalteLinked temp = top;

                while (temp != null)
                {
                    temp = temp.Volgende;
                    count++;
                }

                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            var result = top.Reisduur;
            HalteLinked temp = top.Volgende;

            while (temp != null)
            {
                result += temp.Reisduur;
                temp = temp.Volgende;
            }

            return result;
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
