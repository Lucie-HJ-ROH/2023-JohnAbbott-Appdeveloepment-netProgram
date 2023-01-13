using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04FirstEFConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SocietyDbContext db = new SocietyDbContext();
                Random random = new Random();
                Person p1 = new Person
                {
                    Name = "Foo " + random.Next(1000),
                    Age = random.Next(100)
                };

                db.People.Add(p1);
                db.SaveChanges();
                Console.WriteLine("record added");

            }
            catch (SystemException ex)
            {
                Console.WriteLine("Database operation failed" + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key");
                Console.ReadLine();
            }
        }
    }
}
