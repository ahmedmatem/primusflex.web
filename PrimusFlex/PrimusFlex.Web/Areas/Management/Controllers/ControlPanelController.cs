namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ControlPanelController : BaseAdminController
    {
        // GET: Management/ControlPanel
        public ActionResult Index()
        {
            return View();
        }
    }
}