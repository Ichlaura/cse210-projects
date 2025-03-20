using System;

public class Entry
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
