using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Context
{
    public class SeedData
    {
        public static readonly Random random = new Random();

        // Random
        public static IEnumerable<Contact> GetPermissions(int min = 5, int max = 10)
        {
            var names = new List<string> 
            { 
                "Alan", "Alfredo", "David", "Sunil", "Leonardo", "Alcy", "Juan Carlos", "Ennio",
                "Alberto", "Guillermo", "Charlie", "Quentin", "Butch", "Elvis", "Larry", "Mikhail"
            };

            var lastNames = new List<string> 
            { 
                "Turing", "Richter", "Part", "Sibelius", "Morricone", "Gosling", "Von Doom", "Luzón",
                "Perreira", "Vazquez", "Asuncion", "Velazquez", "Smith", "Norris", "Rotzank", "Kitchen", "Vivaldi"
            };

            var domains = new List<string>
            {
                "recursive.io", "harmony.com", "exhausted.com.do", "playtime.ai", "smothered.com"
            };

            return Enumerable.Range(1, random.Next(min, max) + 1)
                .Select(id =>
                {
                    var contact = new Contact
                    {
                        Name = names[random.Next(0, names.Count)],
                        LastName = lastNames[random.Next(0, lastNames.Count)]
                    };

                    contact.Email = $"{contact.Name[0] + contact.LastName}@{domains[random.Next(0, domains.Count)]}";

                    return contact;
                });
        }
    }
}
