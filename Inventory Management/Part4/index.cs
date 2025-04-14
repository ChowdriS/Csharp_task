using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public static class FileHandler
{
    private static readonly SemaphoreSlim _lock = new(1, 1);

    public static async Task SaveAsJsonAsync<T>(string filePath, Dictionary<Guid, T> items)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        using FileStream fs = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(fs, items, options);
    }

    public static async Task<Dictionary<Guid, T>> LoadFromJsonAsync<T>(string filePath)
    {
        using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await JsonSerializer.DeserializeAsync<Dictionary<Guid, T>>(fs)
            ?? new Dictionary<Guid, T>();
    }



    public static async Task SaveInventoryAsync<T>(string path, Dictionary<Guid, T> items)
    {
        try
        {
            await _lock.WaitAsync();

            await SaveAsJsonAsync(path, items);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
        finally
        {
            _lock.Release();
        }
    }

    // public static void SaveAsBinary<T>(string filePath, Dictionary<Guid, T> items)
    // {
    //     using FileStream fs = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
    //     var formatter = new BinaryFormatter();
    //     formatter.Serialize(fs, items);
    // }

    // public static Dictionary<Guid, T> LoadFromBinary<T>(string filePath)
    // {
    //     using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    //     var formatter = new BinaryFormatter();
    //     return (Dictionary<Guid, T>)formatter.Deserialize(fs);
    // }


    public static async Task<Dictionary<Guid, T>> LoadInventoryAsync<T>(string path)
    {
        try
        {
            await _lock.WaitAsync();

            return File.Exists(path)
                ? await LoadFromJsonAsync<T>(path)
                : new Dictionary<Guid, T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
            return new Dictionary<Guid, T>();
        }
        finally
        {
            _lock.Release();
        }
    }
}
