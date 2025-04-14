using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Utils;

namespace InventoryApp.Commands;

public class RemoveItemCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;
    public string Name => "4. Remove Item";

    public RemoveItemCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public async Task ExecuteAsync()
    {
        var items = _manager.GetAllItems().ToList();

        if (!items.Any())
        {
            Console.WriteLine("No items to remove.");
            return;
        }

        Console.WriteLine("Available Items:");
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i].Name} ({items[i].Id})");
        }

        Console.Write("Enter item number to remove: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= items.Count)
        {
            var item = items[index - 1];
            _manager.RemoveItem(item.Id);
            Logger.Log($"Item removed: {item.Name}");
            await ProgressHelper.ShowProgressAsync("Removing item");
            Console.WriteLine("Item removed.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }
}
