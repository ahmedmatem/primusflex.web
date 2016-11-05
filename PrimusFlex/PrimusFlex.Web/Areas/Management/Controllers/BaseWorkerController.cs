namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System.Data.Entity;
    using System.Web.Mvc;

    using Data;
    using Data.Common;
    using Data.Models;

    [Authorize(Roles ="Worker")]
    public class BaseWorkerController : BaseController
    {
        protected readonly DbContext context = new ApplicationDbContext();

        protected readonly IDbRepository<Report> reports;
        protected readonly IDbRepository<Image> images;
        protected readonly IDbRepository<Site> sites;
        protected readonly IDbRepository<Kitchen> kitchens;

        public BaseWorkerController()
        {
            this.reports = new DbRepository<Report>(context);
            this.images = new DbRepository<Image>(context);
            this.sites = new DbRepository<Site>(context);
            this.kitchens = new DbRepository<Kitchen>(context);
        }
    }
}