public abstract class Shape
{
    private string color;

    // Constructor para inicializar el color
    public Shape(string color)
    {
        this.color = color;
    }

    // Método para obtener el color
    public string GetColor()
    {
        return color;
    }

    // Método abstracto para calcular el área
    public abstract double GetArea();
}
