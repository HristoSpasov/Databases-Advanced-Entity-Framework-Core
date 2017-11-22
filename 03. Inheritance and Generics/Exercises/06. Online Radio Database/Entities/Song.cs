using _06.Online_Radio_Database.Exceptions;

namespace _06.Online_Radio_Database.Entities
{
    public class Song
    {
        private string author;
        private string name;
        private int minutes;
        private int seconds;

        public Song(string author, string name, int minutes, int seconds)
        {
            this.Author = author;
            this.Name = name;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public string Author
        {
            get
            {
                return this.author;
            }

            private set
            {
                if (value.Length < 3 || value.Length > 20)
                {
                    throw new InvalidArtistNameException();
                }

                this.author = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (value.Length < 3 || value.Length > 20)
                {
                    throw new InvalidSongNameException();
                }

                this.name = value;
            }
        }

        public int Minutes
        {
            get
            {
                return this.minutes;
            }

            private set
            {
                if (value < 0 || value > 14)
                {
                    throw new InvalidSongMinutesException();
                }

                this.minutes = value;
            }
        }

        public int Seconds
        {
            get
            {
                return this.seconds;
            }

            private set
            {
                if (value < 0 || value > 59)
                {
                    throw new InvalidSongSecondsException();
                }

                this.seconds = value;
            }
        }
    }
}
