namespace _06.Online_Radio_Database.Exceptions
{
    public class InvalidArtistNameException : InvalidSongException
    {
        private const string DefaultMessage = "Artist name should be between 3 and 20 symbols.";

        public InvalidArtistNameException()
            : base (DefaultMessage)
        {

        }
    }
}
