using System;
using System.Collections.Generic;
using System.Linq;

namespace WE05_oef3
{
    class Program
    {
        static void Main(string[] args)
        {
            var producten = new List<Product> {
                new Product{Naam= "Playstation 4", ProductCode="PS4", Prijs = 399.9M, Categorie = "GAMES"},
                new Product{Naam= "XBOX ONE", ProductCode="XB1", Prijs = 500M, Categorie = "GAMES"},
                new Product{Naam= "The Last of Us II", ProductCode="LOU2", Prijs = 60M, Categorie = "GAMES"},
                new Product{Naam= "SONY Bravia 4K", ProductCode="SB_4K", Prijs = 4000M, Categorie = "TV"},
                new Product{Naam= "SONY Bravia V273", ProductCode="SB_V273", Prijs = 400M, Categorie = "TV"},
            };

            Console.WriteLine(producten.OrderByDescending(p => p.Prijs).First());
            Console.WriteLine(producten.Average(p => p.Prijs));

            var categoryGroups = producten.GroupBy(p => p.Categorie);

            foreach (var group in categoryGroups)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine("Aantal: {0}", group.Count());
                Console.WriteLine("Duurste: {0}", group.OrderByDescending(p => p.Prijs).First().Naam);
                Console.WriteLine("Goedkoopste: {0}", group.OrderBy(p => p.Prijs).First().Naam);
                Console.WriteLine("Gemiddele prijs: {0}", Math.Round(group.Average(p => p.Prijs), 2));
                Console.WriteLine();
            }

            var duurste = from p in producten
                          where p.Prijs == (from prod in producten orderby prod.Prijs descending select p.Prijs).Max()
                          select p;

            var gem = (from p in producten
                       select p.Prijs).Average();

            var groups = from p in producten
                         group p by p.Categorie;


            foreach (var group in groups)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine("Aantal: {0}", group.Count());
                Console.WriteLine("Duurste: {0}", group.OrderByDescending(p => p.Prijs).First().Naam);
                Console.WriteLine("Goedkoopste: {0}", group.OrderBy(p => p.Prijs).First().Naam);
                Console.WriteLine("Gemiddele prijs: {0}", Math.Round(group.Average(p => p.Prijs), 2));
                Console.WriteLine();
            }
        }
    }
}
