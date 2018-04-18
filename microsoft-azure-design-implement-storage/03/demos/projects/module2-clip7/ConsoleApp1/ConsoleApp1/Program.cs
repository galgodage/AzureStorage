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

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("img2.png");
            CloudBlockBlob blockBlobCopy = container.GetBlockBlobReference("img2.png");
            var cb = new AsyncCallback(x => Console.WriteLine("blob copy completed"));
            
            blockBlobCopy.BeginStartCopy(blockBlob.Uri, cb, null);

            Console.ReadKey();
        }       
    }
}
