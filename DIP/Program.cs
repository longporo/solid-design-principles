using System;

namespace DIP
{
    //
    // DEMO: Dependency inversion principle (DIP) 
    //
    // High-level modules should not depend on low-level modules. Both should depend on abstractions.
    // Abstractions should not depend on details. Details should depend on abstractions. 
    // 
    // Always to try to keep the High-level module and Low-level module as loosely coupled as possible.
    //
    // Classes should depend on abstractions instead of knowing about each other.
    //

    //
    // For this example we will return to an early SRP example where we used the OutputFormatter
    // class. Our hig level client was dependent on the functionality provided by the formatter;
    // to output the information usind different formats. The higher-class was dependent on the
    // formats provided and didn't specify the formats. They were highly coupled. 
    //

    //
    // When we work with "Abstractions" we are work on the high-level model of our system. 
    // From this perspective we only care about the behaviour we want and not how it is 
    // implemented.
    //

    //
    // When we talk about "Details" here we are referring to the low-level modules. These are
    // different to abstractions in that they have been developed to solve a specific problem. 
    // Their scope is limited; they typically specific a bounded unit or a subsystem.
    //

    //
    // We saw an example of this dependency earlier when we moved the outputting of messages
    // to another class - we solved one issue (Single REsponsibility Principle) but in reality
    // we just introduced another issue. The OutputFormatter (Details)class used in the main 
    // (Client) class (Program) was determining how the Client class behaved. 
    //

    //
    // Later we moved from a Static class method to an instance method. We could change the
    // dependency by introducing a Messageing Interface and specify that the output behaviour
    // utilises a messaging interface - meaning that we could utilise any kind of message that
    // implements the interface. We could try this.
    //

    // 
    // There is a good example (that I have been basing my examples on) here on DotNetForAll:
    //
    //  https://www.dotnetforall.com/solid-design-principles-examples/
    //
    // which, while clear, breaks SRP (from my perspective anyway as it calculates volume and
    // writes the message - does two tasks), so I'm looking at an alternative excample here.
    //


    //
    // Let's start with some message interfaces and cconcrete classes
    //

    public interface IMessager
    {
        void Message(double value);
    }

    public class JSONMessage : IMessager
    {
        public void Message(double value)
        {
            Console.WriteLine("{{ \"Total Area\" : {0} }}\n", value);
        }
    }

    public class TextMessage : IMessager
    {
        public void Message(double value)
        {
            Console.WriteLine("Total Area: {0}\n", value);
        }
    }

    public class HTMLMessage : IMessager
    {
        public void Message(double value)
        {
            Console.WriteLine("<span><strong>Total Area: </strong></span><span>{0}</span>\n", value);
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {

            //
            // Determining at run time which "messager" to use
            //

            IMessager message;

            message = new HTMLMessage();

            message.Message(100.0);

            new TextMessage().Message(100.0);
        }
    }
}
