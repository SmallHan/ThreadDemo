using System;

namespace ExtendDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            B b = new B();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    class B:A
    {
        public B()
        {
            new A();
            Console.WriteLine("B");
        }
    }

    class A
    {
        public A()
        {
            Console.WriteLine("A");
        }
    }
}
