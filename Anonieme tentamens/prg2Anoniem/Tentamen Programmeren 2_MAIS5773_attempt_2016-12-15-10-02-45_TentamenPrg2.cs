using System;
using System.Collections.Generic;
using NUnit.Framework;

Voornaam: NANCIE
Achternaam: MAISIE
StudentNr: s5773
Klas: i1f
//Datum:
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
            // create a new variable to store the reversed value of string 1 in.
            string reversed = "";

            // loop through string 1 backwards
            for (int i = s1.Length - 1; i >= 0; i--)
            {
                // add the characters of string 1 to the reversed string variable, from end to begin
                reversed += s1[i];
            }

            // check if string2 equals the reversed string, if so return true.
            if (s2.Equals(reversed))
                return true;

            // if it hasn't returned anything, return false cause the strings obviously weren't equal.
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
            // loop through the array
            foreach (int x in xs)
            {
                // if the value at this index is -1, return true
                if (x == -1)
                    return true;
            }

            // otherwise return false
            return false;
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
            // create a temporary array and store the xs array in there so we can edit xs based on calculations of how xs was before we started editing it.
            double[] temp = xs;

            // loop through the xs array, starting at index 1 and ending at the index that comes before the last index.
            for (int i = 1; i < xs.Length - 1; i++)
            {
                // setting a new value to the index, using the original values that it had.
                xs[i] = (temp[i - 1] + temp[i] + temp[i + 1]) / 3;
            }

            // for some reason this function doesn't work accordingly, I have no idea why, the above should work...
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
            // create a list to store the output in
            List<int> rlist = new List<int>();

            // calculate?? using ! does not seem to work.......
            // double calc = k * ( k - 1 )!; <-- doesn't work...
            double calc = k * (k - 1);

            // convert it to a string so we can store the individual characters into the list
            string c = calc.ToString();

            // loop through string
            for (int i = 0; i < c.Length; i++)
            {
                // add each character to the list
                rlist.Add(c[i]);
            }

            // return the list
            return rlist;
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
            // create a new array to store the total amount of times a course has been redone
            int[] courseCount = new int[5] { 0, 0, 0, 0, 0 };

            // create a new integer to store the highest index of the array in
            int highest = 0;

            // loop through all the exams
            foreach (Exam exam in exams)
            {
                // loop through all the courses
                for (int i = 0; i < courses.Count; i++)
                {
                    // if the VakNr of the course of the exam matches one of the courses' VakNr, increase that course count by 1.
                    if (exam.Course.VakNr == courses[i].VakNr)
                        courseCount[i]++;
                }

            }

            // loop through the array
            for (int i = 1; i < courseCount.Length; i++)
            {
                // check if the value at the current index is higher than the previous one, if so, update highest to the new index.
                if (courseCount[i] > courseCount[i - 1])
                    highest = i;
            }

            // return the course with the highest amount.
            return courses[highest].Name;
        }
    }
}
