using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>(); // Lista de entradas en el diario
    private List<string> _prompts = new List<string>()
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
        string prompt = _prompts[random.Next(_prompts.Count)]; // Elegir una pregunta aleatoria
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToShortDateString();
        _entries.Add(new Entry(date, prompt, response));
    }

    // Método para mostrar todas las entradas guardadas en el diario
    public void DisplayJournal()
    {
        foreach (Entry entry in _entries)
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
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    // Método para cargar el diario desde un archivo
    public void LoadFromFile()
    {
        Console.Write("Enter the file name to upload: ");
        string fileName = Console.ReadLine();
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            _entries.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    _entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
            Console.WriteLine("Complete");
        }
        else
        {
            Console.WriteLine("The document doesn't exist.");
        }
    }
}
