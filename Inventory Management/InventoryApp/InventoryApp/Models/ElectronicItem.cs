namespace InventoryApp.Models;

public class ElectronicItem : InventoryItem
{
    public string Manufacturer { get; set; } = string.Empty;
    public int WarrantyPeriod { get; set; } // months

    public override decimal CalculateValue() => Quantity * 500; // example

    public override string GetItemDetails()
    {
        return base.GetItemDetails() + $" - {Manufacturer}, Warranty: {WarrantyPeriod} months";
    }
}
