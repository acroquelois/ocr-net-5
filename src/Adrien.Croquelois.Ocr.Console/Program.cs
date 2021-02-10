using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Adrien.Croquelois.Ocr.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        
        private static async Task MainAsync(string[] args)
        {
            var images = new List<byte[]>();
            foreach(var argument in args)
            {
                var imageBytes = await File.ReadAllBytesAsync(argument);
                images.Add(imageBytes);
            }
            var ocrResults = await new Ocr().ReadAsync(images);
            foreach (var ocrResult in ocrResults)
            {
                System.Console.WriteLine($"Confidence :{ocrResult.Confidence}");
                System.Console.WriteLine($"Text :{ocrResult.Text}"); 
            }
        }
    }
}
