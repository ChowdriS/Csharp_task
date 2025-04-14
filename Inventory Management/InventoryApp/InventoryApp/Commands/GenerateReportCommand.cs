using InventoryApp.Interfaces;
using InventoryApp.Services;
using InventoryApp.Models;
using InventoryApp.Utils;

namespace InventoryApp.Commands;

public class GenerateReportCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;

    public string Name => "6. Generate Reports";

    public GenerateReportCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public Task ExecuteAsync()
    {
        Console.WriteLine("Generating Inventory Report...");

        var report = InventoryAnalytics.GenerateItemTypeReport(_manager.GetAllItems());

        Console.WriteLine("\nInventory Report:");
        foreach (var category in report)
        {
            Console.WriteLine($"Category: {category.Key} - Total Items: {category.Count()}");
            foreach (var item in category)
            {
                Console.WriteLine($"  - {item.Name}");
            }
        }

        return Task.CompletedTask;
    }
}
