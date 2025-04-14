namespace InventoryApp.Models;

public class OfficeSupplyItem : InventoryItem
{
    public string Category { get; set; } = string.Empty;
    public int ReorderThreshold { get; set; }

    public override decimal CalculateValue() => Quantity * 20;

    public override string GetItemDetails()
    {
        return base.GetItemDetails() + $" - Category: {Category}, Reorder at: {ReorderThreshold}";
    }
}
