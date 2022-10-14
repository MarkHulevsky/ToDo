namespace Document.BusinessLogic.Helpers.Interfaces;

public interface IPdfHelper
{
    Stream GeneratePdf(string content);
}