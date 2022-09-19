using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using VS.ECommerce_MVC;
using WebAPI.Models;
using WebAPI.Providers;
using WebAPI.Results;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        // GET api/Account/UserInfo
        [Route("UserInfo")]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            List<string> roles = new List<string>();
            var userId = User.Identity.GetUserId<int>();
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var applicationUser = UserManager.FindById(userId);

            var roleUser = UserManager.GetRoles(applicationUser.Id);
            // var userId = User.Identity.GetUserId();
            UserInfoViewModel info = new UserInfoViewModel();
            info.emailAddress = applicationUser.Email;
            info.creationTime = DateTime.Now;
            info.fullName = applicationUser.UserName;
            info.id = applicationUser.Id;
            info.lastLoginTime = DateTime.Now;
            info.name = applicationUser.UserName;
            info.surname = applicationUser.UserName;
            info.userName = applicationUser.UserName;
            info.roleNames = roleUser.ToList();
            return Ok(info);
        }

        [Route("getUserDetail")]
        public async Task<IHttpActionResult> getUserDetail()
        {
            //List<string> roles = new List<string>();
            //var userId = User.Identity.GetUserId<int>();
            //ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            //var applicationUser = UserManager.FindById(userId);

            //var roleUser = UserManager.GetRoles(applicationUser.Id);
            var userId = User.Identity.GetUserId();
            List<InfoMenberdb> list = SMember.Name_Procedure("S_Member_Info", userId);
            if (list.Count > 0)
            {
                InfoMenber info = new InfoMenber();
                info.HoVaTen = list[0].HoVaTen;
                info.DiaChi = list[0].DiaChi;
                info.DienThoai = list[0].DienThoai;
                info.Email = list[0].Email;
                info.AnhDaiDien = list[0].AnhDaiDien;
                if (list[0].TrangThai == 1)
                {
                    info.isActive = true;
                }
                else
                {
                    info.isActive = false;
                }
                info.GioiThieu = list[0].GioiThieuThanhVien;
                info.NgaySinh = list[0].NgaySinh;
                info.SoCCCD = list[0].SoCCCD;
                info.NgayCap = list[0].NgayCap;
                info.NoiCap = list[0].NoiCap;
                return Ok(info);
            }
            else
            {
                return Ok(new { success = false, Message = "Lỗi" });
            }
        }


        [Route("GetUserBalance")]
        public async Task<IHttpActionResult> GetUserBalance()
        {
            //var roleUser = UserManager.GetRoles(applicationUser.Id);
            var userId = User.Identity.GetUserId();
            List<InfoMenberdb> list = SMember.Name_Procedure("S_Member_Info", userId);
            if (list.Count > 0)
            {
                InfoMenber_GetUserBalance info = new InfoMenber_GetUserBalance();
                info.TongTien = Convert.ToInt32(list[0].TongTien);
                return Ok(info);
            }
            else
            {
                return Ok(new { success = false, Message = "Lỗi" });
            }
        }


        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (((model.OldPassword == "string") || (model.ConfirmPassword == "string")) || (model.NewPassword == "string"))
                {
                    return Ok(new { success = false, Status = 0, Message = "Vui lòng điền đầy đủ thông tin" });
                }
                else if (model.NewPassword.Length < 3)
                {
                    return Ok(new { success = false, Status = 1, Message = "Vui lòng nhập mật khẩu từ 6 ký tự trở lên" });
                }
                else if (!model.NewPassword.Equals(model.ConfirmPassword))
                {
                    return Ok(new { success = false, Status = 2, Message = "Mật khẩu mới không hợp lệ" });
                }
                else
                {
                    List<WebAPI.Models.Member> itel = SMember.Name_Text("select * from Members  where  IDThanhVien='" + userId.Trim() + "' and PasWord='" + model.OldPassword.Trim() + "'");
                    if (itel.Count <= 0)
                    {
                        return Ok(new { success = false, Status = 3, Message = "Mật khẩu cũ không đúng." });
                    }
                    else
                    {
                        IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
                        if (!result.Succeeded)
                        {
                            return Ok(new { success = false, Status = 3, Message = "Mật khẩu cũ không đúng." });
                        }
                    }
                }
            }
            SMember.Name_Text("UPDATE [Members] SET PasWord='" + model.NewPassword + "' WHERE IDThanhVien =" + userId + "");
            return Ok(new { success = true, Status = 5, Message = "Đổi mật khẩu thành công" });
        }

        // POST api/Account/Register
        //[AllowAnonymous]
        //[Route("Register")]
        //public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var roles = new string[] { "role_khach_hang" };

        //    var user = new ApplicationUser()
        //    {
        //        UserName = model.UserName,
        //        Email = string.Format("{0}@gmail.com", model.UserName)
        //    };

        //    IdentityResult result = await UserManager.CreateAsync(user, model.Password);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }
        //    var roleresult = UserManager.AddToRole(user.Id, "role_khach_hang");

        //    return Ok();
        //}

        [Route("Register_Update")]
        public async Task<IHttpActionResult> Register_Update(MemberUpdate model)
        {
            var userId = User.Identity.GetUserId();
            if (model.HoVaTen == "string" || model.DiaChi == "string" || model.Email == "string")
            {
                return Ok(new { success = false, Status = 1, Message = "Vui lòng kiểm tra lại đầu vào" });
            }
            if (model.Email.Length > 0)
            {
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where Email='" + model.Email.Trim().ToLower() + "'  and IDThanhVien!='" + userId + "' ");
                if (list.Count > 0)
                {
                    return Ok(new { success = false, Status = 2, Message = "Email đã tồn tại trong hệ thống" });
                }
            }
            if (model.DienThoai.Length > 0)
            {
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where DienThoai='" + model.DienThoai.Trim().ToLower() + "'  and IDThanhVien!='" + userId + "' ");
                if (list.Count > 0)
                {
                    return Ok(new { success = false, Status = 3, Message = "Điện thoại đã tồn tại trong hệ thống" });
                }
            }
            if (model.GioiThieu.Length > 0)
            {
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where DienThoai='" + model.GioiThieu.Trim().ToLower() + "'");
                if (list.Count < 0)
                {
                    return Ok(new { success = false, Status = 4, Message = "Không tìm thấy người giới thiệu" });
                }
            }
            string Nguoigioithieu = "0";
            string VTree = "0";
            List<WebAPI.Models.Member> lisyt = SMember.Name_Text("select * from Members  where  IDThanhVien='" + userId + "' ");
            if (lisyt.Count > 0)
            {
                if (lisyt[0].GioiThieu == 0)
                {
                    if (model.GioiThieu.Trim().Length > 0)
                    {
                        if (model.DienThoai.Trim() != model.GioiThieu.Trim())
                        {
                            List<WebAPI.Models.Member> ds = SMember.Name_Text("select * from Members  where DienThoai='" + model.GioiThieu.Trim().ToLower() + "' and IDThanhVien!='" + userId + "'  and TrangThai=1");
                            if (ds.Count > 0)
                            {
                                Nguoigioithieu = ds[0].ID.ToString();
                                VTree = ds[0].MTree.ToString();
                            }
                            else
                            {
                                return Ok(new { success = false, Status = 5, Message = "Không tìm thấy người giới thiệu" });
                            }
                        }
                    }
                }
                String mtree = "|0|";
                if (Nguoigioithieu != "0")
                {
                    mtree = VTree;
                }
                DatalinqDataContext db = new DatalinqDataContext();
                String mtrees = mtree;
                APIMember obj = db.APIMembers.SingleOrDefault(p => p.IDThanhVien == userId.Trim());
                obj.IDThanhVien = userId.ToString();
                obj.HoVaTen = model.HoVaTen;
                obj.NgaySinh = model.NgaySinh;
                obj.DiaChi = model.DiaChi;
                obj.DienThoai = model.DienThoai;
                obj.Email = model.Email;
                obj.NgayTao = DateTime.Now;
                if (lisyt[0].GioiThieu == 0 && Nguoigioithieu != "0")
                {
                    obj.GioiThieu = int.Parse(Nguoigioithieu);
                    if (Nguoigioithieu == "0")
                    {
                        obj.MTree = "|0|";
                    }
                    else
                    {
                        obj.MTree = mtrees.Replace("|0|", "|");
                    }
                }
                obj.SoCCCD = model.SoCCCD;
                obj.NgayCap = model.NgayCap;
                obj.NoiCap = model.NoiCap;
                db.SubmitChanges();

                SMember.Name_Text("UPDATE [dbo].[AspNetUsers] SET UserName='" + model.DienThoai + "' WHERE UserName='" + lisyt[0].DienThoai + "' ");
                if (lisyt[0].GioiThieu == 0 && Nguoigioithieu != "0")
                {
                    string Cay = mtrees.Replace("|0|", "|") + lisyt[0].ID.ToString() + "|";
                    SMember.Name_Text("UPDATE [Members] SET MTree='" + Cay + "'  WHERE ID =" + lisyt[0].ID.ToString() + "");
                }
                SMember.Name_Text("UPDATE [Members] SET Search=N'" + MoreAll.RewriteURLNew.NameSearch(model.HoVaTen) + " " + MoreAll.RewriteURLNew.NameSearch(model.DiaChi) + "'  WHERE ID =" + lisyt[0].ID.ToString() + "");
                return Ok(new { success = true, Status = 6, Message = "Cập nhật thành công" });
            }
            return Ok(new { success = true, Status = 7, Message = "Lỗi cập nhật" });

        }

        [AllowAnonymous]
        [Route("Register_New")]
        public async Task<IHttpActionResult> Register_New(MemberInput model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser()
            {
                UserName = model.DienThoai,
                Email = model.Email
            };
            if (model.HoVaTen == "string" || model.Password == "string" || model.DiaChi == "string" || model.Email == "string")
            {
                return Ok(new { success = false, Status = 1, Message = "Vui lòng kiểm tra lại đầu vào" });
            }
            if (model.Email.Length > 0)
            {
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where Email='" + model.Email.Trim().ToLower() + "'");
                if (list.Count > 0)
                {
                    return Ok(new { success = false, Status = 2, Message = "Email đã tồn tại trong hệ thống" });
                }
            }
            if (model.DienThoai.Length > 0)
            {
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where DienThoai='" + model.DienThoai.Trim().ToLower() + "'");
                if (list.Count > 0)
                {
                    return Ok(new { success = false, Status = 3, Message = "Điện thoại đã tồn tại trong hệ thống" });
                }
            }
            if (model.GioiThieu.Length > 0)
            {
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where DienThoai='" + model.GioiThieu.Trim().ToLower() + "'");
                if (list.Count < 0)
                {
                    return Ok(new { success = false, Status = 4, Message = "Không tìm thấy người giới thiệu" });
                }
            }
            if (model.Password.Length < 3)
            {
                return Ok(new { success = false, Status = 5, Message = "Vui lòng nhập mật khẩu từ 6 ký tự trở lên" });
            }
            if (!model.ConfirmPassword.Equals(model.Password))
            {
                return Ok(new { success = false, Status = 6, Message = "Mật khẩu mới không hợp lệ" });
            }
            if (!Commond.Password(model.Password.Trim()) == true)
            {
                return Ok(new { success = false, Status = 7, Message = "Các ký tự từ a đến z và các số từ 0 đến 9" });
            }
            string Nguoigioithieu = "0";
            string VTree = "0";
            if (model.GioiThieu.Trim().Length > 0)
            {
                if (model.DienThoai.Trim() != model.GioiThieu.Trim())
                {
                    List<WebAPI.Models.Member> ds = SMember.Name_Text("select * from Members  where DienThoai='" + model.GioiThieu.Trim().ToLower() + "' and TrangThai=1");
                    if (ds.Count > 0)
                    {
                        Nguoigioithieu = ds[0].ID.ToString();
                        VTree = ds[0].MTree.ToString();
                    }
                    else
                    {
                        return Ok(new { success = false, Status = 8, Message = "Không tìm thấy người giới thiệu" });
                    }
                }
            }
            String mtree = "|0|";
            if (Nguoigioithieu != "0")
            {
                mtree = VTree;
            }
            String mtrees = mtree;
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return Ok(new { success = false, Status = 9, Message = "Vui lòng kiểm tra lại đầu vào" });
            }
            WebAPI.Models.Member obj = new WebAPI.Models.Member();
            string validatekey = DateTime.Now.Ticks.ToString();
            obj.IDThanhVien = user.Id.ToString();//lấy dc id của thành viên  , gán sang bảng member thông qua ID
            obj.PasWord = model.Password;
            obj.HoVaTen = model.HoVaTen;
            obj.GioiTinh = 0;
            obj.NgaySinh = DateTime.Now;
            obj.DiaChi = model.DiaChi;
            obj.DienThoai = model.DienThoai;
            obj.Email = model.Email;
            obj.AnhDaiDien = "";
            obj.NgayTao = DateTime.Now;
            obj.Key = validatekey;
            obj.TrangThai = 1;
            obj.Lang = "VIE";
            obj.GioiThieu = int.Parse(Nguoigioithieu);
            if (Nguoigioithieu == "0")
            {
                obj.MTree = "|0|";
            }
            else
            {
                obj.MTree = mtrees.Replace("|0|", "|");
            }
            obj.TongTien = "0";
            var bn = ThanhVienDataAccess.Save_ThanhVien(obj);

            if (bn == true)
            {
                List<WebAPI.Models.Member> Them = SMember.Name_Text("select top 1 * from Members order by ID desc");
                if (Them.Count > 0)
                {
                    string Cay = mtrees.Replace("|0|", "|") + Them[0].ID.ToString() + "|";
                    SMember.Name_Text("UPDATE [Members] SET MTree='" + Cay + "',SieuThi=0,ImageQRCode='',SoCCCD='',NgayCap='',NoiCap='',ImagesCMNDTruoc='',ImagesCMNDSau='',Search=N'" + MoreAll.RewriteURLNew.NameSearch(model.HoVaTen) + " " + MoreAll.RewriteURLNew.NameSearch(model.DiaChi) + "'  WHERE ID =" + Them[0].ID.ToString() + "");
                }
                return Ok(new { success = true, Status = 10, Message = "Thêm mới thành công" });
            }
            else
            {
                SMember.Name_Text("delete from Members where DienThoai='" + model.DienThoai + "'");
                SMember.Name_Text("delete from [dbo].[AspNetUsers] where UserName='" + model.DienThoai + "'");
                return Ok(new { success = false, Status = 11, Message = "Lỗi" });
            }

            return Ok();
        }

        [Route("SaveImageAvata")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveImageAvata()// lưu ý phải chạy bằng postment cài đặt chứ ko chạy dc trên posment chome, chọn boby, form-data,ở Key chọn File
        {
            try
            {
                #region save raw
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                string filename = Regex.Replace(Path.GetFileName(HttpContext.Current.Server.MapPath(postedFile.FileName)), @"[^0-9a-zA-Z:,.-]+", "");
                string str_dir = HttpContext.Current.Server.MapPath("~/Images/") + DateTime.Now.ToString("yyyy") + @"\" + DateTime.Now.ToString("MM");
                if (!Directory.Exists(str_dir))
                {
                    Directory.CreateDirectory(str_dir);
                }

                string link = str_dir + @"/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_imp_" + filename;
                postedFile.SaveAs(link);

                var matP = Regex.Split(link.Replace("\\", "/"), "Images");
                var links = link = "/Images/" + matP[1];
                var Url = "https://api.minhlongfinance.vn/images" + matP[1].Replace("//", "/");
                #endregion

                var userid = User.Identity.GetUserId();
                List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where IDThanhVien='" + userid.Trim().ToLower() + "' ");
                if (list.Count > 0)
                {
                    var IDThanhVien = list[0].ID;
                    SMember.Name_Text("update Members set AnhDaiDien=N'" + Url + "' where id=" + IDThanhVien.ToString() + "");
                }

                return Ok(Url);
            }
            catch (Exception)
            {
                return Ok("");
            }
            return Ok("");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
