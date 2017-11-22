namespace _05.Football_Team_Generator.IO
{
    using System;
    using _05.Football_Team_Generator.Contracts.IO;

    public class ConsoleReader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}