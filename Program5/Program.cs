using System;
using System.Globalization;

namespace Program5
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------------------------------- .NET STRING AND TEXT HANDLING -----------------------------------

            // Some static methods
            Console.WriteLine(char.ToUpper('c', CultureInfo.InvariantCulture));    // Closely matches American culture.
            Console.WriteLine(char.ToUpper('c', CultureInfo.CurrentCulture));    // Based on settings from the my computer’s control panel.

            Console.WriteLine(char.IsLetter('c'));    // True
            Console.WriteLine(char.IsDigit('c'));    // False
            Console.WriteLine(char.IsWhiteSpace('\t'));     // True

            // Contruct a string

            //@: To indicate that a string literal is to be interpreted verbatim.
            //Reference: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/verbatim
            //The following string looks better than "D:\\Program\\CSharp\\C-Sharp-In-A-Nutshell\\Program5\\Program.cs"
            string path = @"D:\Program\CSharp\C-Sharp-In-A-Nutshell\Program5\Program.cs";
            Console.WriteLine(path);

            // Null and empty strings: string is reference type and can be null 
            string emptyString = "";
            Console.WriteLine(emptyString == "");              // True
            Console.WriteLine(emptyString == string.Empty);    // True
            Console.WriteLine(emptyString.Length == 0);        // True

            string nullString = null;
            Console.WriteLine(nullString == null);        // True
            Console.WriteLine(nullString == "");          // False
            //Console.WriteLine(nullString.Length == 0);    // NullReferenceException

            // Iterating strings: string implements IEnumerable<char> interface
            foreach (char c in "Hello, world!")
            {
                Console.Write(c + " ");
            }

            Console.WriteLine();

            // Manipulating strings: return a new string (string is immutable)
            string s1 = "helloworld".Insert(5, ", ");
            Console.WriteLine(s1);      // s1 = "hello, world"

            string s2 = s1.Remove(5, 2);
            Console.WriteLine(s2);      // s2 = "helloworld"

            string s3 = s1.Replace(", ", " | ");
            Console.WriteLine(s3);
        }
    }
}
