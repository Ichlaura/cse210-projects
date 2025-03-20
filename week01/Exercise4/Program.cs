using System;

class Program
{
    static void Main(string[] args)
    {
         // Crear una lista para almacenar los números ingresados por el usuario
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 to quit.");

        // Bucle para pedir los números al usuario hasta que ingrese 0
        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine()); // Leer y convertir la entrada a un número entero

            if (number == 0) // Si el usuario ingresa 0, salimos del bucle
                break;

            numbers.Add(number); // Agregar el número ingresado a la lista
        }

        // Verificar que la lista no esté vacía antes de realizar los cálculos
        if (numbers.Count > 0)
        {
            // Calcular la suma de todos los números de la lista
            int sum = numbers.Sum();

            // Calcular el promedio de los números en la lista
            double average = numbers.Average();

            // Encontrar el número más grande de la lista
            int max = numbers.Max();

            // Imprimir los resultados básicos
            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");

            // **Desafío Extra 1**: Encontrar el número positivo más pequeño
            // Se usa `Where(n => n > 0)` para filtrar solo los positivos y `.Min()` para encontrar el menor
            int smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty(int.MaxValue).Min();
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");

            // **Desafío Extra 2**: Ordenar la lista de menor a mayor
            numbers.Sort();

            // Imprimir la lista ordenada
            Console.WriteLine("The sorted list is:");
            foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }
        }
        else
        {
            // Si el usuario no ingresó números, mostrar un mensaje de advertencia
            Console.WriteLine("No numbers were entered.");
        }
    }
    
    
}
