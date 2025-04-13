using System;
using System.Collections.Generic;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main(string[] args)
    {
        string input;
        do
        {
            ShowTitle();
            ShowMenu();
            input = Console.ReadLine();
            switch (input)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Goodbye! ✌️"); Console.ResetColor(); break;
                default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid input ❌"); Console.ResetColor(); break;
            }
        } while (input != "4");
    }

    static void ShowTitle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("===========================");
        Console.WriteLine("🌟 Eternal Quest Tracker 🌟");
        Console.WriteLine("===========================");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Current Score: {score} {GetTitle(score)}");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void ShowMenu()
    {
        Console.WriteLine("1. 🛠️ Create New Goal");
        Console.WriteLine("2. 📋 List Goals");
        Console.WriteLine("3. ✅ Record Event");
        Console.WriteLine("4. ❌ Quit");
        Console.Write("Choose an option: ");
    }

    static string GetTitle(int score)
    {
        if (score >= 5000) { Console.Beep(); return "🌟 Celestial Knight"; }
        else if (score >= 2000) return "✨ Eternal Warrior";
        else if (score >= 500) return "🌱 Faithful Novice";
        return "👣 Beginner";
    }

    static void CreateGoal()
    {
        string type = "";
        // Validating goal type selection
        while (type != "1" && type != "2" && type != "3")
        {
            Console.WriteLine("What type of goal?");
            Console.WriteLine("1. 🧩 Simple");
            Console.WriteLine("2. ♾️ Eternal");
            Console.WriteLine("3. 📌 Checklist");
            Console.Write("Choice: ");
            type = Console.ReadLine();
            if (type != "1" && type != "2" && type != "3")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please select a valid goal type (1, 2, or 3).");
                Console.ResetColor();
            }
        }

        Console.Write("Goal Name: ");
        string name = Console.ReadLine();

        int points;
        // Validating points per completion
        while (true)
        {
            Console.Write("Points per completion: ");
            if (int.TryParse(Console.ReadLine(), out points))
            {
                break;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input! Please enter a valid number for points.");
            Console.ResetColor();
        }

        switch (type)
        {
            case "1": goals.Add(new SimpleGoal(name, points)); break;
            case "2": goals.Add(new EternalGoal(name, points)); break;
            case "3":
                int target, bonus;
                // Validating target number
                while (true)
                {
                    Console.Write("How many times to complete?: ");
                    if (int.TryParse(Console.ReadLine(), out target))
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a valid number for target.");
                    Console.ResetColor();
                }
                // Validating bonus points
                while (true)
                {
                    Console.Write("Bonus points on completion: ");
                    if (int.TryParse(Console.ReadLine(), out bonus))
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter a valid number for bonus.");
                    Console.ResetColor();
                }
                goals.Add(new ChecklistGoal(name, points, target, bonus));
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid type selection! Goal creation failed.");
                Console.ResetColor();
                return;
        }
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Goal created successfully! ✨");
        Console.ResetColor();
    }

 static void ListGoals()
{
    Console.WriteLine("\n📋 Your Goals:");
    if (goals.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No goals found. Create some first! ✨");
        Console.ResetColor();
        return;
    }

    int i = 1;
    foreach (Goal g in goals)
    {
        string status = g.GetStatus();
        if (status.Contains("[ ]"))  // Si el goal no está completado
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{i++}. {status} - Keep going, you can do it! 💪");
        }
        else  // Si el goal está completado
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{i++}. {status} - Goal completed! 🎉");
        }
    }
    Console.ResetColor();
    Console.WriteLine();
}


    static void RecordEvent()
    {
        Console.WriteLine("Which goal did you complete?");
        ListGoals();
        int index;
        // Validating goal number input
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= goals.Count)
            {
                index--;  // Adjust for zero-based index
                int gained = goals[index].RecordEvent();
                score += gained;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"🎉 You gained {gained} points!");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please select a valid goal number.");
                Console.ResetColor();
            }
        }
    }
}

abstract class Goal
{
    protected string _name;
    protected int _points;

    public Goal(string name, int points)
    {
        _name = name;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
}

class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override string GetStatus() => _isComplete ? $"[X] {_name}" : $"[ ] {_name}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent() => _points;
    public override string GetStatus() => $"[∞] {_name}";
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount = 0;
    private int _bonus;

    public ChecklistGoal(string name, int points, int target, int bonus) : base(name, points)
    {
        _targetCount = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            if (_currentCount == _targetCount)
            {
                Console.Beep(); // Sonido cuando se completa el objetivo
                return _points + _bonus; // Da puntos adicionales al completar el objetivo
            }
            return _points; // Solo puntos si no se completa aún
        }
        return 0; // No da puntos si el objetivo ya se completó
    }

    public override string GetStatus()
    {
        if (_currentCount >= _targetCount)
        {
            return $"[X] {_name} (Completed {_currentCount}/{_targetCount})";
        }
        return $"[ ] {_name} (Completed {_currentCount}/{_targetCount})";
    }
}
