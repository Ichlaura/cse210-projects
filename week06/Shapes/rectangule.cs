public class Rectangle : Shape
{
    private double length;
    private double width;

    // Constructor que acepta color, largo y ancho
    public Rectangle(string color, double length, double width) : base(color)
    {
        this.length = length;
        this.width = width;
    }

    // Sobrescribir el método GetArea para el rectángulo
    public override double GetArea()
    {
        return length * width;
    }
}
