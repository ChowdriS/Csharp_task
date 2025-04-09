using System;
using System.Collections.Generic;
using System.Linq;

public interface IEntity
{
    public int id { get; set; }
}

public class Product : IEntity
{
    public int id { get; set; }
    public string name { get; set; }
}

public interface IRepository<T> where T : IEntity
{
    void add(T item);
    T getById(int id);
    IEnumerable<T> getAll();
    void update (T item);
    void deleteById(int id);
} 

public class InMemoryRepo<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> items = new List<T>();

    public void add(T item)
    {
        items.Add(item);
    }

    public T getById(int _id)
    {
        return items.FirstOrDefault(x => x.id == _id);
    }

    public IEnumerable<T> getAll()
    {
        return items;
    }

    public void update(T item)
    {
        var ind = items.FindIndex(x => x.id == item.id);
        if(ind != -1)
        {
            items[ind] = item;
        }
    }

    public void deleteById(int _id)
    {
        var item = items.FirstOrDefault(x => x.id == _id);
        if (item != null)
        {
            items.Remove(item);
        }
    }
}

class Program
{
    public static void Main(string []args)
    {
        IRepository<Product> repo = new InMemoryRepo<Product>();

        //Add Items
        repo.add(new Product { id = 1, name = "Laptop" });
        repo.add(new Product { id = 2, name = "Mouse" });
        repo.add(new Product { id = 3, name = "Keyboard" });

        //Get By ID
        var item = repo.getById(1);
        Console.WriteLine($"Get By Id-{item.name}");

        //Update 
        repo.update(new Product { id = 1, name = "Phone" });

        //Delete By Id
        repo.deleteById(2);

        //Get All
        var items = repo.getAll();
        Console.WriteLine("GetAll:");

        foreach (var iter in items)
        {
            Console.WriteLine("->" + iter.name);
        }

    }
}