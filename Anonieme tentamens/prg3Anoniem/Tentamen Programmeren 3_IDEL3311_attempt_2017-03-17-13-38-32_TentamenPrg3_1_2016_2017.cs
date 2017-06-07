using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: REGENA
Achternaam: IDELLA
StudentNr: s33118
Klas: i1a

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    //Ik heb de naam van TestVraag2 en TestVraag3 naar Test_Vraag2 en Test_Vraag3 veranderd zodat
    //de volgorde in de Test Explorer goed is.

    #region Vraag1
    //Vraag 1A
    //Deze wordt aangeroepen om een nieuw object aan te maken. Hieraan worden de verplichte gegevens meegegeven, en 
    //eventueel optionele gegevens.

    //Vraag 1B
    //Bus(busNr:int, lijn:Lijn, vertrektijd:DateTime, voornaam:string, achternaam:string)
    //Lijn(lijnNr:int)
    //Halte(naam:string, reisduur:TimeSpan)

    //Vraag 1C
    //Een instantie variabele is (als je het goed doet) altijd private, en kan daardoor niet van buitenaf gelezen of aangepast worden.
    //Een property heeft een get en set, waarmee een waarde opgevraagd kan worden, of aangepast. Ook kunnen er dan eisen aan de
    //aanpassing gesteld worden. Denk hierbij aan: if(x - input >){x -= input}
    #endregion

    #region Vraag2
    public class Bus
    {
        //de _voornaam en _achternaam variabelen waren vrijwel compleet zinloos, dus ik heb de
        //BestuurderNaam property aangepast zodat deze de _voornaam en _achternaam gebruikt bij
        //het aanroepen. De manier hoe ik het eerst had heb ik als comments laten staan.

        private string _voornaam;
        private string _achternaam;

        public int BusNr { get; set; }
        public int AantalPassagiers { get; set; }
        public Lijn RijdOpLijn { get; set; }
        public DateTime Vertrektijd { get; set; }
        //public string BestuurderNaam { get; set; }
        public string BestuurderNaam
        {
            get
            {
                return _voornaam + " " + _achternaam;
            }
        }

        public Bus(int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            BusNr = busNr;
            RijdOpLijn = lijn;
            Vertrektijd = vertrektijd;
            _voornaam = voornaam;
            _achternaam = achternaam;
            //BestuurderNaam = _voornaam + " " + _achternaam;
        }

        public void StapIn()
        {
            AantalPassagiers += 1;
        }
        public void StapUit()
        {
            if (AantalPassagiers > 0)
            {
                AantalPassagiers -= 1;
            }
        }
    }

    public class Lijn
    {
        private List<Halte> haltes = new List<Halte>();

        public int LijnNr { get; set; }
        public List<Halte> Haltes { get { return haltes; } }

        public Lijn(int lijnNr)
        {
            LijnNr = lijnNr;
        }

        public void AddHalte(Halte halte)
        {
            Haltes.Add(halte);
        }


        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            TimeSpan totalReistijd = TimeSpan.Zero;

            foreach (Halte h in haltes)
            {
                totalReistijd += h.Reisduur;
            }

            return totalReistijd;
        }
        #endregion
    }

    public class Halte
    {
        public string Naam { get; set; }
        public TimeSpan Reisduur { get; set; }

        public Halte(string naam, TimeSpan reisduur)
        {
            Naam = naam;
            Reisduur = reisduur;
        }
    }

    public class TestCasesVraag2
    {
        [Test]
        public void Test_Vraag2()
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
        public void Test_Vraag3()
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
        string[] hs = new string[20];
        int count = -1;

        public bool IsEmpty
        {
            get
            {
                if (hs[0] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig bent
        }

        public void Push(string halteNaam)
        {
            count++;
            hs[count] = halteNaam;
        }

        public string Pop()
        {
            
            string result = hs[count];
            hs[count] = null;
            count--;
            return result;
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            string resultString = "";

            for (int i = count; i > -1; i--)
            {
                resultString += hs[i];
                if (i > 0)
                {
                    resultString += ",";
                }
            }
            return resultString;
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
                HalteLinked current = top;
                while (current != null)
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
            TimeSpan totalReistijd = TimeSpan.Zero;
            HalteLinked current = top;
            while (current != null)
            {
                totalReistijd += current.Reisduur;
                current = current.Volgende;
            }
            return totalReistijd;
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
