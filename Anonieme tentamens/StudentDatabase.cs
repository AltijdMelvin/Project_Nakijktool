using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Dit bestand mag niet aangepast worden! 
namespace Tentamens
{
    public class Student
    {
        public int StudentNr { get; set; }
        public string Name { get; set; }
    }

    public class Course
    {
        public int VakNr { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
    }

    public class Exam
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
        public decimal Score { get; set; }
    }

    public class StudentDatabase
    {
        private static Student jan = new Student() { StudentNr = 1, Name = "Jan" };
        private static Student piet = new Student() { StudentNr = 2, Name = "Piet" };
        private static Student klaas = new Student() { StudentNr = 3, Name = "Klaas" };
        private static Student katrijn = new Student() { StudentNr = 4, Name = "Katrijn" };

        public static List<Student> Students = new List<Student>() {jan, piet, klaas, katrijn};

        private static Course cSharp = new Course() { VakNr = 1, Name = "C#", Teacher = "Joris" };
        private static Course math = new Course() { VakNr = 2, Name = "Wiskunde", Teacher = "Jos" };
        private static Course coo = new Course() { VakNr = 3, Name = "Computer Organisation", Teacher = "Sibbele" };
        private static Course se = new Course() { VakNr = 4, Name = "Software Engineering", Teacher = "David" };
        private static Course python = new Course() { VakNr = 5, Name = "Python", Teacher = "Wouter" };

        public static List<Exam> Exams = new List<Exam>() {
            new Exam() { Student = jan,       Course = math,      Score = 3 },
            new Exam() { Student = piet,      Course = math,      Score = 5 },
            new Exam() { Student = jan,       Course = coo,       Score = 7 },
            new Exam() { Student = klaas,     Course = cSharp,    Score = 9 },
            new Exam() { Student = jan,       Course = cSharp,    Score = 5 },
            new Exam() { Student = jan,       Course = math,      Score = 6 },
            new Exam() { Student = katrijn,   Course = cSharp,    Score = 6 },
            new Exam() { Student = piet,      Course = cSharp,    Score = 5 },
            new Exam() { Student = piet,      Course = math,      Score = 8 },
            new Exam() { Student = piet,      Course = coo,       Score = 5 },
            new Exam() { Student = katrijn,   Course = se,        Score = 8 },
            new Exam() { Student = katrijn,   Course = se,        Score = 9 }
        };

        public static List<Course> Courses = new List<Course>() {
            cSharp, math, coo, se, python
        };


        //Opmerking: dit gaat goed, maar dan ook alleen maar omdat geen enkele sleutel duplicaten heeft!
        private static Dictionary<int, Course> courseByVakNr = Courses.ToDictionary(crs => crs.VakNr);
        private static Dictionary<string, Course> courseByName = Courses.ToDictionary(crs => crs.Name);
        private static Dictionary<string, Course> courseByTeacher = Courses.ToDictionary(crs => crs.Teacher);

        private static Dictionary<string, Student> studentByName = new Dictionary<string, Student>()
        {
            [jan.Name] = jan,
            [piet.Name] = piet,
            [klaas.Name] = klaas,
            [katrijn.Name] = katrijn
        };

        private static Dictionary<int, Student> studentByStudentNr = new Dictionary<int, Student>()
        {
            [jan.StudentNr] = jan,
            [piet.StudentNr] = piet,
            [klaas.StudentNr] = klaas,
            [katrijn.StudentNr] = katrijn
        };

        private static Dictionary<Student, List<Exam>> examsByStudent = CreateExamsByStudent();
        private static Dictionary<Course, List<Exam>> examsByCourse = CreateExamsByCourse();
        private static Dictionary<decimal, List<Exam>> examsByScore = CreateExamsByScore();

        public static Dictionary<Student, List<Exam>> CreateExamsByStudent()
        {
            Dictionary<Student, List<Exam>> res = new Dictionary<Student, List<Exam>>();
            foreach (Exam exam in Exams)
            {
                if (!res.ContainsKey(exam.Student))
                {
                    res[exam.Student] = new List<Exam>();
                }

                res[exam.Student].Add(exam);
            }
            return res;
        }

        public static Dictionary<Course, List<Exam>> CreateExamsByCourse()
        {
            //dit geeft hetzelfde resultaat als de andere twee methoden
            return Exams.GroupBy(x => x.Course)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        public static Dictionary<decimal, List<Exam>> CreateExamsByScore()
        {
            Dictionary<decimal, List<Exam>> res = new Dictionary<decimal, List<Exam>>();
            foreach (Exam exam in Exams)
            {
                if (!res.ContainsKey(exam.Score))
                {
                    res[exam.Score] = new List<Exam>();
                }

                res[exam.Score].Add(exam);
            }
            return res;
        }

        public static decimal GetLowestScoreByStudentName(string naam)
        {
            throw new NotImplementedException();
        }

        public static List<decimal> GetScoreByStudentName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
