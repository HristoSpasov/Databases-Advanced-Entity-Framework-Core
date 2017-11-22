namespace _06.Online_Radio_Database.Exceptions
{
    public class InvalidSongMinutesException : InvalidSongLengthException
    {
        private const string DefaultMessage = "Song minutes should be between 0 and 14.";

        public InvalidSongMinutesException()
            : base (DefaultMessage)
        {
        }
    }
}
