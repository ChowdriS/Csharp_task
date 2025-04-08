using System;

class Program {
    static void Main(string[] args) {
        Console.Write("Enter a number to calculate its factorial: ");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 0) {
            Console.WriteLine($"Factorial of {number} is {Factorial(number)}");
        }
        else {
            Console.WriteLine("Please enter a valid non-negative integer.");
        }
    }

    static long Factorial(int n) {
        if (n == 0 || n == 1)
            return 1;
        return n * Factorial(n - 1);
    }
}