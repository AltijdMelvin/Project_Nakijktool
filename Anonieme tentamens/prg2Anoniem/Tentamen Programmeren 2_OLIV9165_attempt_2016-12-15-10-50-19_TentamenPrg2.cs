using System;
using System.Collections.Generic;
using NUnit.Framework;

//Voornaam: RAINA
//Achternaam: OLIVE
//StudentNr: s91651
//Klas: i1a
//Datum:15-12-2016
namespace Tentamens20162017
{
    class TentamenPrg2Tentamen20162017
    {
        //Vraag 1 (1.5pt)
        //Maak een methode die bepaalt of gegeven twee string of de strings elkaars omgekeerde zijn of niet.
        //“aap”, “paa” --> true
        //“aap”, “paap” --> false
        //“container”, “reniatnoc” --> true
        //“aap”, “gaa” --> false
        //“”, “blaat” --> false

        [Test]
        public void Test_Vraag1()
        {
            Assert.AreEqual(true, Vraag1("aap", "paa"));
            Assert.AreEqual(false, Vraag1("aap", "paap"));
            Assert.AreEqual(true, Vraag1("container", "reniatnoc"));
            Assert.AreEqual(false, Vraag1("aap", "gaa"));
            Assert.AreEqual(false, Vraag1("", "blaat"));
        }

        public bool Vraag1(string s1, string s2)
        {
            //Ik had hier als idee om een empty string aan te maken en string s1 te converten naar een character  array.
            //Deze character arrays wilde ik van achter naarvoren vullen in de empty string dus dan komen de characters in deze string.
            // Dus Aap > chararray > A A P > Andersom in een loop in de empty string > P + A + A = 
            // "PAA". Dan wilde ik kijken of PAA overeenkwam met s2 andersom. Als dit overeenkwam return true. Zo niet return false.
            //Ook checkte ik in het begin de lengte van de strings s1/s2 in chararrays om te zien of de lengte wel hetzelfde was. Zo niet return false.
            string test1 = String.Empty;
            char[] s4 = s2.ToCharArray();
            char[] s3 = s1.ToCharArray();
            if (s4.Length == s3.Length)
            {
                for (int i = s3.Length; i > 0; i--)
                {
                    test1 = test1 + s3[i];
                }
                if (test1 == s2)
                {
                    return true;
                }
            }
            return false;
        }

        //Vraag 2 (1.5pt)  
        //Gegeven is een array met getallen. We beginnen bij index 0. 
        //Het getal op de desbetreffende positie wordt de volgende index. 
        //Vervolgens wordt de waarde op deze index de nieuwe index, enzovoorts.
        //Zo ontstaat een pad hoe we door de array lopen. 
        //Als we op een gegeven moment de waarde -1 vinden zijn we klaar en is het een eindige pad (true). 
        //Echter soms komt het voor dat we buiten de array lopen en dan is het een niet eindige pad (false).
        //{1,3,-1,2} --> true
        //{1,3,6,2} --> false
        //Opmerking: voor het gemak mag je ervanuit gaan dat het pad altijd eindig is 
        //(of je vindt de -1 of komt een index tegen buiten de array). 
        //Voor de laatste 0.5 punt, pas de methode zo aan dat je ook rekening houdt met oneindige paden 
        //zoals hieronder:
        //{1,3,-1,1} --> false
        //Commentarieer de laatste regel in de test hiervoor uit!
        [Test]
        public void Test_Vraag2()
        {
            Assert.AreEqual(true, Vraag2(new int[] { 1, 3, -1, 2 }));
            Assert.AreEqual(false, Vraag2(new int[] { 1, 3, 6, 2 }));

            //Voor de laatste 0.5 punt
            //Assert.AreEqual(false, Vraag2(new int[] { 1, 3, -1, 1 }));
        }

        private bool Vraag2(int[] xs)
        {
            int index = 0; //Maak een index aan
            int initialLength = xs.Length; //pak de lengte van de array hoe lang hij aan het begin was.
            int breakLength = 0; //Hiermee breken we de loop als de array een langer dan de initial length doorgaat.
            for (int i = 0; i > xs.Length;)
            {
                index = xs[index]; // index = het cijfer op de index, dus (false, Vraag2(new int[] { 1, 3, -1, 1 })); begin bij 0, die wordt 1.
                breakLength++; // breaklength 1 erbij.
                if (index == -1)//kijken of het antwoord bij index -1 is? Zowel, dan return true.
                {
                    return true;
                }
                if (breakLength == initialLength)//1 bij breaklength totdat de beginlengte van de array gevonden is om een infinite loop te voorkomen.
                {
                    goto stop;
                }
                i++;// 1 bij i op het laatst.
            }
            stop:
            return false;//als de beginlengte (breakLength==initialLength)gevonden is of als -1 niet gevonden is return false.
        }

