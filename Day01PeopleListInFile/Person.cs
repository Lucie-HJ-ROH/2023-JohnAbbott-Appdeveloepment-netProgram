using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    internal class Person
    {

        private string _name;

        public string Name { 

            get { return _name; }
            set {
                if (!Regex.IsMatch(value, @"^[^;]{2,100]$")) { // not semicolone, and size.
                    throw new ArgumentException("Name must be 2-100j characters long, no ");
                }
                value = _name;
            } 
        } // Name 2-100 characters long, not containing semicolons

        private int _age; // Age 0-150 :  // underscore means don't touch.

        public int Age
        {
            get { return _age; }
            set {
                if (value < 0 || value>150)
                {
                    throw new ArgumentOutOfRangeException("Age must be 0 - 150");
                }
                _age = value; 
            }
        }

        
        private string _city; // City 2-100 characters long, not containing semicolons


        public string City
        {

            get { return _city; }
            set
            {
                if (!Regex.IsMatch(value, @"^[^;]{2,100]$"))
                { // not semicolone, and size.
                    throw new ArgumentException("Name must be 2-100j characters long, no ");
                }
                value = _city;
            }
        } // Name 2-100 characters long, not containing semicolons

        public Person(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public override string ToString()
        {
            return $"{Name} is {Age} from {City}";
        }

    }
}
