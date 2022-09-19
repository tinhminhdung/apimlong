using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{

    public class obj_DauTuTaiChinh
    {
        public double SoTienDauTu { get; set; }
        public int IDGoiDauTu { get; set; }
        public int IdMucDauTu { get; set; }
        public int IdKyHan { get; set; }
    }
    public class RootDauTuTaiChinh
    {
        public string MaGiaoDich { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public string Message { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }


    public class KyHanList
    {
        public int ID { get; set; }
        public string KyHan { get; set; }
        public string TyLe { get; set; }
        public string TienLaiHangThang { get; set; }
        public string PhanTramCTV { get; set; }
        public string TongTienLai { get; set; }
        public string LoaiKyHan { get; set; }
    }

    public class MucDauTuList
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string SoTien { get; set; }
        public List<KyHanList> KyHanList { get; set; }
    }

    public class API_TaiChinh
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string ImageUrl { get; set; }
        public DateTime NgayTao { get; set; }
        public string TenMucDauTu { get; set; }

        public string QuyenLoi;
        public List<MucDauTuList> MucDauTuList { get; set; }
    }

    public class ResultTaiChinh
    {
        public List<API_TaiChinh> items { get; set; }
    }

    public class RootTaiChinh
    {
        public ResultTaiChinh result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
    }

    public class Obj_combox
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

}