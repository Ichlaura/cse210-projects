using System;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello World! This is the Exercise1 Project.");
     //  first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
        
        //  last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();
        
        // Display name
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
    }
