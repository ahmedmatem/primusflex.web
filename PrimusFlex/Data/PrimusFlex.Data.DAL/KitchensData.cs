namespace PrimusFlex.Data.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;

    using Common;
    using Models;
    using Models.Types;
    public class KitchensData
    {
        public readonly IDbRepository<Kitchen> kitchens;

        public KitchensData(DbContext context)
        {
            this.kitchens = new DbRepository<Kitchen>(context);
        }

        public int GetKitchen(string siteName, string plotNumber)
        {
            return this.kitchens.All()
                                    .Where(k => k.SiteName == siteName &&
                                                k.PlotNumber == plotNumber)
                                    .Select(k => k.Id)
                                    .FirstOrDefault();
        }
    }
}
