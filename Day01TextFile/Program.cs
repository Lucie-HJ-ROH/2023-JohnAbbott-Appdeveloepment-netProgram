using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01TextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string fileName = @"C:..\..\data.txt";
            //const string fileName = @"C::data.txt";
            Console.Write("What is your name? ");
            string name = Console.ReadLine();

            //Approach 1 : one-step write all
            try{
                string[] namesArray = { name, name, name };
                File.WriteAllLines(fileName, namesArray);
            }
            catch(SystemException e)
            {
                Console.WriteLine("Exception " + e.Message);
            }
            

            //Approach 2: Java-like, open-write-close in steps
            try {

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(name);
                    sw.WriteLine(name);
                    sw.WriteLine(name);
                }

            } catch (SystemException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }


            //Read it
            try
            {
                { //most common way
                    string[] linesArray = File.ReadAllLines(fileName);
                    foreach (string line in linesArray)
                    {
                        Console.WriteLine(line);
                    }
                }
                { 
                    string allContent = File.ReadAllText(fileName);
                    Console.WriteLine(allContent);
                }
            }catch(SystemException ex)
            {
                Console.WriteLine("Error writing to file : " + ex.Message);
            }

        }








    }


}

