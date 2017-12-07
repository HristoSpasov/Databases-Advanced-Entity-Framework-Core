namespace TeamBuilder.App.Contracts
{
    public interface IOutputStore
    {
        void AppendLine(string line);

        string GetOutput();
    }
}