        //Vraag 3 (2pt)
        //Joris wil metroloog worden. Hij meet de tempratuur van de lucht meerdere keren per dag. 
        //Echter zijn metingen zijn niet heel precies. 
        //Joris denkt dat hij zijn grafiek mooier kan maken (smoothen) als hij het gemiddelde berekent met de twee buur metingen.
        //Het volgende is gemeten:
        //3 5 6 4 5 
        //Het tweede element (5) wordt vervangen door(3 + 5 + 6) / 3 = 4.66666666667,
        //Het derde element (6) wordt vervangen door(5 + 6 + 4) / 3 = 5,
        //Het vierde element (4) wordt vervangen door(6 + 4 + 5) / 3 = 5.
        //Het eerste en laatste getal worden niet aangepast.
        //Maak een method die de input array mooier maakt (smooth). 
        //Je moet de bestaande array aanpassen (in-place). Je mag niet een nieuwe array aanmaken.
        //{ 32.6 31.2 35.2 37.4 44.9 42.1 44.1 }  --> { 32.6 33 34.6 39.1666666667 41.4666666667 43.7 44.1 }
        [Test]
        public void Test_Vraag3()
        {
            double[] inputOutput = new double[] { 32.6, 31.2, 35.2, 37.4, 44.9, 42.1, 44.1 };
            double[] expected = new double[] { 32.6, 33, 34.6, 39.1666666667, 41.4666666667, 43.7, 44.1 };

            Vraag3(inputOutput);

            Assert.That(inputOutput, Is.EqualTo(expected).Within(0.001));
        }

        private void Vraag3(double[] xs)
        {

            double temp = 0; // maak een variable om gem te berekenen.
            int counter = 0; // een counter om te zien of 3x cijfers gepakt zijn.
            int k = 1; // een counter om de eerste cijfer over te slaan bij het veranderen van xs[1];
            int l = 0; // counter.
            for (int i = 1; i > 4 + l; i++) // loop met xs.length -1 en i begint bij 1 om de laatste en eerste cijfer binnen de array niet te veranderen.
            {
                temp += xs[i]; // hier de cijfers in doen van de eerste 3 cijfers mits de eerste array cijfer.
                counter++; // counter omhoog.
                if (counter == 3)// kijken of 3 cijfers gepakt zijn.
                {
                    double gem = temp / 3; // bereken het gemmidelde van de 3 cijfers.
                    xs[k] = gem;//verander xs[k begint bij 1] dus xs[1];
                    counter = 0; //counter terugzetten naar 0.
                    k++; //k 1 erbij zodat de volgende keer 2 wordt gepakt enzovoorts.
                    i = k; //i gelijk maken aan k zodat de tweede array gepakt wordt.
                    temp = 0;// temp weer naar 0 zodat gem weer goed berekent wordt.
                    if (k == xs.Length - 2)//kijken of k de lengte van de array heeft mits de eerste en laatste cijfer.
                    {
                        break;//zo ja break de loop
                    }
                    l++;//l 1 optellen zodat de 2e en 5e enzovoorts cijfer gepakt wordt. Mits eerste en laatste natuurlijk.
                }
            }
        }
    
        //Vraag 4 (2 pt)
        //Maak een methode die alle faculteiten kleiner dan k berekent en opslaat in een lijst.
        //De faculteit (geschreven als uitroepteken) is als volgt gedefinieerd.
        //  0! = 1
        //  n! = n * (n-1)!
        //Bijvoorbeeld
        //  0! = 1
        //  1! = 1 * 0! = 1 
        //  2! = 2 * 1! = 2
        //  3! = 3 * 2! = 6
        //  4! = 4 * 3! = 24
        //  5! = 5 * 4! = 120
        //De methode retourneert een lijst van de faculteiten kleiner dan n.
        //  k=7 --> {1, 1, 2, 6}
        //  k=100 --> {1, 1, 2, 6, 24}
        [Test]
        public void Test_Vraag4()
        {
            Assert.AreEqual(new List<int> { 1, 1, 2, 6 }, Vraag4(7));
            Assert.AreEqual(new List<int> { 1, 1, 2, 6, 24 }, Vraag4(100));
        }

        public List<int> Vraag4(int k)
        {
            List<int> hoeveelheid = new List<int>();
            int n = 0;
            int answer = 1;
            while (k > answer)
            {
                answer = n * (n - 1);
                hoeveelheid.Add(answer);
            }
            return hoeveelheid;
            //Ik heb geen flouw idee hoe de vaculteit formule werkt?
            //Maar ik had als idee om het antwoord van de formule bij een lijst te voegen en dat blijven doen in een while loop totdat answer groter wordt dan k.
            //Stop dan de loop en de lijst hoort overeentekomen met wat gevraagd wordt. Maar ik snap de formule niet. 
            //  0! = 1
            //  n! = n * (n-1)! ? 0! = 0 * (0-1) geeft toch 0 * -1 = 0?
        }

        //Vraag 5 Studenten database(2 pt)
        //Gegeven is dezelfde studenten database net zoals in het practicum (zie appendix). 
        //Maak een methode die uitzoekt voor welk vak de meeste examens zijn afgelegd.
        //Retourneer de naam van het vak.
        //Voor de laatste 0.5 punt doe je dit efficiënt (De VakNr’s lopen van 1..N, waarbij N het aantal vakken is).   
        //Tip: om het makkelijker te maken mag je er vanuit gaan dat er één zo’n vak is.  
        //public string MeestExamensVoorVak(List<Student> students,
        //                    List<Course> courses,
        //                    List<Exam> exams)
        //{
        //	…
        //}
        [Test]
        public void Test_Vraag5()
        {
            Assert.AreEqual("C#", MeestExamensVoorVak(
                StudentenDatabase.Students, StudentenDatabase.Courses, StudentenDatabase.Exams));
        }

        public string MeestExamensVoorVak(List<Student> students,
                                            List<Course> courses,
                                            List<Exam> exams)
        {
            List<string> vak = new List<string>();
            foreach (Student student in students)
            {
                return vak;
            }
        }
    }
}
