namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Common;
    using Data.Models;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Infrastructure;
    using Microsoft.WindowsAzure.Storage;
    using Infrastructure.Helpers;
    using ViewModels;
    [Authorize(Roles = "SupperAdmin,Admin,Manager,Worker")]
    public class ImagesController : BaseController
    {
        // GET: Management/Images
        public ActionResult Index()
        {
            return View();
        }

        // GET: management/image/all
        public ActionResult All()
        {
            CloudStorageAccount storageAccount = StorageHelpers.StorageAccount();

            //Create the blob client object.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Get a reference to a container to use for the sample code, and create it if it does not exist.
            CloudBlobContainer container = blobClient.GetContainerReference(Constant.IMAGES_CONTAINER);
            container.CreateIfNotExists();

            var model = new List<ImageViewModel>();
            // Loop over items within the container and output the URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    model.Add(new ImageViewModel() { Uri = StorageHelpers.GetBlobSasUri(container, blob.Name) });
                }
            }

            return View(model);
        }
    }
}