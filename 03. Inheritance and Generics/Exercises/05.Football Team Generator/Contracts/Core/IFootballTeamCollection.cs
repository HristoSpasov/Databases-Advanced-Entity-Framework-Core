namespace _05.Football_Team_Generator.Contracts.Core
{
    using System.Collections.Generic;
    using _05.Football_Team_Generator.Contracts.Entities;

    public interface IFootballTeamCollection
    {
        IList<IFootballTeam> Teams { get; }
    }
}