namespace _05.Football_Team_Generator.Commands
{
    using System;
    using System.Linq;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Exceptions;

    public class Remove : Command
    {
        public Remove(IFootballTeamCollection footballTeamCollection, string[] commandTokens) : base(footballTeamCollection, commandTokens)
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

                // Check if team has player
                string playerToRemove = this.CommandTokens[1];
                IPlayer player = team.Players.FirstOrDefault(n => n.Name == playerToRemove);

                if (player == null)
                {
                    throw new MissingPlayerException(playerToRemove, teamName);
                }

                // Remove player
                team.Players.Remove(player);
            }
            catch (Exception e)
            {
                toReturn = e.Message;
            }

            return toReturn;
        }
    }
}