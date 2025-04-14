using InventoryApp.Models;

namespace InventoryApp.Utils;

public static class InventoryItemFactory
{
    public static InventoryItem CreateItem(string type)
    {
        switch (type.ToLower())
        {
            case "electronic":
                return new ElectronicItem();
            case "food":
                return new FoodItem();
            case "office":
                return new OfficeSupplyItem();
            default:
                throw new ArgumentException("Unknown item type.");
        }
    }
}
