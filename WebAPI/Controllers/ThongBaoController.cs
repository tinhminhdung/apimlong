using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;
using WebAPI.Models;
using System.Collections.Generic;
using System;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/ThongBao")]
    public class ThongBaoController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("list_All_Active")]
        public async Task<IHttpActionResult> list_All_Active(string SkipCount, string MaxResultCount)
        {
            RootTB it = new RootTB();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThongBaoDataAccess.list_All_Active(SkipCount, MaxResultCount, list[0].ID.ToString());
                if (item.Count > 0)
                {
                    ResultTB its = new ResultTB();
                    its.items = item;
                    its.totalRecord = Convert.ToInt32(DataAccess.TotalRecod("select Count(*) as Count  from ( SELECT  * FROM  ( SELECT a.ID as IDThanhVienMembers from Members as a left join [dbo].[ThongBaoTV] as b On a.ID=b.IDThanhVien left join [dbo].[ThongBaoThanhVien] as c On c.ID=b.[IDThongBao] ) AS G WHERE G.IDThanhVienMembers=" + list[0].ID.ToString() + " ) as t")[0].TotalRecods);


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
        [Route("list_All_Active_Unread")]
        public async Task<IHttpActionResult> list_All_Active_Unread(string SkipCount, string MaxResultCount, int Status)
        {
            RootTB it = new RootTB();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThongBaoDataAccess.list_All_Active_Unread(SkipCount, MaxResultCount, list[0].ID.ToString(), Status.ToString());
                if (item.Count > 0)
                {
                    ResultTB its = new ResultTB();
                    its.items = item;
                    its.totalRecord = Convert.ToInt32(DataAccess.TotalRecod("select Count(*) as Count from ( SELECT  * FROM  ( SELECT a.ID as IDThanhVienMembers ,b.Status from Members as a left join [dbo].[ThongBaoTV] as b On a.ID=b.IDThanhVien left join [dbo].[ThongBaoThanhVien] as c On c.ID=b.[IDThongBao] ) AS G WHERE G.IDThanhVienMembers=" + list[0].ID.ToString() + " ) as t WHERE  1=1 and  t.Status=" + Status + "")[0].TotalRecods);

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
        [Route("list_Lastest_Active")]
        //Danh sách tin tức mới nhất
        public async Task<IHttpActionResult> list_Lastest_Active(string Size)
        {
            RootTB it = new RootTB();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThongBaoDataAccess.list_Lastest_Active(Size, list[0].ID.ToString());
                if (item.Count > 0)
                {
                    ResultTB its = new ResultTB()
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
            RootTBDT it = new RootTBDT();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThongBaoDataAccess.New_Detail(id, list[0].ID.ToString());
                if (item.Count > 0)
                {
                    ResultTB_DT its = new ResultTB_DT()
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
        public async Task<IHttpActionResult> list_Related(string Id, string Size)
        {
            RootTB it = new RootTB();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThongBaoDataAccess.list_Related(Id, Size, list[0].ID.ToString());
                if (item.Count > 0)
                {
                    ResultTB its = new ResultTB()
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

        [Authorize]
        [HttpGet]
        [Route("GetCountWaiting")]
        public async Task<IHttpActionResult> GetCountWaiting()
        {
            Root_GetCountWaiting it = new Root_GetCountWaiting();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThongBaoDataAccess.GetCountWaiting(list[0].ID.ToString());
                if (item.Count > 0)
                {
                    it.Counts = item[0].Counts;
                    it.success = true;
                    it.error = null;
                    it.unAuthorizedRequest = false;
                }
                else
                {
                    it.Counts = 0;
                    it.success = false;
                    it.error = true;
                    it.unAuthorizedRequest = false;
                }
            }
            else
            {
                it.Counts = 0;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }

    }
}
