namespace PrimusFlex.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Phone : BaseModel<int>
    {
        public string Imei { get; set; }

        public string OwnerId { get; set; }
    }
}
