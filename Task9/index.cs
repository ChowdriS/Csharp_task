using System;
using System.Reflection;
using System.Linq;

[AttributeUsage(AttributeTargets.Method)]
public class RunnableAttribute : Attribute { }

public class TaskA
{
    [Runnable]
    public void RunA()
    {
        Console.WriteLine("Running Task A");
    }

    public void RunB()
    {
        Console.WriteLine("This method should not be called");
    }
}

public class TaskB
{
    [Runnable]
    public void RunB()
    {
        Console.WriteLine("Running Task B");
    }
}

public class TaskC
{
    [Runnable]
    public void DoWork()
    {
        Console.WriteLine("Running Task C");
    }
}

class Program
{
    public static void Main(string []args)
    {
        Console.WriteLine("Discovering [Runnable] methods...\n");
        // var types = Assembly.GetExecutingAssembly().GetTypes();

        // foreach (var type in types)
        // {
        //     var methods = type.GetMethods();

        //     foreach (var method in methods)
        //     {
        //         if (method.GetCustomAttribute<RunnableAttribute>() != null)
        //         {
        //             var instance = Activator.CreateInstance(type);
        //             method.Invoke(instance, null);
        //         }
        //     }
        // }
        var methods = Assembly.GetExecutingAssembly()
            .GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(RunnableAttribute), false).Length > 0);

        foreach (var method in methods)
        {
            if (method.GetCustomAttribute<RunnableAttribute>() != null)
            {
                var instance = Activator.CreateInstance(method.DeclaringType);
                method.Invoke(instance, null);
            }
        }
    }
}
