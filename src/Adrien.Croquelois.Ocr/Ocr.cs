using System;
using Tesseract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Adrien.Croquelois.Ocr
{
    public class Ocr
    {
        public async Task<List<OcrResult>> ReadAsync(List<byte[]> images)
        {
            var ocrResult = new List<OcrResult>();
            var tasks = images.Select(image => Task.Run(() =>
            {
                var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
                var executingPath = Path.GetDirectoryName(executingAssemblyPath);
                using var engine = new TesseractEngine(Path.Combine(executingPath,
                    @"tessdata"), "fra", EngineMode.Default);
                using var pix = Pix.LoadFromMemory(image);
                var page = engine.Process(pix);
                ocrResult.Add(new OcrResult()
                {
                    Text = page.GetText(),
                    Confidence = page.GetMeanConfidence()
                });
            }));
            await Task.WhenAll(tasks);
            return ocrResult;
            // foreach (var image in images)
            // {
            //     var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            //     var executingPath = Path.GetDirectoryName(executingAssemblyPath);
            //     using var engine = new TesseractEngine(Path.Combine(executingPath,
            //         @"tessdata"), "fra", EngineMode.Default);
            //     using var pix = Pix.LoadFromMemory(image);
            //     var page = engine.Process(pix);
            //     ocrResult.Add(new OcrResult()
            //     {
            //         Text = page.GetText(),
            //         Confidence = page.GetMeanConfidence()
            //     });
            // }
            // return Task.FromResult(ocrResult);
        }
    }
}
