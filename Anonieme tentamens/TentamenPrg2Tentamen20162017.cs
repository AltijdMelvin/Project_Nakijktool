using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

//Uitwerking tentamen
//Voornaam:     Joris 
//Achternaam:   Lops
//StudentNr:    s15
//Klas:         
//Datum:        
namespace Tentamens
{
    class TentamenPrg2Tentamen20162017
    {
        //Vraag 1 punten 15
        //Maak een methode die bepaalt of gegeven twee string of de strings elkaars omgekeerde zijn of niet.
        //“aap”, “paa” --> true
        //“aap”, “paap” --> false
        //“container”, “reniatnoc” --> true
        //“aap”, “gaa” --> false
        //“’, “blaat” --> false

        [Test]
        public void Test_Vraag1()
        {
            Assert.AreEqual(false, Vraag1("", "blaat"));
            Assert.AreEqual(true, Vraag1("aap", "paa"));
            Assert.AreEqual(false, Vraag1("aap", "paap"));
            Assert.AreEqual(true, Vraag1("container", "reniatnoc"));
            Assert.AreEqual(false, Vraag1("aap", "gaa"));

        }

        //syntax: correct 1/2
        public bool Vraag1(string s1, string s2)
        {
            if (s1.Length != s2.Length)  //1/4               
            {
                return false;
            }

            for (int i = 0; i < s1.Length; i++)    //1/2     
            {
                if (s1[i] != s2[s1.Length - (i + 1)])   //1/2
                {
                    return false;
                }
            }

            return true;
        }

        //Vraag 2 punten 15 
        //Gegeven is een array met getallen. We beginnen bij index 0. 
        //Het getal op de desbetreffende positie wordt de volgende index. 
        //Vervolgens wordt waarde op deze index de nieuwe index, enzovoorts.
        //Zo ontstaat een pad hoe we door de array doorlopen. 
        //Als we op een gegeven moment de waarde -1 vinden zijn we klaar en is het een eindige pad (true). 
        //Echter soms komt het voor dat we buiten de array lopen en dan is het een niet eindige array(false).
        //{1,3,-1,2} --> true
        //{1,3,6,2} --> false
        //Opmerking: voor het gemak maar je ervanuit gaan dat het pad altijd eindig is 
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
            //1/4
            int index = xs[0];
            List<int> visisted = new List<int>();

            //1/4
            while (true)
            {
                //1/2
                if (index == -1)
                {
                    return true;
                }
                else if (index < 0 || index > xs.Length - 1)
                {
                    return false;
                }

                index = xs[index];

                //1/2
                if (visisted.Contains(index))
                {
                    return false;
                }
                visisted.Add(index);
            }
        }

        //Vraag 3 punten 20
        //Joris wil meteoroloog worden. Hij meet de tempratuur van de lucht meerdere keren per dag. 
        //Echter zijn metingen zijn niet heel precies. 
        //Joris denkt dat hij zijn grafiek mooier kan maken (smoothen) als hij het gemiddelde berekent met de twee buur metingen.
        //Het volgende is gemeten:
        //3 5 6 4 5 
        //Het tweede element (5) wordt vervangen door(3 + 5 + 6) / 3 = 4.66666666667,
        //Het derde element(6) wordt vervangen door(5 + 6 + 4) / 3 = 5,
        //Het vierde element(4) wordt vervangen door(6 + 4 + 5) / 3 = 5.
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
            //1/4
            if (xs.Length < 3) return;

            //1/4
            double prev = xs[0];
            //1/4
            for (int i = 1; i < xs.Length - 1; i++)            
            {
                //1
                double avg = (prev + xs[i] + xs[i + 1]) / 3.0;
                //1/4
                prev = xs[i];                                   
                xs[i] = avg;                                    
            }
        }

        //Vraag 4 punten 20
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
            var actual = Vraag4(7);
            var expected = new List<int> { 1, 1, 2, 6 };
            CollectionAssert.AreEqual(expected, actual, TestUtils.DisplayArrays(expected, actual));
            //Assert.AreEqual();
            actual = Vraag4(100);
            expected = new List<int> { 1, 1, 2, 6, 24 };
            Assert.AreEqual(new List<int> { 1, 1, 2, 6, 24 }, Vraag4(100), TestUtils.DisplayArrays(expected, actual));
        }

        public class TestUtils
        {
            public static string DisplayArrays<T>(IEnumerable<T> expected, IEnumerable<T> actual) => $"Expected: {string.Join(",", expected)} {Environment.NewLine} Actual: {string.Join(",", actual)}";
        }


        public List<int> Vraag4(int k)
        {
            //1/2
            List<int> result = new List<int>();
            int fac = 1;
            int i = 1;

            //1/4
            while (i < 20)       
            {
                //1
                result.Add(fac);    
                fac = i * fac;      
                i++;                
            }

            //1/4
            return result;          
        }

        //Vraag 5 Studenten database punten 20
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
            var result = MeestExamensVoorVak(StudentDatabase.Students, StudentDatabase.Courses, StudentDatabase.Exams);
            Assert.True(result == "C#" || result == "Wiskunde");
        }

        public string MeestExamensVoorVak(List<Student> students,
                                            List<Course> courses,
                                            List<Exam> exams)
        {
            //1/4
            int maxCourseCnt = 0;
            string result = String.Empty;

            //1/4
            foreach (var course in courses)         
            {
                int courseCnt = 0;
                //1/4
                foreach (var exam in exams)         
                {
                    //1/4
                    if (course == exam.Course)
                    {
                        courseCnt++;
                    }
                }
                //1
                if (courseCnt > maxCourseCnt)      
                {
                    maxCourseCnt = courseCnt;
                    result = course.Name;
                }
            }
            return result;
        }
    }


}
