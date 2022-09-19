using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    public class obj_ThanhVienNganHang
    {
        public string HoVaTen { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenNganHang { get; set; }
        public string BankNameShort { get; set; }
        public string ChiNhanh { get; set; }

    }
    public class ThanhVienNganHangs
    {
        public int ID { get; set; }
        public int IDThanhVien { get; set; }
        public string HoVaTen { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenNganHang { get; set; }
        public string BankNameShort { get; set; }
        public string ChiNhanh { get; set; }

    }

    public class ResultThanhToanDetail
    {
        public List<ThanhVienNganHangs> items { get; set; }
    }
    public class RootThanhToanDetail
    {
        public ResultThanhToanDetail result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }


    public class RootThanhToan
    {
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }



    public class ResultThanhToanAdmin
    {
        public List<obj_ThanhVienNganHang> items { get; set; }
    }
    public class RootThanhToanAdmin
    {
        public ResultThanhToanAdmin result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    #region RutTien
    public class obj_RutTienNganHangAdd
    {
        public int IDThanhVien { get; set; }
        public double TongTienThanhToan { get; set; }
        public string HoVaTenTK { get; set; }
        public string TenNganHang { get; set; }
        public string SoTaiKhoan { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }
    }
    public class obj_RutTienNganHang
    {
        public double TongTienThanhToan { get; set; }
        public string HoVaTenTK { get; set; }
        public string TenNganHang { get; set; }
        public string SoTaiKhoan { get; set; }
    }
    public class ResultRutTien
    {
        public List<obj_RutTienNganHang> items { get; set; }
    }
    public class RootTVRutTien
    {
        public ResultRutTien result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    #endregion



    #region RutTienView
    public class obj_RutTienNganHangView
    {
        public int ID { get; set; }
        public string TongTienThanhToan { get; set; }
        public string HoVaTenTK { get; set; }
        public string TenNganHang { get; set; }
        public string SoTaiKhoan { get; set; }
    }
    public class ResultRutTienView
    {
        public List<obj_RutTienNganHangView> items { get; set; }
    }
    public class RootTVRutTienView
    {
        public ResultRutTienView result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public string Message { get; set; }
        public string MaGiaoDich { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    #endregion


    #region TongCacVi
    public class ViTien
    {
        public int IDTaiKhoan { get; set; }
        public double TongTien { get; set; }
        public double TongTienTietKiem { get; set; }
    }

    public class ResultViTien
    {
        public List<ViTien> items { get; set; }
    }
    public class RootThanhViTien
    {
        public ResultViTien result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }
    #endregion

    public class ChuyenVi
    {
        public double SoTienThanhToan { get; set; }
    }
    public class RootChuyenVi
    {
        public string Message { get; set; }
        public ChuyenVi result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    public class obj_ThanhToanQRCode
    {
        public double SoTienThanhToan { get; set; }
        public int IDNguoiNhan { get; set; }
        public string NoiDung { get; set; }
        public string UserDisplayName { get; set; }
    }
    public class ThanhToanQRCode
    {
        public int IDNguoiNhan { get; set; }
        public int IDNguoiThanhToan { get; set; }
        public string NoiDung { get; set; }
        public string UserDisplayName { get; set; }
        public double SoTienThanhToan { get; set; }
        public string MaGiaoDich { get; set; }
    }
    public class AddThanhToanQRCode
    {
        public int IDNguoiNhan { get; set; }
        public int IDNguoiThanhToan { get; set; }
        public string NoiDung { get; set; }
        public string UserDisplayName { get; set; }
        public double SoTienThanhToan { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class RootQRcode
    {
        public List<ThanhToanQRCode> result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public string Message { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }
}