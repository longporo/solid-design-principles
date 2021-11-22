using System;

namespace ISP
{
    //
    // DEMO: Interface segregation principle (ISP) 
    //
    // Any client should not be forced to use an interface which is irrelevant to it. 
    //
    // Rather than one fat interface, numerous smaller interfaces are preferred based on 
    // groups of methods with each interface serving one submodule.
    //

    //
    // In this example we will begin using some of the code from the previous LSP example
    //
    // You will remember that we took the IShape interface used previously and added to
    // it to demonstrate how not implementing (throwing Exception) could be seen to be a violation
    // of LSP (for Squares and Rectangles) as replacement of the parent class with a child class
    // changed behaviour.
    //
    // More importantly, however, we introduced a situation where lots of child classes were
    // forced to implement  functionality that was irrelevant.
    //
    // There is a simple solution here: split the IShape interface into two interfaces; one (IArea)
    // for implementing the getArea() interface and one (IVolume) for implementing the getVolume()
    // interface. Note, I have also included a change to the Shape base class here to show how to
    // implement base classes, interfaces and children. Of course you could use IShape also. 
    //
    // Remember that Properties (Attributes) can be declared on an interface in C#. Interface 
    // properties typically, don't have a body, however. You can specify that a getter and setter
    // exist; but you do not implement how they behave. Have a look here:
    //
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/interface-properties
    //
    // You don't INHERIT here; the declaration tells us that you need to IMPLEMENT (RE-DECLARE)
    // in the child class (etc.). The child classes do not inherit (anything) from interfaces, they 
    // implement them! The attributes are not automatically part of the implementation. If you want
    // this then declare an abstract class!
    // 
    // This is a fairly obvious example of good design, actually.
    //


    //
    // Create the Interfaces first (the abstract class shapre is here as an alternative)
    //

    public abstract class Shape
    {
        double Side { get; set; }
    }

    public interface IShape
    {
        double Side { get; set; }
    }

    public interface IVolume
    {
        double getVolume();
    }

    public interface IArea
    {
        double getArea();
    }


    //
    // Now create the concrete classes derived fro mthe base interfaces - note that they
    // only implement the interfaces they require - the conform to ISP!
    // 

    public class Square : IShape, IArea
    {
        private double side;

        public Square(double side)
        {
            Side = side;
        }

        public double Side
        {
            get { return side; }
            set { side = value; }
        }

        public double getArea()
        {
            return Side * Side;
        }
    }

    //
    // You can change the IShape to Shape just to see that using an abstract
    // class works the same way.
    //

    public class Cube : IShape, IArea, IVolume
    {
        private double side;

        public Cube(double side)
        {
            Side = side;
        }

        public double Side
        {
            get { return side; }
            set { side = value; }
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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Interface segregation principle (ISP)");
            Console.WriteLine("=====================================\n");

            Cube cube = new Cube(100.0);
            Console.WriteLine("Cube Area:   {0}\n", cube.getArea());
            Console.WriteLine("Cube Volume: {0}\n", cube.getVolume());

            Square square = new Square(100.0);
            Console.WriteLine("Square Area:   {0}\n", square.getArea());
            // Console.WriteLine("Squarw Volume: {0}\n", square.getVolume());
        }
    }
}
