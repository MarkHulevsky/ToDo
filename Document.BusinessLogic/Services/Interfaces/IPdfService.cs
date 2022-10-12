namespace Pdf.BusinessLogic.Services.Interfaces;

public interface IPdfService
{
    Task<Stream> GetByDirectoryIdAsync(Guid directoryId);
}