namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    public class SitesController : BaseAdminController
    {
        // GET: Management/Sites
        public ActionResult Index()
        {
            var model = this.sites.All()
                            .Where(s => s.IsApproved)
                            .OrderBy(s => s.Name)
                            .Select(s => new SiteViewModel
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Address = s.Address,
                                PostCode = s.PostCode
                            })
                            .ToList();
            ViewBag.NoApprovedSitesCount = this.sites.All().Count() - model.Count;

            return View(model);
        }

        // GET: Management/Sites/Pending
        public ActionResult Pending()
        {
            var model = this.sites.All()
                            .Where(s => !s.IsApproved)
                            .OrderBy(s => s.Name)
                            .Select(s => new SiteViewModel
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Address = s.Address,
                                PostCode = s.PostCode
                            })
                            .ToList();

            return View(model);
        }

        // GET: Management/Sites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Management/Sites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newSite = new Site()
                {
                    Name = model.Name,
                    Address = model.Address,
                    PostCode = model.PostCode,
                    IsApproved = true
                };

                this.sites.Add(newSite);
                this.sites.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Management/Sites/Edit/id
        public ActionResult Edit(int id)
        {
            var siteViewModel = this.sites.All()
                            .Where(s => s.Id == id)
                            .Select(s => new SiteViewModel
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Address = s.Address,
                                PostCode = s.PostCode
                            })
                            .FirstOrDefault();

            return View(siteViewModel);
        }

        // GET: Management/Sites/Approve/id
        public ActionResult Approve(int id)
        {
            var siteViewModel = this.sites.All()
                            .Where(s => s.Id == id)
                            .Select(s => new SiteViewModel
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Address = s.Address,
                                PostCode = s.PostCode
                            })
                            .FirstOrDefault();

            return View(siteViewModel);
        }

        // POST: Management/Sites/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var siteForEdit = this.sites.GetById(model.Id);

                siteForEdit.Name = model.Name;
                siteForEdit.Address = model.Address;
                siteForEdit.PostCode = model.PostCode;

                this.sites.Update(siteForEdit);
                this.sites.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // POST: Management/Sites/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(SiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var siteForApprove = this.sites.GetById(model.Id);

                siteForApprove.Name = model.Name;
                siteForApprove.Address = model.Address;
                siteForApprove.PostCode = model.PostCode;
                siteForApprove.IsApproved = true;

                this.sites.Update(siteForApprove);
                this.sites.Save();

                return RedirectToAction("Pending");
            }

            return View(model);
        }

        // GET: Management/Sites/Delete/id
        public ActionResult Delete(int id)
        {
            var siteForDelete = this.sites.GetById(id);
            this.sites.Delete(siteForDelete);
            this.sites.Save();

            return RedirectToAction("Index");
        }
    }
}