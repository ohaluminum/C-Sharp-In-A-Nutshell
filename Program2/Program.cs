using System;

namespace Program2
{
    class Program
    {
        delegate int Transformer(int i);
        delegate int Calculator(int x, int y);

        static void Main(string[] args)
        {
            // Local static method
            static int Square(int x) => x * x;

            static int Div(int x, int y)
            {
                if (y == 0) throw new DivideByZeroException();

                return x / y;
            }
            
            // ----------------------- LAMBDA EXPRESSIONS ---------------------------

            // A lambda expression is an unnamed method written in place of a delegate instance.

            // Named method Square
            Func<int, int> sqr = Square;

            // Unnamed method: Lambda expressions (expression)
            sqr += x => x * x;

            // Unnamed method: Lambda expressions (statement block)
            sqr += x => { return x * x; };

            // Run many methods at the same time
            Delegate[] delegateList1 = sqr.GetInvocationList();

            foreach (Func<int, int> instance in delegateList1)
            {
                Console.WriteLine("Square of 1: {0}", instance(1));
                Console.WriteLine("Square of 2: {0}", instance(2));
                Console.WriteLine("Square of 3: {0}", instance(3));
                Console.WriteLine();
            }

            // Unnamed method: Lambda expressions (expression)
            Func<int, int, int> calculator = (x, y) => x + y;   // The parenthesis is needed for multiple parameters

            // Unnamed method: Lambda expressions (statement block)
            calculator += (x, y) => { return x - y; };

            // Anonymous Methods
            calculator += delegate (int x, int y) { return x * y; };

            // Named method Div
            calculator += Div;

            // Run many methods at the same time
            Delegate[] delegateList2 = calculator.GetInvocationList();

            foreach (Func<int, int, int> instance in delegateList2)
            {
                Console.WriteLine(instance(10, 2));
            }

            Console.WriteLine();

            // Definition:
            // Outer variables referenced by a lambda expression are called captured variables.
            // A lambda expression that captures variables is called a closure.

            int scalar = 2;     // Captured variable (outer variable)
            Func<int, int> multiplier = n => n * scalar;

            // Static Lambda (New feature in C# 9.0)
            // Try to run the following code: will not compile
            // Func<int, int> multiplier = static n => n * scalar;

            Console.WriteLine(multiplier(3));

            scalar = 10;        // Invoked value
            Console.WriteLine(multiplier(3));
            Console.WriteLine("The output is 30 instead of 6 because captured variables are evaluated when the delegate is actually invoked, not when the variables were captured.");

        }
    }
}
