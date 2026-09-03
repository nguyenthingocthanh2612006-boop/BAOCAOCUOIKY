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

namespace BAOCAOCUOIKY
{
    public partial class FrmTrangChuAdmin : Form
    {
        public FrmTrangChuAdmin()
        {
            InitializeComponent();

            LoadTuyenXeNoiBat();
            LoadThongKe();
            LoadBieuDoDoanhThu();
            HienThiTiLeVeTheoTrangThai();
            HienThiThongTinHeThong();
            HienThiChuyenXeSapKhoiHanh();
        }

        private void LoadThongKe()
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // =========================
                    // 1. TỔNG SỐ TUYẾN
                    // =========================
                    int soTuyen = db.TuyenXes.Count();

                    lblSoTuyen.Text = soTuyen.ToString();


                    // =========================
                    // 2. SỐ CHUYẾN XE HÔM NAY
                    // =========================
                    DateTime homNay = DateTime.Today;

                    int soChuyen = db.ChuyenXes.Count(c =>
                        c.NgayKhoiHanh == homNay);

                    lblSoChuyen.Text = soChuyen.ToString();


                    // =========================
                    // 3. SỐ VÉ ĐÃ BÁN HÔM NAY
                    // =========================
                    int soVe = db.VeXes.Count(v =>
                        v.NgayDat == homNay &&
                        v.TrangThai != "Đã hủy");

                    lblSoVe.Text = soVe.ToString();


                    // =========================
                    // 4. DOANH THU HÔM NAY
                    // =========================
                    decimal doanhThu = db.VeXes
                        .Where(v =>
                            v.NgayDat == homNay &&
                            v.TrangThai == "Đã thanh toán")
                        .Sum(v => (decimal?)v.GiaVe) ?? 0;

                    lblSoDoanhThu.Text = doanhThu.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải thống kê!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadBieuDoDoanhThu()
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // 7 ngày gần nhất, tính cả hôm nay
                    DateTime ngayCuoi = DateTime.Today;
                    DateTime ngayDau = ngayCuoi.AddDays(-6);

                    // Xóa dữ liệu cũ
                    chartDoanhThu.Series.Clear();

                    // Tạo đường biểu đồ
                    Series series = new Series("Doanh thu");
                    series.ChartType = SeriesChartType.Line;
                    series.BorderWidth = 3;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 7;

                    // Lấy doanh thu từng ngày
                    for (int i = 0; i < 7; i++)
                    {
                        DateTime ngay = ngayDau.AddDays(i);
                        DateTime ngayTiepTheo = ngay.AddDays(1);

                        decimal doanhThu = db.HoaDons
                            .Where(h =>
                                h.NgayLap >= ngay &&
                                h.NgayLap < ngayTiepTheo &&
                                h.TrangThai == "Đã thanh toán")
                            .Select(h => (decimal?)h.TongTien)
                            .Sum() ?? 0;

                        // Thêm ngày và doanh thu vào biểu đồ
                        series.Points.AddXY(
                            ngay.ToString("dd/MM"),
                            doanhThu
                        );
                    }

                    chartDoanhThu.Series.Add(series);

                    // Cấu hình trục X
                    chartDoanhThu.ChartAreas[0].AxisX.Title = "Ngày";

                    // Cấu hình trục Y
                    chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
                    chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";

                    // Hiển thị lưới
                    chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.Enabled = true;

