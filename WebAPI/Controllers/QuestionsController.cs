using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;
using System.Collections.Generic;
using System;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/Questions")]
    public class QuestionsController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("list_All_Active")]
        public async Task<IHttpActionResult> list_All_Active(string SkipCount, string MaxResultCount)
        {
            RootFAQ it = new RootFAQ();
            var item = QuestionsDataAccess.list_All_Active(SkipCount, MaxResultCount);
            if (item.Count > 0)
            {
                ResultQuestions its = new ResultQuestions();
                its.items = item;
                its.totalRecord = Convert.ToInt32(DataAccess.TotalRecod("select Count(*) as Count  from [VideoClip] ")[0].TotalRecods);

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
        [Route("GetPolicy")]
        public async Task<IHttpActionResult> GetPolicy()
        {
            RootDieuKhoan it = new RootDieuKhoan();
            var vii = new List<DieuKhoan_Detail>();
            vii.Add(new DieuKhoan_Detail()
            {
                Contens = Commond.Setting("DieuKhoan"),
            });
            it.items = vii;
            it.success = true;
            it.error = null;
            it.unAuthorizedRequest = false;
            return Ok(it);
        }

    }
}
