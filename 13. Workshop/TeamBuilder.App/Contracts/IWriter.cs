namespace TeamBuilder.App.Contracts
{
    internal interface IWriter
    {
        void Write(string text);

        void WriteLine(string text);
    }
}