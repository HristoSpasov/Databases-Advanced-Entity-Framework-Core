namespace _05.Football_Team_Generator.Contracts.IO
{
    public interface IOutputStoreManager
    {
        void AppendLine(string line);

        void Append(string line);

        string GetOutput();
    }
}