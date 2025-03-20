using System;
using System;
using System.Collections.Generic;
using System.IO;

// Clase que representa una entrada en el diario
class Entry
{
    public string Date { get; set; } // Fecha de la entrada
    public string Prompt { get; set; } // Pregunta o indicación para escribir
    public string Response { get; set; } // Respuesta del usuario

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    // Método para mostrar la entrada en la consola
    public void Display()
    {
        Console.WriteLine($"Date: {Date}\nQuestion: {Prompt}\nAnswer: {Response}\n");
    }
}

// Clase que representa el diario en sí
class Journal
{
    private List<Entry> entries = new List<Entry>(); // Lista de entradas en el diario
    private List<string> prompts = new List<string>()
    {
       
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see God's hand in my life today?",
        "What was the strongest emotion I felt today?",
        "If I could relive a moment of my day, what would it be?"
    };

    // Método para escribir una nueva entrada
    public void WriteNewEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)]; // Elegir una pregunta aleatoria
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToShortDateString();
        entries.Add(new Entry(date, prompt, response));
    }

    // Método para mostrar todas las entradas guardadas en el diario
    public void DisplayJournal()
    {
        foreach (Entry entry in entries)
        {
            entry.Display();
        }
    }

    // Método para guardar el diario en un archivo
    public void SaveToFile()
    {
        Console.Write("Enter the file name to save: ");
        string fileName = Console.ReadLine();
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            foreach (Entry entry in entries)
            {
                outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    // Método para cargar el diario desde un archivo
    public void LoadFromFile()
    {
        Console.Write("Enter the file name to upload:");
        string fileName = Console.ReadLine();
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            entries.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
            Console.WriteLine("Complete");
        }
        else
        {
            Console.WriteLine("the document doesnt exist.");
        }
    }
}

// Clase principal del programa
class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        bool running = true;
        
        while (running)
        {
         
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.WriteNewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    journal.SaveToFile();
                    break;
                case "4":
                    journal.LoadFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
