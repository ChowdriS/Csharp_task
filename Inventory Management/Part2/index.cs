public class InventoryManager<T> : IEnumerable<T> where T : InventoryItem
{
    private Dictionary<Guid, T> _items = new();

    public void AddItem(T item)
    {
        _items[item.Id] = item;
    }

    public T GetItem(Guid id)
    {
        return _items.ContainsKey(id) ? _items[id] : null;
    }

    public void UpdateItem(T item)
    {
        if (_items.ContainsKey(item.Id))
            _items[item.Id] = item;
        else
            throw new KeyNotFoundException("Item not found.");
    }

    public void RemoveItem(Guid id)
    {
        _items.Remove(id);
    }

    public void AddItems(IEnumerable<T> items)
    {
        foreach (var item in items)
            AddItem(item);
    }

    public void RemoveItems(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
            RemoveItem(id);
    }

    // for foreach and Linq
    public IEnumerator<T> GetEnumerator() => _items.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


// Eg : ElectronicItem Switch = InventoryItemFactory.CreateItem("electronic")
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
