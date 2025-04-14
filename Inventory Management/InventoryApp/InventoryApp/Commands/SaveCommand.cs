using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.Commands;

public class SaveCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;

    public string Name => "7. Save Inventory Data";

    public SaveCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine("Saving inventory data...");
        await _manager.SaveToFileAsync("inventory.json");
        Console.WriteLine("Inventory data saved.");
    }
}
