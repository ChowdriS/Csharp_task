public abstract class InventoryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int Quantity { get; set; }

    public abstract decimal CalculateValue();

    public virtual string GetItemDetails()
    {
        return $"{Name} ({Description}) - Qty: {Quantity}";
    }
}

public class OfficeSupplyItem : InventoryItem
{
    public string Category { get; set; }
    public int ReorderThreshold { get; set; }

    public override decimal CalculateValue()
    {
        return Quantity * 250;
    }

    public override string GetItemDetails()
    {
        return base.GetItemDetails() + $" | Category: {Category}, ReorderThreshold: {ReorderThreshold}";
    }
}


public class ElectronicItem : InventoryItem
{
    public int WarrantyPeriod { get; set; } 
    public string Manufacturer { get; set; }

    public override decimal CalculateValue()
    {
        return Quantity * 1000; 
    }

    public override string GetItemDetails()
    {
        return base.GetItemDetails() + $" | Manufacturer: {Manufacturer}, Warranty: {WarrantyPeriod} months";
    }
}


public class FoodItem : InventoryItem
{
    public DateTime ExpiryDate { get; set; }
    public double StorageTemperature { get; set; }

    public override decimal CalculateValue()
    {
        return Quantity * 100;
    }

    public override string GetItemDetails()
    {
        return base.GetItemDetails() + $" | Expires: {ExpiryDate.ToShortDateString()}, Temp: {StorageTemperature}°C";
    }
}

