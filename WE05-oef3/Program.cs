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
                new Product{Naam= "The Last of Us II", ProductCode="LAST2", Prijs = 60M, Categorie = "GAMES"},
                new Product{Naam= "SONY Bravia 4K", ProductCode="SB_4K", Prijs = 4000M, Categorie = "TV"},
                new Product{Naam= "SONY Bravia V273", ProductCode="SB_V273", Prijs = 400M, Categorie = "TV"},
            };

            MethodSyntax(producten);
            DrawLine();
            QuerySyntax(producten);
        }

        private static void MethodSyntax(List<Product> producten)
        {
            // volgende query gaat er van uit dat er geen producten zijn met dezelfde prijs
            var duurste = producten.OrderByDescending(p => p.Prijs).First();

            // als er wel twee of meer producten kunnen zijn met dezelfde, hoogste prijs,
            // is deze query een betere oplossing (returnt alle producten met de hoogste prijs):
            var duursten = producten.Where(p => p.Prijs == producten.Max(p => p.Prijs));

            var gem = producten.Average(p => p.Prijs);
            Console.WriteLine("Duurste product: " + duurste);
            Console.WriteLine("Gemiddelde prijs producten: " + gem);

            var productsByCategory =
                producten.GroupBy(p => p.Categorie)
                .Select(group => new
                {
                    CatNaam = group.Key,
                    Aantal = group.Count(),
                    Duurste = group.OrderByDescending(p => p.Prijs).First().Naam,
                    Goedkoopste = group.OrderBy(p => p.Prijs).First().Naam,
                    GemPrijs = group.Average(p => p.Prijs),
                });

            foreach (var product in productsByCategory)
            {
                Console.WriteLine();
                Console.WriteLine("-= " + product.CatNaam + " =-");
                Console.WriteLine("Aantal: {0}", product.Aantal);
                Console.WriteLine("Duurste: {0}", product.Duurste);
                Console.WriteLine("Goedkoopste: {0}", product.Goedkoopste);
                Console.WriteLine("Gemiddele prijs: {0}", Math.Round(product.GemPrijs, 2));
            }

        }

        private static void QuerySyntax(List<Product> producten)
        {
            // volgende query gaat er van uit dat er geen producten zijn met dezelfde prijs
            var duurste = (from p in producten
                           orderby p.Prijs descending
                           select p).First();

            // als er wel twee of meer producten kunnen zijn met dezelfde, hoogste prijs,
            // is deze query een betere oplossing (returnt alle producten met de hoogste prijs):
            var duursten = from p in producten
                           where p.Prijs == (from prod in producten select p.Prijs).Max()
                           select p;


            var gem = (from p in producten
                       select p.Prijs).Average();

            Console.WriteLine("Duurste product: " + duurste);
            Console.WriteLine("Gemiddelde prijs producten: " + gem);

            var productsByCategory = from p in producten
                                     group p by p.Categorie into cat
                                     select new
                                     {
                                         CatNaam = cat.Key,
                                         Aantal = cat.Count(),
                                         Duurste = (from p in cat orderby p.Prijs descending select p).First().Naam,
                                         Goedkoopste = (from p in cat orderby p.Prijs select p).First().Naam,
                                         GemPrijs = (from p in cat select p.Prijs).Average()
                                     };

            foreach (var product in productsByCategory)
            {
                Console.WriteLine();
                Console.WriteLine("-= " + product.CatNaam + " =-");
                Console.WriteLine("Aantal: {0}", product.Aantal);
                Console.WriteLine("Duurste: {0}", product.Duurste);
                Console.WriteLine("Goedkoopste: {0}", product.Goedkoopste);
                Console.WriteLine("Gemiddele prijs: {0}", Math.Round(product.GemPrijs, 2));
            }
        }

        private static void DrawLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
