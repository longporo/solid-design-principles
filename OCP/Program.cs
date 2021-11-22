using System;
using System.Collections.Generic;

namespace OCP
{
    //
    // DEMO: Open Closed Principle (OCP) 
    //
    // A software module/class is open for extension and closed for modification. 
    //

    // 
    // Let's create a Shape class hierarchy that are open for extension and closed
    // for modification - by this we mean that in order to extend the functionality
    // of the application we need to ensure that when we don't change existing code.
    //

    //
    // We introduce a getArea() method in the base class and ensure that all shapes
    // implement this method. We can do this using abstract classes and/or interfaces.
    // We will begin with the abstract class approach
    //

    //
    // Let's retain some of the existing attributes of shapes (only interested) in
    // calculating the area based on one value. 
    //

    public abstract class Shape
    {
        protected double width;
        public abstract double getArea();
    }

    // 
    // Note the shape class specifies an interface method and retains the width instance variable
    //

    //
    // Removing the Area Calculations from the Shape class and devolve to the individual
    // shapes to calculate themselves. What would be do if we removed this and had individual 
    // shapes manage their own information required for calculating area? It's something to 
    // think about in the design at some point. For now let's focus on OCP!
    // 

    public class Square : Shape
    {
        public override double getArea() { return width * width; ; }
        public Square(double width)
        {
            this.width = width;
        }
    }
    public class Circle : Shape
    {
        public override double getArea() { return Math.PI * (width / 2) * (width / 2); }
        public Circle(double width)
        {
            this.width = width;
        }
    }

    public class EquilateralTriangle : Shape
    {
        public override double getArea() { return (Math.Sqrt(3) / 4) * width * width; }
        public EquilateralTriangle(double width)
        {
            this.width = width;
        }
    }

    // 
    // Now Every shape looks after it's own area calculation. However, the Area Calculator, while
    // it is doing a great job in terms of SRP, it also breaks ORP as it needs to change (in the 
    // previous example) whenever a new shap is added. We need to update this class - let's use 
    // the final version from the SRP example.
    // 


    public class AreaCalculator
    {

        //
        // using final SRP version to begin with (returns area - no printing also)
        //

        public static double CalculateTotalArea(List<Shape> shapes)
        {
            double totalArea = 0;

            //
            // Calculate the total area
            //
            
            foreach (var shape in shapes)
                totalArea += shape.getArea();

            // return the total area
            return totalArea;
        }

    }

    public class OutputFormatter
    {
        public static void toJSON(double area)
        {
            Console.WriteLine("{{ \"Total Area\" : {0} }}\n", area);
        }

        public static void toHTML(double area)
        {
            Console.WriteLine("<span><strong>Total Area: </strong></span><span>{0}</span>\n", area);
        }

        public static void toTEXT(double area)
        {
            Console.WriteLine("Total Area: {0}\n", area);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================\n");
            Console.WriteLine("Open closed principle (OCP)");
            Console.WriteLine("===========================\n");

            //
            // Creating as new list of shapes
            //
            var theShapes = new List<Shape>
            {
                new Square (100.0),
                new Circle (100.0),
                new Square (200.0),
                new EquilateralTriangle (100.0)
            };

            //
            // Calculate the total area using the static method - just calling it - as before! - better
            //

            Console.WriteLine("Total Area: {0}\n", AreaCalculator.CalculateTotalArea(theShapes));

            //
            // Using the newly created output formatter
            //

            OutputFormatter.toJSON(AreaCalculator.CalculateTotalArea(theShapes));
            OutputFormatter.toHTML(AreaCalculator.CalculateTotalArea(theShapes));
            OutputFormatter.toTEXT(AreaCalculator.CalculateTotalArea(theShapes));

        }
    }
}
