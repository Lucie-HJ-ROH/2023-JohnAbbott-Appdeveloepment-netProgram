using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01PeopleListInFile
{
    internal class Program
    {

        static List<Person> people = new List<Person>();
        const string fileName = @"C:..\..\people.txt";
        static void Main(string[] args)
        {

            ReadAllPeopleFromFile();


            bool flag = true;
            while (true)
            {
                PrintMenu();
                string num = Console.ReadLine();

                if(!int.TryParse(num, out int number) || number > 4 || number < 0)
                {
                    Console.WriteLine("Please Try Again!(Input 0 - 4) \n");
                    continue; // back to the while
                }

                switch(number)
                {
                    case 0:
                        Console.WriteLine("End of the project!");
                        flag = false; // before input the 0 while loop is true
                        break;

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

            SaveAllPeopleToFile();

        }

         static void SaveAllPeopleToFile()
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    people.ForEach(person =>
                    {
                        sw.WriteLine($"{person.getName()};{person.getAge()};{person.getCity()}");
                    });
                   
                }

            }
            catch (SystemException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }

        }

         static void ReadAllPeopleFromFile()
        {
            try
            {
                { //most common way
                    string[] linesArray = File.ReadAllLines(fileName);
                    Person person = new Person();
                    foreach(string line in linesArray)
                    {
                        string[] peopleInfo = line.Split(';');
                        person.setName(peopleInfo[0]);

                        if (!int.TryParse(peopleInfo[1], out int age){
                            Console.WriteLine("Format is invalide");
                        
                        }

                        person.setAge(age);

                        person.setCity(peopleInfo[2]);

                    }




                }
                {
                    string allContent = File.ReadAllText(fileName);
                    Console.WriteLine(allContent);
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error writing to file : " + ex.Message);
            }
        }

         static void FindPersonYoungerThan()
        {
            throw new NotImplementedException();
        }

         static void FindPersonByName()
        {
            throw new NotImplementedException();
        }

         static void ListAllPersonsInfo()
        {
            throw new NotImplementedException();
        }

         static void AddPersonInfo()
        {
            throw new NotImplementedException();
        }

        static void PrintMenu()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Add person info");
            Console.WriteLine("2. List persons info");
            Console.WriteLine("3. Find and list a person by name");
            Console.WriteLine("4. Find and list persons younger than age");
            Console.WriteLine("0. Exit");
            Console.Write("Choice: ");

        }
    }

}
