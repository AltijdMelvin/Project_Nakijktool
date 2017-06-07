using System;
using System.Collections.Generic;
using NUnit.Framework;

Voornaam: MARRY
Achternaam: DEWEY
StudentNr: s60466
Klas: i1a
//Datum: 15-12-2016
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
            char[] lettersa = new char[] { };
            char[] lettersb = new char[] { };
            char[] lettersc = new char[] { };
            int a = 0;
            int b = 0;
            bool truefalse = false;

            foreach (char karakter in s1) // string 1 in array
            {
                lettersa[a] = karakter;
                a++; 
            }
            foreach (char karakter in s2) // string 2 in array
            {
                lettersb[b] = karakter;
                b++;
            }
            for (int i = lettersb.Length; i > 0; i--) // string 2 achtestevoren
            {
                lettersc[i] = lettersb[lettersb.Length];
                b--;
            }

            for (int i = 0; i < lettersa.Length; i++)
            {
                if(lettersa.Length == lettersc.Length)
                {
                    if(lettersa[i] == lettersc[i])
                    {
                        truefalse = true;
                    }
                    else
                    {
                        truefalse = false;
                    }
                }
            }
            if (lettersa.Length != lettersc.Length)
            {
                truefalse = false;
            }
            return truefalse;
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
            bool truefalse = false;
            for (int i = 0; i < xs.Length; i++)
            {
                if(xs[i] == -1 )
                {
                    if (xs[i + 1] != 1)
                    {
                        truefalse = true;
                    }
                }
                else
                {
                    truefalse = false;
                }
            }
            return truefalse;

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
            double g2 = (xs[0] + xs[1] + xs[2]) / 3;
            double g3 = (xs[1] + xs[2] + xs[3]) /3 ;
            double g4 = (xs[2] + xs[3] + xs[4]) / 3;
            double g5 = (xs[3] + xs[4] + xs[5]) / 3;
            double g6 = (xs[4] + xs[5] + xs[6]) / 3;
            xs[1] = g2;
            xs[2] = g3;
            xs[3] = g4;
            xs[4] = g5;
            xs[5] = g6;
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
            int sum = 0;
            List<int> lijst = new List<int>();
            lijst[0] = 1;
            for (int i = 1; i < lijst.Count; i++)
            {
                sum = i * lijst[i-1];

                if (sum < k)
                {
                    lijst[i] = sum;
                }
                else
                {
                    break;
                }
            }
            return lijst;
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
            Assert.AreEqual("Wiskunde", MeestExamensVoorVak(
                StudentenDatabase.Students, StudentenDatabase.Courses, StudentenDatabase.Exams));
        }

        public string MeestExamensVoorVak(List<Student> students,
                                            List<Course> courses,
                                            List<Exam> exams)
        {
            int counter;
            int mostcourse = 0;
            string result = "";
            foreach (var cursus in courses)
            {
                counter = 0;
                foreach (var examen in exams)
                {
                    if(cursus == examen.Course)
                    {
                        counter++;
                    }
                    if (counter > mostcourse)
                    {
                        mostcourse = counter;
                        result = cursus.Name;
                    }
                }

            }
            return result;
        }
    }
}
