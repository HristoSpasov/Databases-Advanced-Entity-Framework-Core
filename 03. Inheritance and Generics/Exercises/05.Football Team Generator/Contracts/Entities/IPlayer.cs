namespace _05.Football_Team_Generator.Contracts.Entities
{
    using System.Collections.Generic;

    public interface IPlayer
    {
        string Name { get; }

        IList<IStats> Stats { get; }
    }
}