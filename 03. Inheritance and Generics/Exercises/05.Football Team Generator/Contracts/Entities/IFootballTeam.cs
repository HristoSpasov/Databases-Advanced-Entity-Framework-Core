namespace _05.Football_Team_Generator.Contracts.Entities
{
    using System.Collections.Generic;

    public interface IFootballTeam
    {
        string Name { get; }

        IList<IPlayer> Players { get; }
    }
}