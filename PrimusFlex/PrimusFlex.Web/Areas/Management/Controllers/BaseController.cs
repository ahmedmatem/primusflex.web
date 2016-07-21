namespace PrimusFlex.Web.Areas.Management.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;

    [Authorize(Roles = "SupperAdmin,Admin,Manager")]
    public abstract class BaseController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}