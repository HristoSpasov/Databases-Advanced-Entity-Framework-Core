namespace Employees.App.IO
{
    using System;
    using Employees.App.Contracts.IO;

    internal class ConsoleWriter : IWriter
    {
        public void Write(string toWrite)
        {
            Console.Write(toWrite);
        }
    }
}