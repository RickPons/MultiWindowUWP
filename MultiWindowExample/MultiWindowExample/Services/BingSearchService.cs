using System.Threading.Tasks;
using MultiWindowExample.Interfaces;
using MultiWindowExample.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace MultiWindowExample.Services
{
    public class BingSearchService : IBingSearchService
    {
        /// <summary>
        /// This example uses Microsoft Cognitive Servies (Search API) as source.
        /// https://azure.microsoft.com/en-us/services/cognitive-services/?v=17.29
        /// </summary>

        private string BING_IMAGE_KEY = "";
        public async Task<BingImageResult> GetImagesAsync(string topic)
        {
            using(var client= new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", BING_IMAGE_KEY);

                StringBuilder petitionUrl = new StringBuilder("https://api.cognitive.microsoft.com/bing/v5.0/images/");
                petitionUrl.AppendFormat("search?q={0}", topic);

                var result =await client.GetStringAsync(petitionUrl.ToString());
                return  JsonConvert.DeserializeObject<BingImageResult>(result);
            }
        }
    }
}
