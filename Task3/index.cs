using System;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {

        List<string> items = new List<string>();
        string input;

        do {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Display Items");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");
            input = Console.ReadLine().Trim();

            switch (input) {
                case "1":
                    Console.Write("Enter item to add: ");
                    string newItem = Console.ReadLine().Trim();
                    items.Add(newItem);
                    Console.WriteLine("Item added.");
                    break;

                case "2":
                    Console.Write("Enter item to remove: ");
                    string removeItem = Console.ReadLine().Trim();
                    if (items.Remove(removeItem)) {
                        Console.WriteLine("Item removed.");
                    }
                    else {
                        Console.WriteLine("Item not found.");
                    }
                    break;

                case "3":
                    Console.WriteLine("Current Items:");
                    if (items.Count == 0) {
                        Console.WriteLine("No items to display.");
                    }
                    else {
                        foreach (string item in items) {
                            Console.WriteLine("- " + item.ToUpper());
                        }
                    }
                    break;

                case "4":
                    Console.WriteLine("Exiting program.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter 1 to 4.");
                    break;
            }

        } while (input != "4");
    }
}
