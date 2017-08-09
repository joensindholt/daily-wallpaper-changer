using System.Threading.Tasks;

namespace DailyWallpaperChanger
{
    public interface IWallpaperSource
    {
        Task<byte[]> GetRandomImageAsync();
    }
}