using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.Write("What is your name? ");
            string name = Console.ReadLine();

            Console.Write("How old are you? ");
            string ageStr = Console.ReadLine();

            /*
            // version 1
            if (!int.TryParse(ageStr, out int age))
            {
                Console.WriteLine("Error: you must enter an integer number");
            }
            else
            {
                Console.WriteLine("Hello {0}, you are {1} y/o", name, age);
            }
            */

            // int l = ageStr.Length;
            
            // version 1
            try
            {
                int age = int.Parse(ageStr); // ex FormatException, OverflowException

                // name = name.ToLower();
                if (name.ToLower() == "santa")
                {
                    Console.WriteLine("SANTA!!! I can't believe it's you!");
                }
                else
                {
                    Console.WriteLine("Hello {0}, you are {1} y/o, {0}", name, age);
                    string greeting = $"Hello {name}, you are {age} y/o";
                    Console.WriteLine(greeting);
                }
            }
            catch (Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                Console.WriteLine("Error: you must enter an integer number");
            }

            /*
            catch (FormatException ex)
            {
                Console.WriteLine("Error: you must enter an integer number");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Error: integer number too big");
            }
            */

            Console.ReadKey();
        }
    }
}

