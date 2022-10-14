using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Pdf;
using Document.BusinessLogic.Helpers.Interfaces;

namespace Document.BusinessLogic.Helpers;

public class PdfHelper: IPdfHelper
{
    public Stream GeneratePdf(string content)
    {
        var stream = new MemoryStream();
        var pdfWriter = new PdfWriter(stream);
        pdfWriter.SetCloseStream(false);

        ConverterProperties converterProperties = CreateConverterProperties();

        HtmlConverter.ConvertToPdf(content, pdfWriter, converterProperties);

        stream.Position = 0;

        return stream;
    }

    private ConverterProperties CreateConverterProperties()
    {
        var converterProperties = new ConverterProperties();
        var fontProvider = new DefaultFontProvider(true, true, true);

        converterProperties.SetFontProvider(fontProvider);

        return converterProperties;
    }
}