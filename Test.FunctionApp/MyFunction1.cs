using System;
using System.IO;
using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Demo.Function
{
    public class MyFunction1
    {
        [FunctionName("MyFunction1")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            string Connection = Environment.GetEnvironmentVariable("ConnectionString");
            string BlobName = Environment.GetEnvironmentVariable("BlobName");
            string content = "Hi Test content";
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var bytes = Encoding.UTF8.GetBytes(content);
            string filename = Guid.NewGuid().ToString();
            var blobcontainerclient = new BlobContainerClient(Connection, BlobName);
            var blobclient = blobcontainerclient.GetBlobClient(filename + ".txt");
            Stream stream = new MemoryStream(bytes);
            blobclient.UploadAsync(stream);
        }
    }
}
