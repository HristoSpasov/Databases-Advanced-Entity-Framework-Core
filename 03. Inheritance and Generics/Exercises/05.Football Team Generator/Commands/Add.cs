namespace _05.Football_Team_Generator.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Entities;
    using _05.Football_Team_Generator.Exceptions;
    using _05.Football_Team_Generator.Utilities;

    public class Add : Command
    {
        public Add(IFootballTeamCollection footballTeamCollection, string[] commandTokens) : base(footballTeamCollection, commandTokens)
        {
        }

        public override string Execute()
        {
            string toReturn = default(string);

            try
            {
                IFootballTeam team = this.FootballTeamCollection.Teams.FirstOrDefault(n => n.Name == this.CommandTokens[0]);

                if (team == null)
                {
                    throw new MissingTeamException(this.CommandTokens[0]);
                }

                string playerName = this.CommandTokens[1];

                // Create all stats
                IList<IStats> stats = new List<IStats>();

                for (int i = 2, j = 0; i < this.CommandTokens.Length; i++, j++)
                {
                    IStats stat = new Stats(Constants.Stats[j], int.Parse(this.CommandTokens[i]));
                    stats.Add(stat);
                }

                // Create player
                IPlayer player = new Player(playerName, stats);

                // Add to team
                team.Players.Add(player);
            }
            catch (Exception e)
            {
                toReturn = e.Message;
            }

            return toReturn;
        }
    }
}