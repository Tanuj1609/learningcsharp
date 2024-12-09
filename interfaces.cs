using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning
{
    public interface Idrawable
    {
        void draw();
    }
    public class Circle : Idrawable
    {
        public void draw()
        {
            Console.WriteLine("draw a circle");
        }
    }

    public class Rectangle : Idrawable

    {
        public void draw()
        {
            Console.WriteLine("draw a reactangle");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Idrawable d = new Circle();
            Idrawable k = new Rectangle();

            d.draw();
            k.draw();
        }
    }
}