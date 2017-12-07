namespace TeamBuilder.App.IO
{
    using System;
    using TeamBuilder.App.Contracts;

    internal class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}