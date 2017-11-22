namespace _06.Online_Radio_Database.Exceptions
{
    using System;

    public class InvalidSongException : Exception
    {
        private const string DefaultMessage = "Invalid song.";

        public InvalidSongException(string message)
           : base (message)
        {

        }

        public InvalidSongException()
            : base (DefaultMessage)
        {
        }
    }
}
