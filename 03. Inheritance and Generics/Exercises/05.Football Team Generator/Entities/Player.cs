namespace _05.Football_Team_Generator.Entities
{
    using System.Collections.Generic;
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Exceptions;

    public class Player : IPlayer
    {
        private string name;
        private IList<IStats> stats;

        public Player(string name, IList<IStats> stats)
        {
            this.Name = name;
            this.Stats = stats;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidNameException();
                }

                this.name = value;
            }
        }

        public IList<IStats> Stats
        {
            get
            {
                return this.stats;
            }

            private set
            {
                this.stats = value;
            }
        }
    }
}