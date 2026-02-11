// See https://aka.ms/new-console-template for more information
using Shapes;
using EID;
Console.WriteLine("Shapes");

List<Shape> shapes = new()
{
    new Circle("My Circle".Right(6), 5.0),
    new Rectangle("My Rectangle", 4.0, 6.0),
    new Square("My Square", 3.0),
    new Circle(2.5),
    new Rectangle(2.0, 3.0),
    new Square(4.0),
    new Circle(),
    new Rectangle(),
    new Square()
};

foreach (var shape in shapes)
{
    Console.WriteLine(shape.Description());
}
Console.ReadKey();
