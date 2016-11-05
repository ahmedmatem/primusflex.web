namespace PrimusFlex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Common.Models;

    public class Image : BaseModel<int>
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public int KitchenId { get; set; }

        public int? ReportId { get; set; }

        public string OwnerId { get; set; }

        // Navigation properties

        public virtual Kitchen Kitchen { get; set; }

        public virtual Report Report { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}
