using System;

namespace Program1
{
    // Generic delegate types
    public delegate T Transformer<T>(T x);
    public delegate void ProgressReporter(int percentComplete);

    public class Util
    {
        // The Custom Delegates
        public static void Transform1<T>(T[] values, Transformer<T> t)   // Method with delegate parameter.
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
            }
        }

        // The Func and Action Delegates
        public static void Transform2<T>(T[] values, Func<T, T> transformer)   // Method with delegate parameter.
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = transformer(values[i]);
            }
        }

        public static void HardWork(ProgressReporter p)     // Method with delegate parameter.
        {
            for (int i = 0; i < 10; i++)
            {
                p(i * 10);      // Invoke delegate (which know how to call method).
            }
        }

        public static int Square(int x) => x * x;
        public static int Cube(int x) => x * x * x;
        public static int Sqrt(int x) => (int)Math.Sqrt(x);
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------- METHODS WITH DELEGATES PARAMETER --------------------------------

            int[] values = { 1, 2, 3 };

            Console.WriteLine("Original list: ");
            foreach (int i in values)
            {
                Console.Write(i + " ");      // 1 2 3
            }

            // Can pass in different methods.
            Util.Transform1(values, Util.Square);    // Hook in the Square method.
            Console.WriteLine("\nImplementing Square method: ");
            foreach (int i in values)
            {
                Console.Write(i + " ");      // 1 2 3 => 1 4 9
            }

            Util.Transform1(values, Util.Sqrt);    // Hook in the Sqrt method.
            Console.WriteLine("\nImplementing Sqrt method: ");
            foreach (int i in values)
            {
                Console.Write(i + " ");      // 1 4 9 => 1 2 3
            }

            Util.Transform2(values, Util.Cube);      // Hook in the Cube method.
            Console.WriteLine("\nImplementing Cube method: ");
            foreach (int i in values)
            {
                Console.Write(i + " ");      // 1 2 3 => 1 8 27
            }

            Console.WriteLine();

            // ---------------------------- MULTICAST DELEGATES --------------------------------

            Console.WriteLine("\nProcessing: ");

            ProgressReporter p = null;
            p += WriteProgressToConsole;    // Add the first target method.
            p += WriteProgressToFile;       // Add the second target method.
            Util.HardWork(p);

            void WriteProgressToConsole(int percentComplete) => Console.WriteLine(percentComplete);

            void WriteProgressToFile(int percentComplete) => System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
           
            // ---------------------------- STANDARD EVENT PATTERN ------------------------------

            Student student = new Student();

            // Event NameChanged is null so nothing printout
            student.Name = "Lejing";

            student.NameChanged += StudentNameChanged;
            student.Name = "Iris";

            // Event AgeChanged is null so nothing printout
            student.Age = 18;
            student.AgeChanged += StudentAgeChanged;
            student.Age = 21;

            void StudentNameChanged(object sender, NameChangedEventArgs e)
            {
                Console.WriteLine("NOTICE: STUDENT NAME HAVE UPDATED!");
                Console.WriteLine($"Old student name: {e.OldName}");
                Console.WriteLine($"New student name: {e.NewName}");
            }

            void StudentAgeChanged(object sender, AgeChangedEventArgs e)
            {
                Console.WriteLine("NOTICE: STUDENT AGE HAVE UPDATED!");
                Console.WriteLine($"Old student age: {e.OldAge}");
                Console.WriteLine($"New student age: {e.NewAge}");
            }
        }

        class NameChangedEventArgs : EventArgs
        {
            public readonly string OldName;
            public readonly string NewName;

            public NameChangedEventArgs(string oldName, string newName)
            {
                OldName = oldName;
                NewName = newName;
            }
        }

        class AgeChangedEventArgs : EventArgs
        {
            public readonly int OldAge;
            public readonly int NewAge;

            public AgeChangedEventArgs(int oldAge, int newAge)
            {
                OldAge = oldAge;
                NewAge = newAge;
            }
        }

        class Student
        {
            private string name;
            private int age;

            // Event Declaration: System.EventHandler<> is the generic delegate.
            public event EventHandler<NameChangedEventArgs> NameChanged;

            protected virtual void OnNameChanged(NameChangedEventArgs e)
            {
                // If NameChange == null then do nothing
                NameChanged?.Invoke(this, e);
            }

            public event EventHandler<AgeChangedEventArgs> AgeChanged;

            protected virtual void OnAgeChanged(AgeChangedEventArgs e)
            {
                // If AgeChange == null then do nothing
                AgeChanged?.Invoke(this, e);
            }

            public string Name 
            {
                get { return name; }
                set
                {
                    if (String.Compare(name, value) == 0) return;   // No change detected.

                    string oldName = name;   // Save the change. 
                    name = value;

                    OnNameChanged(new NameChangedEventArgs(oldName, name));   // Fire the event.
                }
            }

            public int Age
            {
                get { return age; }
                set
                {
                    if (age == value) return;   // No change detected.

                    int oldAge = age;   // Save the change. 
                    age = value;

                    OnAgeChanged(new AgeChangedEventArgs(oldAge, age));   // Fire the event
                }
            }
        }
    }
}
