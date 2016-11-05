namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Data.Common;
    using Data;

    [Authorize(Roles ="SupperAdmin,Admin")]
    public class BaseAdminController : BaseController
    {
        protected readonly DbContext context = new ApplicationDbContext();

        protected readonly IDbRepository<Site> sites;

        public BaseAdminController()
        {
            this.sites = new DbRepository<Site>(this.context);
        }
    }
}