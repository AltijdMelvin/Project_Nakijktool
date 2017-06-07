using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: ENID
Achternaam: TANNA
StudentNr: s52936
Klas: i1e

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    // Een constructor is een speciale methode om een instantie aan te maken. Dit heeft het doel om voor duidelijkheid en minder typwerk te zorgen.
    //

    //Vraag 1B
    //De constructors in de bovenstaande klassendiagram zijn: Lijn, AddHalte, Reistijd, Bus, Stapin, Stapuit en Halte
    // 

    //Vraag 1C
    // Bij een instantievariabele geef je de variabele meteen een waarde en kun je deze later veranderen bijvoorbeeld private string naam.
    // Bij een property hoort een get; set; (read/write). Hierbij kun je de waarde aanpassen op basis van een andere waarde die je toekent aan de property.
    #endregion

    #region Vraag2
    public class Bus
                        //Behalve de testcases met de list van haltes is de test goed.
    {
        private int aantal;

        public int BusNr
        {
            get
            {
                return _busNr;
            }
        }
        public void StapIn()
        {
            aantal++;
        }
        public void StapUit()
        {
            aantal--;
        }

        public int AantalPassagiers
        {
            get
            {
                return aantal;
            }
        }

        public Lijn RijdOpLijn
        {
            get
            {
                return _lijn;
            }
        }
        public DateTime Vertrektijd
        {
            get
            {
                return _vertrektijd;
            }
        }
        public string BestuurderNaam
        {
            get {
                    return naam;
                }
        }

        private string naam;
        private int _busNr;
        private Lijn _lijn;
        private DateTime _vertrektijd;

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            naam = voornaam + " " + achternaam;
            this._busNr = busNr;
            this._lijn = lijn;
            this._vertrektijd = vertrektijd;
        }
        
    }

    public class Lijn
    {
        public TimeSpan tijd = TimeSpan.FromSeconds(0);

        private int _lijnNr;
        private List<Halte> Halten = new List<Halte>();
        private int halteteller = 0;

        public int LijnNr
        {
            get
            {
                return _lijnNr;
            }
        }
        public List<Halte> Haltes
        {
            get
            {
                return Halten;
            }
        }

        public Lijn(int lijnNr)
        {
            this._lijnNr = lijnNr;
        }

        public void AddHalte(Halte halte)
        {
            Halten[halteteller] = halte;
            halteteller++;
        }

        //Vraag 3 Hier invullen!
        #region Vraag3      
        public TimeSpan Reistijd()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class Halte
    {
        public string _naam;
        private TimeSpan _reisduur;
        public string Naam
        {
            get
            {
                return _naam;
            }
        }
        public TimeSpan Reisduur
        {
            get
            {
                return _reisduur;
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
            Assert.AreEqual("Bushalte Wissesdwinger", bus.RijdOpLijn.Haltes[2].Naam);         //werkt niet
            Assert.AreEqual(TimeSpan.FromMinutes(2), bus.RijdOpLijn.Haltes[2].Reisduur);      //werkt niet

            Assert.AreEqual(0, bus.AantalPassagiers);
            bus.StapIn();
            bus.StapIn();
            Assert.AreEqual(2, bus.AantalPassagiers);
            bus.StapUit();
            Assert.AreEqual(1, bus.AantalPassagiers);
            bus.StapUit();
            Assert.AreEqual(0, bus.AantalPassagiers);

            Assert.AreEqual(6, bus.RijdOpLijn.Haltes.Count);                                  //werkt niet

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
        Stack<string> stapel = new Stack<string>();

        public bool IsEmpty
        {
            get
            {
                if (stapel.Count > 0)       //als de count van de stapel groter is dan 0 dan betekent dat dat hij niet leeg is
                {
                    return false;
                } // is hij wel 0 dan is de stapel leeg
                return true;
            }
        }

        public HalteStack()
        {
            //foreach (string naam in stapel)
            //    if (naam == Halte._naam)
            //    {
            //        Pop();
            //    }
            //    else Push(Halte._naam);
        }

        public void Push(string halteNaam)
        {
            stapel.Push(halteNaam);
        }

        public string Pop()
        {
            //stapel.Pop();
            throw new NotImplementedException();
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string printreversed = "";
            while (IsEmpty == false)
            {
                printreversed += stapel.Pop();
                if (stapel.Count > 0)
                { printreversed += ","; }
            }
                return printreversed;
            
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
        private int teller = 0;

        //Vraag 5A
        public int Count
        {
            get
            {
                return teller;
            }
            set
            {
                foreach (LijnLinked)
                {
                    teller++;
                }
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            throw new NotImplementedException();        //?
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
