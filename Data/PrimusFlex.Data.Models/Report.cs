namespace PrimusFlex.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using Types;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Report : BaseModel<int>
    {
        public Report()
        {
            this.Images = new HashSet<Image>();
        }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int SiteId { get; set; }

        [Required]
        public string Plot { get; set; }

        [Required]
        public KitchenModel KitchenModel { get; set; }

        public string Info { get; set; }

        //Navigation Properties

        public virtual ICollection<Image> Images { get; set; }

        public virtual Site Site { get; set; }
    }
}
