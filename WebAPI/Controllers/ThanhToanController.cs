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
    [RoutePrefix("api/ThanhToan")]
    public class ThanhToanController : ApiController
    {
        DatalinqDataContext db = new DatalinqDataContext();

        [Authorize]
        [HttpPost]
        [Route("SaveBankAccount")]
        public async Task<IHttpActionResult> SaveBankAccount(obj_ThanhVienNganHang obj)
        {
            RootThanhToan it = new RootThanhToan();
            if (obj.HoVaTen == "string" || obj.ChiNhanh == "string" || obj.TenNganHang == "string" || obj.BankNameShort == "string" || obj.SoTaiKhoan == "string")
            {
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }

            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {

                var item = ThanhToanDataAccess.SaveBankAccount(obj, list[0].ID.ToString());
                if (item)
                {
                    it.success = true;
                    it.error = null;
                    it.unAuthorizedRequest = false;
                }
                else
                {
                    it.success = false;
                    it.error = true;
                    it.unAuthorizedRequest = false;
                }
            }
            else
            {
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }

        [Authorize]
        [HttpGet]
        [Route("ListBankAccount")]
        public async Task<IHttpActionResult> ListBankAccount()
        {
            RootThanhToanAdmin it = new RootThanhToanAdmin();
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var item = ThanhToanDataAccess.GETBYID(list[0].ID.ToString());
                if (item.Count > 0)
                {
                    ResultThanhToanAdmin its = new ResultThanhToanAdmin()
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
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }

        [Authorize]
        [HttpGet]
        [Route("ListCompanyBankAccount")]
        public async Task<IHttpActionResult> ListCompanyBankAccount()
        {
            RootThanhToanAdmin it = new RootThanhToanAdmin();
            var item = ThanhToanDataAccess.ThanhVienNganHang_ADMIN_Detail();
            if (item.Count > 0)
            {
                ResultThanhToanAdmin its = new ResultThanhToanAdmin()
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
        [HttpPost]
        [Route("TransferToBankAccount")]
        public async Task<IHttpActionResult> TransferToBankAccount(obj_RutTienNganHang obj)
        {

            RootTVRutTienView it = new RootTVRutTienView();
            if (obj.TongTienThanhToan == 0 || obj.TenNganHang == "string" || obj.SoTaiKhoan == "string")
            {
                it.Message = "Vui lòng nhập đầy đủ thông tin";
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }

            if (obj.TongTienThanhToan == 0)
            {
                it.Message = "Vui lòng nhập số tiền lớn hơn số 0";
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }

            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var IDThanhVien = list[0].ID;
                var TongTienTT = obj.TongTienThanhToan.ToString().Replace(",", "");
                #region RutTienNganHang
                obj_RutTienNganHangAdd dt = new obj_RutTienNganHangAdd();
                dt.IDThanhVien = IDThanhVien;
                dt.TongTienThanhToan = Convert.ToDouble(TongTienTT);
                dt.HoVaTenTK = obj.HoVaTenTK;
                dt.TenNganHang = obj.TenNganHang;
                dt.SoTaiKhoan = obj.SoTaiKhoan;
                dt.NgayTao = DateTime.Now;
                dt.TrangThai = 1;
                #endregion

                #region Trừ tiền ở thành viên TongTien
                double SoCoin = Convert.ToDouble(TongTienTT.ToString());
                {
                    double TongTienHienTai = Convert.ToDouble(list[0].TongTien);
                    double TongTienRut = Convert.ToDouble(TongTienTT.ToString());
                    if (TongTienHienTai < TongTienRut)
                    {
                        it.Message = "Số tiền không đủ để rút";
                        it.MaGiaoDich = "";
                        it.success = false;
                        it.error = false;
                        it.unAuthorizedRequest = false;
                        return Ok(it);
                    }
                    double Conglai = 0;
                    Conglai = ((TongTienHienTai) - (TongTienRut));
                    SMember.Name_Text("update Members set TongTien=" + Conglai.ToString() + " where id=" + IDThanhVien.ToString() + "");
                }
                #endregion

                int RutTienID = ThanhToanDataAccess.Insert_RutTien_NganHang(dt);
                if (RutTienID != 0)
                {
                    var item = ThanhToanDataAccess.RutTien_GetDetail(RutTienID.ToString());
                    if (item.Count > 0)
                    {
                        #region ThongBaoRutTien
                        List<CHThongBao> dts = ThongBaoDataAccess.GETBYID("2");
                        {
                            string Chuoi = dts[0].Contents;

                            Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(IDThanhVien.ToString()) + "</b>");
                            Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(TongTienTT.ToString()) + "</b>");

                            // Push thông báo vào cho thành viên
                            ThongBaoThanhVien tb = new ThongBaoThanhVien();
                            tb.Title = dts[0].Title;
                            tb.Brief = dts[0].Brief;
                            tb.Contents = Chuoi;
                            tb.Images = dts[0].Images;
                            tb.Create_Date = DateTime.Now;
                            tb.Views = 0;
                            tb.Status = 1;
                            tb.LoaiThongBao = "RutTien";
                            db.ThongBaoThanhViens.InsertOnSubmit(tb);
                            db.SubmitChanges();

                            ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                            string IDTB = tbn.ID.ToString();
                            SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                        }
                        #endregion

                        #region LichSuRutTienNganHang
                        LichSuRutTienNganHang tvb = new LichSuRutTienNganHang();
                        tvb.IDThanhVien = IDThanhVien;
                        tvb.SoTienRut = obj.TongTienThanhToan.ToString().Replace(",", "");
                        tvb.HoVaTenTK = obj.HoVaTenTK;
                        tvb.TenNganHang = obj.TenNganHang;
                        tvb.SoTaiKhoan = obj.SoTaiKhoan;
                        tvb.ChiNhanh = "";
                        tvb.Type = "VNPAY";
                        tvb.NgayTao = DateTime.Now;
                        tvb.NoiDung = "";
                        tvb.NgayDuyet = DateTime.Now;
                        tvb.NguoiDuyet = "";
                        tvb.TrangThai = 1;
                        db.LichSuRutTienNganHangs.InsertOnSubmit(tvb);
                        db.SubmitChanges();
                        LichSuRutTienNganHang IDrtien = db.LichSuRutTienNganHangs.OrderByDescending(s => s.ID).FirstOrDefault();
                        string IDRT = IDrtien.ID.ToString();
                        #endregion

                        // ok thanh toán ngân hàng vnpay
                        ResultRutTienView its = new ResultRutTienView()
                        {
                            items = item
                        };
                        it.Message = "Rút tiền thành công";
                        it.MaGiaoDich = "#TT" + IDRT;
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
            }
            else
            {

                it.MaGiaoDich = "";
                it.Message = "Không tìm thấy thành viên";
                it.result = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }

        [Authorize]
        [HttpGet]
        [Route("ListWallets")]
        public async Task<IHttpActionResult> ListWallets()
        {
            var userId = User.Identity.GetUserId();
            RootThanhViTien it = new RootThanhViTien();
            List<WebAPI.Models.Member> item = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (item.Count > 0)
            {
                var vii = new List<ViTien>();
                vii.Add(new ViTien()
                {
                    IDTaiKhoan = Convert.ToInt32(item[0].ID),
                    TongTien = Convert.ToDouble(item[0].TongTien),
                    TongTienTietKiem = Convert.ToDouble(item[0].TongTienTietKiem),
                });
                ResultViTien its = new ResultViTien()
                {
                    items = vii
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
        [Route("TransferToMainAccount")]
        public async Task<IHttpActionResult> TransferToMainAccount(decimal soTienThanhToan)
        {
            var userId = User.Identity.GetUserId();
            RootChuyenVi it = new RootChuyenVi();
            List<WebAPI.Models.Member> item = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (item.Count > 0)
            {
                double SoTienThanhToan = Convert.ToDouble(soTienThanhToan);
                double TongTienTietKiem = Convert.ToDouble(item[0].TongTienTietKiem);
                if (TongTienTietKiem >= SoTienThanhToan)
                {
                    #region Trừ ví Tiết Kiệm
                    double TongTienTietKiems = 0;
                    TongTienTietKiems = ((TongTienTietKiem) - (SoTienThanhToan));
                    SMember.Name_Text("update Members set TongTienTietKiem=" + TongTienTietKiems.ToString() + " where ID=" + item[0].ID.ToString() + "");
                    #endregion

                    #region Cộng Ví Chính
                    double TongTien = Convert.ToDouble(item[0].TongTien);
                    double SumTongTien = 0;
                    SumTongTien = ((TongTien) + (SoTienThanhToan));
                    SMember.Name_Text("update Members set TongTien=" + SumTongTien.ToString() + " where ID=" + item[0].ID.ToString() + "");

                    #endregion

                    // Thông báo 

                    #region ThongBaoChuyển tiền
                    List<CHThongBao> dts = ThongBaoDataAccess.GETBYID("3");
                    {
                        var IDThanhVien = item[0].ID.ToString();
                        string Chuoi = dts[0].Contents;
                        Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(IDThanhVien.ToString()) + "</b>");
                        Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(soTienThanhToan.ToString()) + "</b>");

                        // Push thông báo vào cho thành viên
                        ThongBaoThanhVien tb = new ThongBaoThanhVien();
                        tb.Title = dts[0].Title;
                        tb.Brief = dts[0].Brief;
                        tb.Contents = Chuoi;
                        tb.Images = dts[0].Images;
                        tb.Create_Date = DateTime.Now;
                        tb.Views = 0;
                        tb.Status = 1;
                        tb.LoaiThongBao = "ChuyenViTietKiem";
                        db.ThongBaoThanhViens.InsertOnSubmit(tb);
                        db.SubmitChanges();

                        ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                        string IDTB = tbn.ID.ToString();
                        SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                    }
                    #endregion
                }
                else
                {
                    it.Message = "Số tiền ví tiết kiệm không đủ để chuyển sang ví chính";
                    it.result = null;
                    it.success = false;
                    it.error = true;
                    it.unAuthorizedRequest = false;
                    return Ok(it);
                }

                ChuyenVi its = new ChuyenVi()
                {
                    SoTienThanhToan = Convert.ToDouble(soTienThanhToan)
                };
                // Tổng tiền (TongTien)= Tiền gốc nạp vào + tiền lãi + QRcode
                // Tổng tiền gốc (SoTienGoc) = Tiền nạp vào  + QRcode
                // Số tiền còn lại(SoTienConLai) =  Tiền gốc nạp vào + tiền lãi + QRcode - rút - chuyển tiền QRcode - chuyển sang ví tiết kiệm
                it.Message = "Chuyển tiền trong ví tiết kiệm sang ví chính thành công.";
                it.result = its;
                it.success = true;
                it.error = null;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.Message = "Không tìm thấy thành viên";
                it.result = null;
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }

        [Authorize]
        [HttpPost]
        [Route("SavePayment")]
        public async Task<IHttpActionResult> SavePayment(obj_ThanhToanQRCode obj)
        {
            RootQRcode it = new RootQRcode();
            if (obj.SoTienThanhToan == 0 || obj.IDNguoiNhan == 0 || obj.NoiDung == "string" || obj.UserDisplayName == "string")
            {
                it.result = null;
                it.Message = "Vui lòng nhập đầy đủ thông tin";
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }
            if (obj.SoTienThanhToan == 0)
            {
                it.result = null;
                it.Message = "Vui lòng nhập số tiền lớn hơn số 0";
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }

            List<WebAPI.Models.Member> listn = SMember.Name_Text("select * from Members  where ID=" + obj.IDNguoiNhan + " and TrangThai=1 ");
            if (listn.Count <= 0)
            {
                it.result = null;
                it.Message = "Không tìm thấy người nhận";
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }
            var userId = User.Identity.GetUserId();
            List<WebAPI.Models.Member> list = SMember.Name_Text("select * from Members  where  IDThanhVien=" + userId + " and TrangThai=1 ");
            if (list.Count > 0)
            {
                var IDThanhVien = list[0].ID;
                var TongTienTT = obj.SoTienThanhToan.ToString().Replace(",", "");
                #region RutTienNganHang
                AddThanhToanQRCode dt = new AddThanhToanQRCode();
                dt.IDNguoiThanhToan = IDThanhVien;
                dt.IDNguoiNhan = obj.IDNguoiNhan;
                dt.SoTienThanhToan = Convert.ToDouble(TongTienTT);
                dt.NoiDung = obj.NoiDung;
                dt.UserDisplayName = obj.UserDisplayName;
                dt.NgayTao = DateTime.Now;
                #endregion

                int ThanhToanID = ThanhToanDataAccess.Insert_QRCode(dt);
                if (ThanhToanID != 0)
                {
                    #region Trừ tiền ở thành viên TongTien
                    double SoCoin = Convert.ToDouble(TongTienTT.ToString());
                    {
                        double TongTienHienTai = Convert.ToDouble(list[0].TongTien);
                        double TongTienRut = Convert.ToDouble(TongTienTT.ToString());
                        if (TongTienHienTai < TongTienRut)
                        {
                            it.Message = "Số tiền thanh toán QrCode không đủ";
                            it.success = false;
                            it.error = false;
                            it.unAuthorizedRequest = false;
                            return Ok(it);
                        }
                        double Conglai = 0;
                        Conglai = ((TongTienHienTai) - (TongTienRut));
                        SMember.Name_Text("update Members set TongTien=" + Conglai.ToString() + " where id=" + IDThanhVien.ToString() + "");
                    }
                    #endregion

                    #region Cộng tiền Cho cửa hàng nhận
                    List<WebAPI.Models.Member> ngnhan = SMember.Name_Text("select * from Members  where ID=" + obj.IDNguoiNhan + " and TrangThai=1 ");
                    if (ngnhan.Count > 0)
                    {
                        double TongTT = Convert.ToDouble(TongTienTT.ToString());
                        {
                            double TongTienHienTai = Convert.ToDouble(ngnhan[0].TongTien);
                            double TongTienNhan = Convert.ToDouble(TongTienTT.ToString());
                            double Conglai = 0;
                            Conglai = ((TongTienHienTai) + (TongTienNhan));
                            SMember.Name_Text("update Members set TongTien=" + Conglai.ToString() + " where id=" + obj.IDNguoiNhan.ToString() + "");
                        }
                    }
                    #endregion

                    #region Thông báo thanh toán qua QRCode
                    List<CHThongBao> dts = ThongBaoDataAccess.GETBYID("7");
                    {
                        string Chuoi = dts[0].Contents;
                        Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(IDThanhVien.ToString()) + "</b>");
                        Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(TongTienTT.ToString()) + "</b>");

                        // Push thông báo vào cho thành viên
                        ThongBaoThanhVien tb = new ThongBaoThanhVien();
                        tb.Title = dts[0].Title;
                        tb.Brief = dts[0].Brief;
                        tb.Contents = Chuoi;
                        tb.Images = dts[0].Images;
                        tb.Create_Date = DateTime.Now;
                        tb.Views = 0;
                        tb.Status = 1;
                        tb.LoaiThongBao = "ThanhToanQRCode";
                        db.ThongBaoThanhViens.InsertOnSubmit(tb);
                        db.SubmitChanges();

                        ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                        string IDTB = tbn.ID.ToString();
                        SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                    }
                    #endregion

                    #region Thông báo Nhận tiền qua QRCode
                    List<CHThongBao> dts3 = ThongBaoDataAccess.GETBYID("8");
                    {
                        string Chuoi = dts3[0].Contents;
                        Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(IDThanhVien.ToString()) + "</b>");
                        Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(TongTienTT.ToString()) + "</b>");

                        // Push thông báo vào cho thành viên
                        ThongBaoThanhVien tb = new ThongBaoThanhVien();
                        tb.Title = dts3[0].Title;
                        tb.Brief = dts3[0].Brief;
                        tb.Contents = Chuoi;
                        tb.Images = dts3[0].Images;
                        tb.Create_Date = DateTime.Now;
                        tb.Views = 0;
                        tb.Status = 1;
                        tb.LoaiThongBao = "NhanTienQRCode";
                        db.ThongBaoThanhViens.InsertOnSubmit(tb);
                        db.SubmitChanges();

                        ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                        string IDTB = tbn.ID.ToString();
                        SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + obj.IDNguoiNhan + "," + IDTB + ",0) ");
                    }
                    #endregion
                    // ok thanh toán qrcode

                    // Tặng điểm theo cấu hình khi thanh toán bằng QRCode
                    Double PhanTramTichDiem = Convert.ToDouble(Commond.Setting("TichDiem"));
                    if (PhanTramTichDiem > 0)
                    {
                        double SoTienHuong = (Convert.ToDouble(TongTienTT) * PhanTramTichDiem) / 100;
                        List<WebAPI.Models.Member> Tichdiem = SMember.Name_Text("select * from Members  where ID=" + IDThanhVien + " and TrangThai=1 ");
                        if (Tichdiem.Count > 0)
                        {
                            HoaHongThanhVien obtt = new HoaHongThanhVien();
                            obtt.IDThanhVien = Convert.ToInt32(IDThanhVien);
                            obtt.IDThanhVienHuong = Convert.ToInt32(IDThanhVien.ToString());
                            obtt.LoaiHoaHong = "TangDiemKhiThanhToanQRCode";
                            obtt.PhanTramHoaHong = Convert.ToInt32(PhanTramTichDiem);
                            obtt.SoTienHuong = SoTienHuong.ToString();
                            obtt.SoTienDauTu = TongTienTT.ToString().Replace(",", "").Replace(".", "");
                            obtt.NgayTao = DateTime.Now;
                            db.HoaHongThanhViens.InsertOnSubmit(obtt);
                            db.SubmitChanges();

                            #region Cộng tiền Cho ng thanh toán QRCode
                            double TongTienHienTai = Convert.ToDouble(Tichdiem[0].TongTien);
                            double TongTienNhan = Convert.ToDouble(SoTienHuong.ToString());
                            double Conglai = 0;
                            Conglai = ((TongTienHienTai) + (SoTienHuong));
                            SMember.Name_Text("update Members set TongTien=" + Conglai.ToString() + " where id=" + IDThanhVien.ToString() + "");
                            #endregion
                        }
                        #region Thông báo Tặng điểm khi thanh toán QRCode
                        List<CHThongBao> dts4 = ThongBaoDataAccess.GETBYID("9");
                        {
                            string Chuoi = dts4[0].Contents;
                            Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(IDThanhVien.ToString()) + "</b>");
                            Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(SoTienHuong.ToString()) + "</b>");

                            // Push thông báo vào cho thành viên
                            ThongBaoThanhVien tb = new ThongBaoThanhVien();
                            tb.Title = dts4[0].Title;
                            tb.Brief = dts4[0].Brief;
                            tb.Contents = Chuoi;
                            tb.Images = dts4[0].Images;
                            tb.Create_Date = DateTime.Now;
                            tb.Views = 0;
                            tb.Status = 1;
                            tb.LoaiThongBao = "TangDiemKhiThanhToanQRCode";
                            db.ThongBaoThanhViens.InsertOnSubmit(tb);
                            db.SubmitChanges();

                            ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                            string IDTB = tbn.ID.ToString();
                            SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                        }
                        #endregion
                    }

                    var vii = new List<ThanhToanQRCode>();
                    vii.Add(new ThanhToanQRCode()
                    {
                        IDNguoiNhan = obj.IDNguoiNhan,
                        IDNguoiThanhToan = IDThanhVien,
                        NoiDung = obj.NoiDung,
                        UserDisplayName = obj.UserDisplayName,
                        SoTienThanhToan = obj.SoTienThanhToan,
                        MaGiaoDich = "#QRCode" + ThanhToanID,
                    });
                    it.result = vii;
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

    }
}
