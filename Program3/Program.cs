using System;
using System.IO;
using System.Net;

namespace Program3
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------ TRY & CATCH & FINALLY ----------------------------------------

            Div1(4, 0);
            Div1(4, 2);
            Div2(4, 0);
            Div2(4, 2);

            // ------------------------------------ USING STATEMENT ----------------------------------------------

            DownloadOnline();

            // ------------------------------------ RETHROWING ---------------------------------------------------

            try
            {
                Method1();
            }
            catch (Exception ex)
            {
                // StackTrace (Exception Property): A string representing all the methods that are called from the origin of the exception to the catch block.
                Console.WriteLine("\nStackTrace: ");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void Method1()
        {
            try
            {
                Method2();
            }
            catch (Exception ex)
            {
                throw;
            }

            // If we replaced throw with throw ex, the example would still work, but the StackTrace property of the newly propagated exception would no longer reflect the original error.
        }

        public static void Method2()
        {
            try
            {
                throw new NotImplementedException("Method has not been implemented.");
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine(ex.Message);

                throw;       // Rethrow the exception: Rethrowing in this manner lets you log an error without swallowing it.
            }
        }


        public static void Div1(int x, int y)
        {
            try
            {
                int result = x / y;     // The exception throw at the runtime

                Console.WriteLine($"{x} / {y} = {result}");
            }
            catch (DivideByZeroException)   // No variable 
            {
                Console.WriteLine("x cannot be zero");
            }
            finally
            {
                Console.WriteLine("program completed");
            }
        }

        public static void Div2(int x, int y)
        {   
            /* 
             * NOTE: 
             * - checking for preventable errors in advance is preferable to relying on try/catch blocks because exceptions are relatively expensive to handle.
             * - it takes hundreds of clock cycles or more.
             */

            try
            {
                if (y == 0)
                {
                    throw new DivideByZeroException("x cannot be zero");
                }

                int result = x / y;
                Console.WriteLine($"{x} / {y} = {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Console.WriteLine("program completed");
            }
        }


        public static void DownloadOnline()
        {
            string s = null;

            // With 'using' keyword, the WebClient object will be dispose when execution falls outside of the statement block.
            using (WebClient wc = new WebClient())
            {
                try 
                { 
                    s = wc.DownloadString("http://www.albahari.com/nutshell/");

                    // Get page source (HTML)
                    Console.WriteLine(s);
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
                {
                    Console.WriteLine("Timeout");         
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.SendFailure)
                {
                    Console.WriteLine("Send Failure");
                }

                // With exception filter, the program can handle other sorts of WebException 
            }
        }

        
    }
}
