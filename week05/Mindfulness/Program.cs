using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    protected int duration;
    protected string name;
    protected string description;

     // Exceeding requirement: Added countdown feature for reflection activity.
    // Exceeding requirement: Added a spinner to simulate activity progress.

    // Exceeding requirement: Added colorful text output and better user interaction.
    public void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Starting {name} Activity\n");
        Console.ResetColor();
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
        Console.ResetColor();

        PerformActivity();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nGreat job!");
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        ShowSpinner(3);
        Console.ResetColor();
    }

  
    public void PerformActivity()
    {
     
    }

    // Exceeding requirement: Added a spinner to simulate activity progress.
    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds * 2; i++)
        {
            Console.Write("/"); 
            Thread.Sleep(250);
            Console.Write("\b \b");
            Console.Write("-"); 
            Thread.Sleep(250);
            Console.Write("\b \b");
        }
    }

    // Exceeding requirement: Added countdown feature for reflection activity.
    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b  \b\b");
        }
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        name = "Breathing";
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

   
    public new void PerformActivity()
    {
        DateTime end = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < end)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nBreathe in...");
            Countdown(4);
            Console.WriteLine("Breathe out...");
            Countdown(4);
            Console.ResetColor();
        }
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
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
        name = "Reflection";
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

  
    public new void PerformActivity()
    {
        Random rand = new Random();
        Console.WriteLine("\n" + prompts[rand.Next(prompts.Count)]);
        Console.WriteLine("\nReflect on the following questions:");

        DateTime end = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < end)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(questions[rand.Next(questions.Count)]);
            ShowSpinner(5);
            Console.ResetColor();
        }
    }
}

class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        name = "Listing";
        description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    
    public new void PerformActivity()
    {
        Random rand = new Random();
        Console.WriteLine("\n" + prompts[rand.Next(prompts.Count)]);
        Console.WriteLine("\nYou will have a few seconds to think before you begin...");
        Countdown(5);

        Console.WriteLine("Start listing items. Press Enter after each item:");
        DateTime end = DateTime.Now.AddSeconds(duration);
        int count = 0;

        while (DateTime.Now < end)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadLine();
                count++;
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou listed {count} items.");
        Console.ResetColor();
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.ResetColor();
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": return;
                default:
                    Console.WriteLine("Invalid choice.");
                    Thread.Sleep(1000);
                    continue;
            }

            activity.Start();
        }
    }
}
