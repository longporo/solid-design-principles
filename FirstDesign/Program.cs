using System;
using System.Collections.Generic;

namespace FirstDesign
{
    //
    // DEMO: SOLID OOP Development (First Design) 
    //
    // Here is a working program that does not obey the SOLID design principles 
    //
    // Based on the example given in:
    //
    //      SOLID Design principles with C# practical examples (Do Net For All)
    //      https://www.dotnetforall.com/solid-design-principles-examples/
    //

    public enum ShapeType
    {
        Square,
        Circle,
        EquilateralTriangle,
        Unknown
    }
    public class Shape
    {
        // Can add more shapes if needed!

        private readonly double width;
        public ShapeType type = ShapeType.Unknown;

        public Shape(ShapeType type, double width)
        {
            this.type = type;
            this.width = width;
        }

        //
        // calculate the area of a shape
        //
        public double getArea()
        {
            switch (type)
            {
                case ShapeType.Square:
                    return width * width;
                case ShapeType.Circle:
                    return Math.PI * (width / 2) * (width / 2);
                case ShapeType.EquilateralTriangle:
                    return (Math.Sqrt(3) / 4) * width * width;
            }
            throw new SystemException("Can`t compute area of unknown shape!");
        }

        // 
        // calculate the total area of a list of shapes
        //
        public static void CalculateTotalArea(List<Shape> shapes)
        {
            double totalArea = 0;

            //
            // Calculate the total area
            //

            foreach (var shape in shapes)
                totalArea += shape.getArea();

            //
            // Output the information to the display
            //
            
            Console.WriteLine("Total Area: {0}\n", totalArea);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine("=========================================================");
            Console.WriteLine("SOLID OOP Development (First Design) - WORKING BAD DESIGN");
            Console.WriteLine("=========================================================\n\n");

            Console.WriteLine("For every shape added we have to change the GetArea()");
            Console.WriteLine("and the CalculateTotalArea() method!\n\n");

            Console.WriteLine("There are many reasons for the class to change. ");
            Console.WriteLine("It means the class is violating the SRP.\n\n");

            Console.WriteLine("whenever we have to add a new shape we have to add ");
            Console.WriteLine("a new const as well as add a switch and if statement");
            Console.WriteLine("in both of the methods. \n\n");

            Console.WriteLine("The class is not closed for modifications and not easily");
            Console.WriteLine("extendable. It means the class is violating the OCP.\n\n");


            //
            // create a list of shapes
            //
            var theShapes = new List<Shape>
            {
                new Shape(ShapeType.Square, 100.0),
                new Shape(ShapeType.Circle, 100.0),
                new Shape(ShapeType.Square, 200.0),
                new Shape(ShapeType.EquilateralTriangle, 100.0)
            };

            //
            // calculate the total area using the static method - just calling it!
            //

            Shape.CalculateTotalArea(theShapes);

            Console.WriteLine("=========================================================");
            Console.WriteLine("SOLID OOP Development (First Design) - WORKING BAD DESIGN");
            Console.WriteLine("=========================================================\n\n");
        }
    }
}
