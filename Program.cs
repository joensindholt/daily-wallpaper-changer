using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    public class Program
    {
        [DllImport("User32", CharSet = CharSet.Ansi)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, uint fWinIni);

        public static void Main(string[] args)
        {
            var httpClient = new HttpClient();

            var response = httpClient.GetStringAsync("http://www.nationalgeographic.com/photography/photo-of-the-day/").Result;
            var imageUrlRegex = new Regex("'aemLeadImage'\\: '(?<url>.*?)'");
            var match = imageUrlRegex.Match(response);
            var imageUrl = match.Groups["url"].Value;

            var imageBytes = httpClient.GetByteArrayAsync(imageUrl).Result;

            var storageDirectory = "pictures";
            var storagePath = Path.Combine(storageDirectory, "picture-of-the-day");

            if (!Directory.Exists(storageDirectory))
            {
                Directory.CreateDirectory(storageDirectory);
            }

            File.WriteAllBytes(storagePath, imageBytes);

            var fileInfo = new FileInfo(storagePath);

            SystemParametersInfo(0x0014, 0, fileInfo.FullName, 0x0001);
        }
    }
}
