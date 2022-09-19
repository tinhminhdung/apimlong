using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebAPI.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class MemberInput
    {
        public string HoVaTen { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string GioiThieu { get; set; }

    }

    public class Member
    {
        //News
        public int ID { get; set; }
        public string PasWord { get; set; }
        public string HoVaTen { get; set; }
        public int GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public DateTime NgayTao { get; set; }
        public string Key { get; set; }
        public int TrangThai { get; set; }
        public string Lang { get; set; }
        public int GioiThieu { get; set; }
        public string MTree { get; set; }
        public string IDThanhVien { get; set; }
        public string TongTien { get; set; }
        public string TongTienTietKiem { get; set; }
        public int SieuThi { get; set; }
        public string ImageQRCode { get; set; }
        public string SoCCCD { get; set; }
        public DateTime NgayCap { get; set; }
        public string NoiCap { get; set; }
        public string ImagesCMNDTruoc { get; set; }
        public string ImagesCMNDSau { get; set; }
        public string Search { get; set; }
    }

    public class MemberUpdate
    {
        public string HoVaTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string GioiThieu { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCCCD { get; set; }
        public DateTime NgayCap { get; set; }
        public string NoiCap { get; set; }
    }

}


public class RegisterExternalBindingModel
{
}

public class RemoveLoginBindingModel
{
    [Required]
    [Display(Name = "Login provider")]
    public string LoginProvider { get; set; }

    [Required]
    [Display(Name = "Provider key")]
    public string ProviderKey { get; set; }
}

public class SetPasswordBindingModel
{
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
