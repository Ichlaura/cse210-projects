using System;
using System.Collections.Generic;

/*
 * EXCEEDS REQUIREMENTS:
 * 1. Includes 3 different scriptures that can be practiced
 * 2. Randomly selects scripture each time program runs
 * 3. Allows user to select difficulty level (1-3) which determines
 *    how many words are hidden each turn (2-4 words)
 */

/*
*  REQUISITOS　EXTRAS:
* 1. Incluye 3 escrituras diferentes para practicar
* 2. Selecciona una escritura al azar cada vez que se ejecuta el programa
* 3. Permite al usuario seleccionar el nivel de dificultad (1-3), lo que determina
* cuántas palabras se ocultan en cada turno (2-4 palabras)
*/

//1. Reference Class
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int? _endVerse;

    // Constructor for single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    // Constructor for verse range
    public Reference(string book, int chapter, int verse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_endVerse.HasValue)
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        else
            return $"{_book} {_chapter}:{_verse}";
    }
}


//2. Word Class
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
            return new string('_', _text.Length);
        else
            return _text;
    }
}
//3. Scripture Class

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        
        // Split text into words
        string[] wordArray = text.Split(' ');
        foreach (string wordText in wordArray)
        {
            _words.Add(new Word(wordText));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        int wordsHidden = 0;

        while (wordsHidden < numberToHide && !IsCompletelyHidden())
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                wordsHidden++;
            }
        }
    }

    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText() + "\n\n";
        
        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }

        return displayText.Trim();
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }
}

//4. Program Class

//class Program
//{
  //  static void Main(string[] args)
    //{
        // Create a sample scripture
      //  Reference reference = new Reference("John", 3, 16);
        //Scripture scripture = new Scripture(reference, 
          //  "For God so loved the world that he gave his one and only Son, " +
            //"that whoever believes in him shall not perish but have eternal life.");

        // Main loop
        //while (true)
        //{
          //  Console.Clear();
           // Console.WriteLine(scripture.GetDisplayText());
            
            //if (scripture.IsCompletelyHidden())
              //  break;

            //Console.WriteLine("\nPress Enter to continue or type 'quit' to finish:");
            //string input = Console.ReadLine();
            
            //if (input.ToLower() == "quit")
              //  break;

        //    scripture.HideRandomWords(3); // Hide 3 words at a time
        //}
    //}
//}
 //



 //Exceeding Requirements


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Scripture Memorizer!");
        
        // Create a list of scriptures
        List<Scripture> scriptures = new List<Scripture>();
        
        // Add multiple scriptures
        scriptures.Add(new Scripture(
            new Reference("John", 3, 16),
            "For God so loved the world that he gave his one and only Son, " +
            "that whoever believes in him shall not perish but have eternal life."
        ));
        
        scriptures.Add(new Scripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all your heart and lean not on your own understanding; " +
            "in all your ways submit to him, and he will make your paths straight."
        ));
        
        scriptures.Add(new Scripture(
            new Reference("Philippians", 4, 13),
            "I can do all things through Christ who strengthens me."
        ));

        // Select a random scripture
        Random random = new Random();
        Scripture currentScripture = scriptures[random.Next(scriptures.Count)];

        // Ask for difficulty level
        Console.WriteLine("\nChoose difficulty (1-3 where 1 is easiest and 3 is hardest):");
        int difficulty = GetNumberBetween(1, 3);
        int wordsToHide = difficulty + 1; // 2-4 words per turn

        // Main loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine(currentScripture.GetDisplayText());
            
            if (currentScripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nCongratulations! You've memorized the entire scripture!");
                break;
            }

            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit:");
            string input = Console.ReadLine().ToLower();
            
            if (input == "quit")
                break;

            currentScripture.HideRandomWords(wordsToHide);
        }
    }

    // Helper method to get valid number input
    static int GetNumberBetween(int min, int max)
    {
        int number;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out number) && number >= min && number <= max)
                return number;
            Console.Write($"Please enter a number between {min} and {max}: ");
        }
    }
}