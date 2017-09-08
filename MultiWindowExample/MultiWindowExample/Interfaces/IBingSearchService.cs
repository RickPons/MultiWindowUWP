using MultiWindowExample.Models;
using System.Threading.Tasks;

namespace MultiWindowExample.Interfaces
{
    public interface IBingSearchService
    {
        Task<BingImageResult> GetImagesAsync(string topic);
    }
}
