using System;

namespace Program1
{
    public delegate int Transformer(int x);

    public delegate void ProgressReporter(int percentComplete);

    public class Util
    {
        public static void Transform(int[] values, Transformer t)   // Method with delegate parameter.
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------- METHODS WITH DELEGATES PARAMETER --------------------------------

            int[] values = { 1, 2, 3 };

            // Can pass in different methods.
            Util.Transform(values, Util.Square);    // Hook in the Square method.
            Util.Transform(values, Util.Cube);      // Hook in the Cube method.

            foreach (int i in values)
            {
                Console.Write(i + " ");      // 1 2 3 => 1 4 9
            }

            // ---------------------------- MULTICAST DELEGATES --------------------------------

            ProgressReporter p = null;
            p += WriteProgressToConsole;    // Add the first target method.
            p += WriteProgressToFile;       // Add the second target method.
            Util.HardWork(p);

            void WriteProgressToConsole(int percentComplete) => Console.WriteLine(percentComplete);

            void WriteProgressToFile(int percentComplete) => System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
        }
    }
}
