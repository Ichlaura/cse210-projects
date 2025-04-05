using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

// Programa de Mindfulness con Seguimiento de Estado de Ánimo
// Mindfulness Program with Mood Tracker
// Este programa ayuda a practicar mindfulness con varias actividades y ahora incluye un seguimiento del estado de ánimo
// This program helps practice mindfulness with various activities and now includes mood tracking


class Program
{
    static void Main(string[] args)
    {
        MoodTracker moodTracker = new MoodTracker();

        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Track Your Mood");
                Console.WriteLine("5. View Mood History");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an activity: ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        moodTracker.TrackMood();
                        continue;
                    case "5":
                        moodTracker.ShowMoodHistory();
                        continue;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Thread.Sleep(1000);
                        continue;
                }

                if (activity != null)
                {
                    activity.Run();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("The program will restart...");
                Thread.Sleep(3000);
            }
        }
    }
}

class MoodTracker
{
    private readonly string _moodHistoryFile = "mood_history.txt";

    public void TrackMood()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("How are you feeling today?");
            Console.WriteLine("1. 😊 Happy");
            Console.WriteLine("2. 😌 Calm");
            Console.WriteLine("3. 😞 Sad");
            Console.WriteLine("4. 😠 Angry");
            Console.WriteLine("5. 😨 Anxious");
            Console.Write("Choose your mood (1-5): ");

            int moodChoice;
            if (!int.TryParse(Console.ReadLine(), out moodChoice) || moodChoice < 1 || moodChoice > 5)
            {
                Console.WriteLine("Invalid selection. Please choose a number between 1 and 5.");
                Thread.Sleep(2000);
                return;
            }

            string[] moodNames = { "Happy", "Calm", "Sad", "Angry", "Anxious" };
            string[] moodEmojis = { "😊", "😌", "😞", "😠", "😨" };

            string selectedMood = moodNames[moodChoice - 1];
            string selectedEmoji = moodEmojis[moodChoice - 1];

            Console.WriteLine($"\nYou selected: {selectedMood} {selectedEmoji}");
            Console.Write("Would you like to add any notes about your mood? (y/n): ");
            
            string notes = string.Empty;
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Enter your notes: ");
                notes = Console.ReadLine() ?? string.Empty;
            }

            SaveMoodEntry(selectedMood, selectedEmoji, notes);

            Console.WriteLine("\nThank you for tracking your mood!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error tracking mood: {ex.Message}");
            Thread.Sleep(2000);
        }
    }

    private void SaveMoodEntry(string mood, string emoji, string notes)
    {
        try
        {
            string directory = Path.GetDirectoryName(_moodHistoryFile);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = File.AppendText(_moodHistoryFile))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {mood} {emoji} {(string.IsNullOrEmpty(notes) ? "" : $"| Notes: {notes}")}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Could not save mood entry: {ex.Message}");
        }
    }

    public void ShowMoodHistory()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Your Mood History");
            Console.WriteLine("-----------------");

            if (!File.Exists(_moodHistoryFile))
            {
                Console.WriteLine("No mood history found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            string[] entries = File.ReadAllLines(_moodHistoryFile);
            if (entries.Length == 0)
            {
                Console.WriteLine("No mood entries yet.");
            }
            else
            {
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing mood history: {ex.Message}");
            Thread.Sleep(2000);
        }
    }
}

abstract class Activity
{
    protected string Name { get; set; } = string.Empty;
    protected string Description { get; set; } = string.Empty;
    protected int Duration { get; set; }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Starting {Name}");
        Console.WriteLine(Description);
        
        while (true)
        {
            Console.Write("Enter duration in seconds: ");
            int duration;
            if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
            {
                Duration = duration;
                break;
            }
            Console.WriteLine("Invalid input. Please enter a positive number.");
        }

        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Good job!");
        ShowSpinner(2);
        Console.WriteLine($"You have completed the {Name} for {Duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        char[] spinnerChars = new[] { '/', '-', '\\', '|' };
        for (int i = 0; i < seconds * 2; i++)
        {
            foreach (char c in spinnerChars)
            {
                Console.Write(c);
                Thread.Sleep(250);
                Console.Write("\b \b");
            }
        }
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public abstract void Run();
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void Run()
    {
        DisplayStartingMessage();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(4);
            Console.WriteLine();
            Console.Write("Breathe out... ");
            ShowCountdown(6);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}

class ReflectionActivity : Activity
{
    private readonly List<string> _prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly List<string> _questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection Activity";
        Description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Random random = new Random();

        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.WriteLine();

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            string question = _questions[random.Next(_questions.Count)];
            Console.Write(question + " ");
            ShowSpinner(5);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}

class ListingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        Name = "Listing Activity";
        Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void Run()
    {
        DisplayStartingMessage();
        Random random = new Random();

        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.WriteLine();

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        Console.WriteLine("Start listing items:");
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"\nYou listed {items.Count} items:");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item}");
        }

        DisplayEndingMessage();
    }
}