namespace PrimusFlex.Data.DAL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ImagesData
    {
        protected IDbRepository<Image> images;

        public ImagesData(IDbRepository<Image> images)
        {
            this.images = images;
        }
    }
}
