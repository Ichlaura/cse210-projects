using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;
    static string filename = "goals.txt";

    static void Main(string[] args)
    {
        LoadGoals();

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
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Goodbye! ✌");
                    Console.ResetColor();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input ❌");
                    Console.ResetColor();
                    break;
            }
        } while (input != "6");
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
        Console.WriteLine("4. 💾 Save Goals");
        Console.WriteLine("5. 📂 Load Goals");
        Console.WriteLine("6. ❌ Quit");
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
            case "1":
                goals.Add(new SimpleGoal(name, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, points));
                break;
            case "3":
                int target, bonus;
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
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Goal created successfully! ✨");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ListGoals()
    {
        Console.WriteLine("\n📋 Your Goals:");
        if (goals.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No goals found. Create some first! ✨");
            Console.ResetColor();
        }
        else
        {
            int i = 1;
            foreach (Goal g in goals)
            {
                string status = g.GetStatus();
                if (status.Contains("[ ]"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{i++}. {status} - Keep going, you can do it! 💪");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i++}. {status} - Goal completed! 🎉");
                }
            }
            Console.ResetColor();
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No goals available to record. Create some first! ✨");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Which goal did you complete?");
        ListGoals();
        Console.Write("Enter goal number: ");

        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > goals.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input! Please select a valid goal number.");
            Console.ResetColor();
            Console.Write("Enter goal number: ");
        }

        index--;
        int gained = goals[index].RecordEvent();
        score += gained;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"🎉 You gained {gained} points!");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void SaveGoals()
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine(score);
                foreach (Goal goal in goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Goals saved successfully! 💾");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving goals: {ex.Message}");
            Console.ResetColor();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void LoadGoals()
    {
        if (!File.Exists(filename))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No saved goals found. Starting fresh! 🌱");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            goals.Clear();

            if (lines.Length > 0 && int.TryParse(lines[0], out int loadedScore))
            {
                score = loadedScore;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string type = parts[0];
                string[] details = parts[1].Split(',');

                switch (type)
                {
                    case "SimpleGoal":
                        bool isComplete = bool.Parse(details[1]);
                        var simpleGoal = new SimpleGoal(details[0], int.Parse(details[2]));
                        if (isComplete) simpleGoal.RecordEvent();
                        goals.Add(simpleGoal);
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(details[0], int.Parse(details[1])));
                        break;
                    case "ChecklistGoal":
                        int current = int.Parse(details[2]);
                        int target = int.Parse(details[3]);
                        var checklistGoal = new ChecklistGoal(
                            details[0],
                            int.Parse(details[1]),
                            target,
                            int.Parse(details[4]));
                        for (int j = 0; j < current; j++)
                        {
                            checklistGoal.RecordEvent();
                        }
                        goals.Add(checklistGoal);
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Goals loaded successfully! 📂");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading goals: {ex.Message}");
            Console.ResetColor();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
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
    public abstract string GetStringRepresentation();
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

    public override string GetStringRepresentation() =>
        $"SimpleGoal:{_name},{_isComplete},{_points}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent() => _points;

    public override string GetStatus() => $"[∞] {_name}";

    public override string GetStringRepresentation() =>
        $"EternalGoal:{_name},{_points}";
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
                Console.Beep();
                return _points + _bonus;
            }
            return _points;
        }
        return 0;
    }

    public override string GetStatus() =>
        $"{(_currentCount >= _targetCount ? "[X]" : "[ ]")} {_name} (Completed {_currentCount}/{_targetCount})";

    public override string GetStringRepresentation() =>
        $"ChecklistGoal:{_name},{_points},{_currentCount},{_targetCount},{_bonus}";
}