public class Square : Shape
{
    private double side;

    // Constructor que acepta color y lado
    public Square(string color, double side) : base(color)
    {
        this.side = side;
    }

    // Sobrescribir el método GetArea para el cuadrado
    public override double GetArea()
    {
        return side * side;
    }
}
