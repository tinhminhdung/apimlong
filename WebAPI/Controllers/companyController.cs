using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;
using WebAPI.Models;
using System.Collections.Generic;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("getDetail")]
        public async Task<IHttpActionResult> getDetail()
        {
            RootCompany it = new RootCompany();
            var vii = new List<Company_Detail>();
            vii.Add(new Company_Detail()
            {
                TenCongTy = Commond.Setting("TenCongTy"),
                Email = Commond.Setting("EmailCT"),
                DienThoai = Commond.Setting("DienThoaiCT"),
                DiaChi = Commond.Setting("DiachiCT")
            });
            it.items = vii;
            it.success = true;
            it.error = null;
            it.unAuthorizedRequest = false;
            return Ok(it);
        }

    }
}
