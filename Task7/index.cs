using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program {

    public static async Task Main(string[] args) {
        try {
            Task<string> task1 = FetchDataFromSource("Source A", 2000);
            Task<string> task2 = FetchDataFromSource("Source B", 3000);
            Task<string> task3 = FetchDataFromSource("Source C", 1000);

            string[] results = await Task.WhenAll(task1, task2, task3);

            Console.WriteLine("All data fetched:");
            foreach (string result in results) {
                Console.WriteLine("->" + result);
            }
            Task.Delay(1000);
        }
        catch (Exception ex) {
            Console.WriteLine("\nError " + ex.Message);
        }
    }

    static async Task<string> FetchDataFromSource(string sourceName, int delay) {
        //if(sourceName == "Source B") {
        //    throw new Exception("Failed to Fetch");
        //}
        await Task.Delay(delay);
        return $"{sourceName} data received after {delay} ms.";
    }
}