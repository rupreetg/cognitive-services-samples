using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace VisionAPI
{
    public class APIManager
    {
        private HttpClient httpClient;
        private string uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/recognizeText";
        public APIManager()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> PostToVisionAPIAsync(string key, byte[] payload, bool handwritten)
        {
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            string queryString = string.Format("?handwriting={0}", handwritten.ToString().ToLower());
            
            HttpResponseMessage responseMesage = null;
            string responseContent = null;
            string operationLocation = null;

            using (var content = new ByteArrayContent(payload))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                responseMesage = await httpClient.PostAsync(uri + queryString, content);
                responseContent = await responseMesage.Content.ReadAsStringAsync();
            }

            //In case its a handwritten image, it's a 2 call process. In first call, we post the image to the API and in the second 
            //call, we get the result of the processing and result.
            if(handwritten)
            {
                if (responseMesage.IsSuccessStatusCode)
                    operationLocation = responseMesage.Headers.GetValues("Operation-Location").FirstOrDefault();

                int index = 0;
                do
                {
                    Thread.Sleep(1000);
                    responseMesage = await httpClient.GetAsync(operationLocation);
                    responseContent = await responseMesage.Content.ReadAsStringAsync();
                    ++index;
                }
                while (index < 10 && responseContent.IndexOf("\"status\":\"Succeeded\"") == -1);

            }

            return responseContent;
        }

    }
}
