using System;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name} and I'm {Age} years old.");
    }
}
class Program
{
    static void Main()
    {
        Person person1 = new Person("Alice", 25);
        Person person2 = new Person("Bob", 30);

        person1.Introduce();
        person2.Introduce();
    }
}

