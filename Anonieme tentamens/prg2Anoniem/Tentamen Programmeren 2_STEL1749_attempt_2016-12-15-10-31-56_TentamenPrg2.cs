using System;
using System.Collections.Generic;
using NUnit.Framework;

Voornaam: ALINE
Achternaam: STELLA
StudentNr: s17499
Klas: i1f
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
            char[] arr = s2.ToCharArray();
            Array.Reverse(arr);
            string reverse = new string(arr);
            {
                if(s1 == reverse)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            System.Diagnostics.Debug.Write("");
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
            System.Diagnostics.Debug.Write("test" + xs);
            foreach (int x in xs)
            {
                if (x == -1)
                {
                    return true;
                }
                else if (x > xs.Length)
                {
                    return false;
                }
                xs[x] = x;
            }
            return true;
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

        private void Vraag3(double[] xs) //werktniet
        {
            string backup1 = "";
            string backup2 = "";
            string backup3 = "";
            string backup4 = "";
            string backup5 = "";
            for (int x = 1; x < xs.Length - 1; x++)
            {
                double newvar = ((xs[x - 1] + xs[x] + xs[x + 1]) / 3);
                if (string.IsNullOrEmpty(backup1))
                {
                    backup1 = newvar.ToString();
                }
                else if (string.IsNullOrEmpty(backup2))
                {
                    backup2 = newvar.ToString();
                }
                else if (string.IsNullOrEmpty(backup3))
                {
                    backup3 = newvar.ToString();
                }
                else if (string.IsNullOrEmpty(backup4))
                {
                    backup4 = newvar.ToString();
                }
                else if (string.IsNullOrEmpty(backup5))
                {
                    backup5 = newvar.ToString();
                }
            }
            xs[1] = Convert.ToDouble(backup1);
            xs[2] = Convert.ToDouble(backup2);
            xs[3] = Convert.ToDouble(backup3);
            xs[4] = Convert.ToDouble(backup4);
            xs[5] = Convert.ToDouble(backup5);

            //double backup = 0;
            //double backup2 = 0;
            //for (int x = 3;x < xs.Length - 1;x++)
            //{
            //    backup = xs[x - 3];
            //    double newvar = ((xs[x - 3] + xs[x -2] + xs[x - 1]) / 3);
            //    xs[(x - 3)] = backup2;
            //    backup2 = newvar;
            //}
            //Dit kreeg ik niet werkend
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
            List<int> facult = new List<int>();
            List<int> faculteiten = new List<int>() { 1, 1, 2, 6, 24, 120 };
            foreach(int faculteit in faculteiten)
            {
                if(faculteit < k)
                {
                    facult.Add(faculteit);
                }
            }
            return facult;
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
            int cSharp = 0;
            int math = 0;
            int coo = 0;
            int se = 0;
            int python = 0;
            foreach (Exam x in exams)
            {
                Course mostexams = x.Course;
                if(x.Course.VakNr == 1)
                {
                    cSharp++;
                }
                else if (x.Course.VakNr == 2)
                {
                    math++;
                }
                else if (x.Course.VakNr == 3)
                {
                    coo++;
                }
                else if (x.Course.VakNr == 4)
                {
                    se++;
                }
                else if (x.Course.VakNr == 5)
                {
                    python++;
                }
            }
            if (cSharp > se && cSharp > coo && cSharp > math && cSharp > python) return "C#";
            if (math > se && math > coo && math > python && math > cSharp) return "Wiskunde";
            if (coo > se && coo > python && coo > math && coo > cSharp) return "Computer Organisation";
            if (se > python && se > coo && se > math && se > cSharp) return "Software Engineering";
            if (python > se && python > coo && python > math && python > cSharp) return "Python";
            else return null;
        }
    }
}
