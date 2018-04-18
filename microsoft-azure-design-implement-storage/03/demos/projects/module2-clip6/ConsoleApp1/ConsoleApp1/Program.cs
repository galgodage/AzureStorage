using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("images");

            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //upload a blob
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("img2.png");

            using (var fileStream = System.IO.File.OpenRead(@"c:\img2.png"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            //list blobs
            var blobs = container.ListBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine(blob.Uri);
            }

            //download blob
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("img2.png");

            using (var fileStream = System.IO.File.OpenWrite(@"c:\downloads\img2.png"))
            {
                blockBlob2.DownloadToStream(fileStream);
            }

            Console.ReadKey();
        }       
    }
}
