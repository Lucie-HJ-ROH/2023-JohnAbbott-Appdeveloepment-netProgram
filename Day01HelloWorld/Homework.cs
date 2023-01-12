using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01HelloWorld
{
    internal class Homework
    {
        static List<Person> people = new List<Person>();

        static void AddPersonInfo() {

        }

        static void ListAllPersonalsInfo()
        {

        }

        static void FindPersonByName()
        {

        }

        static void FindPersonYoungerThan()
        {

        }

        static void ReadAllPeopleFromFile()
        {

        }

        static void SaveAllPeopleFromFile()
        {

        }

        static void ShowList()
        {
            Console.WriteLine("1. Add Person Info ?");
            Console.WriteLine("2. List Person Info");
            Console.WriteLine("3. Find a person by name");
            Console.WriteLine("4. Find All person yourger than age");
            Console.WriteLine("0. Exit");
        }

        static void Main(string[] args)
        {
            {
            ShowList();
        }
    }

    public class Person
    {
        public string Name;
        public string Age;
        public string City;
        public string Region;   
    }


}
