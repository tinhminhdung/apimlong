using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;
using System;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/News")]
    public class NewsController : ApiController
    {

        //http://localhost:59020/token?username=admin&password=Admin@2022&grant_type=password
        // username: admin
        // password:Admin@2022
        // grant_type:password
        //bearer f1UEB6wSL4lxEsV8UJM0W1AaJn7ok2_oLVOHYtXUkVMWjVsPV8FroxiLageviIXoAeZHsswsMXd5wzd7ARmTFiyyZnSeIrP8cYLnoKN_W9lXVMO4Y6Rn7T-ZlZM1sxZmhtGd5rE12f8LAbD8zfLNCKW_WdIJWsPVaETXszIKFQHsgij-NrDxdOPkyfHu0cjnMziaTkS49VUq04BnBGpwACoeGl0oYHr4goUArq1f62ao63QaTELdcdHupaJH0oSsbVO7Wd-L3iKUT-IMhxtYMFw-c3H5ZYN_XILXO8Pe-KXSG-feya8GNMT4jvVyNXXs3DQnTzIrlbZcLcM77w7cignOM3o33-KJRevawMCPZs9UcDkLxsaGEDbcDwJdrPOOP8bRF9drBOMxKAASGpjDl58uWkr7NlVMhkeWqfl3jVyLgi2iaMu_-YeoQJG7uF4SY8sFkRpNDqXVwScEZTZede6euyf6KiivFkvkP_Raf88

        [Authorize]
        [HttpGet]
        [Route("list_All_Active")]///{SkipCount}/{MaxResultCount}
        //Ds tất cả các tin tức
        public async Task<IHttpActionResult> list_All_Active(string SkipCount, string MaxResultCount)
        {
            // var userId = User.Identity.GetUserId();
            Root it = new Root();
            var item = DataAccess.list_All_Active(SkipCount, MaxResultCount);
            if (item.Count > 0)
            {
                ResultTinTuc its = new ResultTinTuc();
                its.items = item;
                its.totalRecord = Convert.ToInt32(DataAccess.TotalRecod("select Count(*) as Count  from News")[0].TotalRecods);


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
        [Route("List_NewsBy_Category")]///{SkipCount}/{MaxResultCount}
        //Ds tất cả các tin tức
        public async Task<IHttpActionResult> List_NewsBy_Category(int categoryId, string SkipCount, string MaxResultCount)
        {
            // var userId = User.Identity.GetUserId();
            Root it = new Root();
            var item = DataAccess.List_NewsBy_Category(categoryId, SkipCount, MaxResultCount);
            if (item.Count > 0)
            {
                ResultTinTuc its = new ResultTinTuc();
                its.items = item;
                its.totalRecord = Convert.ToInt32(DataAccess.TotalRecod("select Count(*) as Count  from News where icid =" + categoryId + "")[0].TotalRecods);

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
        [Route("list_Lastest_Active/")]//{CategoryId}/{Size}
        //Danh sách tin tức mới nhất
        public async Task<IHttpActionResult> list_Lastest_Active(string CategoryId, string Size)
        {
            Root it = new Root();
            var item = DataAccess.list_Lastest_Active(CategoryId, Size);
            if (item.Count > 0)
            {
                ResultTinTuc its = new ResultTinTuc()
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
        [Route("Get_Detail")]//{Id}
        //Chi tiết tin tức
        public async Task<IHttpActionResult> Get_Detail(string id)
        {
            RootDT it = new RootDT();
            var item = DataAccess.New_Detail(id);
            if (item.Count > 0)
            {
                ResultTinTuc_DT its = new ResultTinTuc_DT()
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
        [Route("list_Related")]///{Id}/{CategoryId}/{Size}
        //Danh sách tin tức mới nhất
        public async Task<IHttpActionResult> list_Related(string Id, string CategoryId, string Size)
        {
            Root it = new Root();
            var item = DataAccess.list_Related(Id, CategoryId, Size);
            if (item.Count > 0)
            {
                ResultTinTuc its = new ResultTinTuc()
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


        //

        ////[Authorize] API nay can dang nhap
        //[Authorize]
        //[HttpGet]
        //[Route("GetAll_Count_Skip")]
        //public async Task<IHttpActionResult> GetAll_Count_Skip(string SkipCount, string MaxResultCount)
        //{
        //    //Get UserId hiện tại
        //    var userId = User.Identity.GetUserId();
        //    RootKM it = new RootKM();
        //    var item = DataAccess.GetAll_ItemKM_To_Skip(SkipCount, MaxResultCount);
        //    if (item.Count > 0)
        //    {
        //        ResultKM its = new ResultKM()
        //        {
        //            items = item
        //        };
        //        it.result = its;
        //        it.success = true;
        //        it.error = null;
        //        it.unAuthorizedRequest = false;
        //    }
        //    else
        //    {
        //        it.result = null;
        //        it.success = false;
        //        it.error = true;
        //        it.unAuthorizedRequest = false;
        //    }
        //    return Ok(it);
        //}

        //// API nay can dang nhap bang tai khoan co role la "Role_Admin")
        //[Authorize(Roles = "Role_Admin")]
        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IHttpActionResult> GetAll(string SkipCount, string MaxResultCount)
        //{
        //    //Get UserId hiện tại
        //    var userId = User.Identity.GetUserId();
        //    RootKM it = new RootKM();
        //    var item = DataAccess.GetAll_ItemKM_To_Skip(SkipCount, MaxResultCount);
        //    if (item.Count > 0)
        //    {
        //        ResultKM its = new ResultKM()
        //        {
        //            items = item
        //        };
        //        it.result = its;
        //        it.success = true;
        //        it.error = null;
        //        it.unAuthorizedRequest = false;
        //    }
        //    else
        //    {
        //        it.result = null;
        //        it.success = false;
        //        it.error = true;
        //        it.unAuthorizedRequest = false;
        //    }
        //    return Ok(it);
        //}


    }
}
