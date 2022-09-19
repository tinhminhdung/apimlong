using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VS.ECommerce_MVC.Models;
using System.Collections.Generic;
using System;

namespace VS.ECommerce_MVC.Api
{
    [RoutePrefix("api/TaiChinh")]
    public class TaiChinhController : ApiController
    {
        DatalinqDataContext db = new DatalinqDataContext();

        [Authorize]
        [HttpGet]
        [Route("ListPackagesActive")]///{SkipCount}/{MaxResultCount}
        //Ds tất cả các tin tức
        public async Task<IHttpActionResult> ListPackagesActive()
        {
            RootTaiChinh it = new RootTaiChinh();
            var item = TaiChinhDataAccess.ListPackagesActive();
            if (item.Count > 0)
            {
                var TaiChinh = new List<API_TaiChinh>();
                foreach (var itett in item)
                {
                    var item3 = TaiChinhDataAccess.Get_MucDauTuList(itett.ID.ToString());
                    var Dautu = new List<MucDauTuList>();
                    foreach (var item3s in item3)
                    {
                        var item4 = TaiChinhDataAccess.Get_MucKyHan(item3s.ID.ToString());
                        var iKyHan = new List<KyHanList>();
                        foreach (var itemf in item4)
                        {
                            iKyHan.Add(new KyHanList()
                            {
                                ID = itemf.ID,
                                TyLe = itemf.TyLe,
                                KyHan = itemf.KyHan,
                                TienLaiHangThang = itemf.TienLaiHangThang,
                                PhanTramCTV = itemf.PhanTramCTV,
                                TongTienLai = itemf.TongTienLai,
                                LoaiKyHan = itemf.LoaiKyHan,
                            });
                        }

                        Dautu.Add(new MucDauTuList()
                        {
                            ID = item3s.ID,
                            TieuDe = item3s.TieuDe,
                            SoTien = item3s.SoTien,
                            KyHanList = iKyHan,
                        });
                    }
                    TaiChinh.Add(new API_TaiChinh()
                    {
                        ID = itett.ID,
                        TieuDe = itett.TieuDe,
                        MoTa = itett.MoTa,
                        ImageUrl = itett.ImageUrl,
                        NgayTao = itett.NgayTao,
                        TenMucDauTu = itett.TenMucDauTu,
                        QuyenLoi = itett.QuyenLoi,
                        MucDauTuList = Dautu,
                    });
                }
                ResultTaiChinh its = new ResultTaiChinh()
                {
                    items = TaiChinh
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
        [Route("SaveContract")]
        public async Task<IHttpActionResult> SaveContract(obj_DauTuTaiChinh obj)
        {
            RootDauTuTaiChinh it = new RootDauTuTaiChinh();
            if (!ModelState.IsValid)
            {
                it.Message = "Vui lòng nhập đầy đủ thông tin";
                it.MaGiaoDich = "";
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }

            if (obj.SoTienDauTu == 0 || obj.IDGoiDauTu == 0 || obj.IdKyHan == 0 || obj.IdMucDauTu == 0)
            {
                it.Message = "Vui lòng nhập đầy đủ thông tin";
                it.success = false;
                it.error = false;
                it.unAuthorizedRequest = false;
                return Ok(it);
            }

            if (obj.SoTienDauTu == 0)
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
                var IDThanhVien = list[0].ID.ToString();
                double SoTienDauTu = Convert.ToDouble(obj.SoTienDauTu.ToString().Replace(",", "").Replace(".", ""));
                // chuyển tiền từ ví chính sang ví tiết kiệm
                #region Trừ Ví Chính số tiền còn lại
                double TongTien = Convert.ToDouble(list[0].TongTien);
                if ( TongTien>= SoTienDauTu)
                {
                    double SumSoTien = 0;
                    SumSoTien = ((TongTien) - (SoTienDauTu));
                    SMember.Name_Text("update Members set TongTien=" + SumSoTien.ToString() + " where ID=" + IDThanhVien.ToString() + "");
                }
                else
                {
                    it.Message = "Số tiền trong ví không đủ để đầu tư";
                    it.MaGiaoDich = "";
                    it.success = false;
                    it.error = true;
                    it.unAuthorizedRequest = false;
                    return Ok(it);
                }
                #endregion

                #region Cộng sang ví Tiết Kiệm
                double TongTienTietKiem = Convert.ToDouble(list[0].TongTienTietKiem);

                double SumTongTienTietKiem = 0;
                SumTongTienTietKiem = ((TongTienTietKiem) + (SoTienDauTu));

                SMember.Name_Text("update Members set TongTienTietKiem=" + SumTongTienTietKiem.ToString() + " where ID=" + IDThanhVien.ToString() + "");
                #endregion

                // Thông báo 
                #region ThongBaoChuyển tiền
                List<CHThongBao> dts = ThongBaoDataAccess.GETBYID("4");
                {
                    string Chuoi = dts[0].Contents;

                    Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(IDThanhVien.ToString()) + "</b>");
                    Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(SoTienDauTu.ToString()) + "</b>");
                    // Push thông báo vào cho thành viên
                    ThongBaoThanhVien tb = new ThongBaoThanhVien();
                    tb.Title = dts[0].Title;
                    tb.Brief = dts[0].Brief;
                    tb.Contents = Chuoi;
                    tb.Images = dts[0].Images;
                    tb.Create_Date = DateTime.Now;
                    tb.Views = 0;
                    tb.Status = 1;
                    tb.LoaiThongBao = "DauTuTaiChinh";
                    db.ThongBaoThanhViens.InsertOnSubmit(tb);
                    db.SubmitChanges();

                    ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                    string IDTB = tbn.ID.ToString();
                    SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                }
                #endregion

                #region LaiSuatThanhVien
                // Thêm vào bảng đầu tư LaiSuatThanhVien
                // 1: sau 1 tháng sẽ chạy tự động trả lãi vào bảng LaiSuatThanhVien_LaiSuatThang
                // 2:  +1 % xiên suốt của B khi lãi suất hàng tháng của B được nhận thì A cũng được nhận 1 % giá trị đầu tư
                //   Ghi chú: (Tháng đầu tiền thì luôn được, Tháng thứ 2 trở đi phải giới thiệu ra 1 F1 hoặc tham gia gói đầu tư khác trị giá 10 triệu) 
                //Ví dụ: B vào gói 10 triệu thì A dc 1 %= 100 K
                // 3: + 2 % Trực tiếp-- > Khi A giới thiệu ra B mà B vào gói hợp đồng thì A sẽ nhận dc 2 % của giá trị gói HD đó

                #region LaiSuatThanhVien
                LaiSuatThanhVien obk = new LaiSuatThanhVien();
                obk.IDThanhVien = Convert.ToInt32(IDThanhVien);
                obk.IDTaiChinh = obj.IDGoiDauTu;
                obk.IDMucDauTu = obj.IdMucDauTu;
                obk.IDKyHan = obj.IdKyHan;
                obk.SoTienThamGia = obj.SoTienDauTu.ToString().Replace(",", "").Replace(".", "");
                var item1 = TaiChinhDataAccess.GetKyHan(obj.IdKyHan.ToString());
                if (item1 != null)
                {
                    obk.SoTienLaiThang = item1[0].Code;
                    obk.KyHan = Convert.ToInt32(item1[0].Name);
                }
                else
                {
                    obk.SoTienLaiThang = "0";
                    obk.KyHan = 0;
                }
                obk.ThoiGianThamGia = DateTime.Now;
                obk.NgayChay = DateTime.Now.AddDays(30);
                obk.TrangThaiChay = 1;
                obk.DemLanChay = 0;
                db.LaiSuatThanhViens.InsertOnSubmit(obk);
                db.SubmitChanges();
                #endregion

                #region Chia hoa hồng
                // Cho A 2% trực tiếp
                List<WebAPI.Models.Member> listTrucTiep = SMember.Name_Text("select * from Members  where ID=" + list[0].GioiThieu.ToString() + " ");
                if (listTrucTiep.Count > 0)
                {
                    // Cộng tiền cho thành viên trực tiếp
                    // Lưu lịch sử
                    Double HoaHong = Convert.ToDouble(Commond.Setting("PhanTramTrucTiep"));
                    if (HoaHong > 0)
                    {
                        double SoTienHuongs = (SoTienDauTu * HoaHong) / 100;
                        HoaHongThanhVien obtt = new HoaHongThanhVien();
                        obtt.IDThanhVien = Convert.ToInt32(IDThanhVien);
                        obtt.IDThanhVienHuong = Convert.ToInt32(listTrucTiep[0].ID.ToString());
                        obtt.LoaiHoaHong = "TrucTiep";
                        obtt.PhanTramHoaHong = Convert.ToInt32(HoaHong);
                        obtt.SoTienHuong = SoTienHuongs.ToString();
                        obtt.SoTienDauTu = obj.SoTienDauTu.ToString().Replace(",", "").Replace(".", "");
                        obtt.NgayTao = DateTime.Now;

                        db.HoaHongThanhViens.InsertOnSubmit(obtt);
                        db.SubmitChanges();

                        #region  Thông báo Cho trực tiếp nhận 2%
                        List<CHThongBao> dtsTT = ThongBaoDataAccess.GETBYID("5");
                        {
                            string Chuoi = dtsTT[0].Contents;

                            Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(listTrucTiep[0].ID.ToString()) + "</b>");
                            Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(SoTienHuongs.ToString()) + "</b>");

                            // Push thông báo vào cho thành viên
                            ThongBaoThanhVien tb = new ThongBaoThanhVien();
                            tb.Title = dtsTT[0].Title;
                            tb.Brief = dtsTT[0].Brief;
                            tb.Contents = Chuoi;
                            tb.Images = dtsTT[0].Images;
                            tb.Create_Date = DateTime.Now;
                            tb.Views = 0;
                            tb.Status = 1;
                            tb.LoaiThongBao = "TrucTiep";
                            db.ThongBaoThanhViens.InsertOnSubmit(tb);
                            db.SubmitChanges();

                            ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                            string IDTB = tbn.ID.ToString();
                            SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                        }
                        #endregion

                        Commond.CongTien(listTrucTiep[0].ID.ToString(), SoTienHuongs.ToString());
                    }

                    // Cho  B  thêm tiền 1 % xiên suốt
                    // Cộng tiền cho thành viên trực tiếp
                    // Lưu lịch sử
                    Double PhanTramXienSuot = Convert.ToDouble(Commond.Setting("PhanTramXienSuot"));
                    if (PhanTramXienSuot > 0)
                    {
                        double SoTienHuongs = (SoTienDauTu * PhanTramXienSuot) / 100;
                        HoaHongThanhVien obtt = new HoaHongThanhVien();
                        obtt.IDThanhVien = Convert.ToInt32(IDThanhVien);
                        obtt.IDThanhVienHuong = Convert.ToInt32(listTrucTiep[0].ID.ToString());
                        obtt.LoaiHoaHong = "XienSuot";
                        obtt.PhanTramHoaHong = Convert.ToInt32(PhanTramXienSuot);
                        obtt.SoTienHuong = SoTienHuongs.ToString();
                        obtt.SoTienDauTu = obj.SoTienDauTu.ToString().Replace(",", "").Replace(".", "");
                        obtt.NgayTao = DateTime.Now;
                        db.HoaHongThanhViens.InsertOnSubmit(obtt);
                        db.SubmitChanges();

                        #region  Thông báo Cho B 1% xiên suốt
                        List<CHThongBao> dtsTT = ThongBaoDataAccess.GETBYID("6");
                        {
                            string Chuoi = dtsTT[0].Contents;
                            Chuoi = Chuoi.Replace("[NAME]", "<b>" + Commond.Show_Ten_ThanhVien(listTrucTiep[0].ID.ToString()) + "</b>");
                            Chuoi = Chuoi.Replace("[TongTien]", "<b>" + Commond.Fomart_Price(SoTienHuongs.ToString()) + "</b>");

                            // Push thông báo vào cho thành viên
                            ThongBaoThanhVien tb = new ThongBaoThanhVien();
                            tb.Title = dtsTT[0].Title;
                            tb.Brief = dtsTT[0].Brief;
                            tb.Contents = Chuoi;
                            tb.Images = dtsTT[0].Images;
                            tb.Create_Date = DateTime.Now;
                            tb.Views = 0;
                            tb.Status = 1;
                            tb.LoaiThongBao = "XienSuot";
                            db.ThongBaoThanhViens.InsertOnSubmit(tb);
                            db.SubmitChanges();

                            ThongBaoThanhVien tbn = db.ThongBaoThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                            string IDTB = tbn.ID.ToString();
                            SMember.Name_Text("INSERT INTO ThongBaoTV ( IDThanhVien, IDThongBao, [Status]) values (" + IDThanhVien + "," + IDTB + ",0) ");
                        }

                        Commond.CongTien(listTrucTiep[0].ID.ToString(), SoTienHuongs.ToString());
                        #endregion
                    }
                }
                #endregion

                #endregion
                LaiSuatThanhVien tbnb = db.LaiSuatThanhViens.OrderByDescending(s => s.ID).FirstOrDefault();
                it.MaGiaoDich = "#GDT" + tbnb.ID;
                it.Message = "Thêm mới thành công";
                it.success = true;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            else
            {
                it.MaGiaoDich = "";
                it.Message = "Không tìm thấy thành viên";
                it.success = false;
                it.error = true;
                it.unAuthorizedRequest = false;
            }
            return Ok(it);
        }

    }
}
