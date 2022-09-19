using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;
using System;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/BatDongSan")]
    public class BatDongSanController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("list_All_Active")]
        public async Task<IHttpActionResult> list_All_Active(string SkipCount, string MaxResultCount)
        {
            RootBDS it = new RootBDS();
            var item = BatDongSanDataAccess.list_All_Active(SkipCount, MaxResultCount);
            if (item.Count > 0)
            {
                ResultBDS its = new ResultBDS();
                its.items = item;
                its.totalRecord = Convert.ToInt32(DataAccess.TotalRecod("select Count(*) as Count  from BatDongSan")[0].TotalRecods);


                it.result = its;
                it.success = true;
                it.error = null;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.result = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }


        [Authorize]
        [HttpGet]
        [Route("list_Lastest_Active/")]
        //Danh sách tin tức mới nhất
        public async Task<IHttpActionResult> list_Lastest_Active(string Size)
        {
            RootBDS it = new RootBDS();
            var item = BatDongSanDataAccess.list_Lastest_Active(Size);
            if (item.Count > 0)
            {
                ResultBDS its = new ResultBDS()
                {
                    items = item
                };
                it.result = its;
                it.success = true;
                it.error = null;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.result = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }


        [Authorize]
        [HttpGet]
        [Route("Get_Detail")]
        //Chi tiết tin tức
        public async Task<IHttpActionResult> Get_Detail(string id)
        {
            RootBDSDT it = new RootBDSDT();
            var item = BatDongSanDataAccess.New_Detail(id);
            if (item.Count > 0)
            {
                ResultBDS_DT its = new ResultBDS_DT()
                {
                    items = item
                };
                it.result = its;
                it.success = true;
                it.error = null;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.result = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }


        [Authorize]
        [HttpGet]
        [Route("list_Related")]
        //Danh sách tin tức mới nhất
        public async Task<IHttpActionResult> list_Related(string Id,  string Size)
        {
            RootBDS it = new RootBDS();
            var item = BatDongSanDataAccess.list_Related(Id, Size);
            if (item.Count > 0)
            {
                ResultBDS its = new ResultBDS()
                {
                    items = item
                };
                it.result = its;
                it.success = true;
                it.error = null;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.result = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }


    }
}
