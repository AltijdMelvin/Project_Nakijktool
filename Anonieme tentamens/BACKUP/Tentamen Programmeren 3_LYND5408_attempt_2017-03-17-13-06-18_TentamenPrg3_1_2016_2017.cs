using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


Voornaam: GIL
Achternaam: LYNDA
StudentNr: s54084
Klas: i1c

//!-----------------------!
//De TestCases staan na de vraag in de region. De region kan je uitklappen
//Let op sommige testcases hebben de code in commentaar staan, 
//uncomment dit dan alvorens de test te draaien
//!-----------------------!
namespace TentamenPrg3_1_2016_2017
{
    #region Vraag1
    //Vraag 1A
    // Bij het aanmaken van een instantie van een class kan je parameters meegeven als dat is gedefinieerd in de constructor van die class, als er dan parameters zijn meegegeven kan daarmee in de constructor van de class wat mee worden gedaan, bijvoorbeeld het toekennen aan een prive variable binnen de class
    //

    //Vraag 1B
    // Lijn( int lijnNr )
    // Halte( string naam, TimeSpan reisduur )
    // Bus( int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam )

    //Vraag 1C
    // Met een property variable kan je meegeven wat voor waardes mogen worden toegekend aan de variable doormiddel van getters en setters, met een instantie variable kan dat niet, wel kan je een property koppelen met een instantie
    // private int something = 0;
    // public int Something {
    //      get { return something; }
    //      set { something = value; }
    // }
    // ^^^^^ voorbeeld om met een property variable een instantie variable aan te passen

    #endregion

    #region Vraag2
    public class Bus
    {
        // properties aanmaken
        public int BusNr { get; set; }
        public int AantalPassagiers { get; set; }
        public Lijn RijdOpLijn { get; set; }
        public DateTime Vertrektijd { get; set; }
        public string BestuurderNaam { get; set; }

        // variables aanmaken
        private string _voornaam;
        private string _achternaam;

        public Bus( int busNr, Lijn lijn, DateTime vertrektijd, string voornaam, string achternaam )
        {
            // parameters toekennen aan de variables van de class
            this.BusNr = busNr;
            this.RijdOpLijn = lijn;
            this.Vertrektijd = vertrektijd;
            this._voornaam = voornaam;
            this._achternaam = achternaam;
            this.BestuurderNaam = voornaam + " " + achternaam;
        }

        public void StapIn( )
        {
            // aantal passagiers van de bus omhoog doen als er iemand instapt
            AantalPassagiers++;
        }

        public void StapUit( )
        {
            // aantal passagiers van de bus omlaag doen als er iemand uitstapt
            AantalPassagiers--;
        }
    }

    public class Lijn
    {
        // properties en variables aanmaken
        private List<Halte> haltes = new List<Halte>();

        public int LijnNr { get; set; }
        public List<Halte> Haltes
        {
            get { return haltes; }
            set { haltes = value; }
        }
        public Lijn( int lijnNr )
        {
            // parameter toekennen aan de variable van de class
            this.LijnNr = lijnNr;
        }

        public void AddHalte( Halte halte )
        {
            // een halte toevoegen aan de lijst met haltes
            Haltes.Add( halte );
        }

        //Vraag 3 Hier invullen!
        #region Vraag3
        public TimeSpan Reistijd()
        {
            // variable aanmaken waar de totale reisduur in wordt bewaard
            TimeSpan reisduur = TimeSpan.FromSeconds( 0 );

            foreach( Halte halte in Haltes )
            {
                // de reisduur van de huidige halte toevoegen aan de totale reistijd
                reisduur += halte.Reisduur;
            }

            // reisduur teruggeven
            return reisduur;
        }
        #endregion
    }

    public class Halte
    {
        // properties en variables aanmaken
        public string Naam { get; set; }
        public TimeSpan Reisduur { get; set; }

        public Halte( string naam, TimeSpan reisduur )
        {
            // parameters toekennen aan variables van de class
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
        // stack variable aanmaken waar de string variables in kunnen worden gestopt
        private string[] stack = new string[20];
        // top variable aanmaken om bij te houden waar de volgende waarde moet worden opgeslagen
        private int top = 0;

        public bool IsEmpty
        {
            get
            {
                return top == 0; // returned true als de top 0 is(dus lege stack) en anders false
            }
        }

        public HalteStack()
        {
            //Je mag de constructor ook leeg laten indien je deze niet nodig hebt - constructor is niet nodig
        }

        public void Push(string halteNaam)
        {
            // item toevoegen aan de stack
            stack[ top ] = halteNaam;
            // top variable 1 omhoog doen zodat het volgende item deze niet overwrite
            top++;
        }

        public string Pop()
        {
            // omdat de top variable altijd 1 hoger is dan de laatste waarde moeten we de variable eerst met 1 verkleinen en dan teruggeven
            --top;
            return stack[ top ];
        }

        //Vraag 4b
        public string PrintHaltesReversed()
        {
            // nieuwe string aanmaken waar het resultaat in wordt gestopt
            string output = "";

            // net zolang door de stack loopen totdat er niks meer in zit
            while( top > 0 )
            {
                // de waarde van het huidige item toevoegen aan de output string
                output += Pop() + ",";
            }

            // de laatste ',' eruit halen zodat de strings overeenkomen
            output = output.Remove( output.Length - 1, 1 );

            // de output teruggeven
            return output;
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
        // variables en properties aanmaken
        public string Naam { get; }
        public TimeSpan Reisduur { get; set; }
        public HalteLinked Volgende { get; set; }

        public HalteLinked(string naam, TimeSpan reisduur)
        {
            // parameters toekennen aan de variables van de class
            Naam = naam;
            Reisduur = reisduur;
        }
    }

    public class LijnLinked
    {
        // variables en properties aanmaken
        private HalteLinked top = null;
        public int LijnNr { get; set; }

        public LijnLinked(int lijnNr, HalteLinked halte)
        {
            // parameters toekennen aan de variables van de class
            LijnNr = lijnNr;
            top = halte;
        }

        //Vraag 5A
        public int Count
        {
            get
            {
                // als de top null is zitten er nog geen items in de stack en dus geven we 0 terug
                if (top == null)
                    return 0;

                // variable om aantal items in te stoppen
                int count = 0;
                // tmp variable aanmaken zodat we de globale top variable niet per ongeluk aanpassen
                HalteLinked tmp = top;

                // zolang tmp nog niet null is is er dus nog een item en blijven we doorgaan met de loop
                while( tmp != null )
                {
                    // aantal items met 1 verhogen
                    count++;
                    // het volgende item toekennen aan de tmp variable
                    tmp = tmp.Volgende;
                }

                // aantal items teruggeven
                return count;
            }
        }

        //Vraag 5B
        public TimeSpan Reistijd()
        {
            // globale variable aanmaken om de totale reistijd aan toe te kennen
            TimeSpan reistijd = TimeSpan.FromMinutes(0);

            // tmp variable aanmaken om de top aan toe te kennen zodat we niet per ongeluk de top aanpassen
            HalteLinked tmp = top;

            // zolang het item niet null is moet de loop doorgaan
            while (tmp != null)
            {
                // de reistijd van het huidige item bij de reistijd van de rest optellen
                reistijd += tmp.Reisduur;
                // het volgende item toekennen aan de tmp variable
                tmp = tmp.Volgende;
            }

            // de totale reistijd teruggeven
            return reistijd;
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