                    // Tiêu đề
                    chartDoanhThu.Titles.Clear();
                    chartDoanhThu.Titles.Add("DOANH THU 7 NGÀY GẦN NHẤT");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải biểu đồ doanh thu!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void HienThiTiLeVeTheoTrangThai()
        {
            using (var db = new QuanLyVeXeModel())
            {
                DateTime homNay = DateTime.Today;
                DateTime ngayMai = homNay.AddDays(1);

                // Lấy dữ liệu trước, sau đó mới lọc ngày bằng C#
                var veXe = db.VeXes
                    .ToList()
                    .Where(v => v.NgayDat >= homNay &&
                                v.NgayDat < ngayMai)
                    .ToList();

                var duLieu = veXe
                    .GroupBy(v => v.TrangThai)
                    .Select(g => new
                    {
                        TrangThai = g.Key,
                        SoLuong = g.Count()
                    })
                    .ToList();

                chartTyLeVe.Series.Clear();
                chartTyLeVe.Legends.Clear();

                Legend legend = new Legend();
                legend.Docking = Docking.Right;
                chartTyLeVe.Legends.Add(legend);

                Series series = new Series("Tỷ lệ vé");
                series.ChartType = SeriesChartType.Doughnut;
                series.IsValueShownAsLabel = true;
                series.Label = "#PERCENT{P0}";

                foreach (var item in duLieu)
                {
                    DataPoint point = new DataPoint();

                    point.SetValueY(item.SoLuong);
                    point.LegendText = item.TrangThai;

                    series.Points.Add(point);
                }

                chartTyLeVe.Series.Add(series);
            }
        }

        private void HienThiThongTinHeThong()
        {
            using (var db = new QuanLyVeXeModel())
            {
                // Phạm vi hoạt động
                lblGiaTriPhamVi.Text = "Xe buýt nội tỉnh Đồng Tháp";

                // Số tuyến
                int soTuyen = db.TuyenXes.Count();
                lblGiaTriSoTuyen.Text = soTuyen + " tuyến";

                // Số xe
                int soXe = db.Xes.Count();
                lblGiaTriXe.Text = soXe + " xe";

                // Số tài xế
                int soTaiXe = db.TaiXes.Count();
                lblGiaTriTaiXe.Text = soTaiXe + " tài xế";

                // Số nhân viên
                int soNhanVien = db.NhanViens.Count();
                lblMoTaNhanVien.Text = soNhanVien + " nhân viên";

                // Thời gian hoạt động
                lblMoTaThoiGianHoatDong.Text = "05:00-18:00 Hằng ngày";
            }
        }

        private void HienThiChuyenXeSapKhoiHanh()
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    DateTime homNay = DateTime.Today;
                    DateTime denNgay = homNay.AddDays(7);

                    // Lấy các chuyến xe trong 7 ngày tới
                    var danhSach = db.ChuyenXes
                        .Where(c =>
                            c.NgayKhoiHanh >= homNay &&
                            c.NgayKhoiHanh < denNgay &&
                            c.TrangThai == "Chưa khởi hành")
                        .OrderBy(c => c.NgayKhoiHanh)
                        .ThenBy(c => c.GioKhoiHanh)
                        .ToList();

                    // Xóa dữ liệu cũ trên bảng
                    dgvChuyenXeSapKhoiHanh.Rows.Clear();

