using System;

public class Fraction
{
    private int numerator;
    private int denominator;

    //Constructor that has no parameters that initializes the number to 1/1.
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    //Constructor that has one parameter for the top and that initializes the denominator to 1.
    public Fraction(int top)
    {
        numerator = top;
        denominator = 1;
    }

    // 
    // Constructor that has two parameters, one for the top and one for the bottom.
    // Constructor that initializes with a numerator and denominator
    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        numerator = top;
        denominator = bottom;
    }

    // Getter and Setter for numerator
    public int GetNumerator()
    {
        return numerator;
    }

    public void SetNumerator(int value)
    {
        numerator = value;
    }

    // Getter and Setter for denominator
    public int GetDenominator()
    {
        return denominator;
    }

    public void SetDenominator(int value)
    {
        if (value == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        denominator = value;
    }

    // Method to return the fraction as a string
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    // Method to return the decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        Fraction f2 = new Fraction(5);
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        Fraction f4 = new Fraction(1, 3);
        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());
    }
}