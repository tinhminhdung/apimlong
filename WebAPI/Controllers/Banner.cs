using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/Banner")]
    public class BannerController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("Get_List_Home_Banners")]
        public async Task<IHttpActionResult> Get_List_Home_Banners()
        {
            RootBanner it = new RootBanner();
            var item = BannerDataAccess.Get_List_Home_Banners("100");
            if (item.Count > 0)
            {
                it.items = item;
                it.success = true;
                it.error = null;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.items = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }
    }
}
