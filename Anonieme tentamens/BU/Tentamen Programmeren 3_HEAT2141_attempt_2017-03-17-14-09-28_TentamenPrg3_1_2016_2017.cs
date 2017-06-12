using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


//Voornaam: JERROD
//Achternaam: HEATH
//StudentNr: s21415
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
    //Het doel van een contructor is om een bepaald object te maken met de meegegeven argumenten
    //Hierdoor is het makkelijker om objecten te maken.

    //Vraag 1B
    // Lijn (lijnNr)
    // Bus(busNr, lijn, vertrektijd, voornaam, achternaam)
    // Halte (naam, reisduur)

    //Vraag 1C
    // Een instantie variabele wordt aan het begin van een class aangemaakt, altijd private. deze kun je van buitenaf niet aanpassen.
    // Een property is een read of write only methode, met een property kun je met veiligheid bepaalde variabelen aanpassen.
    //
    #endregion

    #region Vraag2
    public class Bus
    {
        private string _voornaam;
        private string _achternaam;
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

        public Bus (int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
                   
            this._achternaam = achternaam;
            this._voornaam = voornaam;

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


        public int LijnNr
        {
            get
            {
                return lijnNr ;
            }

        }

        public List<Halte> Haltes
        {
            get;

        }

        public Lijn(int lijnNr)
        {
           
        }

        public void AddHalte(Halte halte)
        {
            Haltes.Add(halte);

        }

        

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan t = TimeSpan.FromMinutes(0);
            foreach (var h in Haltes)
            {
               
            }

            return t;

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
        public string[] arraystack = new string[20];


       
        int count = 0;

        public bool IsEmpty
        {
            get
            {
                return count <= 0;
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            arraystack[count] = halteNaam;
            count++;



        }

        public string Pop()
        {
            string popped;
            arraystack[count-1].Remove(count-1);
            popped = arraystack[count-1];
            count--;
            return popped;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            //string result;
            // for (int i = arraystack.Length; i == 0; i--)
            // {
            //     result += arraystack[i];
            //     arraystack[i].Remove(i);

            // }
            // return result;
            throw new NotFiniteNumberException();
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
            get;
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
