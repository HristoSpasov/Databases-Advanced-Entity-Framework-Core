namespace TeamBuilder.App.IO
{
    using System.Text;
    using TeamBuilder.App.Contracts;

    internal class OutputStore : IOutputStore
    {
        private StringBuilder sb;

        public OutputStore()
        {
            this.sb = new StringBuilder();
        }

        public void AppendLine(string line)
        {
            this.sb.AppendLine(line);
        }

        public string GetOutput()
        {
            return this.sb.ToString().Trim();
        }
    }
}