namespace PrimusFlex.Data.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Web.Infrastructure;
    using Web.Infrastructure.Helpers;

    public class AzureStorage
    {
        public IEnumerable<IListBlobItem> GetListOfAllBlobs()
        {
            CloudStorageAccount storageAccount = StorageHelpers.StorageAccount();

            //Create the blob client object.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Get a reference to a container to use for the sample code, and create it if it does not exist.
            CloudBlobContainer container = blobClient.GetContainerReference(Constant.IMAGES_CONTAINER);
            container.CreateIfNotExists();

            return container.ListBlobs(null, false);
        }


    }
}
