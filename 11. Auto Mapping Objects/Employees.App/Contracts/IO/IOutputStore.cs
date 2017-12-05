namespace Employees.App.Contracts.IO
{
    internal interface IOutputStore
    {
        void AppendLine(string toAppend);

        void Append(string toAppend);

        string GetOutput();

        void Clear();
    }
}