using InventoryApp;
using InventoryApp.Commands;
using InventoryApp.Services;
using InventoryApp.Models;
using InventoryApp.Interfaces;

class Program
{
    static async Task Main()
    {
        var manager = new InventoryManager<InventoryItem>();

        var commands = new List<ICommand>
        {
            new ViewAllCommand<InventoryItem>(manager),
            new AddItemCommand<InventoryItem>(manager),
            new UpdateItemCommand<InventoryItem>(manager),
            new RemoveItemCommand<InventoryItem>(manager),
            new SearchInventoryCommand<InventoryItem>(manager),
            new GenerateReportCommand<InventoryItem>(manager),
            new SaveCommand<InventoryItem>(manager),
            new LoadCommand<InventoryItem>(manager)
        };

        var menu = new MenuRunner(commands);

        await menu.RunAsync();
    }
}
