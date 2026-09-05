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
    public partial class FrmHoaDon : Form
    {
        private string maTK;
        public FrmHoaDon(string maTK)
        {
            InitializeComponent();
            lblHoaDon.ForeColor = Color.FromArgb(0, 102, 204);
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Bold);
            this.maTK = maTK;
            HienThiDanhSachVe();
        }
        private void HienThiDanhSachVe()
        {
            dgvLuoi1.AutoGenerateColumns = false;

            using (var db = new QuanLyVeXeModel())
            {
                var dsVe = db.VeXes
                    .Where(v => v.KhachHang.MaTK == maTK
                     && v.TrangThai != "Chờ duyệt")
                    .Select(v => new
                    {
                        MaVe = v.MaVe,
                        TuyenXe = v.ChuyenXe.TuyenXe.TenTuyen,
                        NgayDi = v.ChuyenXe.NgayKhoiHanh,
                        GioKhoiHanh = v.ChuyenXe.GioKhoiHanh,
                        Ghe = v.Ghe.SoGhe,
                        Gia = v.GiaVe,
                        ThanhTien = v.GiaVe,
                        TrangThai = v.TrangThai
                    })
                    .OrderByDescending(v => v.NgayDi)
                    .ThenByDescending(v => v.GioKhoiHanh)
                    .ToList();

                dgvLuoi1.Rows.Clear();

                int stt = 1;

                foreach (var ve in dsVe)
                {
                    dgvLuoi1.Rows.Add(
                        stt++,
                        ve.MaVe,
                        ve.TuyenXe,
                        ve.NgayDi.ToString("dd/MM/yyyy"),
                        ve.GioKhoiHanh.ToString(@"hh\:mm"),
                        "Ghế " + ve.Ghe,
                        ve.Gia.ToString("N0") + " đ",
                        ve.ThanhTien.ToString("N0") + " đ",
                        ve.TrangThai
                    );
                }
            }
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            HienThiDanhSachVe();
        }

        private void btnChoDuyet_Click(object sender, EventArgs e)
        {
            HienThiVeChoDuyet();
        }
        private void HienThiVeChoDuyet()
        {
            dgvLuoi1.AutoGenerateColumns = false;
            dgvLuoi1.Rows.Clear();

            using (var db = new QuanLyVeXeModel())
            {
                var dsVe = db.VeXes
                     .Where(v => v.KhachHang.MaTK == maTK
                             && v.TrangThai == "Chờ duyệt")
                    .Select(v => new
                    {
                        MaVe = v.MaVe,
                        TuyenXe = v.ChuyenXe.TuyenXe.TenTuyen,
                        NgayDi = v.ChuyenXe.NgayKhoiHanh,
                        GioKhoiHanh = v.ChuyenXe.GioKhoiHanh,
                        Ghe = v.Ghe.SoGhe,
                        Gia = v.GiaVe,
                        ThanhTien = v.GiaVe,
                        TrangThai = v.TrangThai
                    })
                    .OrderByDescending(v => v.NgayDi)
                    .ThenByDescending(v => v.GioKhoiHanh)
                    .ToList();

                int stt = 1;

                foreach (var ve in dsVe)
                {
                    dgvLuoi1.Rows.Add(
                        stt++,
                        ve.MaVe,
                        ve.TuyenXe,
                        ve.NgayDi.ToString("dd/MM/yyyy"),
                        ve.GioKhoiHanh.ToString(@"hh\:mm"),
                        "Ghế " + ve.Ghe,
                        ve.Gia.ToString("N0") + " đ",
                        ve.ThanhTien.ToString("N0") + " đ",
                        ve.TrangThai
                    );
                }
            }
        }

        private void lblHoaDon_Click(object sender, EventArgs e)
        {
            FrmHoaDon hd = new FrmHoaDon(maTK);
            hd.ShowDialog();
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;
        }

        private void dgvLuoi1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvLuoi1.Rows[e.RowIndex];

            if (row.Cells[1].Value == null)
                return;

            string maVe = row.Cells[1].Value.ToString();

            using (var db = new QuanLyVeXeModel())
            {
                var ve = db.VeXes
                    .Where(v => v.MaVe == maVe)
                    .Select(v => new
                    {
                        MaVe = v.MaVe,
                        MaKH = v.MaKH,
                        MaChuyen = v.MaChuyen,
                        NgayDat = v.NgayDat,

                        PhuongThuc = v.PhuongThucThanhToan,
                        TrangThai = v.TrangThai,

                        TenTuyen = v.ChuyenXe.TuyenXe.TenTuyen,
                        NgayDi = v.ChuyenXe.NgayKhoiHanh,
                        GioDi = v.ChuyenXe.GioKhoiHanh,

                        SoGhe = v.Ghe.SoGhe
                    })
                    .FirstOrDefault();

                if (ve == null)
                    return;

                // ===== THÔNG TIN HÓA ĐƠN =====
                txtMaHD.Text = ve.MaVe;
                txtNgayLap.Text = ve.NgayDat.ToString("dd/MM/yyyy");
                txtPhuongThuc.Text = ve.PhuongThuc;
                txtTrangThai.Text = ve.TrangThai;

                // ===== THÔNG TIN TUYẾN XE =====
                txtTuyen.Text = ve.TenTuyen;
                txtNgay.Text = ve.NgayDi.ToString("dd/MM/yyyy");
                txtGio.Text = ve.GioDi.ToString(@"hh\:mm");

                // ===== HIỂN THỊ DANH SÁCH GHẾ =====
                var dsGhe = db.VeXes
                    .Where(v => v.MaKH == ve.MaKH
                             && v.MaChuyen == ve.MaChuyen
                             && v.NgayDat == ve.NgayDat
                             && v.TrangThai != "Đã hủy")
                    .Select(v => new
                    {
                        SoGhe = v.Ghe.SoGhe
                    })
                    .OrderBy(v => v.SoGhe)
                    .ToList();

                dgvLuoi2.Rows.Clear();

                int tongGhe = dsGhe.Count;
                int stt = 1;

                foreach (var ghe in dsGhe)
                {
                    dgvLuoi2.Rows.Add(
                        stt++,
                        "Ghế " + ghe.SoGhe,
                        tongGhe
                    );
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (dgvLuoi1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vé cần hủy!");
                return;
            }

            if (dgvLuoi1.CurrentRow.Cells[1].Value == null)
                return;

            string maVe = dgvLuoi1.CurrentRow.Cells[1].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy chuyến đi này không?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            using (var db = new QuanLyVeXeModel())
            {
                var ve = db.VeXes.FirstOrDefault(v => v.MaVe == maVe);

                if (ve == null)
                {
                    MessageBox.Show("Không tìm thấy vé!");
                    return;
                }

                // Chỉ vé Chờ duyệt mới được hủy
                if (ve.TrangThai != "Chờ duyệt")
                {
                    MessageBox.Show(
                        "Chỉ có vé đang Chờ duyệt mới được hủy!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Hủy vé
                ve.TrangThai = "Đã hủy";

                db.SaveChanges();
            }

            MessageBox.Show("Hủy chuyến đi thành công!");

            // Cập nhật lại danh sách chờ duyệt
            HienThiVeChoDuyet();
        }
    }
}
