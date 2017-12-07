namespace TeamBuilder.App.IO
{
    using System;
    using TeamBuilder.App.Contracts;

    internal class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}