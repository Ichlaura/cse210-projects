using System;

class Program
{
    static void Main(string[] args)
    {
      // Ask grade percentage
        Console.Write("Enter your grade percentage: ");
        int grade = int.Parse(Console.ReadLine());

        string letter;
        string sign = "";

        // letter grade
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // (+ or -)
        int lastDigit = grade % 10;
        if (grade >= 60 && letter != "A" && letter != "F")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        //  A+ and F grades
        if (letter == "A" && grade >= 90 && grade < 93)
        {
            sign = "-";
        }
        else if (letter == "F")
        {
            sign = "";
        }

        // letter grade
        Console.WriteLine($"Your grade is {letter}{sign}.");

        //  pass or fail
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep trying! You'll do better next time.");
        }
   
    }
}