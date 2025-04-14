namespace InventoryApp.Models;

public class FoodItem : InventoryItem
{
    public DateTime ExpiryDate { get; set; }
    public double StorageTemperature { get; set; }

    public override decimal CalculateValue() => Quantity * 10;

    public override string GetItemDetails()
    {
        return base.GetItemDetails() + $" - Expires: {ExpiryDate.ToShortDateString()}, Temp: {StorageTemperature}°C";
    }
}
