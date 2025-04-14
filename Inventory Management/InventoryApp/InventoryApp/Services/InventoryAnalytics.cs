using InventoryApp.Models;

namespace InventoryApp.Services;

public static class InventoryAnalytics
{
    public static Dictionary<string, int> ItemsPerCategory(IEnumerable<InventoryItem> items)
    {
        return items
            .GroupBy(item => item.GetType().Name)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public static Dictionary<string, decimal> TotalValuePerCategory(IEnumerable<InventoryItem> items)
    {
        return items
            .GroupBy(item => item.GetType().Name)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(item => item.CalculateValue())
            );
    }

    public static IEnumerable<InventoryItem> FilterByName(IEnumerable<InventoryItem> items, string keyword)
    {
        return items
            .Where(item => item.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    public static Dictionary<string, List<InventoryItem>> GenerateItemTypeReport(IEnumerable<InventoryItem> items)
    {
        return items
            .GroupBy(item => item.GetType().Name)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public static void PrintItemTypeReport(Dictionary<string, List<InventoryItem>> report)
    {
        foreach (var group in report)
        {
            Console.WriteLine($"\nCategory: {group.Key} ({group.Value.Count} items)");
            foreach (var item in group.Value)
            {
                Console.WriteLine($"- {item.Name} (Qty: {item.Quantity})");
            }
        }
    }
}
