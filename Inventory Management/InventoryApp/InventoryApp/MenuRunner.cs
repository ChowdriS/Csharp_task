using InventoryApp.Interfaces;

namespace InventoryApp;

public class MenuRunner
{
    private readonly List<ICommand> _commands;

    public MenuRunner(List<ICommand> commands)
    {
        _commands = commands;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("** Inventory Management System **");
            Console.WriteLine("---------------------------------");

            foreach (var command in _commands)
            {
                Console.WriteLine(command.Name);
            }

            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            if (choice == "0") break;

            if (int.TryParse(choice, out int ind) && ind > 0 && ind <= _commands.Count)
            {
                Console.Clear();
                await _commands[ind - 1].ExecuteAsync();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
