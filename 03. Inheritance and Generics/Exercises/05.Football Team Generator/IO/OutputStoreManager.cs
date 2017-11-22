namespace _05.Football_Team_Generator.IO
{
    using System.Text;
    using _05.Football_Team_Generator.Contracts.IO;

    public class OutputStoreManager : IOutputStoreManager
    {
        private StringBuilder output;

        public OutputStoreManager()
        {
            this.output = new StringBuilder();
        }

        public OutputStoreManager(string initialInputState)
        {
            this.output = new StringBuilder(initialInputState);
        }

        public void Append(string line)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                this.output.Append(line);
            }
        }

        public void AppendLine(string line)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                this.output.AppendLine(line);
            }
        }

        public string GetOutput()
        {
            return this.output.ToString().Trim();
        }
    }
}