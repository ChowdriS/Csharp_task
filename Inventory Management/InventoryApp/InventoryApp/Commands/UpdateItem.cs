using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Utils;

namespace InventoryApp.Commands;

public class UpdateItemCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;

    public string Name => "3. Update Existing Item";

    public UpdateItemCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public async Task ExecuteAsync()
    {
        var items = _manager.GetAllItems().ToList();

        if (!items.Any())
        {
            Console.WriteLine("No items to update.");
            return;
        }

        Console.WriteLine("Available Items:");
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i].Name} ({items[i].Id})");
        }

        Console.Write("Enter item number to update: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= items.Count)
        {
            var item = items[index - 1];

            Console.Write("New Name (leave empty to keep current): ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) item.Name = newName;

            Console.Write("New Description (leave empty to keep current): ");
            var newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription)) item.Description = newDescription;

            Console.Write("New Quantity: ");
            item.Quantity = int.TryParse(Console.ReadLine(), out int qty) ? qty : item.Quantity;

            _manager.UpdateItem(item.Id, item);

            Logger.Log($"Item updated: {item.Name}");
            await ProgressHelper.ShowProgressAsync("Updating item");
            Console.WriteLine("Item updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }
}
