namespace _05.Football_Team_Generator.Commands
{
    using System;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Entities;

    public class Team : Command
    {
        public Team(IFootballTeamCollection footballTeamCollection, string[] commandTokens) : base(footballTeamCollection, commandTokens)
        {
        }

        public override string Execute()
        {
            string toReturn = default(string);

            try
            {
                string teamName = this.CommandTokens[0];
                IFootballTeam team = new FootballTeam(teamName);
                this.FootballTeamCollection.Teams.Add(team);
            }
            catch (Exception e)
            {
                toReturn = e.Message;
            }

            return toReturn;
        }
    }
}