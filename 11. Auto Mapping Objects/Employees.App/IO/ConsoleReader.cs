namespace Employees.App.IO
{
    using System;

    using Employees.App.Contracts.IO;

    internal class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}