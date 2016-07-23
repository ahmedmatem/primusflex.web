namespace PrimusFlex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using Types;

    public class Kitchen : BaseModel<int>
    {
        public Kitchen()
        {
            this.Images = new HashSet<Image>();
        }

        [Required]
        public KitchenModel Model { get; set; }

        [Required]
        public string SiteName { get; set; }

        [Required]
        public string PlotNumber { get; set; }

        // Navigation properties

        public virtual ICollection<Image> Images { get; set; }

    }
}
