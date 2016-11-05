namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Net;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.AspNet.Identity;

    using ViewModels;
    using Data.Models;
    using Infrastructure;
    using Infrastructure.Helpers;
    using Data.DAL;

    public class ReportsController : BaseWorkerController
    {
        // GET: Management/Reports
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            var model = this.reports
                            .All()
                            .Where(r => r.OwnerId == userId)
                            .Select(r => new ReportViewModel
                            {
                                Id = r.Id,
                                Date = r.Date,
                                SiteId = r.SiteId,
                                SiteName = r.Site.Name,
                                Plot = r.Plot,
                                Info = r.Info,
                                KitchenModel = r.KitchenModel,
                            })
                            .ToList();

            return View(model);
        }

        // GET: Management/Reports/Create
        [HttpGet]
        public ActionResult Create()
        {
            CloudStorageAccount storageAccount = StorageHelpers.StorageAccount();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Constant.IMAGES_CONTAINER);

            List<ImageViewModel> images = new List<ImageViewModel>();
            foreach (var blob in container.ListBlobs())
            {
                images.Add(new ImageViewModel()
                {
                    Uri = blob.Uri.ToString()
                });
            }

            var userId = this.User.Identity.GetUserId();

            var model = new ReportViewModel();
            model.OwnerId = userId;
            model.Date = DateTime.Now;

            ViewBag.Images = images.Take(6);

            ViewBag.Sites = GetSitesAsSelectList(userId);

            return View(model);
        }

        // POST: Management/Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportViewModel model)
        {
            if (model.IsNotApprovedSite)
            {
                // Create not approved site in DB (it will be pending to be approved from manager or above role user)
                var newSite = new Site()
                {
                    Name = model.NoApprovedSiteName,
                    CreatorId = this.User.Identity.GetUserId(),
                };
                this.sites.Add(newSite);
                this.sites.Save();

                model.SiteName = model.NoApprovedSiteName;
                model.SiteId = newSite.Id;
            }
            else
            {
                model.SiteName = this.sites.GetById(model.SiteId).Name;
            }

            model.SiteName = model.SiteName.Replace(' ', '_');

            if (ModelState.IsValid || model.IsNotApprovedSite)
            {
                var newReport = new Report
                {
                    OwnerId = model.OwnerId,
                    Date = model.Date,
                    SiteId = model.SiteId,
                    Plot = model.Plot,
                    KitchenModel = model.KitchenModel,
                    Info = model.Info
                };

                var kitchenId = new KitchensData(context).GetKitchen(model.SiteName, model.Plot);

                var reportImages = this.images.All()
                                        .Where(i => i.OwnerId == model.OwnerId &&
                                        i.KitchenId == kitchenId)
                                        .ToList<Image>();

                foreach (var img in reportImages)
                {
                    img.ReportId = newReport.Id;
                    this.images.Update(img);
                }

                this.reports.Add(newReport);
                this.reports.Save();
                //this.images.Save();

                return RedirectToAction("Index");
            }

            string userId = this.User.Identity.GetUserId();
            ViewBag.Sites = GetSitesAsSelectList(userId);

            return View(model);
        }

        // GET: Management/Reports/GetImages
        [HttpGet]
        public PartialViewResult GetImages(string ownerId, string site = "", string plot = "", string model = null)
        {
            // TODO: Do GetImages 
            var modelForView = this.images.All()
                                .Where(i => i.OwnerId == ownerId &&
                                    i.Name.Contains("sn" + site) &&
                                    i.Name.Contains("pl" + plot) &&
                                    i.Name.Contains("m" + model))
                                .Select(i => new ImageViewModel
                                {
                                    Uri = i.Uri
                                })
                                .ToList();

            return PartialView("_ImagesPartial", modelForView);
        }

        // GET: Management/Reports/Info
        public string Info(int id)
        {
            return this.reports.GetById(id).Info;
        }

        private ICollection<SelectListItem> GetSitesAsSelectList(string userId)
        {
            return this.sites.All()
                                .Where(s => s.IsApproved || s.CreatorId == userId)
                                .Select(s => new SelectListItem
                                {
                                    Text = s.Name,
                                    Value = s.Id.ToString()
                                })
                                .ToList();
        }
    }
}