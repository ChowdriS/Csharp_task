using InventoryApp;
using InventoryApp.Commands;
using InventoryApp.Services;
using InventoryApp.Models;
using InventoryApp.Interfaces;
using InventoryApp.Utils;

class Program
{
    static async Task Main()
    {
        if (!LoginHandler.Login(AuthHandler.AdminAuth))
        {
            Console.WriteLine("Authentication failed. Exiting...");
            return;
        }

        Console.WriteLine("Logged in successfully!\n");

        var manager = new InventoryManager<InventoryItem>();
        var Log = new Logger();

        manager.InventoryChanged += Log.Log;

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
