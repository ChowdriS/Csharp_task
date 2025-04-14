// Eg : InventoryAnalytics.GroupByType(allItems)
public static class InventoryAnalytics
{
    public static Dictionary<string, List<InventoryItem>> GroupByType(IEnumerable<InventoryItem> items)
    {
        return items
            .GroupBy(item => item.GetType().Name)
            .ToDictionary(
                group => group.Key,
                group => group.ToList()
            );
    }


    public static IEnumerable<InventoryItem> FilterByValue(IEnumerable<InventoryItem> items, decimal minValue)
    {
        return items.Where(item => item.CalculateValue() >= minValue);
    }

    public static decimal GetTotalValue(IEnumerable<InventoryItem> items)
    {
        return items.Sum(item => item.CalculateValue());
    }

    public static IEnumerable<InventoryItem> GetTopItemsByQuantity(IEnumerable<InventoryItem> items, int topN)
    {
        return items.OrderByDescending(i => i.Quantity).Take(topN);
    }

    public static Dictionary<string, int> ItemsPerCategory(IEnumerable<InventoryItem> items)
    {
        return items
            .GroupBy(item => item.GetType().Name)
            .ToDictionary(group => group.Key, group => group.Count());
    }

}

// Eg : var ReorderedItems = allItems.NeedsReorder()
public static class InventoryExtensions
{
    public static IEnumerable<InventoryItem> NeedsReorder(this IEnumerable<InventoryItem> items)
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
