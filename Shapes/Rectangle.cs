using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Rectangle : Shape    
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(string name, double width, double height) : base(name)
        {
            Width = width;
            Height = height;
        }
        public Rectangle(double width, double height) : this("Rectangle", width, height) { }
        public Rectangle() : this(1.0, 1.0) { }
        public override double Area()
        {
            return Width * Height;
        }
        public override double Perimeter()
        {
            return 2 * (Width + Height);
        }
        public override string Description()
        {
            return $"This is a rectangle called {Name} with width {Width}, height {Height}, area {Area()}, and perimeter {Perimeter()}.";
        }
    }
}
