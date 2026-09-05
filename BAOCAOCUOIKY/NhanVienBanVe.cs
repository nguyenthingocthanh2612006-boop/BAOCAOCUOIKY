using BAOCAOCUOIKY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAOCAOCUOIKY
{
    public partial class FrmNhanVienBanVe : Form
    {
        private string maVeDangChon = "";

        public FrmNhanVienBanVe()
        {
            InitializeComponent();
        }

        private void FrmNhanVienBanVe_Load(object sender, EventArgs e)
        {
            dtpNgayDi.Value = DateTime.Today;
            LoadVe();
        }

        private void LoadVe()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            using (var db = new QuanLyVeXeModel())
            {
                DateTime ngay = dtpNgayDi.Value.Date;

                var dsVe = db.VeXes
                    .Where(v =>
                        v.ChuyenXe.NgayKhoiHanh == ngay &&
                        v.TrangThai == "Chờ duyệt"
                    )
                    .Select(v => new
                    {
                        MaVe = v.MaVe,
                        TenTuyen = v.ChuyenXe.TuyenXe.TenTuyen,
                        NgayDi = v.ChuyenXe.NgayKhoiHanh,
                        GioDi = v.ChuyenXe.GioKhoiHanh,
                        MaChuyen = v.MaChuyen,
                        SoGhe = v.Ghe.SoGhe,
                        GiaVe = v.GiaVe,
                        TrangThai = v.TrangThai
                    })
                    .OrderBy(v => v.GioDi)
                    .ToList();

                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    dsVe = dsVe
                        .Where(v =>
                            v.MaVe.Contains(tuKhoa) ||
                            v.TenTuyen.Contains(tuKhoa) ||
                            v.MaChuyen.Contains(tuKhoa)
                        )
                        .ToList();
                }

                dgvVeChoDuyet.Rows.Clear();

                foreach (var ve in dsVe)
                {
                    dgvVeChoDuyet.Rows.Add(
                        ve.MaVe,
                        ve.TenTuyen,
                        ve.NgayDi.ToString("dd/MM/yyyy"),
                        ve.GioDi.ToString(@"hh\:mm"),
                        "Ghế " + ve.SoGhe,
                        ve.GiaVe.ToString("N0") + " đ",
                        ve.TrangThai
                    );
                }
            }
        }

        private void dgvVeChoDuyet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvVeChoDuyet.Rows[e.RowIndex].Cells[0].Value == null)
                return;

            string maVe = dgvVeChoDuyet.Rows[e.RowIndex]
                .Cells[0]
                .Value.ToString();

            HienThiChiTietVe(maVe);
        }

        private void HienThiChiTietVe(string maVe)
        {
            using (var db = new QuanLyVeXeModel())
            {
                var ve = db.VeXes
                    .Where(v => v.MaVe == maVe)
                    .Select(v => new
                    {
                        MaVe = v.MaVe,
                        HoTen = v.KhachHang.HoTen,
                        SDT = v.KhachHang.SoDienThoai,

                        TenTuyen = v.ChuyenXe.TuyenXe.TenTuyen,
                        NgayDi = v.ChuyenXe.NgayKhoiHanh,
                        MaChuyen = v.MaChuyen,
                        BienSo = v.ChuyenXe.Xe.BienSo,

                        SoGhe = v.Ghe.SoGhe,
                        GiaVe = v.GiaVe,
                        TrangThai = v.TrangThai,
                        PhuongThucThanhToan = v.PhuongThucThanhToan
                    })
                    .FirstOrDefault();

                if (ve == null)
                    return;

                maVeDangChon = ve.MaVe;

                lblMaVe2.Text = ve.MaVe;
                lblKhachHang1.Text = ve.HoTen;
                lblSDT.Text = ve.SDT;

                lblTuyen7.Text = ve.TenTuyen;
                lblNgayDi2.Text = ve.NgayDi.ToString("dd/MM/yyyy");
                lblChuyen1.Text = ve.MaChuyen;
                lblBienSo1.Text = ve.BienSo;

                lblGheDaChon1.Text = "Ghế " + ve.SoGhe;
                lblGia5.Text = ve.GiaVe.ToString("N0") + " đ";
                lblTrangThai1.Text = ve.TrangThai;
                lblPhuongThuc.Text = ve.PhuongThucThanhToan;
            }
        }
        private void btnTimKiem2_Click(object sender, EventArgs e)
        {
            LoadVe();
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dgvVeChoDuyet.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vé!");
                return;
            }

            string maVe = dgvVeChoDuyet.CurrentRow.Cells[0].Value.ToString();

            using (var db = new QuanLyVeXeModel())
            {
                var ve = db.VeXes.FirstOrDefault(x => x.MaVe == maVe);

                if (ve == null)
                {
                    MessageBox.Show("Không tìm thấy vé!");
                    return;
                }

                if (ve.TrangThai != "Chờ duyệt")
                {
                    MessageBox.Show("Chỉ được từ chối vé đang Chờ duyệt!");
                    return;
                }

                ve.TrangThai = "Đã hủy";
                db.SaveChanges();
            }

            MessageBox.Show("Đã từ chối vé!");

            btnTimKiem2_Click(null, null);
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (dgvVeChoDuyet.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vé cần duyệt!");
                return;
            }

            string maVe = dgvVeChoDuyet.CurrentRow.Cells[0].Value.ToString();

            using (var db = new QuanLyVeXeModel())
            {
                var ve = db.VeXes.FirstOrDefault(x => x.MaVe == maVe);

                if (ve == null)
                {
                    MessageBox.Show("Không tìm thấy vé!");
                    return;
                }

                // Chỉ được duyệt vé đang chờ duyệt
                if (ve.TrangThai != "Chờ duyệt")
                {
                    MessageBox.Show("Chỉ được duyệt vé đang ở trạng thái Chờ duyệt!");
                    return;
                }

                // Cập nhật theo phương thức thanh toán
                if (ve.PhuongThucThanhToan == "Tiền mặt")
                    ve.TrangThai = "Chưa thanh toán";
                else if (ve.PhuongThucThanhToan == "Chuyển khoản")
                    ve.TrangThai = "Đã thanh toán";

                db.SaveChanges();
                btnTimKiem2_Click(null, null);
            }

            MessageBox.Show("Duyệt vé thành công!");
            btnTimKiem2_Click(null, null);
        }

        private void btnChoDuyet_Click(object sender, EventArgs e)
        {
            LoadVeChoDuyet();
        }
        private void LoadVeChoDuyet()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            using (var db = new QuanLyVeXeModel())
            {
                DateTime ngay = dtpNgayDi.Value.Date;

                var dsVe = db.VeXes
                    .Where(v =>
                        v.ChuyenXe.NgayKhoiHanh == ngay &&
                        v.TrangThai == "Chờ duyệt"
                    )
                    .Select(v => new
                    {
                        MaVe = v.MaVe,
                        TenTuyen = v.ChuyenXe.TuyenXe.TenTuyen,
                        NgayDi = v.ChuyenXe.NgayKhoiHanh,
                        GioDi = v.ChuyenXe.GioKhoiHanh,
                        MaChuyen = v.MaChuyen,
                        SoGhe = v.Ghe.SoGhe,
                        GiaVe = v.GiaVe,
                        TrangThai = v.TrangThai
                    })
                    .OrderBy(v => v.GioDi)
                    .ToList();

                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    dsVe = dsVe
                        .Where(v =>
                            v.MaVe.Contains(tuKhoa) ||
                            v.TenTuyen.Contains(tuKhoa) ||
                            v.MaChuyen.Contains(tuKhoa)
                        )
                        .ToList();
                }

                dgvVeChoDuyet.Rows.Clear();

                foreach (var ve in dsVe)
                {
                    dgvVeChoDuyet.Rows.Add(
                        ve.MaVe,
                        ve.TenTuyen,
                        ve.NgayDi.ToString("dd/MM/yyyy"),
                        ve.GioDi.ToString(@"hh\:mm"),
                        "Ghế " + ve.SoGhe,
                        ve.GiaVe.ToString("N0") + " đ",
                        ve.TrangThai
                    );
                }
            }
        }
    }
}
