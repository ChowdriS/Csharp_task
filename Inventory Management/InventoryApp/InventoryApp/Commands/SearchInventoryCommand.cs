using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.Commands;

public class SearchInventoryCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;
    public string Name => "5. Search Inventory";

    public SearchInventoryCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public Task ExecuteAsync()
    {
        Console.Write("Enter search term: ");
        var term = Console.ReadLine()?.ToLower();

        var results = _manager
            .Where(item => item.Name.ToLower().Contains(term) || item.Description.ToLower().Contains(term))
            .ToList();

        if (results.Any())
        {
            Console.WriteLine($"Found {results.Count} items:");
            foreach (var item in results)
            {
                Console.WriteLine(item.GetItemDetails());
            }
        }
        else
        {
            Console.WriteLine("No matching items found.");
        }

        return Task.CompletedTask;
    }
}
