using System;
using System.Collections.Generic;

namespace HomeGoodsECommerce
{
    public class Program
    {
        static Dictionary<string, Product> products = new Dictionary<string, Product>
        {
            { "M1", new Product { Name = "Television", Price = 25000, Stock = 6 }},
            { "M2", new Product { Name = "Washing Machine", Price = 7000, Stock = 3 }},
            { "M3", new Product { Name = "Water Dispenser", Price = 5000, Stock = 8 }},
            { "M4", new Product { Name = "Refrigerator", Price = 32000, Stock = 5 }},
            { "M5", new Product { Name = "Lamp", Price = 4000, Stock = 9 }}
        };

        static Dictionary<string, int> cart = new Dictionary<string, int>();
        static List<Order> orders = new List<Order>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Home Goods E-commerce Order Handling System!");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Display products");
                Console.WriteLine("2. Place in cart");
                Console.WriteLine("3. View cart");
                Console.WriteLine("4. Complete order");
                Console.WriteLine("5. Review orders");
                Console.WriteLine("6. Exit");
                Console.Write("Please indicate your preference: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    DisplayProducts();
                }
                else if (choice == "2")
                {
                    PlaceInCart();
                }
                else if (choice == "3")
                {
                    ViewCart();
                }
                else if (choice == "4")
                {
                    CompleteOrder();
                }
                else if (choice == "5")
                {
                    ReviewOrders();
                }
                else if (choice == "6")
                {
                    Console.WriteLine("Exiting the system. Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        static void DisplayProducts()
        {
            Console.WriteLine("\nAvailable Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Key}: {product.Value.Name} - Php{product.Value.Price} (Stock: {product.Value.Stock})");
            }
        }

        static void PlaceInCart()
        {
            DisplayProducts();
            Console.Write("Enter the product code to add to cart: ");
            string productCode = Console.ReadLine();

            if (products.ContainsKey(productCode))
            {
                Console.Write($"Enter the quantity of {products[productCode].Name} to add to cart: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0 && quantity <= products[productCode].Stock)
                {
                    if (cart.ContainsKey(productCode))
                    {
                        cart[productCode] += quantity;
                    }
                    else
                    {
                        cart[productCode] = quantity;
                    }
                    products[productCode].Stock -= quantity;
                    Console.WriteLine($"Added {quantity} of {products[productCode].Name} to cart.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity or insufficient stock.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product code.");
            }
        }

        static void ViewCart()
        {
            if (cart.Count > 0)
            {
                Console.WriteLine("\nYour Cart:");
                foreach (var item in cart)
                {
                    Console.WriteLine($"{products[item.Key].Name}: {item.Value} @ Php{products[item.Key].Price} each");
                }
            }
            else
            {
                Console.WriteLine("\nYour cart is empty.");
            }
        }

        static void CompleteOrder()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty. Add items to cart before placing an order.");
                return;
            }

            Random rand = new Random();
            int orderId = rand.Next(1000, 9999);
            double totalAmount = 0;

            foreach (var item in cart)
            {
                totalAmount += products[item.Key].Price * item.Value;
            }

            orders.Add(new Order { OrderId = orderId, Items = new Dictionary<string, int>(cart), TotalAmount = totalAmount, Status = "Placed" });
            cart.Clear();
            Console.WriteLine($"Order placed successfully. Your order ID is {orderId}. Total amount: Php{totalAmount}");
        }

        static void ReviewOrders()
        {
            if (orders.Count > 0)
            {
                Console.WriteLine("\nYour Orders:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Status: {order.Status}, Total Amount: Php{order.TotalAmount}");
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"  {products[item.Key].Name}: {item.Value} @ Php{products[item.Key].Price} each");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nYou have no orders.");
            }
        }

        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public int Stock { get; set; }
        }

        public class Order
        {
            public int OrderId { get; set; }
            public Dictionary<string, int> Items { get; set; }
            public double TotalAmount { get; set; }
            public string Status { get; set; }
        }
    }
}