using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Square : Rectangle
    {
        public Square(string name, double side) : base(name, side, side)
        {
        }
        public Square(double side) : this("Square", side) { }
        public Square() : this(1.0) { }
        public override string Description()
        {
            //Width = Height * 2; // This line would cause a compile-time error because Width and Height are inherited from Rectangle and are not accessible here if they are not marked as protected or public.
            return $"This is a square called {Name} with side {Width}, area {Area()}, and perimeter {Perimeter()}.";
        }
    }
}
