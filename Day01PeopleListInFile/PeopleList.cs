using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01PeopleListInFile
{
    internal class Program2
    {
        static List<Person> people = new List<Person>();

        private static int getMenuChoice()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Add person info");
            Console.WriteLine("2. List persons info");
            Console.WriteLine("3. Find and list a person by name");
            Console.WriteLine("4. Find and list persons younger than age");
            Console.WriteLine("0. Exit");
            Console.Write("Choice: ");
            return (int.TryParse(Console.ReadLine(), out int choice))?choice:-1;
        }

        static void Main(string[] args)
        {
            ReadAllPeopleFromFile();
          
            while (true)
            {
               
                int choice = getMenuChoice();

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("End of the project!");
                        SaveAllPeopleToFile();
                        return;


                    case 1:
                        AddPersonInfo();
                        break;

                    case 2:
                        ListAllPersonsInfo();

                        break;

                    case 3:
                        FindPersonByName();
                        break;

                    case 4:
                        FindPersonYoungerThan();
                        break;

                }
            }

            
        }

    }
}
