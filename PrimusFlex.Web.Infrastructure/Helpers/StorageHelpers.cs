namespace PrimusFlex.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage;

    using Data.Common;
    using Data.Models;
    using Data;
    using Microsoft.WindowsAzure.Storage.Blob;
    public static class StorageHelpers
    {
        public static IDbRepository<StorageAccount> storageAccount = new DbRepository<StorageAccount>(new ApplicationDbContext());

        public static CloudStorageAccount StorageAccount()
        {
            var accountKey = GetAccountPair();
            
            //Parse the connection string and return a reference to the storage account.
            string ConnectionString =
                string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}",
                accountKey["AccountName"], accountKey["AccountKey"]);

            return CloudStorageAccount.Parse(ConnectionString);
        }

        public static string GetContainerSasUri(CloudBlobContainer container)
        {
            //Set the expiry time and permissions for the container.
            //In this case no start time is specified, so the shared access signature becomes valid immediately.
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List;

            //Generate the shared access signature on the container, setting the constraints directly on the signature.
            string sasContainerToken = container.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return container.Uri + sasContainerToken;
        }

        public static string GetBlobSasUri(CloudBlobContainer container, string blobName)
        {
            //Get a reference to a blob within the container.
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            //Set the expiry time and permissions for the blob.
            //In this case the start time is specified as a few minutes in the past, to mitigate clock skew.
            //The shared access signature will be valid immediately.
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write;

            //Generate the shared access signature on the blob, setting the constraints directly on the signature.
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return blob.Uri + sasBlobToken;
        }

        private static Dictionary<string, string> GetAccountPair()
        {
            Dictionary<string, string> accountKey = new Dictionary<string, string>();

            var account = storageAccount.All().FirstOrDefault();

            accountKey.Add("AccountName", account.AccountName);
            accountKey.Add("AccountKey", account.AccountKey);

            return accountKey;
        }
    }
}
