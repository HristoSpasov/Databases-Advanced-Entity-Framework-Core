namespace Employees.App.IO
{
    using System.Text;
    using Employees.App.Contracts.IO;

    public class OutputStore : IOutputStore
    {
        private readonly StringBuilder sb;

        internal OutputStore()
        {
            this.sb = new StringBuilder();
        }

        internal OutputStore(StringBuilder sb)
        {
            this.sb = sb;
        }

        public void Append(string toAppend)
        {
            this.sb.Append(toAppend);
        }

        public void AppendLine(string toAppend)
        {
            this.sb.AppendLine(toAppend);
        }

        public void Clear()
        {
            this.sb.Clear();
        }

        public string GetOutput()
        {
            return this.sb.ToString();
        }
    }
}