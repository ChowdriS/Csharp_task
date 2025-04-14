using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;

namespace InventoryApp.Commands;

public class LoadCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;

    public string Name => "8. Load Inventory Data";

    public LoadCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine("Loading inventory data...");
        await _manager.LoadFromFileAsync("inventory.json");
        Console.WriteLine("Inventory data loaded.");
    }
}
