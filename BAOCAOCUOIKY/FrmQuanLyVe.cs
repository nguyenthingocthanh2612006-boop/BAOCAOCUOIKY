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
    public partial class FrmQuanLyVe : Form
    {
        QuanLyVeXeModel db = new QuanLyVeXeModel();
        public FrmQuanLyVe()
        {
            InitializeComponent();

            dgvDanhSachVe.AutoGenerateColumns = false;

            LoadTuyenXe();
            LoadChuyenXe();
            LoadTrangThai();
            LoadTrangThaiChiTiet();

            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;
            dtpTTNgayDi.Value = DateTime.Today;
                
            LoadDanhSachVe();
        }

        private void LoadTuyenXe()
        {
            cboTuyenXe.Items.Clear();
            cboTuyenXe.Items.Add("Tất cả");

            var ds = db.TuyenXes
                       .Where(t => t.TrangThai != "Ngừng hoạt động")
                       .ToList();

            foreach (var t in ds)
            {
                cboTuyenXe.Items.Add(t.TenTuyen);
            }

            cboTuyenXe.SelectedIndex = 0;
        }

        private void LoadTrangThaiChiTiet()
        {
            cboTTTrangThai.Items.Clear();

            cboTTTrangThai.Items.Add("Đã đặt");
            cboTTTrangThai.Items.Add("Đã thanh toán");
            cboTTTrangThai.Items.Add("Chờ thanh toán");
            cboTTTrangThai.Items.Add("Đã hủy");

            cboTTTrangThai.SelectedIndex = -1;
        }

        private void LoadChuyenXe()
        {
            cboChuyenXe.Items.Clear();
            cboChuyenXe.Items.Add("Tất cả");

            var ds = db.ChuyenXes
                       .OrderByDescending(c => c.NgayKhoiHanh)
                       .ToList();

            foreach (var c in ds)
            {
                cboChuyenXe.Items.Add(c.MaChuyen);
            }

            cboChuyenXe.SelectedIndex = 0;
        }

        private void LoadTrangThai()
        {
            cboTrangThai.Items.Clear();

            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Đã thanh toán");
            cboTrangThai.Items.Add("Đã hủy");

            cboTrangThai.SelectedIndex = 0;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            string maVe = txtMaVe.Text.Trim();

            var query = from v in db.VeXes
                        join cx in db.ChuyenXes
                            on v.MaChuyen equals cx.MaChuyen
                        join tx in db.TuyenXes
                            on cx.MaTuyen equals tx.MaTuyen
                        join kh in db.KhachHangs
                            on v.MaKH equals kh.MaKH
                        join g in db.Ghes
                            on v.MaGhe equals g.MaGhe
                        select new
                        {
                            MaVe = v.MaVe,
                            TuyenXe = tx.TenTuyen,
                            ChuyenXe = cx.MaChuyen,
                            NgayDi = cx.NgayKhoiHanh,
                            GioDi = cx.GioKhoiHanh,
                            Ghe = g.SoGhe,
                            HanhKhach = kh.HoTen,
                            SDT = kh.SoDienThoai,
                            GiaVe = v.GiaVe,
                            TrangThai = v.TrangThai
                        };

            // Lọc ngày đi
            query = query.Where(x =>
                x.NgayDi >= tuNgay &&
                x.NgayDi <= denNgay);

            // Lọc mã vé
            if (!string.IsNullOrWhiteSpace(maVe))
            {
                query = query.Where(x => x.MaVe.Contains(maVe));
            }

            // Lọc tuyến xe
            string tuyenXe = cboTuyenXe.Text.Trim();

            if (!string.IsNullOrEmpty(tuyenXe) && tuyenXe != "Tất cả")
            {
                query = query.Where(x => x.TuyenXe == tuyenXe);
            }

            // Lọc chuyến xe
            string chuyenXe = cboChuyenXe.Text.Trim();

            if (!string.IsNullOrEmpty(chuyenXe) && chuyenXe != "Tất cả")
            {
                query = query.Where(x => x.ChuyenXe == chuyenXe);
            }

            // Lọc trạng thái
            string trangThai = cboTrangThai.Text.Trim();

            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                query = query.Where(x => x.TrangThai == trangThai);
            }

            dgvDanhSachVe.DataSource = query
                .OrderByDescending(x => x.NgayDi)
                .ToList();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Reset phần tìm kiếm
            txtMaVe.Clear();

            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            cboTuyenXe.SelectedIndex = 0;
            cboChuyenXe.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;

            // Load lại toàn bộ danh sách vé
            LoadDanhSachVe();

            // Xóa thông tin chi tiết bên phải
            XoaThongTinVe();

            // Đưa con trỏ về ô mã vé
            txtMaVe.Focus();
        }

        private void XoaThongTinVe()
        {
            txtTTMaVe.Clear();
            txtTTTuyenXe.Clear();
            txtTTChuyenXe.Clear();

            dtpTTNgayDi.Value = DateTime.Today;

            txtTTGioDi.Clear();
            txtTTGhe.Clear();
            txtTTHanhKhach.Clear();
            txtTTSDT.Clear();
            txtTTGiaVe.Clear();

            cboTTTrangThai.SelectedIndex = -1;

            txtTTNhanVien.Clear();
        }

        private void LoadDanhSachVe()
        {
            DateTime homNay = DateTime.Today;
            DateTime ngayMai = homNay.AddDays(1);

            var ds = from v in db.VeXes
                     join cx in db.ChuyenXes
                         on v.MaChuyen equals cx.MaChuyen
                     join tx in db.TuyenXes
                         on cx.MaTuyen equals tx.MaTuyen
                     join kh in db.KhachHangs
                         on v.MaKH equals kh.MaKH
                     join g in db.Ghes
                         on v.MaGhe equals g.MaGhe
                     where cx.NgayKhoiHanh >= homNay
                        && cx.NgayKhoiHanh < ngayMai
                     select new
                     {
                         MaVe = v.MaVe,
                         TuyenXe = tx.TenTuyen,
                         ChuyenXe = cx.MaChuyen,
                         NgayDi = cx.NgayKhoiHanh,
                         GioDi = cx.GioKhoiHanh,
                         Ghe = g.SoGhe,
                         HanhKhach = kh.HoTen,
                         SDT = kh.SoDienThoai,
                         GiaVe = v.GiaVe,
                         TrangThai = v.TrangThai
                     };

            dgvDanhSachVe.DataSource = ds
                .OrderBy(x => x.GioDi)
                .ToList();
        }

        private void dgvDanhSachVe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvDanhSachVe.Rows[e.RowIndex];

            string maVe = row.Cells["colMaVe"].Value?.ToString();

            if (string.IsNullOrEmpty(maVe))
                return;

            // Đổ thông tin vé
            txtTTMaVe.Text = row.Cells["colMaVe"].Value?.ToString();
            txtTTTuyenXe.Text = row.Cells["colTuyenXe"].Value?.ToString();
            txtTTChuyenXe.Text = row.Cells["colChuyenXe"].Value?.ToString();

            if (row.Cells["colNgayDi"].Value != null)
            {
                dtpTTNgayDi.Value =
                    Convert.ToDateTime(row.Cells["colNgayDi"].Value);
            }

            txtTTGioDi.Text = row.Cells["colGioDi"].Value?.ToString();
            txtTTGhe.Text = row.Cells["colGhe"].Value?.ToString();
            txtTTHanhKhach.Text = row.Cells["colHanhKhach"].Value?.ToString();
            txtTTSDT.Text = row.Cells["colSDT"].Value?.ToString();
            txtTTGiaVe.Text = row.Cells["colGiaVe"].Value?.ToString();

            // Lấy vé từ database
            var ve = db.VeXes
                .FirstOrDefault(v => v.MaVe == maVe);

            if (ve == null)
                return;

            // Trạng thái
            cboTTTrangThai.SelectedIndex = -1;
            cboTTTrangThai.Text = ve.TrangThai;

            // Nhân viên
            txtTTNhanVien.Clear();

            if (!string.IsNullOrWhiteSpace(ve.MaNV))
            {
                var nhanVien = db.NhanViens
                    .FirstOrDefault(nv => nv.MaNV == ve.MaNV);

                if (nhanVien != null)
                {
                    txtTTNhanVien.Text =
                        nhanVien.MaNV + " - " + nhanVien.HoTen;
                }
            }
        }
    }
}
