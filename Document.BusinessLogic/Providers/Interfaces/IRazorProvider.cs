namespace Pdf.BusinessLogic.Providers.Interfaces;

public interface IRazorProvider
{
    Task<string> RenderPageAsync<T>(string path, T model);
}