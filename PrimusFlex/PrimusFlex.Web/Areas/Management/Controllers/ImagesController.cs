namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.AspNet.Identity;

    using Data;
    using Data.DAL;
    using Data.Common;
    using Data.Models;

    using Infrastructure;
    using Infrastructure.Helpers;

    using ViewModels;

    [Authorize(Roles = "SupperAdmin,Admin,Manager,Worker")]
    public class ImagesController : BaseController
    {
        protected DbContext context = new ApplicationDbContext();

        protected IDbRepository<Image> images;
        protected IDbRepository<Kitchen> kitchens;

        public ImagesController()
        {
            this.images = new DbRepository<Image>(this.context);
            this.kitchens = new DbRepository<Kitchen>(this.context);
        }

        // GET: Management/Images/id
        public ActionResult Index()
        {
            var model = new List<ImagesIndexViewModel>();

            string userName = this.User.Identity.GetUserName();
            List<Kitchen> kitchens = this.kitchens.All()
                                        .Where(k => k.UserName == userName)
                                        .ToList<Kitchen>();
            
            foreach (var kitchen in kitchens)
            {
                model.Add(new ImagesIndexViewModel()
                {
                    SiteName = kitchen.SiteName,
                    PlotNumber = kitchen.PlotNumber,
                    Images = kitchen.Images
                });
            }

            return View(model);
        }

        // GET: management/images/all
        public ActionResult All()
        {
            IEnumerable<IListBlobItem> blobs = new AzureStorage().GetListOfAllBlobs();

            var model = new List<ImageViewModel>();
            // Loop over items within the container and output the URI.
            foreach (IListBlobItem item in blobs)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    model.Add(new ImageViewModel() { Uri = blob.Name });
                }
            }

            return View(model);
        }



        //// GET: management/images/all
        //public ActionResult All()
        //{
        //    CloudStorageAccount storageAccount = StorageHelpers.StorageAccount();

        //    //Create the blob client object.
        //    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        //    //Get a reference to a container to use for the sample code, and create it if it does not exist.
        //    CloudBlobContainer container = blobClient.GetContainerReference(Constant.IMAGES_CONTAINER);
        //    container.CreateIfNotExists();

        //    var model = new List<ImageViewModel>();
        //    // Loop over items within the container and output the URI.
        //    foreach (IListBlobItem item in container.ListBlobs(null, false))
        //    {
        //        if (item.GetType() == typeof(CloudBlockBlob))
        //        {
        //            CloudBlockBlob blob = (CloudBlockBlob)item;
        //            model.Add(new ImageViewModel() { Uri = StorageHelpers.GetBlobSasUri(container, blob.Name) });
        //        }
        //    }

        //    return View(model);
        //}
    }
}

public class Image_KitchenId
{
    public string Image { get; set; }

    public int KitchenId { get; set; }
}