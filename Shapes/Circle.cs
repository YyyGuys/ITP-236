using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(string name, double radius) : base(name)
        {
            Radius = radius;
        }
        public Circle(double radius) : this("Circle", radius) { }
        public Circle() : this(1.0) { }
        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }
        public override double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }
        public override string Description()
        {
            return $"This is a circle called {Name} with radius {Radius}, area {Area()}, and perimeter {Perimeter()}.";
        }
    }
}
