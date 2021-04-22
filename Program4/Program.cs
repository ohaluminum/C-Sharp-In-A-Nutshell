using System;
using System.Collections;
using System.Collections.Generic;

namespace Program4
{
    class Program
    {
        static void Main(string[] args)
        {
            // An array is already an IEnumerable => foreach works
            var array = new int[] { 1, 2, 3 };

            foreach (var a in array)
            {
                Console.WriteLine($"A is {a}");
            }

            // The foreach loop will be compile down to this version (low-level)
            var enumerator = array.GetEnumerator();
            
            while (enumerator.MoveNext())    // Return true if there is another item   
            {
                Console.WriteLine($"Enumerator A is {enumerator.Current}");
            }

            // ------------------------ CUSTOM ENUMERABLE --------------------------------

            GradeBook gradebook = new GradeBook();
            gradebook.AddStudent(new Student() { FirstName = "Lejing", LastName = "Huang", UHID = 1800000, Grade = 85 });
            gradebook.AddStudent(new Student() { FirstName = "Tony", LastName = "Stark", UHID = 1800001, Grade = 60.5m });
            gradebook.AddStudent(new Student() { FirstName = "Winnie", LastName = "Li", UHID = 1800002, Grade = 90 });
            gradebook.AddStudent(new Student() { FirstName = "Simon", LastName = "Shaw", UHID = 1800003, Grade = 10 });
            
            foreach (Student student in gradebook)
            {
                Console.WriteLine($"Name: {student.FirstName} {student.LastName}, UHID: {student.UHID}, Grade: {student.Grade}");
            }
        }

        public class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int UHID { get; set; }
            public decimal Grade { get; set; }
        }

        public class GradeBook : IEnumerable<Student>    //Strengthen static type safety
        {
            // List of students
            private IList<Student> studentList;

            public GradeBook()
            {
                studentList = new List<Student>();
            }

            public void AddStudent(Student student)
            {
                studentList.Add(student);
            }

            // Generic enumerator 
            public IEnumerator<Student> GetEnumerator()
            {
                foreach (Student student in studentList)
                {
                    yield return student;
                }
            }

            // Non-generic enumerator: explicit implementation keeps it hidden: call the generic version
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


            /*
             * NOTE:
             * - This interface has two methods to implement and they are both called GetEnumerator. 
             * You only need to worry about one of them. 
             * The second is is there from the old non-generic IEnumerable interface. 
             * You can safely call the generic version of the GetEnumerator method from the non-generic one.
             */
        }
    }
}
