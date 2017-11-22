namespace _06.Online_Radio_Database
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using _06.Online_Radio_Database.Entities;
    using _06.Online_Radio_Database.Exceptions;

    public class Program
    {
        private const string SongValidatePattern = @"(.+);(.+);(.+)";
        private const string TimeValidatePattern = @"([0-9]+):([0-9]+)";
        private const string SuccessMessage = "Song added.";
        private static Song[] songs;
        private static StringBuilder output;

        public static void Main()
        {
            // Read song count and initialize collection
            ReadSongCountAndInitialize();

            // Read and validate songs
            ReadAndValidateSongs();

            // Add statistics to outpit
            GetStatistics();

            // Print result
            Console.Write(output.ToString());
        }

        private static void GetStatistics()
        {
            // Count total added songs
            output.AppendLine($"Songs added: {songs.Where(s => s != null).Count()}");

            // Calculate total seconds of all songs
            long playlistTotalSeconds = songs.Where(s => s != null).Select(seconds => seconds.Minutes * 60 + seconds.Seconds).Sum();
            TimeSpan span = TimeSpan.FromSeconds(playlistTotalSeconds);
            output.AppendLine($"Playlist length: {span.Hours}h {span.Minutes}m {span.Seconds}s");
        }

        private static void ReadAndValidateSongs()
        {
            for (int i = 0; i < songs.Length; i++)
            {
                try
                {
                    string line = Console.ReadLine();

                    // Validate whole string
                    if (!Regex.IsMatch(line, SongValidatePattern))
                    {
                        throw new InvalidSongException();
                    }

                    Match songMatch = Regex.Match(line, SongValidatePattern);

                    // Get song tokens
                    string artist = songMatch.Groups[1].Value;
                    string name = songMatch.Groups[2].Value;
                    string time = songMatch.Groups[3].Value;

                    // Validate time
                    if (!Regex.IsMatch(time, TimeValidatePattern))
                    {
                        throw new InvalidSongLengthException();
                    }

                    Match timeMatch = Regex.Match(time, TimeValidatePattern);

                    int minutes = int.Parse(timeMatch.Groups[1].Value);
                    int seconds = int.Parse(timeMatch.Groups[2].Value);

                    // Try create song object
                    Song song = new Song(artist, name, minutes, seconds);

                    // Add song to array
                    songs[i] = song;

                    // Add success message
                    output.AppendLine(SuccessMessage);
                }
                catch (Exception e)
                {
                    output.AppendLine(e.Message);
                }
            }
        }

        private static void ReadSongCountAndInitialize()
        {
            int count = int.Parse(Console.ReadLine());

            songs = new Song[count];
            output = new StringBuilder();
        }
    }
}
