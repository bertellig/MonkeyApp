using MyMonkeyApp;

namespace MyMonkeyApp;

/// <summary>
/// Main entry point for the MonkeyApp console application.
/// </summary>
public class Program
{
    private static readonly Dictionary<string, int> monkeyAccessCounts = new(StringComparer.OrdinalIgnoreCase);

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("🐒 Welcome to MonkeyApp! 🐒\n");
        bool exitRequested = false;
        while (!exitRequested)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. List all monkeys");
            Console.WriteLine("2. Get details for a specific monkey by name");
            Console.WriteLine("3. Get a random monkey");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");
            var userInput = Console.ReadLine();
            Console.WriteLine();
            switch (userInput)
            {
                case "1":
                    DisplayAllMonkeys();
                    break;
                case "2":
                    Console.Write("Enter monkey name: ");
                    var name = Console.ReadLine();
                    var monkey = MonkeyHelper.GetMonkeyByName(name ?? string.Empty);
                    if (monkey is not null)
                    {
                        IncrementAccessCount(monkey.Name);
                        DisplayMonkey(monkey);
                    }
                    else
                        Console.WriteLine($"🙈 Monkey '{name}' not found.\n");
                    break;
                case "3":
                    var randomMonkey = MonkeyHelper.GetRandomMonkey();
                    IncrementAccessCount(randomMonkey.Name);
                    DisplayMonkey(randomMonkey);
                    break;
                case "4":
                    exitRequested = true;
                    Console.WriteLine("👋 Goodbye!");
                    break;
                default:
                    Console.WriteLine("⚠️ Invalid choice. Please try again.\n");
                    break;
            }
        }
    }

    private static void DisplayAllMonkeys()
    {
        var monkeys = MonkeyHelper.GetMonkeys();
        Console.WriteLine($"Total monkeys: {monkeys.Count}\n");
        foreach (var monkey in monkeys)
        {
            var count = monkeyAccessCounts.TryGetValue(monkey.Name, out var c) ? c : 0;
            Console.WriteLine($"- {monkey.Name} ({monkey.Location}) 🐵 | Accessed: {count} times");
        }
        Console.WriteLine();
    }

    private static void DisplayMonkey(Monkey monkey)
    {
        Console.WriteLine();
        Console.WriteLine("   .-\"\"\"\"-.   ");
        Console.WriteLine("  / .===. \\");
        Console.WriteLine("  \\/ 6 6 \\/");
        Console.WriteLine("  ( \\___/ )");
        Console.WriteLine("___ooo__ooo___");
        Console.WriteLine();
        Console.WriteLine($"🐵 {monkey.Name} 🐵");
        Console.WriteLine($"Location: {monkey.Location}");
        Console.WriteLine($"Population: {monkey.Population:N0}");
        Console.WriteLine($"Coordinates: {monkey.Latitude}, {monkey.Longitude}");
        Console.WriteLine($"Details: {monkey.Details}");
        Console.WriteLine($"Image: {monkey.Image}");
        var count = monkeyAccessCounts.TryGetValue(monkey.Name, out var c) ? c : 0;
        Console.WriteLine($"Accessed: {count} times");
        Console.WriteLine();
    }

    private static void IncrementAccessCount(string name)
    {
        if (monkeyAccessCounts.ContainsKey(name))
            monkeyAccessCounts[name]++;
        else
            monkeyAccessCounts[name] = 1;
    }
}
