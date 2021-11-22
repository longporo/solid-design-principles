using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace LSP
{
    //
    // DEMO: Liskov Substitution Principle (LSP) 
    //
    // This is a Substitutability Principle in object-oriented programming languages.
    //
    // This principle states that, if S is a subtype of T, then objects of type T should 
    // be replaced with the objects of type S.
    //
    // Child class should not break parent class’s type definition and behaviour. 
    //
    // In general terms, when we have a base class and child class, i.e. we have an inheritance 
    // relationships. Then, if we can successfully replace the object/instance of a parent class 
    // with an object/instance of the child class, without affecting the behavior of the base class
    // instance, then it doesn't violate the Liskov Substitution Principle.
    //

    //
    // Let's change our Shape class setup - I disliked many features of the class structure 
    // introduced in the previous examples. I don't like that the Shape class contains an instance
    // variable for "width" for all shapes. This doesn't make sense. I would prefer if shapes
    // handles their own descriptors. If we had ellipses or polygons, then we would need to have
    // to change Shape anyway - so let's do it now. 
    //
    // So what is left? Just the abstract method. A shape in this model only has an area calculation
    // then. If it is just behaviour, and not structure, then we can replace the Shape class with
    // an Interface: IShape (let's do this before anything else)
    //

    //
    // Set up the Shape Interface
    //

    public interface IShape
    {
        public double getArea();

        //
        // for second LSP Example
        //

        // we know all shapes don't have volume
        // public double getVolume();  

    }

    // 
    // Now let shapes inherit this interface - they can look after wheit own attributes
    //

    public class Circle : IShape
    {
        private double radius;
        public double getArea() { return Math.PI * (radius) * (radius); }
        public Circle(double radius)
        {
            this.radius = radius;
        }
        /*
        public double getVolume()
        {
            throw new NotImplementedException();
        }
        */
    }

    public class EquilateralTriangle : IShape
    {
        private double side;
        public double getArea() { return (Math.Sqrt(3) / 4) * side * side; }
        public EquilateralTriangle(double side)
        {
            this.side = side;
        }
        /*
        public double getVolume()
        {
            throw new NotImplementedException();
        }
        */
    }

    public class Rectangle : IShape
    {
        public virtual double Height { set; get; }
        public virtual double Width { set; get; }
        public double getArea() { return Height * Width; }
        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }
        /*
        public double getVolume()
        {
            throw new NotImplementedException();
        }
        */
    }


    public class Square : Rectangle
    {
        public override double Width { get { return Side; } set { Side = value; } }

        public override double Height { get { return Side; } set { Side = value; } }

        public double Side { get; set; }

        public Square(double side) : base(side, side)
        {
            Side = side;
        }
    }



    //
    // Let's make an interface for the AreaCalculator class also; notice that we have
    // changed the class - it now requires instantiation (make an object) and we need
    // to call the object instance's CalculateTotalArea() method. Why? Because it is
    // an alternative for you :)
    // 
    //

    public interface IAreaCalculator
    {
        public double CalculateTotalArea(List<IShape> shapes);
    }

    public class AreaCalculator
    {

        //
        // using final SRP version to begin with (returns area - no printing also)
        //

        public double CalculateTotalArea(List<IShape> shapes)
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


    //
    // For second Example Make a 3D shape that extends the base (interface) 
    // IShape. Remember we are using Interfaces as the base has no properties
    // or behaviour, and only specifies functionalties.
    //

    /*
    public class Cube : IShape
    {
        private double side;
        public double Side                      // Note all the different ways that I
        {                                       // am handling properties - stick to one!
            get { return side; }                // This is not the best way I think!
            set { side = value; }
        }

        // public double Side { get; set; }     

        public Cube(double side)
        {
            Side = side;
        }

        public double getArea()
        {
            return 6 * Side * Side;
        }

        public double getVolume()
        {
            return Side * Side * Side;
        }

    }

    public interface IVolumeCalculator
    {
        public double CalculateTotalVolume(List<IShape> shapes);
    }

    public class VolumeCalculator
    {

        //
        // using SRP version that returns volume - no printing
        //

        public double CalculateTotalVolume(List<IShape> shapes)
        {
            double totalVolume = 0;

            //
            // Calculate the total volume
            //

            foreach (var shape in shapes)
                totalVolume += shape.getVolume();

            // return the total volume
            return totalVolume;
        }

    }
    */

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("===================================\n");
            Console.WriteLine("Liskov Substitution Principle (LSP)");
            Console.WriteLine("===================================\n");

            //
            // This all looks great and we can implement the same examples as before and 
            // now include rectangles. 
            //

            //
            // Creating as new list of shapes
            //
            var theShapes = new List<IShape>
            {
                new Rectangle (100.0,100.0),  // our rectangle is a square shape 
                new Circle (50.0),            // changed to work with radius rather than circumference!
                new Square (200.0),
                new EquilateralTriangle (100.0)
            };

            var areaCalculator = new AreaCalculator();

            OutputFormatter.toTEXT(areaCalculator.CalculateTotalArea(theShapes));

            //
            // Now let's look a LSP issues
            //

            Console.WriteLine("===================================\n");

            // 
            // First play nicely with the objects and their declared types!
            //

            Rectangle r = new Rectangle(100.0, 200.0);
            Console.WriteLine("Rectangle Area: {0}\n", r.getArea());
            Square s = new Square(100.0);
            Console.WriteLine("Square Area: {0}\n", s.getArea());

            Console.WriteLine("===================================\n");

            //
            // Now let's look at some problems with this design when we try to
            // replace the parent type with a child type!
            // 


            Rectangle rectangleA = new Rectangle(200.0, 100.0);  // create a new Rectangle (using Square) replacing parent with child
            rectangleA.Width = 100.00;                           // change the width of the rectangle to be 100.0 (keeping it the same)

            Console.WriteLine("New Rectangle Area: {0}\n", rectangleA.getArea());

            Debug.Assert(rectangleA.getArea() == 20000.0, "Area should be 20000!");  // use debugger -> we want to check if we get
                                                                                     // what we expect output (better than WriteLine)

            Console.WriteLine("===================================\n");


            Rectangle rectangleB = new Square(200.0);     // create a new Rectangle (using Square) replacing parent with child
            rectangleB.Width = 100.00;                    // change the width of the rectangle to be 100.0

            Console.WriteLine("New Rectangle Area: {0}\n", rectangleB.getArea());

            Debug.Assert(rectangleB.getArea() == 20000.0, "Area should be 20000!");

            // Now we have a Square Object that has a Width of 100. What is the Height?

            Console.WriteLine("===================================\n");

            //
            // What do we learn from this?
            //
            // That we should not inherit Square from Rectangle here. We are trying to have the relation between 
            // our abstractions to be same as the relation between objects which they represent (shapres).
            //
            // This is because "the representatives of things do not share the relationships of things they 
            // represent" © Uncle Bob 
            //
            // Our Square class represents square geometric shape; but it is NOT a geometric shape - it is 
            // a representation of a geometric shape. Similarly for the  Rectangle class. And the preceived relationship 
            // between the geometric shapes are not shared by these represnetations.
            //
            // That is why we will have problems in our code!
            //
            // In our example we had a "Square" object that started with a different height and width than originally
            // intended. We have done some work on synchronising but there were side effects. But if the base type is
            // a rectangle we don't reaslly want this. 
            // 
            // Squares have a side AND a height AND a width. 
            //
            // We see that we cannot we can successfully replace the object/instance of a parent class with an object/instance 
            // of the child class, without affecting the behavior of the base class instance. We get different areas computed.
            //
            // We can see that we violate  Liskov Substitution Principle (LSP) here. 
            //
            // Great discussion on this here:
            //      https://stackoverflow.com/questions/42769195/correct-way-to-implement-inheritance-in-c-sharp
            //
            //
            // The correct solution is for Square and Rectangle to be siblings, (children of Shape), and not have 
            // a parent/child relationship.
            //

            //
            // For the second example uncomment the following!
            //

            /*
            Cube cube = new Cube(100.0);
            Console.WriteLine("Cube Area:   {0}\n", cube.getArea());
            Console.WriteLine("Cube Volume: {0}\n", cube.getVolume());

            Square square = new Square (100.0);
            Console.WriteLine("Square Area:   {0}\n", square.getArea());
            Console.WriteLine("Square Volume: {0}\n", square.getVolume());
            */
        }
    }
}
