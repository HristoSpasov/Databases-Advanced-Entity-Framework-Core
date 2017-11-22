namespace _05.Football_Team_Generator.IO
{
    using System;
    using _05.Football_Team_Generator.Contracts.IO;

    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}