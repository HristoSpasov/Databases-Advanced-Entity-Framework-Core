namespace _05.Football_Team_Generator.Commands
{
    using System;
    using System.Linq;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Exceptions;

    public class Rating : Command
    {
        public Rating(IFootballTeamCollection footballTeamCollection, string[] commandTokens) : base(footballTeamCollection, commandTokens)
        {
        }

        public override string Execute()
        {
            string toReturn = default(string);

            try
            {
                string teamName = this.CommandTokens[0];

                // Check if team exists
                IFootballTeam team = this.FootballTeamCollection.Teams.FirstOrDefault(n => n.Name == teamName);

                if (team == null)
                {
                    throw new MissingTeamException(this.CommandTokens[0]);
                }

                // Calculate rating
                var rating = team.Players.Select(s => s.Stats.Average(p => p.Value)).DefaultIfEmpty().First();

                toReturn = $"{team.Name} - {(int)Math.Round(rating)}";
            }
            catch (Exception e)
            {
                toReturn = e.Message;
            }

            return toReturn;
        }
    }
}