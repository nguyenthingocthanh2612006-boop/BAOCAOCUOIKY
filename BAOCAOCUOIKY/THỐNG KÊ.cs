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
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.Entity;

namespace BAOCAOCUOIKY
{
    public partial class FrmThongKe : Form
    {
        private Timer timer;
        public FrmThongKe()
        {
            InitializeComponent();

            ChonMenu(btnThongKe);
        }

        private void LoadTuyenXe()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var ds = db.TuyenXes
                    .OrderBy(x => x.TenTuyen)
                    .ToList();

                cboTuyenXe.DataSource = null;

                var data = new List<object>();

                data.Add(new
                {
                    MaTuyen = "",
                    TenTuyen = "Tất cả"
                });

                foreach (var t in ds)
                {
                    data.Add(new
                    {
                        MaTuyen = t.MaTuyen,
                        TenTuyen = t.TenTuyen
                    });
                }

                cboTuyenXe.DataSource = data;
                cboTuyenXe.DisplayMember = "TenTuyen";
                cboTuyenXe.ValueMember = "MaTuyen";
                cboTuyenXe.SelectedIndex = 0;
            }
        }

        private void LoadTrangThai()
        {
            cboTrangThai.Items.Clear();

            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Đã thanh toán");
            cboTrangThai.Items.Add("Đã hủy");

            cboTrangThai.SelectedIndex = 0;
        }

        private void cboTuyenXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            LoadTuyenXe();
            LoadTrangThai();
            // Hiển thị ngày giờ ngay khi mở form
            CapNhatNgayGio();

            // Tạo đồng hồ cập nhật mỗi giây
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            dtpTuNgay.Value = DateTime.Today.AddDays(-7);
            dtpDenNgay.Value = DateTime.Today;

            // Chưa thống kê thì để trống
            lblTongDoanhThu.Text = "";
            lblTongVe.Text = "";
            lblTongChuyen.Text = "";
            lblKhachHangMoi.Text = "";

            // Xóa dữ liệu biểu đồ ban đầu
            chartDoanhThuNgay.Series.Clear();
            chartDoanhThuTuyen.Series.Clear();
            chartSoLuongVeTuyen.Series.Clear();
            chartTrangThaiVe.Series.Clear();

            // Xóa bảng Top nhân viên
            dgvTopNhanVien.Rows.Clear();
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

            lblNgay.Text = thu[(int)now.DayOfWeek]
                           + ", " + now.ToString("dd/MM/yyyy");

            lblGio.Text = now.ToString("HH:mm:ss");

            lblAdmin.Text = "ADMIN";
        }
        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgayGoc = dtpDenNgay.Value.Date;

            if (tuNgay > denNgayGoc)
            {
                MessageBox.Show(
                    "Từ ngày không được lớn hơn đến ngày!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DateTime denNgay = denNgayGoc.AddDays(1);

            using (var db = new QuanLyVeXeModel())
            {
                // =====================================================
                // 1. QUERY VÉ - ENTITY FRAMEWORK + LINQ
                // =====================================================

                IQueryable<VeXe> veQuery = db.VeXes
                    .Where(v =>
                        v.NgayDat >= tuNgay &&
                        v.NgayDat < denNgay);


                // =====================================================
                // 2. FILTER TRẠNG THÁI
                // =====================================================

                string trangThai = cboTrangThai.Text.Trim();

                if (!string.IsNullOrEmpty(trangThai) &&
                    trangThai != "Tất cả")
                {
                    veQuery = veQuery.Where(v =>
                        v.TrangThai == trangThai);
                }


                // =====================================================
                // 3. FILTER TUYẾN XE
                // =====================================================

                string maTuyen = "";

                if (cboTuyenXe.SelectedValue != null)
                {
                    maTuyen = cboTuyenXe.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(maTuyen))
                {
                    veQuery = veQuery.Where(v =>
                        db.ChuyenXes.Any(c =>
                            c.MaChuyen == v.MaChuyen &&
                            c.MaTuyen == maTuyen));
                }


                // =====================================================
                // 4. TỔNG DOANH THU
                // =====================================================

                decimal tongDoanhThu = veQuery
                    .Where(v => v.TrangThai == "Đã thanh toán")
                    .Select(v => (decimal?)v.GiaVe)
                    .Sum() ?? 0;

                lblTongDoanhThu.Text =
                    tongDoanhThu.ToString("N0");


                // =====================================================
                // 5. TỔNG SỐ VÉ
                // =====================================================

                int tongVe = veQuery
                    .Count(v => v.TrangThai != "Đã hủy");

                lblTongVe.Text =
                    tongVe.ToString();


                // =====================================================
                // 6. QUERY CHUYẾN
                // =====================================================

                IQueryable<ChuyenXe> chuyenQuery = db.ChuyenXes
                    .Where(c =>
                        c.NgayKhoiHanh >= tuNgay &&
                        c.NgayKhoiHanh < denNgay);

                if (!string.IsNullOrEmpty(maTuyen))
                {
                    chuyenQuery = chuyenQuery.Where(c =>
                        c.MaTuyen == maTuyen);
                }

                int tongChuyen = chuyenQuery.Count();

                lblTongChuyen.Text =
                    tongChuyen.ToString();


                // =====================================================
                // 7. KHÁCH HÀNG MỚI
                // =====================================================

                int khachHangMoi = db.KhachHangs
                    .Count(k =>
                        k.NgayDangKy >= tuNgay &&
                        k.NgayDangKy < denNgay);

                lblKhachHangMoi.Text =
                    khachHangMoi.ToString();


                // =====================================================
                // 8. LẤY QUERY RA LIST
                //    SAU KHI FILTER XONG
                // =====================================================

                var dsVe = veQuery.ToList();


                // =====================================================
                // 9. BIỂU ĐỒ DOANH THU THEO NGÀY
                // =====================================================

                chartDoanhThuNgay.Series.Clear();

                Series seriesNgay = new Series("Doanh Thu");

                seriesNgay.ChartType =
                    SeriesChartType.Line;

                seriesNgay.BorderWidth = 3;
                seriesNgay.MarkerStyle =
                    MarkerStyle.Circle;

                for (DateTime ngay = tuNgay.Date;
                     ngay < denNgay.Date;
                     ngay = ngay.AddDays(1))
                {
                    decimal doanhThuNgay = dsVe
                        .Where(v =>
                            v.TrangThai == "Đã thanh toán" &&
                            v.NgayDat.Date == ngay.Date)
                        .Sum(v => v.GiaVe);

                    seriesNgay.Points.AddXY(
                        ngay.ToString("dd/MM"),
                        doanhThuNgay);
                }

                chartDoanhThuNgay.Series.Add(seriesNgay);


                // =====================================================
                // 10. DOANH THU THEO TUYẾN
                // =====================================================

                chartDoanhThuTuyen.Series.Clear();

                Series seriesTuyen =
                    new Series("Doanh Thu");

                seriesTuyen.ChartType =
                    SeriesChartType.Doughnut;

                seriesTuyen.IsValueShownAsLabel = true;


                var doanhThuTuyen =
                     from v in dsVe
                     join c in db.ChuyenXes
                         on v.MaChuyen equals c.MaChuyen
                     join t in db.TuyenXes
                         on c.MaTuyen equals t.MaTuyen
                     where v.TrangThai == "Đã thanh toán"
                     group v by t.TenTuyen into g
                     select new
                     {
                         TenTuyen = g.Key,
                         DoanhThu = g.Sum(x => x.GiaVe)
                     };

                var dsDoanhThuTuyen = doanhThuTuyen.ToList();

                foreach (var item in dsDoanhThuTuyen)
                {
                    seriesTuyen.Points.AddXY(
                        item.TenTuyen,
                        item.DoanhThu);
                }

                chartDoanhThuTuyen.Series.Add(seriesTuyen);


                // =====================================================
                // 11. SỐ LƯỢNG VÉ THEO TUYẾN
                // =====================================================

                chartSoLuongVeTuyen.Series.Clear();

                Series seriesSoVe =
                    new Series("Số Vé");

                seriesSoVe.ChartType =
                    SeriesChartType.Column;

                seriesSoVe.IsValueShownAsLabel = true;


                    var soLuongVe =
                         from v in dsVe
                         join c in db.ChuyenXes
                             on v.MaChuyen equals c.MaChuyen
                         join t in db.TuyenXes
                             on c.MaTuyen equals t.MaTuyen
                         where v.TrangThai != "Đã hủy"
                         group v by t.TenTuyen into g
                         select new
                         {
                             TenTuyen = g.Key,
                             SoVe = g.Count()
                         };

                    var dsSoLuongVe = soLuongVe.ToList();

                    foreach (var item in dsSoLuongVe)
                    {
                        seriesSoVe.Points.AddXY(
                            item.TenTuyen,
                            item.SoVe);
                    }

                chartSoLuongVeTuyen.Series.Add(seriesSoVe);


                // =====================================================
                // 12. TRẠNG THÁI VÉ
                // =====================================================

                chartTrangThaiVe.Series.Clear();

                Series seriesTrangThai =
                    new Series("Trạng Thái");

                seriesTrangThai.ChartType =
                    SeriesChartType.Doughnut;

                seriesTrangThai.IsValueShownAsLabel =
                    true;

                seriesTrangThai.Label =
                    "#PERCENT{P0}";


                var trangThaiVe = dsVe
                    .GroupBy(v => v.TrangThai)
                    .Select(g => new
                    {
                        TrangThai = g.Key,
                        SoLuong = g.Count()
                    })
                    .ToList();


                foreach (var item in trangThaiVe)
                {
                    DataPoint point =
                        new DataPoint();

                    point.SetValueY(
                        item.SoLuong);

                    point.LegendText =
                        item.TrangThai;

                    seriesTrangThai.Points.Add(point);
                }

                chartTrangThaiVe.Series.Add(
                    seriesTrangThai);
                // =====================================================
                // TOP NHÂN VIÊN BÁN VÉ
                // =====================================================

                dgvTopNhanVien.Rows.Clear();

                var topNhanVien =
                    from v in dsVe
                    join nv in db.NhanViens
                        on v.MaNV equals nv.MaNV
                    where v.TrangThai == "Đã thanh toán"
                    group v by new
                    {
                        nv.MaNV,
                        nv.HoTen
                    }
                    into g
                    select new
                    {
                        MaNV = g.Key.MaNV,
                        HoTen = g.Key.HoTen,
                        SoVe = g.Count(),
                        DoanhThu = g.Sum(x => x.GiaVe)
                    };

                var dsTopNhanVien = topNhanVien
                    .OrderByDescending(x => x.SoVe)
                    .ThenByDescending(x => x.DoanhThu)
                    .Take(5)
                    .ToList();

                int stt = 1;

                foreach (var nv in dsTopNhanVien)
                {
                    dgvTopNhanVien.Rows.Add(
                        stt++,
                        nv.HoTen,
                        nv.SoVe,
                        nv.DoanhThu.ToString("N0")
                    );
                }
            }
        }

        private void ChonMenu(Guna.UI2.WinForms.Guna2Button btn)
        {
            btnTrangChu.FillColor = Color.FromArgb(70, 130, 220);
            btnQuanLyVe.FillColor = Color.FromArgb(70, 130, 220);
            btnChuyenXe.FillColor = Color.FromArgb(70, 130, 220);
            btnTuyenXe.FillColor = Color.FromArgb(70, 130, 220);
            btnXe.FillColor = Color.FromArgb(70, 130, 220);
            btnTaiXe.FillColor = Color.FromArgb(70, 130, 220);
            btnNhanVien.FillColor = Color.FromArgb(70, 130, 220);
            btnThongKe.FillColor = Color.FromArgb(70, 130, 220);

            // Nút đang chọn
            btn.FillColor = Color.FromArgb(35, 85, 180);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FrmTrangChuAdmin frm = new FrmTrangChuAdmin();
            frm.Show();
            this.Hide();
        }

        private void btnQuanLyVe_Click(object sender, EventArgs e)
        {

        }

        private void btnChuyenXe_Click(object sender, EventArgs e)
        {
            FrmChuyenXe frm = new FrmChuyenXe();
            frm.Show();
            this.Hide();
        }

        private void btnTuyenXe_Click(object sender, EventArgs e)
        {
            FrmTuyenXe frm = new FrmTuyenXe();
            frm.Show();
            this.Hide();
        }

        private void btnXe_Click(object sender, EventArgs e)
        {
            FrmQuanLyXe frm = new FrmQuanLyXe();
            frm.Show();
            this.Hide();
        }

        private void btnTaiXe_Click(object sender, EventArgs e)
        {
            FrmTaiXe frm = new FrmTaiXe();
            frm.Show();
            this.Hide();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FrmNhanVien frm = new FrmNhanVien();
            frm.Show();
            this.Hide();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ChonMenu(btnThongKe);
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
    }
}
