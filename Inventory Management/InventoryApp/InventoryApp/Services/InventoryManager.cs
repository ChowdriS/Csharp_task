using InventoryApp.Models;
using System.Collections;
using System.Text.Json;

namespace InventoryApp.Services;

public class InventoryManager<T> : IEnumerable<T> where T : InventoryItem
{

    private readonly Dictionary<Guid, T> _items = new Dictionary<Guid, T>();

    public event Action<string> InventoryChanged;


    private static readonly SemaphoreSlim _lock = new(1, 1);

    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public async Task SaveToFileAsync(string filePath, Dictionary<Guid, T> items)
    {
        await _lock.WaitAsync();
        try
        {
            using FileStream fs = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await JsonSerializer.SerializeAsync(fs, items, options);
            InventoryChanged?.Invoke("Items are saved to file Inventory.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving: {ex.Message}");
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<Dictionary<Guid, T>> LoadFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
            return new Dictionary<Guid, T>();
        await _lock.WaitAsync();
        try
        {
            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Dictionary<Guid, T> item =  await JsonSerializer.DeserializeAsync<Dictionary<Guid, T>>(fs)
                   ?? new Dictionary<Guid, T>();
            InventoryChanged?.Invoke("Items are loaded from file Inventory.json");
            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading: {ex.Message}");
            return new Dictionary<Guid, T>();
        }
        finally
        {
            _lock.Release();
        }
    }
    // CRUD
    public void AddItem(T item)
    {
        _items[item.Id] = item;
        InventoryChanged?.Invoke($"Item {item.Name} is added to Inventory");
    }

    public void RemoveItem(Guid id) { 
        _items.Remove(id);
        InventoryChanged?.Invoke($"ItemId {id} is removed from Inventory");
    } 

    public bool UpdateItem(Guid id, T updatedItem)
    {
        if (_items.ContainsKey(id))
        {
            _items[id] = updatedItem;
            InventoryChanged?.Invoke($"ItemId {id} is updated in Inventory");
            return true;
        }
        return false;
    }

    public T? GetItem(Guid id) =>
        _items.TryGetValue(id, out var item) ? item : null;

    public IEnumerable<T> GetAllItems() => _items.Values;

    public void AddMultiple(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            _items[item.Id] = item;
        }
        InventoryChanged?.Invoke($"Multiple Items are added to inventory");
    }

    public void RemoveMultiple(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            _items.Remove(id);
        }
        InventoryChanged?.Invoke($"Multiple Items are removed from inventory");
    }

    // For JSON saving
    public Dictionary<Guid, T> ToDictionary() => _items;

    public async void LoadFromDictionary(Dictionary<Guid, T> items)
    {
        _items.Clear();
        foreach (var kv in items)
        {
            _items[kv.Key] = kv.Value;
        }
        await Task.Delay(1000);
    }

    // IEnumerable Support (LINQ + foreach)
    public IEnumerator<T> GetEnumerator() => _items.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
