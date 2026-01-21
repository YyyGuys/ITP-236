using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal abstract class Shape
    {
        public string Name { get; set; }
        public Shape(string name)
        {
            Name = name;
        }
        public Shape() : this("Unknown Shape") { }
        public abstract double Area();
        public abstract double Perimeter();
        public virtual string Description()
        {
            return $"This is a shape called {Name} with area {Area()} and perimeter {Perimeter()}.";
        }
    }
}
