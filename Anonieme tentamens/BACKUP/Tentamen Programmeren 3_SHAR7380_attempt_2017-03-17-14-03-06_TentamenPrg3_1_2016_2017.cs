using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: SIOBHAN
Achternaam: SHARLENE
StudentNr: s73800
Klas: i1e
//Datum: 17-03-17

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    //We often need to have different ways of instantiating a class. 
    //We might use the class in many places. 
    //With overloading, we can add many entry points for creating the class. 

    //Vraag 1B
    //Bus(busNr:int ...)
    //Lijn(lijnNr:int)
    //Halte(naam: string, reisduur: TimeSpan)


    //Vraag 1C
    //Property is public, dus kan aangepast worden 
    //Instantie variabele is private, dus wil je liever niet aan gepast hebben (wel beschermd hebben)
    #endregion

    #region Vraag2
    public class Bus
    {
        public int BusNr
        {
            get
            {
                return this.BusNr;
            }
            set
            {
                this.BusNr = value;
            }
        }
        public int AantalPassagiers
        {
            get
            {
             return this.AantalPassagiers;
            }
            set
            {
            this.AantalPassagiers = value;
            }
        }   
        public Lijn RijdOpLijn
        {
            get
            {
                return this.RijdOpLijn;
            }
            set
            {
                this.RijdOpLijn = value;
            }
        }
        public DateTime Vertrektijd
        {
            get
            {
                return this.Vertrektijd;
            }
            set
            {
                this.Vertrektijd = value;
            }
        }
        public string BestuurderNaam
        {
            get
            {
                return this.BestuurderNaam;
            }
            set
            {
                this.BestuurderNaam = value;
            }
        }
        private string _voornaam;
        private string _achternaam; 

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            this.BusNr = busNr;
            this.RijdOpLijn = lijn;
            this.Vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam; 
        }

        public void StapIn()
        {

        }

        public void StapUit()
        {

        }
    }

    public class Lijn
    {
        public int LijnNr
        {
            get
            {
                return this.LijnNr;
            }
            set
            {
                this.LijnNr = value;
            }
        }
        public List<Halte> Haltes = new List<Halte>();

        public Lijn(int lijnNr)
        {
            this.LijnNr = lijnNr;
        }

        public void AddHalte(Halte halte)
        {
                
        }
        
        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromSeconds(0);
            
            for (int i = 0; i < Haltes.Count; i++)
            {
                t = t + reisduur; //Ik weet niet meer hoe je een property mee kan geven aan een andere klasse 
            }
            return t; 
        }
        #endregion
    }

    public class Halte
    {
        public string Naam
        {
            get
            {
                return this.Naam;
            }
            set
            {
                this.Naam = value;
            }
        }
        public TimeSpan Reisduur
        {
            get
            {
                return this.Reisduur;
            }
            set
            {
                this.Reisduur = value;
            }
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
            */
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
                if (stack = null)
                {
                    return IsEmpty;
                }
                else
                {
                    return !IsEmpty; 
                }
            }
        }

        public HalteStack()
        {
            int[] values = new int[20];
            var stack = new Stack<int>(values);
        }

        public void Push(string halteNaam)
        {
            for (int i = 0; i < Halte.; i++)         //iets als length of count achter .
            {
                stack.Push(naam);       //Ik weet niet meer hoe je iets uit een constructor van een andere klasse kan meegeven
            }
            return stack; 
        }

        public string Pop()
        {
            for (int i = 0; i < Halte.; i++)         //iets als length of count achter .
            {
                stack.Pop(naam);       //Ik weet niet meer hoe je iets uit een constructor van een andere klasse kan meegeven
            }
            return stack;
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
                int count = 0; 
                for(int i = 0; i <= List.top; i++)      //gaat door tot het laatste element (index) 
                {
                    count++; 
                }
                return count; 
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            throw new NotImplementedException();
            //is dit niet de zelfde vraag als 3?
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
