namespace EFP.Services.Contracts
{
    public interface IDataExportService
    {
        void ExportToJson();

        void ExportToXml();
    }
}