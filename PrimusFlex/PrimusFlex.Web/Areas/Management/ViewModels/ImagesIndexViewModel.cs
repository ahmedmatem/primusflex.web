namespace PrimusFlex.Web.Areas.Management.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Data.Models;
    using Data.Models.Types;

    public class ImagesIndexViewModel
    {
        public string SiteName { get; set; }

        public string PlotNumber { get; set; }

        public KitchenModel KitchcenModel { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}