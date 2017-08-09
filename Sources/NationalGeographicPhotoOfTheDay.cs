using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyWallpaperChanger.Sources
{
    public class NationalGeographicPhotoOfTheDay : IWallpaperSource
    {
        public async Task<byte[]> GetRandomImageAsync()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync("http://www.nationalgeographic.com/photography/photo-of-the-day/").Result;
            var imageUrlRegex = new Regex("'aemLeadImage'\\: '(?<url>.*?)'");
            var match = imageUrlRegex.Match(response);
            var imageUrl = match.Groups["url"].Value;
            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

            return imageBytes;
        }
    }
}