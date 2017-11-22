namespace _05.Football_Team_Generator.Core
{
    using System.Collections.Generic;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.Entities;

    public class FootballTeamCollection : IFootballTeamCollection
    {
        public FootballTeamCollection()
        {
            this.Teams = new List<IFootballTeam>();
        }

        public IList<IFootballTeam> Teams { get; }
    }
}