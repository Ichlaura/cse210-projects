public class Circle : Shape
{
    private double radius;

    // Constructor que acepta color y radio
    public Circle(string color, double radius) : base(color)
    {
        this.radius = radius;
    }

    // Sobrescribir el método GetArea para el círculo
    public override double GetArea()
    {
        return Math.PI * radius * radius;
    }
}
