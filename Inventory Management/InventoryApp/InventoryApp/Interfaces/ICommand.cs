namespace InventoryApp.Interfaces;

public interface ICommand
{
    string Name { get; }
    Task ExecuteAsync();
}
