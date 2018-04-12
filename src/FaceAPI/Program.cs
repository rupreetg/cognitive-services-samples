using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionAPI
{
    class Program
    {
       
        static void Main(string[] args)
        {
            FaceAPISample sample = new FaceAPISample();

            sample.FaceAPI().Wait();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        
        
    }
}
