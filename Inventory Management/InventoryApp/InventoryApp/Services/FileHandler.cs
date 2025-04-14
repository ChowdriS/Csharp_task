using System.Text.Json;
using InventoryApp.Models;

namespace InventoryApp.Services;

public static class FileHandler<T> where T : InventoryItem
{
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
}
