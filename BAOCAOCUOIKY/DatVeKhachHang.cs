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
    public partial class FrmDatVe : Form
    {
        private List<string> gheDangChon = new List<string>();
        private bool daChonChuyen = false;
        private string gioDiDangChon = "";
        private string gioDenDangChon = "";
        private string noiDiDangChon = "";
        private string noiDenDangChon = "";
        private string bienSoDangChon = "";
        private decimal giaVeDangChon = 0;
        public FrmDatVe(string noiDi, string noiDen, DateTime ngayDi)
        {
            InitializeComponent();
            lblDatVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Bold);

            btnThanhToan.Enabled = false;
            for (int i = 1; i <= 29; i++)
            {
                var btn = pnlXe.Controls["btnGhe" + i]
                    as Guna.UI2.WinForms.Guna2GradientButton;

                if (btn != null)
                {
                    btn.Enabled = true;
                    btn.Click += btnGhe_Click;
                }
            }

            // Hiện thông tin chuyến xe được truyền từ trang chủ
            cboNoiDi.Text = noiDi;
            cboNoiDen.Text = noiDen;
            dtpNgayDi.Value = ngayDi;

            // Mặc định 1 vé
            cboSoLuongVe.Text = "1 vé";
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
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
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
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
            // MỞ FORM TRA CỨU VÉ
            FrmTraCuuVe frm = new FrmTraCuuVe("", "", DateTime.Today, "");
            this.Hide();
            frm.ShowDialog();
            this.Show();
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
            lblHoaDon.ForeColor = Color.FromArgb(0, 102, 204);
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Bold);
            FrmHoaDon frm = new FrmHoaDon(ThongTinDangNhap.MaTK);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void FrmDatVe_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;
            if (!string.IsNullOrWhiteSpace(cboNoiDi.Text) &&
                !string.IsNullOrWhiteSpace(cboNoiDen.Text))
            {
                btnTimKiem_Click(null, null);
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboNoiDi.Text))
            {
                MessageBox.Show("Vui lòng chọn nơi đi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(cboNoiDen.Text))
            {
                MessageBox.Show("Vui lòng chọn nơi đến!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboNoiDi.Text == cboNoiDen.Text)
            {
                MessageBox.Show("Nơi đi và nơi đến không được giống nhau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngayDi = dtpNgayDi.Value.Date;
            using (var db = new QuanLyVeXeModel())
            {
                var benDi = db.BenXes.FirstOrDefault(x =>
                    x.TenBenXe == cboNoiDi.Text);

                var benDen = db.BenXes.FirstOrDefault(x =>
                    x.TenBenXe == cboNoiDen.Text);

                if (benDi == null || benDen == null)
                {
                    MessageBox.Show("Không tìm thấy tuyến xe!");
                    return;
                }

                var tuyen = db.TuyenXes.FirstOrDefault(x =>
                    x.MaBenXeDi == benDi.MaBenXe &&
                    x.MaBenXeDen == benDen.MaBenXe);
                if (tuyen == null)
                {
                    MessageBox.Show("Không tìm thấy tuyến xe!");
                    return;
                }
                dgvDanhSachChuyen.Rows.Clear();

                var dsChuyen = db.ChuyenXes
                    .Include(x => x.Xe)
                    .Where(x => x.MaTuyen == tuyen.MaTuyen)
                    .Where(x => x.NgayKhoiHanh == ngayDi)
                    .Where(x => x.TrangThai == "Chưa khởi hành")
                    .OrderBy(x => x.GioKhoiHanh)
                    .ToList();

                if (dsChuyen.Count == 0)
                {
                    MessageBox.Show(
                        "Không có chuyến xe!\n" +
                        "Mã tuyến: " + tuyen.MaTuyen + "\n" +
                        "Ngày: " + ngayDi.ToString("dd/MM/yyyy"));
                    return;
                }

                foreach (var chuyen in dsChuyen)
                {
                    dgvDanhSachChuyen.Rows.Add(
                        chuyen.GioKhoiHanh.ToString(@"hh\:mm"),
                        chuyen.GioDenDuKien.HasValue
                            ? chuyen.GioDenDuKien.Value.ToString(@"hh\:mm")
                            : "",
                        cboNoiDi.Text,
                        cboNoiDen.Text,
                        chuyen.Xe.BienSo,
                        chuyen.GiaVe.ToString("N0")
                    );
                }
            }
        }

        private void dgvDanhSachChuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            DataGridViewRow row = dgvDanhSachChuyen.Rows[e.RowIndex];
            gioDiDangChon = row.Cells[0].Value?.ToString() ?? "";
            gioDenDangChon = row.Cells[1].Value?.ToString() ?? "";
            noiDiDangChon = row.Cells[2].Value?.ToString() ?? "";
            noiDenDangChon = row.Cells[3].Value?.ToString() ?? "";
            bienSoDangChon = row.Cells[4].Value?.ToString() ?? "";

            decimal.TryParse(
                row.Cells[5].Value?.ToString(),
                out giaVeDangChon
            );
            // Hiện thông tin chuyến
            lblTuyen7.Text = noiDiDangChon + " → " + noiDenDangChon;
            lblChuyen1.Text = gioDiDangChon + " → " + gioDenDangChon;
            lblBienSo1.Text = bienSoDangChon;
            lblXe4.Text = "29 chỗ";
            // Giá
            lblGia5.Text = giaVeDangChon.ToString("N0") + "đ";

            daChonChuyen = true;

            // Đọc SQL và tô ghế đã đặt
            HienThiGheDaDat();

            CapNhatThongTinHanhKhach();
            btnThanhToan.Enabled = gheDangChon.Count > 0;
        }
        private void CapNhatThongTinHanhKhach()
        {
            // Tuyến xe
            lblTuyen7.Text = noiDiDangChon + " → " + noiDenDangChon;

            // Chuyến xe
            lblChuyen1.Text = gioDiDangChon + " → " + gioDenDangChon;

            // Xe
            lblXe4.Text = "29 chỗ";

            //Biển số   
            lblBienSo1.Text = bienSoDangChon;

            // Ngày đi
            lblNgayDi2.Text = dtpNgayDi.Value.ToString("dd-MM-yyyy");

            // Ghế đã chọn
            if (gheDangChon.Count > 0)
            {
                lblGheDaChon1.Text = string.Join(", ", gheDangChon);
            }
            else
            {
                lblGheDaChon1.Text = "Chưa chọn";
            }
            // Số lượng vé
            int soLuong = gheDangChon.Count;
            lblSoLuong3.Text = gheDangChon.Count + " vé";

            // Giá 1 vé
            lblGia5.Text = giaVeDangChon.ToString("N0") + "đ";

            // Tạm tính
            decimal tamTinh =
                giaVeDangChon * soLuong;

            lblTien.Text =
                tamTinh.ToString("N0") + "đ";
            //Phí dịch vụ
            decimal phiDichVu = tamTinh * 0.1m; // Giả sử phí dịch vụ là 10%
            lblPhiDichVu.Text = phiDichVu.ToString("N0") + "đ";
            // Tổng cộng
            lblTongTien.Text =
                (tamTinh + phiDichVu).ToString("N0") + "đ";
        }
        private void btnGhe_Click(object sender, EventArgs e)
        {
            if (!daChonChuyen)
            {
                MessageBox.Show(
                    "Vui lòng chọn chuyến xe trước khi chọn ghế!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            Guna.UI2.WinForms.Guna2GradientButton btn = sender as Guna.UI2.WinForms.Guna2GradientButton;

            if (btn == null)
                return;

            string soGhe = btn.Text.Trim();

            // Đã chọn -> bỏ chọn
            if (gheDangChon.Contains(soGhe))
            {
                gheDangChon.Remove(soGhe);

                // Màu ghế trống
                btn.FillColor = Color.MediumPurple;
                btn.FillColor2 = Color.MediumPurple;
                btn.ForeColor = Color.White;
            }
            else
            {
                // Lấy số vé cần đặt
                int soLuongVe = int.Parse(cboSoLuongVe.Text.Split(' ')[0]);

                // Không cho chọn quá số vé
                if (gheDangChon.Count >= soLuongVe)
                {
                    MessageBox.Show(
                        "Bạn chỉ được chọn " + soLuongVe + " ghế!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                // Chọn ghế
                gheDangChon.Add(soGhe);

                // Màu ghế đang chọn
                btn.FillColor = Color.LightPink;
                btn.FillColor2 = Color.LightPink;
                btn.ForeColor = Color.Black;
                CapNhatThongTinHanhKhach();
            }

            // Hiện ghế đã chọn
            if (gheDangChon.Count == 0)
            {
                lblGheDaChon.Text = "Ghế đã chọn:";
            }
            else
            {// Hiển thị ghế đã chọn
                lblGheDaChon.Text = "Ghế đã chọn: " + string.Join(", ", gheDangChon) + " - Tổng: " + gheDangChon.Count + " ghế";
                
                CapNhatThongTinHanhKhach();
                btnThanhToan.Enabled = daChonChuyen && gheDangChon.Count > 0;
            }
        }
        private void cboSoLuongVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            gheDangChon.Clear();

            for (int i = 1; i <= 29; i++)
            {
                var btn = pnlXe.Controls["btnGhe" + i]
                    as Guna.UI2.WinForms.Guna2GradientButton;

                if (btn != null)
                {
                    btn.FillColor = Color.MediumPurple;
                    btn.FillColor2 = Color.MediumPurple;
                    btn.ForeColor = Color.White;
                }
            }
            // Tô lại ghế đã đặt → XANH DƯƠNG
            HienThiGheDaDat();
            lblGheDaChon.Text = "Ghế đã chọn:";

            CapNhatThongTinHanhKhach();
        }
        private void HienThiGheDaDat()
        {
            if (!daChonChuyen)
                return;

            using (var db = new QuanLyVeXeModel())
            {
                // Tìm chuyến đang chọn
                TimeSpan gioKhoiHanh;

                if (!TimeSpan.TryParse(gioDiDangChon, out gioKhoiHanh))
                    return;

                var chuyen = db.ChuyenXes.FirstOrDefault(x =>
                    x.NgayKhoiHanh == dtpNgayDi.Value.Date &&
                    x.GioKhoiHanh == gioKhoiHanh &&
                    x.Xe.BienSo == bienSoDangChon);

                if (chuyen == null)
                    return;

                // Lấy danh sách ghế đã đặt
                var gheDaDat = db.VeXes
                    .Where(v =>
                        v.MaChuyen == chuyen.MaChuyen &&
                        v.TrangThai != "Đã hủy")
                    .Select(v => v.MaGhe)
                    .ToList();

                // Tô màu 29 ghế
                for (int i = 1; i <= 29; i++)
                {
                    var btn = pnlXe.Controls["btnGhe" + i]
                        as Guna.UI2.WinForms.Guna2GradientButton;

                    if (btn == null)
                        continue;

                    var ghe = db.Ghes.FirstOrDefault(x =>
                        x.MaXe == chuyen.MaXe &&
                        x.SoGhe == i);

                    if (ghe != null && gheDaDat.Contains(ghe.MaGhe))
                    {
                        // ĐÃ ĐẶT → XANH DƯƠNG
                        btn.FillColor = Color.Blue;
                        btn.FillColor2 = Color.Blue;
                        btn.ForeColor = Color.White;

                        // Giữ màu xanh khi khóa nút
                        btn.DisabledState.FillColor = Color.Blue;
                        btn.DisabledState.FillColor2 = Color.Blue;
                        btn.DisabledState.ForeColor = Color.White;

                        btn.Enabled = false;
                    }
                    else
                    {
                        // GHẾ TRỐNG → TÍM
                        btn.FillColor = Color.MediumPurple;
                        btn.FillColor2 = Color.MediumPurple;
                        btn.ForeColor = Color.White;
                        btn.Enabled = true;
                    }
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int soLuong = cboSoLuongVe.SelectedIndex + 1;

            if (gheDangChon.Count != soLuong)
            {
                MessageBox.Show(
                    "Bạn đang chọn " + soLuong + " vé.\n" +
                    "Vui lòng chọn đúng " + soLuong + " ghế!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!daChonChuyen)
            {
                MessageBox.Show("Vui lòng chọn chuyến xe!");
                return;
            }

            if (gheDangChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ghế!");
                return;
            }

            // Tạm tính
            decimal tamTinh = giaVeDangChon * soLuong;

            // Phí dịch vụ 10%
            decimal phiDichVu = tamTinh * 0.1m;

            // Tổng tiền có phí
            decimal tongTien = tamTinh + phiDichVu;

            FrmThanhToan frm = new FrmThanhToan(
                noiDiDangChon,
                noiDenDangChon,
                gioDiDangChon,
                gioDenDangChon,
                dtpNgayDi.Value,
                bienSoDangChon,
                string.Join(", ", gheDangChon),
                soLuong,
                giaVeDangChon,
                tongTien
            );

            frm.ShowDialog();

            // Cập nhật lại trạng thái ghế từ SQL
            HienThiGheDaDat();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Xóa chuyến đã chọn
            daChonChuyen = false;
            gioDiDangChon = "";
            gioDenDangChon = "";
            noiDiDangChon = "";
            noiDenDangChon = "";
            bienSoDangChon = "";
            giaVeDangChon = 0;

            // Xóa ghế
            gheDangChon.Clear();

            // Xóa danh sách chuyến
            dgvDanhSachChuyen.Rows.Clear();

            // Reset nơi đi, nơi đến
            cboNoiDi.SelectedIndex = -1;
            cboNoiDen.SelectedIndex = -1;

            // Reset ngày
            dtpNgayDi.Value = DateTime.Today;

            // Reset số lượng vé
            cboSoLuongVe.Text = "1 vé";

            // Reset màu 29 ghế
            for (int i = 1; i <= 29; i++)
            {
                var btn = pnlXe.Controls["btnGhe" + i]
                    as Guna.UI2.WinForms.Guna2GradientButton;

                if (btn != null)
                {
                    btn.Enabled = true;
                    btn.BackColor = Color.MediumPurple;
                    btn.ForeColor = Color.White;
                }
            }

            // Reset thông tin bên phải
            lblTuyen7.Text = "";
            lblChuyen1.Text = "";
            lblXe4.Text = "";
            lblBienSo1.Text = "";
            lblNgayDi2.Text = "";
            lblGheDaChon1.Text = "Chưa chọn";
            lblGia5.Text = "0đ";
            lblSoLuong3.Text = "0 vé";
            lblTien.Text = "0đ";
            lblPhiDichVu.Text = "0đ";
            lblTongTien.Text = "0đ";

            // Reset thông tin ghế
            lblGheDaChon.Text = "Ghế đã chọn:";

            // Khóa nút thanh toán
            btnThanhToan.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
