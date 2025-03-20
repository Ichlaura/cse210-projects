using System;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello World! This is the Exercise5 Project.");
    
     // Llamamos a la función para mostrar el mensaje de bienvenida
        DisplayWelcome();

        // Llamamos a la función que pide el nombre del usuario y guardamos el valor retornado
        string userName = PromptUserName();

        // Llamamos a la función que pide el número favorito del usuario y guardamos el valor retornado
        int favoriteNumber = PromptUserNumber();

        // Llamamos a la función que calcula el cuadrado del número
        int squaredNumber = SquareNumber(favoriteNumber);

        // Llamamos a la función que muestra el resultado final
        DisplayResult(userName, squaredNumber);
    }

    // Función que muestra un mensaje de bienvenida
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // Función que pide el nombre del usuario y lo devuelve como string
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Función que pide el número favorito del usuario y lo devuelve como entero
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    // Función que recibe un número y devuelve su cuadrado
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Función que muestra el resultado con el nombre y el número al cuadrado
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }
    
    
}