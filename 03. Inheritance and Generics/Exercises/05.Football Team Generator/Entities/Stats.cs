namespace _05.Football_Team_Generator.Entities
{
    using _05.Football_Team_Generator.Contracts.Entities;
    using _05.Football_Team_Generator.Utilities;

    public class Stats : IStats
    {
        private string type;
        private int value;

        public Stats(string type, int value)
        {
            Validator.ValidateStats(type, value);
            this.type = type;
            this.value = value;
        }

        public string Type
        {
            get
            {
                return this.type;
            }

            private set
            {
                this.type = value;
            }
        }

        public int Value
        {
            get
            {
                return this.value;
            }

            private set
            {
                this.value = value;
            }
        }
    }
}