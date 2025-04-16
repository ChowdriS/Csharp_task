using System.Collections.Generic;
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
        Dictionary < Guid, T > items = await _manager.LoadFromFileAsync("D:\\Presidio\\Csharp_task\\Inventory Management\\InventoryApp\\InventoryApp\\Inventory.json");
        _manager.LoadFromDictionary(items);
        Console.WriteLine("Inventory data loaded.");
    }
}
