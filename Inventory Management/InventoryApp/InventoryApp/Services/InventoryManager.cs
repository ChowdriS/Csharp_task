using InventoryApp.Models;
using System.Collections;
using System.Text.Json;

namespace InventoryApp.Services;

public class InventoryManager<T> : IEnumerable<T> where T : InventoryItem
{

    private readonly Dictionary<Guid, T> _items = new();


    private static readonly SemaphoreSlim _lock = new(1, 1);

    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public static async Task SaveToFileAsync(string filePath, Dictionary<Guid, T> items)
    {
        await _lock.WaitAsync();
        try
        {
            using FileStream fs = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await JsonSerializer.SerializeAsync(fs, items, options);
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

    public static async Task<Dictionary<Guid, T>> LoadFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
            return new Dictionary<Guid, T>();

        await _lock.WaitAsync();
        try
        {
            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await JsonSerializer.DeserializeAsync<Dictionary<Guid, T>>(fs)
                   ?? new Dictionary<Guid, T>();
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
    public void AddItem(T item) => _items[item.Id] = item;

    public bool RemoveItem(Guid id) => _items.Remove(id);

    public bool UpdateItem(Guid id, T updatedItem)
    {
        if (_items.ContainsKey(id))
        {
            _items[id] = updatedItem;
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
    }

    public void RemoveMultiple(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            _items.Remove(id);
        }
    }

    // For JSON saving
    public Dictionary<Guid, T> ToDictionary() => _items;

    public void LoadFromDictionary(Dictionary<Guid, T> items)
    {
        _items.Clear();
        foreach (var kv in items)
        {
            _items[kv.Key] = kv.Value;
        }
    }

    // IEnumerable Support (LINQ + foreach)
    public IEnumerator<T> GetEnumerator() => _items.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
