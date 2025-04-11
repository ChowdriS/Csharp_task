
using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public int Grade { get; set; }
    public int Age { get; set; }
}

class Program
{

    static char categorise(int grade)
    {
        if (grade < 40) return 'C';
        if (grade < 60) return 'B';
        if (grade < 80) return 'A';
        return 'O';
    }
    static void Main(string[] args)
    {
        List<Student> students = new List<Student> {
            new Student { Name = "Chowdri", Grade = 85, Age = 20 },
            new Student { Name = "Kaviraj", Grade = 72, Age = 19 },
            new Student { Name = "Arun", Grade = 90, Age = 21 },
            new Student { Name = "Koushik", Grade = 68, Age = 22 },
            new Student { Name = "sakthivel", Grade = 95, Age = 20 }
        };

        Console.Write("Enter grade threshold: ");
        int threshold = int.Parse(Console.ReadLine());

        //var result = students
        //    .Where(s => s.Grade > threshold)
        //    .OrderBy(s => s.Name)
        //    .ToList();

        var groups = students
            .GroupBy(s => categorise(s.Grade));

        foreach (var group in groups)
        {
            Console.WriteLine("*" + group.Key);
            foreach (var student in group)
            {
                Console.WriteLine(student.Name);
            }
        }

        //Console.WriteLine("\nStudents with grade above " + threshold + ":");
        //foreach (var student in result)
        //{
        //    Console.WriteLine($"->Name: {student.Name}, Grade: {student.Grade}, Age: {student.Age}");
        //}
    }
}
