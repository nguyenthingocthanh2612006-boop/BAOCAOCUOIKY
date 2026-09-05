using BAOCAOCUOIKY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAOCAOCUOIKY
{
    public partial class FrmThanhToan : Form
    {
        private string noiDi;
        private string noiDen;
        private string gioDi;
        private string gioDen;
        private DateTime ngayDi;
        private string bienSo;
        private string ghe;
        private int soLuong;
        private decimal giaVe;
        private decimal tongTien;
            public FrmThanhToan(
                string noiDi,
                string noiDen,
                string gioDi,
                string gioDen,
                DateTime ngayDi,
                string bienSo,
                string ghe,
                int soLuong,
                decimal giaVe,
                decimal tongTien)
        {
            InitializeComponent();

            this.noiDi = noiDi;
            this.noiDen = noiDen;
            this.gioDi = gioDi;
            this.gioDen = gioDen;
            this.ngayDi = ngayDi;
            this.bienSo = bienSo;
            this.ghe = ghe;
            this.soLuong = soLuong;
            this.giaVe = giaVe;
            this.tongTien = tongTien;

            HienThiThongTin();
        }
        private void HienThiThongTin()
        {
            lblTuyen7.Text = noiDi + " → " + noiDen;
            lblChuyen1.Text = gioDi + " → " + gioDen;
            lblXe4.Text = "29 chỗ";
            lblBienSo1.Text = bienSo;
            lblNgayDi2.Text = ngayDi.ToString("dd-MM-yyyy");
            lblGheDaChon1.Text = ghe;

            // Giá 1 vé
            lblGia5.Text = giaVe.ToString("N0") + "đ";

            // Số lượng vé
            lblSoLuong3.Text = soLuong + " vé";

            // Tạm tính
            decimal tamTinh = giaVe * soLuong;

            // Phí dịch vụ = Tổng tiền - Tạm tính
            decimal phiDichVu = tongTien - tamTinh;

            lblTien.Text = tamTinh.ToString("N0") + "đ";
            lblPhiDichVu.Text = phiDichVu.ToString("N0") + "đ";
            lblTongTien.Text = tongTien.ToString("N0") + "đ";
        }
        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            // Lấy thông tin khách hàng đang đăng nhập
            using (var db = new QuanLyVeXeModel())
            {
                string tenDangNhap = ThongTinDangNhap.TenDangNhap;

                var taiKhoan = db.TaiKhoans
                    .FirstOrDefault(x => x.TenDangNhap == tenDangNhap);

                if (taiKhoan != null)
                {
                    txtDangNhap.Text = taiKhoan.TenDangNhap;
                    txtSoDienThoai.Text = taiKhoan.SoDienThoai;
                    txtEmail.Text = taiKhoan.Email;
                }
            }
        }

        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            // Chưa chọn phương thức
            if (!rdoTienMat.Checked && !rdoChuyenKhoan.Checked)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Chọn tiền mặt
            if (rdoTienMat.Checked)
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // Lấy tài khoản đang đăng nhập
                    var taiKhoan = db.TaiKhoans.FirstOrDefault(
                        x => x.TenDangNhap == ThongTinDangNhap.TenDangNhap);

                    if (taiKhoan == null)
                    {
                        MessageBox.Show("Không tìm thấy tài khoản!");
                        return;
                    }

                    // Lấy khách hàng
                    var khachHang = db.KhachHangs.FirstOrDefault(
                        x => x.MaTK == taiKhoan.MaTK);

                    if (khachHang == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng!");
                        return;
                    }

                    // Tìm bến đi
                    var benDi = db.BenXes.FirstOrDefault(
                        x => x.TenBenXe == noiDi);

                    // Tìm bến đến
                    var benDen = db.BenXes.FirstOrDefault(
                        x => x.TenBenXe == noiDen);

                    if (benDi == null || benDen == null)
                    {
                        MessageBox.Show("Không tìm thấy tuyến xe!");
                        return;
                    }

                    // Tìm tuyến
                    var tuyen = db.TuyenXes.FirstOrDefault(
                        x => x.MaBenXeDi == benDi.MaBenXe &&
                             x.MaBenXeDen == benDen.MaBenXe);

                    if (tuyen == null)
                    {
                        MessageBox.Show("Không tìm thấy tuyến xe!");
                        return;
                    }

                    // Chuyển giờ khởi hành sang TimeSpan
                    TimeSpan gioKhoiHanh;

                    if (!TimeSpan.TryParse(gioDi, out gioKhoiHanh))
                    {
                        MessageBox.Show("Giờ khởi hành không hợp lệ!");
                        return;
                    }

                    // Tìm chuyến
                    var chuyen = db.ChuyenXes.FirstOrDefault(
                        x => x.MaTuyen == tuyen.MaTuyen &&
                             x.NgayKhoiHanh == ngayDi &&
                             x.GioKhoiHanh == gioKhoiHanh);

                    if (chuyen == null)
                    {
                        MessageBox.Show("Không tìm thấy chuyến xe!");
                        return;
                    }
                    string[] danhSachGhe = ghe.Split(',');
                    // Lưu từng ghế thành 1 vé
                    foreach (string item in danhSachGhe)
                    {
                        int soGhe;

                        if (!int.TryParse(
                            item.Trim().Replace("Ghế ", ""),
                            out soGhe))
                        {
                            MessageBox.Show("Ghế không hợp lệ!");
                            return;
                        }

                        var gheDb = db.Ghes.FirstOrDefault(
                            x => x.MaXe == chuyen.MaXe &&
                                 x.SoGhe == soGhe);

                        if (gheDb == null)
                        {
                            MessageBox.Show("Không tìm thấy ghế " + soGhe + "!");
                            return;
                        }

                        VeXe ve = new VeXe
                        {
                            MaVe = "VE" + Guid.NewGuid()
                                .ToString("N")
                                .Substring(0, 8)
                                .ToUpper(),

                            MaChuyen = chuyen.MaChuyen,
                            MaKH = khachHang.MaKH,
                            MaGhe = gheDb.MaGhe,

                            MaNV = null,

                            NgayDat = DateTime.Today,
                            ThoiGianDat = DateTime.Now.TimeOfDay,

                            GiaVe = giaVe,
                            PhuongThucThanhToan = "Tiền mặt",
                            TrangThai = "Chờ duyệt",
                        };

                        db.VeXes.Add(ve);
                    }

                    db.SaveChanges();
                }

                MessageBox.Show(
                    "Đã gửi thông tin đặt vé đến nhân viên.\n\n" +
                    "Tổng tiền: " + tongTien.ToString("N0") + "đ\n" +
                    "Trạng thái: Chờ duyệt",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
                return;
            }
            if (rdoChuyenKhoan.Checked)
            {
                int soLuongVe = soLuong;

                string[] danhSachGhe = ghe
                    .Split(',')
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToArray();

                if (danhSachGhe.Length != soLuongVe)
                {
                    MessageBox.Show(
                        "Bạn đang mua " + soLuongVe + " vé nhưng đang chọn " +
                        danhSachGhe.Length + " ghế!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                FrmMaQRThanhToan frm = new FrmMaQRThanhToan(tongTien);

                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                // Lưu vé chuyển khoản
                using (var db = new QuanLyVeXeModel())
                {
                    var tk = db.TaiKhoans.FirstOrDefault(
                        x => x.TenDangNhap == ThongTinDangNhap.TenDangNhap);

                    var kh = db.KhachHangs.FirstOrDefault(
                        x => x.MaTK == tk.MaTK);

                    var benDi = db.BenXes.FirstOrDefault(x => x.TenBenXe == noiDi);
                    var benDen = db.BenXes.FirstOrDefault(x => x.TenBenXe == noiDen);

                    var tuyen = db.TuyenXes.FirstOrDefault(x =>
                        x.MaBenXeDi == benDi.MaBenXe &&
                        x.MaBenXeDen == benDen.MaBenXe);

                    TimeSpan gio = TimeSpan.Parse(gioDi);

                    var chuyen = db.ChuyenXes.FirstOrDefault(x =>
                        x.MaTuyen == tuyen.MaTuyen &&
                        x.NgayKhoiHanh == ngayDi &&
                        x.GioKhoiHanh == gio);

                    foreach (string s in danhSachGhe)
                    {
                        int soGhe = int.Parse(s.Replace("Ghế ", "").Trim());

                        var g = db.Ghes.FirstOrDefault(x =>
                            x.MaXe == chuyen.MaXe && x.SoGhe == soGhe);

                        db.VeXes.Add(new VeXe
                        {
                            MaVe = "VE" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                            MaChuyen = chuyen.MaChuyen,
                            MaKH = kh.MaKH,
                            MaGhe = g.MaGhe,
                            MaNV = null,
                            NgayDat = DateTime.Today,
                            ThoiGianDat = DateTime.Now.TimeOfDay,
                            GiaVe = giaVe,
                            PhuongThucThanhToan = "Chuyển khoản",
                            TrangThai = "Chờ duyệt"
                        });
                    }

                    db.SaveChanges();
                }
            }

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
