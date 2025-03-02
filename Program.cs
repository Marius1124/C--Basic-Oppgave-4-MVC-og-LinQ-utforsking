namespace C__Basic_Task_4_MVC_and_LinQ_Exploration
{
    class LegoSet
    {
        public required string SetNum { get; set; }
        public required string Name { get; set; }
        public int Year { get; set; }
        public int NumParts { get; set; }
    }

    class Program
    {
        static List<LegoSet> legoSets = new List<LegoSet>();

        static void Main()
        {
            string filePath = "./csv/sets.csv";
            LoadLegoData(filePath);

            while (true)
            {
                Console.WriteLine("Choose a query:");
                Console.WriteLine("1. Find set by name");
                Console.WriteLine("2. Find set by year");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                        SearchByName();
                        break;
                    case "2":
                        SearchByYear();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static void LoadLegoData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("CSV file not found.");
                return;
            }

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip the header line
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue; // Skip any empty lines

                    var values = line.Split(',');

                    if (values.Length >= 4 && int.TryParse(values[2], out int year) && int.TryParse(values[3], out int numParts))
                    {
                        legoSets.Add(new LegoSet
                        {
                            SetNum = values[0],
                            Name = values[1],
                            Year = year,
                            NumParts = numParts
                        });
                    }
                }
            }
            Console.WriteLine($"Loaded {legoSets.Count} LEGO sets.");
        }

        // Method to search for LEGO sets by name.
        static void SearchByName()
        {
            Console.Write("Enter a name or part of a name: ");
            string? searchTerm = Console.ReadLine();

            // Check if the user has entered a valid search term.
            if (string.IsNullOrEmpty(searchTerm))
            {
                // If no search term is provided Exit
                Console.WriteLine("You must enter something.");
                return;
            }

            var results = legoSets.Where(set => set.Name?.ToLower().Contains(searchTerm.ToLower()) ?? false).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No results found.");
            }
            else
            {
                foreach (var set in results)
                {
                    Console.WriteLine($"{set.SetNum}: {set.Name} ({set.Year}) - {set.NumParts} parts");
                }
            }
        }

        static void SearchByYear()
        {
            Console.Write("Enter a year: ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int year))
                {
                    // Filter the LEGO sets by the entered year
                    var results = legoSets.Where(set => set.Year == year).ToList();

                    if (results.Count == 0)
                    {
                        Console.WriteLine("No results found.");
                    }
                    else
                    {
                        foreach (var set in results)
                        {
                            Console.WriteLine($"{set.SetNum}: {set.Name} ({set.Year}) - {set.NumParts} parts");
                        }
                    }
                    return; // Exit the method after processing results
                }
                else
                {
                    // Ask for a valid year if input is invalid.
                    Console.WriteLine("Invalid year, please try again.");
                }
            }
        }
    }
}


