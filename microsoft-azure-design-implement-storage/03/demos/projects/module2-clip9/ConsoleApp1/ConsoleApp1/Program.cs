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

            SetMetaData(container);
            GetMetaData(container);

            Console.ReadKey();
        }

        static void GetMetaData(CloudBlobContainer container)
        {
            container.FetchAttributes();
            Console.WriteLine("Container MetaData: \n");
            foreach (var item in container.Metadata)
            {
                Console.WriteLine(
                    string.Format("{0}: {1}", item.Key, item.Value));
            }
        }

        static void SetMetaData(CloudBlobContainer container)
        {
            container.Metadata.Clear();
            container.Metadata.Add("Owner", "Mike Pfeiffer");
            container.Metadata["LastUpdated"] = DateTime.Now.ToString();
            container.SetMetadata();
        }
    }
}
