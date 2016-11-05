namespace PrimusFlex.Web.Areas.Management.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SiteViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public bool IsApproved { get; set; }    // false by default
    }
}