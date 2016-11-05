namespace PrimusFlex.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Site : BaseModel<int>
    {
        public Site()
        {
            this.Reports = new HashSet<Report>();
        }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public bool IsApproved { get; set; } // false by default

        public string CreatorId { get; set; }

        // Navigation properties

        public virtual ICollection<Report> Reports { get; set; }
    }
}
