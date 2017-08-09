using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyWallpaperChanger
{
    public class Program
    {
        [DllImport("User32", CharSet = CharSet.Ansi)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, uint fWinIni);

        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        public static async Task MainAsync(string[] args)
        {
            IWallpaperSource source = new Sources.ChromecastBackgrounds();
            byte[] imageBytes = await source.GetRandomImageAsync();

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
