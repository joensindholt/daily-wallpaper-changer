using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DailyWallpaperChanger.Sources
{
    public class ChromecastBackgrounds : IWallpaperSource
    {
        public async Task<byte[]> GetRandomImageAsync()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://raw.githubusercontent.com/dconnolly/chromecast-backgrounds/master/backgrounds.json");
            var imageInfos = JsonConvert.DeserializeObject<List<ImageInfo>>(json);
            var randomIndex = new Random().Next(imageInfos.Count);
            var imageInfo = imageInfos[randomIndex];
            var bytes = await httpClient.GetByteArrayAsync(imageInfo.Url);
            return bytes;
        }

        private class ImageInfo
        {
            public string Url { get; set; }

            public string Author { get; set; }
        }
    }
}