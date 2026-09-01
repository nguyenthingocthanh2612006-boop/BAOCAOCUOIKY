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
    public partial class FrmTraCuuVe : Form
    {
        private QuanLyVeXeModel db = new QuanLyVeXeModel();
        public FrmTraCuuVe()
        {
            InitializeComponent();

            // Tra cứu vé sáng
            lblTraCuuVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTraCuuVe.Font = new Font(
                lblTraCuuVe.Font,
                FontStyle.Bold
            );
        }
        private void lblTrangChu_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.FromArgb(0, 102, 204);
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Bold);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
            // MỞ FORM TRANG CHỦ
            FrmTrangChuKhachHang frm = new FrmTrangChuKhachHang();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void lblDatVe_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Bold);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
            // MỞ FORM ĐẶT VÉ
            FrmDatVe frm = new FrmDatVe("", "", DateTime.Today);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void lblTraCuuVe_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Bold);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
        }

        private void lblTuyenXe_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Bold);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
        }

        private void lblHoaDon_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.FromArgb(0, 102, 204);
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Bold);
        }

        private void FrmTraCuuVe_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;

            // ComboBox trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Đã thanh toán");
            cboTrangThai.Items.Add("Đã hủy");

            cboTrangThai.SelectedIndex = 0;

            // Khoảng ngày mặc định
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;

            // Cấu hình lưới
            CaiDatLuoi();

            // Hiển thị lịch sử
            LoadLichSuMuaVe();
        }

        private void CaiDatLuoi()
        {
            dgvLichSuVe.AutoGenerateColumns = false;

            dgvLichSuVe.AllowUserToAddRows = false;
            dgvLichSuVe.AllowUserToDeleteRows = false;
            dgvLichSuVe.ReadOnly = true;

            dgvLichSuVe.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLichSuVe.MultiSelect = false;
            dgvLichSuVe.RowHeadersVisible = false;

            MaVe.DataPropertyName = "MaVe";
            TuyenXe.DataPropertyName = "TuyenXe";
            NgayDi.DataPropertyName = "NgayDi";
            GioDi.DataPropertyName = "GioDi";
            GioDen.DataPropertyName = "GioDen";
            BienSo.DataPropertyName = "BienSo";
            SoGhe.DataPropertyName = "SoGhe";
            SoLuong.DataPropertyName = "SoLuong";
            ThanhTien.DataPropertyName = "ThanhTien";
            TrangThai.DataPropertyName = "TrangThai";
        }

        private void LoadLichSuMuaVe()
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date;

                    var query =
                        from hd in db.HoaDons

                        join ve in db.VeXes
                            on hd.MaVe equals ve.MaVe

                        join cx in db.ChuyenXes
                            on ve.MaChuyen equals cx.MaChuyen

                        join tx in db.TuyenXes
                            on cx.MaTuyen equals tx.MaTuyen

                        join xe in db.Xes
                            on cx.MaXe equals xe.MaXe

                        join ghe in db.Ghes
                            on ve.MaGhe equals ghe.MaGhe

                        join cthd in db.ChiTietHoaDons
                            on new { hd.MaHD, ve.MaVe }
                            equals new { cthd.MaHD, cthd.MaVe }

                        where hd.NgayLap >= tuNgay
                           && hd.NgayLap <= denNgay

                        select new
                        {
                            MaVe = ve.MaVe,

                            TuyenXe = tx.TenTuyen,

                            NgayDi = cx.NgayKhoiHanh,

                            GioDi = cx.GioKhoiHanh,

                            GioDen = cx.GioDenDuKien,

                            BienSo = xe.BienSo,

                            SoGhe = ghe.SoGhe,

                            SoLuong = cthd.SoLuong,

                            ThanhTien = cthd.ThanhTien,

                            TrangThai = hd.TrangThai
                        };

                    // Lọc trạng thái
                    if (!string.IsNullOrWhiteSpace(cboTrangThai.Text)
                        && cboTrangThai.Text != "Tất cả")
                    {
                        string trangThai = cboTrangThai.Text;

                        query = query.Where(x =>
                            x.TrangThai == trangThai);
                    }

                    dgvLichSuVe.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải lịch sử mua vé:\n" + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }   

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadLichSuMuaVe();
        }

        private void btnXoaLoc_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;

            cboTrangThai.SelectedIndex = 0;

            LoadLichSuMuaVe();
        }
    }
}
