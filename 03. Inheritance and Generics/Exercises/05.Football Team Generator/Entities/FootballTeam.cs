namespace _05.Football_Team_Generator.Entities
{
    using System.Collections.Generic;
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Exceptions;

    public class FootballTeam : IFootballTeam
    {
        private string name;
        private IList<IPlayer> players;

        public FootballTeam(string name)
        {
            this.Name = name;
            this.Players = new List<IPlayer>();
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

        public IList<IPlayer> Players
        {
            get
            {
                return this.players;
            }

            private set
            {
                this.players = value;
            }
        }
    }
}