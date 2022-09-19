using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string userName { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string emailAddress { get; set; }
        public string fullName { get; set; }
        public bool isActive { get; set; }
        public DateTime lastLoginTime { get; set; }
        public DateTime creationTime { get; set; }
        public int id { get; set; }
        public List<string> roleNames { get; set; }
    }


    public class InfoMenberdb
    {
        public string HoVaTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public int TrangThai { get; set; }
        public int GioiThieu { get; set; }
        public string GioiThieuThanhVien { get; set; }
        public string TongTien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCCCD { get; set; }
        public DateTime NgayCap { get; set; }
        public string NoiCap { get; set; }
    }
    public class InfoMenber
    {
        public string HoVaTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public bool isActive { get; set; }
        public string GioiThieu { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCCCD { get; set; }
        public DateTime NgayCap { get; set; }
        public string NoiCap { get; set; }

    }
    public class InfoMenber_GetUserBalance
    {
        public Double TongTien { get; set; }
    }
    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

}
