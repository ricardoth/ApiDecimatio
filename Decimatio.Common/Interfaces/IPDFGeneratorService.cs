using QuestPDF.Infrastructure;

namespace Decimatio.Common.Interfaces
{
    public interface IPDFGeneratorService 
    {
        byte[] GeneratePDFVoucher(string base64Pdf, TicketBodyQRDto ticket);
        string CombinePdfFiles(List<string> strList);
    }
}
