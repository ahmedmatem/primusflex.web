namespace PrimusFlex.Web.Areas.Management.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using Data.Models.Types;
    using Infrastructure.Mapping;
    using Data.Models;
    using AutoMapper;

    public class ReportViewModel
    {
        public int Id { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Site")]
        public int SiteId { get; set; }

        [Display(Name = "Site")]
        public string SiteName { get; set; }

        public string NoApprovedSiteName { get; set; }

        public bool IsNotApprovedSite { get; set; }

        [Required]
        public string Plot { get; set; }

        [Required]
        [Display(Name = "Kitchen model")]
        public KitchenModel KitchenModel { get; set; }

        public string Info { get; set; }

        // images
        public ICollection<ImageViewModel> Images { get; set; }
    }
}