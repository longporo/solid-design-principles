using System;
using System.Collections.Generic;

namespace SRP
{
    //
    // DEMO: Single Responsibility Principle (SRP) 
    //
    // A class should take one responsibility and there should be one reason to change that class. 
    //

    //
    // Let's rework the example shown earlier. We should start looking at having a single responsibility
    // for each class - this means breaking the class into some extra classes.
    // 

    //
    // Let's start with the example (FirstDesign) and just work on SRP and Total Area Calculation
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
        // Let's remove this total area calculation and move it to a new class
        // So the Shape class just handles shapes and individual areas (meaning
        // there is some kind of single responsibility going on in the Shape class)
        //

        /*
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
        */
    }

    //
    // Let's create an AreaCalculator class (knowing this violates OCP also so do it anyway - more later)
    //

    public class AreaCalculator {

        //
        // note this is the bad option as it calculates and prints
        //
        //
        // comment out (remove) this method for the final version
        //
        public static void CalculateTotalAreaBad(List<Shape> shapes)
        {
            double totalArea = 0;

            //
            // Calculate the total area
            //

            foreach (var shape in shapes)
                totalArea += shape.getArea();

            //
            // output the information to the display
            //

            Console.WriteLine("Total Area: {0}\n", totalArea);
        }


        //
        // final version  - this is better and doesn't violate SRP
        //
        public static double CalculateTotalArea(List<Shape> shapes)
        {
            double totalArea = 0;

            //
            // Calculate the total area
            //

            foreach (var shape in shapes)
                totalArea += shape.getArea();

            //
            // Return the total area
            //

            return totalArea;
        }
    }

    public class OutputFormatter {
        public static void toJSON (double area) {
            Console.WriteLine("{{ \"Total Area\" : {0} }}\n", area);
        }

        public static void toHTML (double area) {
            Console.WriteLine("<span><strong>Total Area: </strong></span><span>{0}</span>\n", area);
        }

        public static void toTEXT (double area) {
            Console.WriteLine("Total Area: {0}\n", area);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Single Responsibility Principle (SRP)");
            Console.WriteLine("=====================================\n");


        //
        // Create a list of shapes
        //
        var theShapes = new List<Shape>
            {
                new Shape(ShapeType.Square, 100.0),
                new Shape(ShapeType.Circle, 100.0),
                new Shape(ShapeType.Square, 200.0),
                new Shape(ShapeType.EquilateralTriangle, 100.0)
            };

        //
        // Calculate the total area using the static method - just calling it - as before - still bad!
        //

        AreaCalculator.CalculateTotalAreaBad(theShapes);

        //
        // Calculate the total area using the static method - just calling it - as before! - better
        //

        Console.WriteLine("Total Area: {0}\n", AreaCalculator.CalculateTotalArea(theShapes));

        //
        // Using the newly created output formatter
        //

        OutputFormatter.toJSON (AreaCalculator.CalculateTotalArea(theShapes));
        OutputFormatter.toHTML(AreaCalculator.CalculateTotalArea(theShapes));
        OutputFormatter.toTEXT(AreaCalculator.CalculateTotalArea(theShapes));

        }
    }
}
