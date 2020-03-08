using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new SalesContext())
            {
                var productsToAdd = GetProductsToSeed();

                db.Products.AddRange(productsToAdd);

                db.SaveChanges();

            }

        }

        private static List<Product> GetProductsToSeed()
        {
            List<Product> products = new List<Product>();
            var product01 = new Product
            {
                Name = "Chery",
                Price = 1.50m,
                Quantity = 10
            };

            var product02 = new Product
            {
                Name = "Cury",
                Price = 2.99m,
                Quantity = 5
            };

            var product03 = new Product
            {
                Name = "Cat",
                Price = 0.99m,
                Quantity = 20
            };

            products.Add(product03);
            products.Add(product02);
            products.Add(product01);
            return products;
        }
    }
}
