
using Decimatio.Common.DTOs;

namespace Decimatio.Common.Interfaces
{
    public interface IPDFGeneratorService 
    {
        byte[] GeneratePDFVoucher(string base64Pdf, RequestTicketBodyQRDto ticket);
        string CombinePdfFiles(List<string> strList);
    }
}
