using System;
using System.Collections.Generic;
using System.IO;

class Program {
    static void Main() {
        string inputFile = "students.csv";
        string outputFile = "result.txt";

        // List<string> sampleData = new List<string> {
        //     "Name,Grade,Age",
        //     "Chowdri,85,20",
        //     "kaviraj,72,19",
        //     "Arun,90,21",
        //     "Koushik,68,22",
        //     "Sakthivel,95,20",
        // };

        try {
            // File.WriteAllLines(inputFile, sampleData);
            // Console.WriteLine("Sample student data written to file.");

            string[] lines = File.ReadAllLines(inputFile);

            int studentCount = lines.Length - 1;

            string result = "Number of students: " + studentCount;
            File.WriteAllText(outputFile, result);

            Console.WriteLine("Processed successfully. Result written to '" + outputFile + "'.");
        }
        catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
