namespace InventoryApp.Models;

public class InventoryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public int Quantity { get; set; }
    public InventoryItem() { }

    public virtual decimal CalculateValue()
    {
        return Quantity;
    }

    public virtual string GetItemDetails()
    {
        return $"{Name} - Qty: {Quantity} - Purchased: {PurchaseDate.ToShortDateString()}";
    }
}
