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
            HienThiThongKe();
            HienThiBieuDoDoanhThu();
            HienThiBieuDoTyLeVe();
            HienThiChuyenXeSapKhoiHanh();
            HienThiTuyenXeNoiBat();
        }
        private void HienThiThongKe()
        {
            using (var db = new QuanLyVeXeModel())
            {
                DateTime homNay = DateTime.Today;

                // 1. Tổng số tuyến
                int tongSoTuyen = db.TuyenXes.Count();

                // 2. Chuyến xe hôm nay
                int chuyenXeHomNay = db.ChuyenXes
                    .Count(x => x.NgayKhoiHanh == homNay);

                // 3. Vé đã đặt hôm nay
                int veDaBanHomNay = db.VeXes
                    .Count(x => x.NgayDat == homNay);

                // 4. Doanh thu hôm nay
                decimal doanhThuHomNay = db.HoaDons
                    .Where(x => x.NgayLap == homNay)
                    .Select(x => (decimal?)x.TongTien)
                    .Sum() ?? 0;

                // Hiển thị lên giao diện
                lblTongSoTuyen.Text = tongSoTuyen.ToString();
                lblChuyenXeHomNay.Text = chuyenXeHomNay.ToString();
                lblVeDaBanHomNay.Text = veDaBanHomNay.ToString();
                lblDoanhThuHomNay.Text = doanhThuHomNay.ToString("#,##0");
            }
        }
        //NÀY CHỈ LÀ DỮ LIỆU MẪU
        private void HienThiBieuDoDoanhThu()
        {
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();

            // Tạo khu vực biểu đồ
            ChartArea area = new ChartArea("DoanhThu");
            chartDoanhThu.ChartAreas.Add(area);

            // Tạo đường doanh thu
            Series series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 7;

            // 7 ngày gần nhất
            DateTime ngayHienTai = DateTime.Today;

            double[] doanhThu =
            {
        1500000,
        2100000,
        1800000,
        2500000,
        1700000,
        2900000,
        2350000
    };

            for (int i = 0; i < 7; i++)
            {
                DateTime ngay = ngayHienTai.AddDays(i - 6);

                series.Points.AddXY(
                    ngay.ToString("dd/MM"),
                    doanhThu[i]
                );
            }

            chartDoanhThu.Series.Add(series);

            // Tiêu đề trục
            area.AxisX.Title = "Ngày";
            area.AxisY.Title = "Doanh thu (VNĐ)";

            // Hiển thị tiền
            area.AxisY.LabelStyle.Format = "#,##0";

            // Hiển thị đường lưới
            area.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            area.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        }
        //NÀY CHỈ LÀ DỮ LIỆU MẪU
        private void HienThiBieuDoTyLeVe()
        {
            chartTyLeVe.Series.Clear();
            chartTyLeVe.ChartAreas.Clear();

            // Tạo khu vực biểu đồ
            ChartArea area = new ChartArea("TyLeVe");
            chartTyLeVe.ChartAreas.Add(area);

            // Tạo biểu đồ tròn
            Series series = new Series("Trạng thái vé");
            series.ChartType = SeriesChartType.Doughnut;

            // Dữ liệu mẫu
            series.Points.AddXY("Đã đặt", 86);
            series.Points.AddXY("Đã thanh toán", 45);
            series.Points.AddXY("Đã hủy", 15);
            series.Points.AddXY("Chờ thanh toán", 10);

            // Hiển thị tên + phần trăm
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT{P0}";

            // Đưa chú thích ra ngoài
            series.LegendText = "#VALX";

            chartTyLeVe.Series.Add(series);

            // Tiêu đề
            chartTyLeVe.Titles.Clear();
            chartTyLeVe.Titles.Add("TỶ LỆ VÉ THEO TRẠNG THÁI");
        }
        //NÀY CHỈ LÀ DỮ LIỆU MẪU
        private void HienThiChuyenXeSapKhoiHanh()
        {
            using (var db = new QuanLyVeXeModel())
            {
                DateTime hienTai = DateTime.Now;

                var danhSach = db.ChuyenXes
                    .Where(x => x.NgayKhoiHanh >= hienTai)
                    .OrderBy(x => x.NgayKhoiHanh)
                    .Take(10)
                    .ToList();

                dgvChuyenXeSapKhoiHanh.Rows.Clear();

                int stt = 1;

                foreach (var chuyen in danhSach)
                {
                    dgvChuyenXeSapKhoiHanh.Rows.Add(
                        stt++,
                        chuyen.MaChuyen,
                        chuyen.TuyenXe != null
                            ? chuyen.TuyenXe.TenTuyen
                            : "",
                        chuyen.Xe != null
                            ? chuyen.Xe.BienSo
                            : "",
                        chuyen.TaiXe != null
                            ? chuyen.TaiXe.HoTen
                            : "",
                        chuyen.NgayKhoiHanh.ToString("dd/MM/yyyy HH:mm"),
                        chuyen.TrangThai
                    );
                }
            }
        }
        private void HienThiTuyenXeNoiBat()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var danhSach = db.TuyenXes
                    .Select(t => new
                    {
                        TenTuyen = t.TenTuyen,

                        SoVe = t.ChuyenXes
                            .SelectMany(c => c.VeXes)
                            .Count(),

                        SoChuyen = t.ChuyenXes.Count(),

                        DoanhThu = t.ChuyenXes
                            .SelectMany(c => c.VeXes)
                            .Select(v => (decimal?)v.GiaVe)
                            .Sum() ?? 0
                    })
                    .OrderByDescending(x => x.SoVe)
                    .Take(3)
                    .ToList();

                // Xóa dữ liệu cũ
                lblTenTuyen1.Text = "";
                lblSoChuyen1.Text = "";
                lblVeDaBan1.Text = "";
                lblDoanhThu1.Text = "";

                lblTenTuyen2.Text = "";
                lblSoChuyen2.Text = "";
                lblVeDaBan2.Text = "";
                lblDoanhThu2.Text = "";

                lblTenTuyen3.Text = "";
                lblSoChuyen3.Text = "";
                lblVeDaBan3.Text = "";
                lblDoanhThu3.Text = "";

                // Hiển thị 3 tuyến
                if (danhSach.Count > 0)
                {
                    lblTenTuyen1.Text = danhSach[0].TenTuyen;
                    lblSoChuyen1.Text = danhSach[0].SoChuyen + " chuyến";
                    lblVeDaBan1.Text = danhSach[0].SoVe + " vé";
                    lblDoanhThu1.Text = danhSach[0].DoanhThu.ToString("#,##0") + " VND";
                }

                if (danhSach.Count > 1)
                {
                    lblTenTuyen2.Text = danhSach[1].TenTuyen;
                    lblSoChuyen2.Text = danhSach[1].SoChuyen + " chuyến";
                    lblVeDaBan2.Text = danhSach[1].SoVe + " vé";
                    lblDoanhThu2.Text = danhSach[1].DoanhThu.ToString("#,##0") + " VND";
                }

                if (danhSach.Count > 2)
                {
                    lblTenTuyen3.Text = danhSach[2].TenTuyen;
                    lblSoChuyen3.Text = danhSach[2].SoChuyen + " chuyến";
                    lblVeDaBan3.Text = danhSach[2].SoVe + " vé";
                    lblDoanhThu3.Text = danhSach[2].DoanhThu.ToString("#,##0") + " VND";
                }
            }
        }
    }
}
