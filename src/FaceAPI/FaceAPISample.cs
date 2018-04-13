using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

//DOCUMENTATION: https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236
//Quick Start: https://docs.microsoft.com/en-us/azure/cognitive-services/face/quickstarts/csharp

namespace VisionAPI
{
    public class FaceAPISample
    {
        private string key = ConfigurationManager.AppSettings["FaceAPIKey"];
        private APIManager apiManager = null;

        public FaceAPISample()
        {
            
            apiManager = new APIManager();
        }

        public async Task FaceAPI()
        {
            Console.WriteLine("Enter the path of the image file: ");
            string imagePath = Console.ReadLine();

            byte[] rawImageArray = CreateByteArrayFromImage(imagePath);

            //string attributes = "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";
            string attributes = "age,emotion,glasses";
            string response = await apiManager.PostToVisionAPIAsync(key, rawImageArray, attributes);
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
