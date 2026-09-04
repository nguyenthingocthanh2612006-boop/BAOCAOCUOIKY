using BAOCAOCUOIKY.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BAOCAOCUOIKY
{
    public partial class FrmTrangChuNhanVien : Form
    {
        QuanLyVeXeModel db = new QuanLyVeXeModel();
        Timer timer = new Timer();

        public FrmTrangChuNhanVien()
        {
            InitializeComponent();
            CapNhatThongKe();
            CapNhatNgayGio();
            LoadChuyenXeHomNay();
            VeBieuDoTrangThaiVe();
            VeBieuDoDoanhThu7Ngay();

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            ChonMenu(btnTrangChu);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            CapNhatNgayGio();
        }

        private void CapNhatNgayGio()
        {
            DateTime now = DateTime.Now;

            string[] thu =
            {
        "Chủ nhật",
        "Thứ 2",
        "Thứ 3",
        "Thứ 4",
        "Thứ 5",
        "Thứ 6",
        "Thứ 7"
    };

            lblNgay.Text = thu[(int)now.DayOfWeek] + ", " + now.ToString("dd/MM/yyyy");
            lblGio.Text = now.ToString("HH:mm:ss");
            lblAdmin.Text = "ADMIN";
        }

        private void CapNhatThongKe()
        {
            lblTongSoVe.Text = db.VeXes.Count().ToString();

            lblVeDaBan.Text = db.VeXes.Count(v =>
                v.TrangThai == "Đã thanh toán" &&
                DbFunctions.TruncateTime(v.NgayDat) == DateTime.Today
            ).ToString();

            decimal doanhThu = db.VeXes
                .Where(v =>
                    v.TrangThai == "Đã thanh toán" &&
                    DbFunctions.TruncateTime(v.NgayDat) == DateTime.Today
                )
                .Select(v => (decimal?)v.GiaVe)
                .Sum() ?? 0;

            lblDoanhThu.Text = doanhThu.ToString("N0");
        }

        private void VeBieuDoTrangThaiVe()
        {
            chartTrangThaiVe.Series.Clear();

            Series series = new Series("TyLeVe");
            series.ChartType = SeriesChartType.Pie;

            int daThanhToan = db.VeXes.Count(v =>
                v.TrangThai == "Đã thanh toán");

            int daHuy = db.VeXes.Count(v =>
                v.TrangThai == "Đã hủy");

            series.Points.AddXY("Đã thanh toán", daThanhToan);
            series.Points.AddXY("Đã hủy", daHuy);

            chartTrangThaiVe.Series.Add(series);
        }

        private void VeBieuDoDoanhThu7Ngay()
        {
            chartDoanhThu7Ngay.Series.Clear();

            Series series = new Series("DoanhThu7Ngay");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;

            DateTime ngayBatDau = DateTime.Today.AddDays(-6);

            for (int i = 0; i < 7; i++)
            {
                DateTime ngay = ngayBatDau.AddDays(i);

                decimal doanhThu = db.VeXes
                    .Where(v =>
                        v.TrangThai == "Đã thanh toán" &&
                        DbFunctions.TruncateTime(v.NgayDat) == ngay)
                    .Select(v => (decimal?)v.GiaVe)
                    .Sum() ?? 0;

                series.Points.AddXY(
                    ngay.ToString("dd/MM"),
                    doanhThu);
            }

            chartDoanhThu7Ngay.Series.Add(series);

            chartDoanhThu7Ngay.ChartAreas[0].AxisX.Title = "Ngày";
            chartDoanhThu7Ngay.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
        }
        private void ChonMenu(Guna.UI2.WinForms.Guna2Button btn)
        {
            btnTrangChu.FillColor = Color.FromArgb(70, 130, 220);
            btnBanVe.FillColor = Color.FromArgb(70, 130, 220);
            btnXuatHoaDon.FillColor = Color.FromArgb(70, 130, 220);
            btnTraCuuChuyenXe.FillColor = Color.FromArgb(70, 130, 220);

            btn.FillColor = Color.FromArgb(35, 85, 180);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            ChonMenu(btnTrangChu);
        }

        private void btnBanVe_Click(object sender, EventArgs e)
        {
            
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTraCuuChuyenXe_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất không?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FrmDangNhap frm = new FrmDangNhap();
                frm.Show();

                foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (f != frm)
                        f.Hide();
                }
            }
        }

        private void LoadChuyenXeHomNay()
        {
            var ds = db.ChuyenXes
                .Where(c => DbFunctions.TruncateTime(c.NgayKhoiHanh) == DateTime.Today)
                .Select(c => new
                {
                    c.GioKhoiHanh,
                    TuyenXe = c.TuyenXe.TenTuyen,
                    Xe = c.Xe.BienSo,
                    TaiXe = c.TaiXe.HoTen,
                    MaChuyen = c.MaChuyen,
                    SoLuongVe = c.SoLuongVe
                })
                .ToList();

            dgvChuyenXeHomNay.Rows.Clear();

            foreach (var c in ds)
            {
                int gheDaBan = db.VeXes.Count(v =>
                    v.MaChuyen == c.MaChuyen &&
                    v.TrangThai == "Đã thanh toán");

                int gheCon = c.SoLuongVe - gheDaBan;

                dgvChuyenXeHomNay.Rows.Add(
                    c.GioKhoiHanh,
                    c.TuyenXe,
                    c.Xe,
                    c.TaiXe,
                    gheDaBan,
                    gheCon
                );
            }
        }

        private void btnBanVe_Click_1(object sender, EventArgs e)
        {
            FrmBanVe frm = new FrmBanVe();
            frm.Show();

            this.Hide();
        }
    }
}