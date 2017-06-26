using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tentamens;

//Voornaam: 
//Achternaam:
//StudentNr:
//Klas:
//InleverCode:

namespace TentamenPrg3_1_2016_2017
{
    //Vraag 1 punten 10
        //Vraag 1A
        //
        //

        //Vraag 1B
        //
        //

        //Vraag 1C
        //
        //

    //Vraag 2 punten 20
    public class Bus 
    {
        public int BusNr { get; }
        public int AantalPassagiers { get; private set; }
        public Lijn RijdOpLijn { get; }
        public DateTime Vertrektijd { get; }
        private string _voornaam;
        private string _achternaam;

        public string BestuurderNaam
        {
            get { return $"{_voornaam} {_achternaam}"; }
        }

        public Bus(int busNr, Lijn rijdOpLijn, DateTime vertrektijd, string voornaam, string achternaam)
        {
            if (rijdOpLijn == null) throw new ArgumentNullException(nameof(rijdOpLijn));
            if (voornaam == null) throw new ArgumentNullException(nameof(voornaam));
            if (achternaam == null) throw new ArgumentNullException(nameof(achternaam));

            BusNr = busNr;
            RijdOpLijn = rijdOpLijn;
            Vertrektijd = vertrektijd;
            AantalPassagiers = 0;
            _voornaam = voornaam;
            _achternaam = achternaam;
        }

        public void StapIn()
        {
            AantalPassagiers++;
        }

        public void StapUit()
        {
            AantalPassagiers--;
        }
    }

    //Vraag 3 punten 15
    public class Lijn
    {
        //twee varianten!
        public int LijnNr { get; }

        private List<Halte> _haltes;

        public List<Halte> Haltes 
        {
            get { return _haltes; }
        } 

        public Lijn(int lijnNr)
        {
            LijnNr = lijnNr;
            _haltes = new List<Halte>();
        }

        public void AddHalte(Halte halte)
        {
            if (halte == null) throw new ArgumentNullException(nameof(halte));

            _haltes.Add(halte);
        }

        //Vraag 2
        public TimeSpan Reistijd()
        {
            return _haltes
                .Aggregate(TimeSpan.Zero,
                    (current, halte) => current + halte.Reisduur);

            TimeSpan t = TimeSpan.Zero;
            foreach (var halte in _haltes)
            {
                t += halte.Reisduur;
            }
            return t;
        }
    }

    public class Halte
    {
        public string Naam { get; }

        public Halte(string naam, TimeSpan reisduur = default(TimeSpan))
        {
            Naam = naam;
            Reisduur = reisduur;
        }

        public TimeSpan Reisduur { get; set; }
    }
    //Eind Vraag 2

    //Vraag 4 punten 20
    public class HalteStack
    {
        private string[] stack = new string[20];
        private int index = 0;

        public bool IsEmpty
        {
            get { return index == 0; }
        }

        public void Push(string halteNaam)
        {
            stack[index] = halteNaam;
            index++;
        }

        public string Pop()
        {
            string halteNaam = stack[index];
            index--;
            return halteNaam;
        }

        public string PrintHaltesReversed()
        {
            string reversed = "";
            while (!IsEmpty)
            {
                string halteNaam = Pop();

                reversed += halteNaam;

                if (!IsEmpty)
                {
                    reversed += ",";
                }
            }
            return reversed;
        }
    }

    public class HalteLinked
    {
        public string Naam { get; }

        public HalteLinked(string naam, TimeSpan reisduur)
        {
            Naam = naam;
            Reisduur = reisduur;
        }

        public TimeSpan Reisduur { get; set; }
        public HalteLinked Volgende { get; set; }
    }

    //Vraag 5 punten 25
    public class LijnLinked
    {
        private HalteLinked _begin = null;
        public int LijnNr { get; set; }

        public HalteLinked Lijn
        {
            get { return _begin; }
        }

        public LijnLinked(int lijnNr, HalteLinked halte)
        {
            LijnNr = lijnNr;
            _begin = halte;
        }

        //Vraag 5A
        public void RemoveLastHalte()
        {
            HalteLinked current = _begin;

            while (current != null)
            {
                if (current.Volgende == null)
                {
                    _begin = null;
                }

                if (current.Volgende != null && 
                    current.Volgende.Volgende == null)
                {
                    current.Volgende = null;
                    break;
                } 

                current = current.Volgende;
            }
        }

        //Vraag 5B
        public int Count
        {
            get
            {
                int cnt = 0;
                HalteLinked current = _begin;
                while (current != null)
                {
                    cnt++;
                    current = current.Volgende;
                }
                return cnt;
            }
        }

        public object Reistijd()
        {
            throw new NotImplementedException();
        }
    }

    //Testcases voor de verschillende vragen
    public class ProefTentamen_2016_2017
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

        public class TestCasesVraag4
        {
            [Test]
            public void TestVraag4A()
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
            public void TestVraag4B()
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



        [Test]
        public void TestVraag5A()
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
        public void TestVraag5B()
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
}
