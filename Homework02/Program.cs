using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework02
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "hello" });
            users.Add(new Models.User { Name = "Steve", Password = "steve" });
            users.Add(new Models.User { Name = "Lisa", Password = "hello" });
            //users.Add(new Models.User { Name = "Tom", Password = "hello" });

            // 1.Display to the console, all the passwords that are "hello".Hint: Where
            DisplayHelloPasswords(users);

            // 2. Delete any passwords that are the lower-cased version of their Name. Do not just look for "steve". It has to be data-driven. Hint: Remove or RemoveAll
            DeleteUsersWithLowerNamePassword(users);

            // 3. Delete the first User that has the password "hello". Hint: First or FirstOrDefault
            DeleteFirstUserwithHelloPassword(users);

            // 4. Display to the console the remaining users with their Name and Password.
            DisplayUsers(users);
        }


        // 1.Display to the console, all the passwords that are "hello".Hint: Where
        protected static void DisplayHelloPasswords(List<Models.User> people)
        {
            var pp = people.Where(p => p.Password == "hello");

            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("People with 'hello' passwords.");
            Console.WriteLine("-----------------------------------------");

            if (pp.ToArray().Count() > 0)
            {
                foreach (var user in pp)
                {
                    string record = $" Name={user.Name},  Password={user.Password}";
                    Console.WriteLine(record);
                }
                Console.WriteLine("\n");
            }
        }


        // 2. Delete any passwords that are the lower-cased version of their Name. Do not just look for "steve". It has to be data-driven. Hint: Remove or RemoveAll
        protected static void DeleteUsersWithLowerNamePassword(List<Models.User> people)
        {
            // remove people whose passwords are te same as lower-cased of their name
            var pp = people.RemoveAll(p => p.Password == p.Name.ToLower());
        }


        // 3. Delete the first User that has the password "hello". Hint: First or FirstOrDefault
        protected static void DeleteFirstUserwithHelloPassword(List<Models.User> people)
        {
            // remove people whose passwords are te same as lower-cased of their name
            var person = people.OrderBy(p => p.Password == "hello").First();

            if (person != null)
            {
                people.Remove(person);
            }
        }


        // 4. Display to the console the remaining users with their Name and Password.
        protected static void DisplayUsers(List<Models.User> people)
        {
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine(" The remaining users are:");
            Console.WriteLine("---------------------------------------");

            if (people.ToArray().Count() > 0)
            {
                foreach (var user in people)
                {
                    string record = $" Name={user.Name},  Password={user.Password}";
                    Console.WriteLine(record);
                }
                Console.ReadLine();
                Console.WriteLine("\n");
            }
        }

    }
}
