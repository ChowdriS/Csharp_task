using InventoryApp.Interfaces;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Utils;

namespace InventoryApp.Commands;

public class AddItemCommand<T> : ICommand where T : InventoryItem
{
    private readonly InventoryManager<T> _manager;

    public string Name => "2. Add New Item";

    public AddItemCommand(InventoryManager<T> manager)
    {
        _manager = manager;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Enter item type (electronic, food, office): ");
        string type = Console.ReadLine()?.ToLower();

        InventoryItem item;
        try
        {
            item = InventoryItemFactory.CreateItem(type);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.Write("Name: ");
        item.Name = Console.ReadLine();

        Console.Write("Description: ");
        item.Description = Console.ReadLine();

        Console.Write("Quantity: ");
        item.Quantity = int.TryParse(Console.ReadLine(), out int qty) ? qty : 0;

        item.PurchaseDate = DateTime.Now;


        _manager.AddItem((T)item);
        
        await ProgressHelper.ShowProgressAsync("Adding item");
        Console.WriteLine("Item added successfully.");
    }
}
