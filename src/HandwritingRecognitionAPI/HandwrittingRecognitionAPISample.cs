using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//DOCUMENTATION: https://westus.dev.cognitive.microsoft.com/docs/services/56f91f2d778daf23d8ec6739/operations/56f91f2e778daf14a499e1fa
//Quick Start: https://docs.microsoft.com/en-in/azure/cognitive-services/computer-vision/quickstarts/csharp

namespace VisionAPI
{
    public class HandwrittingRecognitionAPISample
    {
        private string key = ConfigurationManager.AppSettings["HandwrittenRecognitionAPIKey"];
        private APIManager apiManager = null;

        public HandwrittingRecognitionAPISample()
        {

            apiManager = new APIManager();
        }

        public async Task RecogizeHandwrittingAPI()
        {
            Console.WriteLine("Enter the path of the image file: ");
            string imagePath = Console.ReadLine();

            byte[] rawImageArray = CreateByteArrayFromImage(imagePath);

            bool isHandwritten = true;

            string response = await apiManager.PostToVisionAPIAsync(key, rawImageArray, isHandwritten);
            Console.WriteLine();
            Console.WriteLine("-------API Response -------");
            Console.WriteLine(response);
        }


        private byte[] CreateByteArrayFromImage(string imagePath)
        {
            using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    return binaryReader.ReadBytes((int)fileStream.Length);
                }
            }
        }

    }
    
    }
