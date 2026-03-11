using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IFileToolService
    {
        Task<byte[]> GeneratePdfFromText(string input);
        Task<List<string>> ConvertPdfToImages(byte[] pdfBytes);
    }
}