                    foreach (var c in danhSach)
                    {
                        // Tìm tuyến xe
                        var tuyen = db.TuyenXes
                            .FirstOrDefault(t => t.MaTuyen == c.MaTuyen);

                        string tenTuyen = "";
                        string diemDau = "";
                        string diemCuoi = "";

                        if (tuyen != null)
                        {
                            // Tên tuyến
                            tenTuyen = tuyen.TenTuyen;

                            // Tìm bến đầu
                            var benDau = db.BenXes
                                .FirstOrDefault(b => b.MaBenXe == tuyen.MaBenXeDi);

                            // Tìm bến cuối
                            var benCuoi = db.BenXes
                                .FirstOrDefault(b => b.MaBenXe == tuyen.MaBenXeDen);

                            if (benDau != null)
                            {
                                diemDau = benDau.TenBenXe
                                    .Replace("Bến xe ", "");
                            }

                            if (benCuoi != null)
                            {
                                diemCuoi = benCuoi.TenBenXe
                                    .Replace("Bến xe ", "");
                            }
                        }

                        // Thêm vào DataGridView
                        dgvChuyenXeSapKhoiHanh.Rows.Add(
                            c.MaChuyen,
                            tenTuyen,
                            c.MaXe,
                            c.MaTX,
                            c.GioKhoiHanh.ToString(@"hh\:mm"),
                            diemDau,
                            diemCuoi,
                            c.TrangThai
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải chuyến xe sắp khởi hành!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadTuyenXeNoiBat()
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // Lấy dữ liệu từ đúng 3 bảng:
                    // TuyenXe -> ChuyenXe -> VeXe

                    var tuyenXe = db.TuyenXes.ToList();
                    var chuyenXe = db.ChuyenXes.ToList();
                    var veXe = db.VeXes.ToList();

                    var danhSach = tuyenXe
                        .Select(t => new
                        {
                            MaTuyen = t.MaTuyen,
                            TenTuyen = t.TenTuyen,

                            // Số chuyến của tuyến
                            SoChuyen = chuyenXe.Count(c =>
                                c.MaTuyen == t.MaTuyen),

                            // Số vé đã bán/đặt, không tính vé đã hủy
                            SoVe = veXe.Count(v =>
                                v.TrangThai != "Đã hủy" &&
                                chuyenXe.Any(c =>
                                    c.MaChuyen == v.MaChuyen &&
                                    c.MaTuyen == t.MaTuyen)),

                            // Doanh thu chỉ tính vé đã thanh toán
                            DoanhThu = veXe
                                .Where(v =>
                                    v.TrangThai == "Đã thanh toán" &&
                                    chuyenXe.Any(c =>
                                        c.MaChuyen == v.MaChuyen &&
                                        c.MaTuyen == t.MaTuyen))
                                .Sum(v => (decimal?)v.GiaVe) ?? 0
                        })
                        .OrderByDescending(x => x.SoVe)
                        .ThenByDescending(x => x.DoanhThu)
                        .Take(3)
                        .ToList();


                    // ==============================
                    // XÓA DỮ LIỆU CŨ
                    // ==============================

                    lblTenTuyen1.Text = "";
                    lblSoChuyen1.Text = "";
                    lblSoVe1.Text = "";
                    lblTien1.Text = "";

                    lblTenTuyen2.Text = "";
                    lblSoChuyen2.Text = "";
                    lblSoVe2.Text = "";
                    lblTien2.Text = "";

                    lblTenTuyen3.Text = "";
                    lblSoChuyen3.Text = "";
                    lblSoVe3.Text = "";
                    lblTien3.Text = "";


                    // ==============================
                    // TUYẾN NỔI BẬT 1
                    // ==============================

                    if (danhSach.Count > 0)
                    {
                        var t = danhSach[0];

                        lblTenTuyen1.Text = "01 " + t.TenTuyen;
                        lblSoChuyen1.Text = t.SoChuyen + " chuyến";
                        lblSoVe1.Text = t.SoVe.ToString();
                        lblTien1.Text = t.DoanhThu.ToString("N0");
                    }


                    // ==============================
                    // TUYẾN NỔI BẬT 2
                    // ==============================

                    if (danhSach.Count > 1)
                    {
                        var t = danhSach[1];

                        lblTenTuyen2.Text = "02 " + t.TenTuyen;
                        lblSoChuyen2.Text = t.SoChuyen + " chuyến";
                        lblSoVe2.Text = t.SoVe.ToString();
                        lblTien2.Text = t.DoanhThu.ToString("N0");
                    }


                    // ==============================
                    // TUYẾN NỔI BẬT 3
                    // ==============================

                    if (danhSach.Count > 2)
                    {
                        var t = danhSach[2];

                        lblTenTuyen3.Text = "03 " + t.TenTuyen;
                        lblSoChuyen3.Text = t.SoChuyen + " chuyến";
                        lblSoVe3.Text = t.SoVe.ToString();
                        lblTien3.Text = t.DoanhThu.ToString("N0");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải tuyến xe nổi bật!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnTuyenXe_Click(object sender, EventArgs e)
        {
            FrmTuyenXe frm = new FrmTuyenXe();
            frm.ShowDialog();
        }


        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FrmNhanVien frm = new FrmNhanVien();
            frm.ShowDialog();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();

            FrmDangNhap frmDangNhap = new FrmDangNhap();
            frmDangNhap.ShowDialog();

            this.Close();
        }

        private void btnTaiXe_Click(object sender, EventArgs e)
        {
            FrmTaiXe frm = new FrmTaiXe();
            frm.ShowDialog();
        }

        private void btnXe_Click(object sender, EventArgs e)
        {
            FrmQuanLyXe frm = new FrmQuanLyXe();
            frm.Show();
        }
    }
}
