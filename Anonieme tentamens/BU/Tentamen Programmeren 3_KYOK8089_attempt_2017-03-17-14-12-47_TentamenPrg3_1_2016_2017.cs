using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


//Voornaam: BENNETT
//Achternaam: KYOKO
//StudentNr: s80899
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
    //een methode die zorgt voor het opbouw van het object
    //

    //Vraag 1B
    //Bus,Lijn,Halte
    //

    //Vraag 1C
    //property's zijn public terwijl instantie variable private zijn
    //
    #endregion

    #region Vraag2
    public class Bus
    {
        private string voornaam;
        private string achternaam;
        public int Busnr
        {
            get;
            set;
        }
        public int AantalPassagiers
        {
            get;
            set;
        }
        public Lijn Rijdoplijn
        {
            get;
            set;
        }
        public DateTime Vertrektijd
        {
            get;
            set;
        }
        public string BestuurderNaam
        {
            get;
            set;
        }

        public Bus (int busnr,Lijn lijn, DateTime vertrektijd, string voornaam,string achternaam)
        {
            this.Busnr = busnr;
            this.Rijdoplijn = lijn;
            this.Vertrektijd = vertrektijd;
            this.voornaam = voornaam;
            this.achternaam = achternaam;
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
        public int Lijnnr
        {
            get;
            set;
        }
        public List<Halte> Haltes
        {
            get;
            set;
          
        }
        public void AddHalte(Halte halte)
        {
            //Haltes.Add(halte);
            //List initialiseren om haltes op te slaan
        }
        public Lijn(int lijnnr)
        {
            this.Lijnnr = lijnnr;
        }
        public TimeSpan ReisTijd()
        {
            
            TimeSpan t = TimeSpan.FromSeconds(0);
            t += Halte.reisduur;
            return t;

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
        public string Naam
        {
            get;
            set;
        }
        public TimeSpan Reisduur
        {
            get;
            set;
        }
        public Halte (string naam, TimeSpan reisduur)
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

            //DateTime vertrek = new DateTime(2016, 09, 12, 14, 26, 0);

            //Lijn lijn = new Lijn(612);
            //Bus bus = new Bus(10, lijn, vertrek, "Joris", "Lops");

            //lijn.AddHalte(new Halte("Bushalte NHL Hogeschool", TimeSpan.Zero));
            //lijn.AddHalte(new Halte("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1)));
            //lijn.AddHalte(new Halte("Bushalte Wissesdwinger", TimeSpan.FromMinutes(2)));
            //lijn.AddHalte(new Halte("Bushalte Harmonie", TimeSpan.FromMinutes(3)));
            //lijn.AddHalte(new Halte("Bushalte Zaailand", TimeSpan.FromMinutes(1)));
            //lijn.AddHalte(new Halte("Bushalte Busstation", TimeSpan.FromMinutes(5)));


            //Assert.AreEqual("Joris Lops", bus.BestuurderNaam);
            //Assert.AreEqual(612, bus.RijdOpLijn.LijnNr);
            //Assert.AreEqual("Bushalte Wissesdwinger", bus.RijdOpLijn.Haltes[2].Naam);
            //Assert.AreEqual(TimeSpan.FromMinutes(2), bus.RijdOpLijn.Haltes[2].Reisduur);

            //Assert.AreEqual(0, bus.AantalPassagiers);
            //bus.StapIn();
            //bus.StapIn();
            //Assert.AreEqual(2, bus.AantalPassagiers);
            //bus.StapUit();
            //Assert.AreEqual(1, bus.AantalPassagiers);
            //bus.StapUit();
            //Assert.AreEqual(0, bus.AantalPassagiers);

            //Assert.AreEqual(6, bus.RijdOpLijn.Haltes.Count);

            //Assert.AreEqual(10, bus.BusNr);

            //Assert.AreEqual(612, bus.RijdOpLijn.LijnNr);

            //Assert.AreEqual(new DateTime(2016, 09, 12, 14, 26, 0), bus.Vertrektijd);

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

            //DateTime vertrek = new DateTime(2016, 09, 12, 14, 26, 0);

            //Lijn lijn = new Lijn(612);
            //Bus bus = new Bus(10, lijn, vertrek, "Joris", "Lops");

            //lijn.AddHalte(new Halte("Bushalte NHL Hogeschool", TimeSpan.Zero));
            //lijn.AddHalte(new Halte("Bushalte Stenden Hogeschool", TimeSpan.FromMinutes(1)));
            //lijn.AddHalte(new Halte("Bushalte Wissesdwinger", TimeSpan.FromMinutes(2)));
            //lijn.AddHalte(new Halte("Bushalte Harmonie", TimeSpan.FromMinutes(3)));
            //lijn.AddHalte(new Halte("Bushalte Zaailand", TimeSpan.FromMinutes(1)));
            //lijn.AddHalte(new Halte("Bushalte Busstation", TimeSpan.FromMinutes(5)));

            //TimeSpan reistijd = lijn.Reistijd();
            //Assert.AreEqual(TimeSpan.FromMinutes(12), reistijd);

            //DateTime aankomst = vertrek + reistijd;
            //Assert.AreEqual(new DateTime(2016, 09, 12, 14, 38, 0), aankomst);

        }
    }
    #endregion

    #region Vraag4
    public class HalteStack
    {
        HalteStack[] s = new HalteStack[20];
        int top = -1;
        public bool IsEmpty
        {

            get
            {
                return true;
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            top++;
            s[top].Push(halteNaam);
        }

        public string Pop()
        {
            top--;
            HalteStack temp = s[top + 1];
            s[top + 1] = null;
            return temp.ToString();

        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            int index = 0;
            string Reversed = "";
            while (index >= 0 && s != null)
            {
                Reversed += s[index] + " ";
                Pop();
                index++;
            }
            return Reversed;
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
        private int count = 0;
        TimeSpan sum;
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
                
                while(top != null)
                {
                    count++;
                }
                return count;
                //throw new NotImplementedException();
            }

        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            //het binnen halen van de tijd value in halte
            //daarna met die value het bijelkaar optellen met een bijv een int en dan +=
            // uiteindelijk kom je op een totaal uit en die return je dan
            TimeSpan sum = TimeSpan.FromMinutes(0);
            while(top != null)
            {
                sum += top.Reisduur;
            }
            return sum;
            //throw new NotImplementedException();
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
