using App.Application.Interfaces;
using Docnet.Core;
using Docnet.Core.Models;
using Docnet.Core.Readers;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;

namespace App.Application.Services
{
    public class FileToolService : IFileToolService
    {
        public async Task<byte[]> GeneratePdfFromText(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new Exception("Input text cannot be empty.");

            using var memoryStream = new MemoryStream();

            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            foreach (var line in input.Split(Environment.NewLine))
            {
                document.Add(new Paragraph(line));
            }

            document.Close();

            return memoryStream.ToArray();
        }

        public async Task<List<string>> ConvertPdfToImages(byte[] pdfBytes)
        {
            var images = new List<string>();

            using var docReader = DocLib.Instance.GetDocReader(pdfBytes, new PageDimensions(1080, 1920));
            int pageCount = docReader.GetPageCount();

            for (int i = 0; i < pageCount; i++)
            {
                using var pageReader = docReader.GetPageReader(i);

                var rawBytes = pageReader.GetImage();
                int width = pageReader.GetPageWidth();
                int height = pageReader.GetPageHeight();

                using var image = Image.LoadPixelData<Rgba32>(rawBytes, width, height);
                // 🔹 Create white background
                using var finalImage = new Image<Rgba32>(width, height, Color.White);

                // 🔹 Draw PDF image on white background
                finalImage.Mutate(x => x.DrawImage(image, 1f));

                using var ms = new MemoryStream();
                finalImage.Save(ms, new PngEncoder());

                var base64 = Convert.ToBase64String(ms.ToArray());
                images.Add($"data:image/png;base64,{base64}");
            }

            return images;
        }

        public async Task<byte[]> ConvertImagesToPdf(List<byte[]> imageFiles)
        {
            using var stream = new MemoryStream();

            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            foreach (var imageBytes in imageFiles)
            {
                var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
                var image = new iText.Layout.Element.Image(imageData);

                image.SetAutoScale(true);

                document.Add(image);

                if (imageFiles.Last() != imageBytes)
                    document.Add(new AreaBreak());
            }

            document.Close();

            return stream.ToArray();
        }
    }
}
