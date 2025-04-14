using InventoryApp.Models;

namespace InventoryApp.Utils;

public static class ExtensionMethods
{
    public static IEnumerable<OfficeSupplyItem> NeedsReorder(this IEnumerable<InventoryItem> items)
    {
        return items
            .OfType<OfficeSupplyItem>()
            .Where(item => item.Quantity < item.ReorderThreshold);
    }

    public static decimal TotalInventoryValue(this IEnumerable<InventoryItem> items)
    {
        return items.Sum(item => item.CalculateValue());
    }
}
