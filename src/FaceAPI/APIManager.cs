using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VisionAPI
{
    public class APIManager
    {
        private HttpClient httpClient;
        private string uri = "https://eastus.api.cognitive.microsoft.com/face/v1.0/detect?";
        public APIManager()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> PostToVisionAPIAsync(string key, byte[] payload, string attributes)
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            string queryString = string.Format("returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes={0}", attributes);
            
            HttpResponseMessage responseMesage = null;
            string responseContent = null;

            using (var content = new  ByteArrayContent(payload))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                responseMesage = await httpClient.PostAsync(uri + queryString, content);
                responseContent = responseMesage.Content.ReadAsStringAsync().Result;
            }

            return responseContent;
        }

    }
}
