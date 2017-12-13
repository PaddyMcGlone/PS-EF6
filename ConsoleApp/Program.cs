using NinjaDomain.Classes;
using NinjaDomain.Classes.Enums;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp
{
    public class Program
    {
        static void Main()
        {            
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            //AddNewNinja();
            //AddMultipleNinjas();
            //SimpleQueryAndUpdate();
            //QueryAndUpdateDisconnected();
            //RetrieveDataWithFind();
            //RemoveNinja();
            AddRelatedData();
            Console.ReadLine();
        }

        private static void AddRelatedData()
        {
            var ninja = new Ninja
            {
                Name        = "Patrick",
                DateOfBirth = DateTime.Today.AddYears(-30),
                Clan        = new Clan { ClanName = "Test", Id = 2 }
            };

            var equipment1 = new NinjaEquipment
            {
                Name = "Flying",
                Type = EquipmentType.Tool
            };

            var equipment2 = new NinjaEquipment
            {
                Name = "Sword",
                Type = EquipmentType.Weapon
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                context.Ninjas.Add(ninja);  
                ninja.EquipmentOwned.Add(equipment1);                
                ninja.EquipmentOwned.Add(equipment2);                

                context.SaveChanges();
            }
        }

        private static void RemoveNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var itemToDelete = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(itemToDelete);

                context.SaveChanges();
            }
        }


        /// <summary>
        /// Adding multiple ninja into the context
        /// </summary>
        private static void AddMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name        = "Murphy",
                Clan        = new Clan { Id = 1, ClanName = "McGlones" },
                ClanId      = 1,
                DateOfBirth = DateTime.Today.AddYears(-1)
            };

            var ninja2 = new Ninja
            {
                Name        = "Christina",
                Clan        = new Clan { Id = 1, ClanName = "McGlones" },
                ClanId      = 1,
                DateOfBirth = DateTime.Today.AddYears(-30)
            };

            //Using the AddRange method to add multiple ninja objects
            using (var context = new NinjaContext())
            {
                context.Ninjas.AddRange(new List<Ninja> {ninja1, ninja2});
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adding a new Ninja into the context
        /// </summary>
        private static void AddNewNinja()
        {
            Console.WriteLine(@"Adding a new Ninja to context");

            var newNinja = new Ninja
            {
                Name        = "Patrick",
                Clan        = new Clan { Id = 1, ClanName = "McGlones" },
                ClanId      = 1,
                DateOfBirth = DateTime.Today.AddYears(-30)                
            };

            using (var context = new NinjaContext())
            {
                // Log all interactions on the database.
                context.Database.Log = Console.WriteLine;

                context.Ninjas.Add(newNinja);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Simple find and update method
        /// </summary>
        private static void SimpleQueryAndUpdate()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var results = context.Ninjas.FirstOrDefault();
                if (results != null) results.DateOfBirth = DateTime.Today.AddYears(-25);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Simple query and update via different dBContexts
        /// </summary>
        private static void QueryAndUpdateDisconnected()
        {
            Ninja ninja = null;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                ninja = context.Ninjas.FirstOrDefault(n => n.Name.ToLower().Contains("patrick"));
                ninja.DateOfBirth = DateTime.Today.AddYears(-60);
            }

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //Without these lines EF has no way of knowing what has changed
                //the reason for this is it is a different context

                context.Ninjas.Attach(ninja); // observe this entity
                context.Entry(ninja).State = EntityState.Modified; // informing EF the state has changed

                context.SaveChanges(); // Call save changes
            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyVal = 1;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var results = context.Ninjas.Find(keyVal);

                Console.WriteLine($"After Find #1:{results.Name}");

                results = context.Ninjas.Find(keyVal);

                Console.WriteLine($"After Find #1:{results.Name}");
            }
        }
    }
}
