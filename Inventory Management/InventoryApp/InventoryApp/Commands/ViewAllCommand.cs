using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.Commands;

public class ViewAllCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;

    public string Name => "1. View All Inventory";

    public ViewAllCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public async Task ExecuteAsync()
    {
        var items = _manager.GetAllItems();

        if (items.Count() ==0)
        {
            Console.WriteLine("Inventory is Empty!");
            return;
        }
        Console.WriteLine("\nInventory:");
        foreach (var item in items)
        {
            Console.WriteLine(item.GetItemDetails());
        }

        await Task.Delay(1000);
    }
}
