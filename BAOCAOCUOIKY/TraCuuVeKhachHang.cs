using BAOCAOCUOIKY.Models;
using DocumentFormat.OpenXml.EMMA;
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
        private string noiDi = "";
        private string noiDen = "";
        private DateTime ngayDi = DateTime.Today;
        private string soLuongVe = "";
        public FrmTraCuuVe(string noiDi, string noiDen, DateTime ngayDi, string soLuongVe)
        {
            InitializeComponent();
            lblTraCuuVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Bold);

            this.noiDi = noiDi;
            this.noiDen = noiDen;
            this.ngayDi = ngayDi;
            this.soLuongVe = soLuongVe;
            cboNoiDi.Text = noiDi;
            cboNoiDen.Text = noiDen;
            dtpNgayDi.Value = ngayDi;
            cboSoLuongVe.Text = soLuongVe;
            if (!string.IsNullOrEmpty(noiDi) &&
                !string.IsNullOrEmpty(noiDen))
            {
                TimChuyen();
            }

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
            lblHoaDon.ForeColor = Color.FromArgb(0, 102, 204);
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Bold);
        }

        private void HienThiChuyenHomNay()
        {
            DateTime ngayHomNay = DateTime.Today;

            using (var db = new QuanLyVeXeModel())
            {
                var dsChuyen = db.ChuyenXes
                    .Where(c =>
                        c.NgayKhoiHanh == ngayHomNay &&
                        c.TrangThai != "Đã hủy"
                    )
                    .Select(c => new
                    {
                        MaChuyen = c.MaChuyen,
                        TenTuyen = c.TuyenXe.TenTuyen,
                        NgayDi = c.NgayKhoiHanh,
                        GioDi = c.GioKhoiHanh,
                        GioDen = c.GioDenDuKien,
                        GiaVe = c.GiaVe
                    })
                    .OrderBy(c => c.GioDi)
                    .ToList();

                dgvVe.Rows.Clear();

                int stt = 1;

                foreach (var chuyen in dsChuyen)
                {
                    int rowIndex = dgvVe.Rows.Add(
                        stt++,
                        chuyen.TenTuyen,
                        chuyen.NgayDi.ToString("dd/MM/yyyy"),
                        chuyen.GioDi.ToString(@"hh\:mm"),
                        chuyen.GioDen.Value.ToString(@"hh\:mm"),
                        chuyen.GiaVe.ToString("N0") + " đ"
                    );

                    // Lưu mã chuyến
                    dgvVe.Rows[rowIndex].Tag = chuyen.MaChuyen;
                }
            }
        }

        private void FrmTraCuuVe_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;

            if (string.IsNullOrWhiteSpace(noiDi) ||
        string.IsNullOrWhiteSpace(noiDen))
            {
                HienThiChuyenHomNay();
            }
        }
        private void TimChuyen()
        {
            string diemDi = noiDi.Trim();
            string diemDen = noiDen.Trim();
            DateTime ngay = ngayDi.Date;

            using (var db = new QuanLyVeXeModel())
            {
                var dsChuyen = db.ChuyenXes
                    .Where(c =>
                        c.NgayKhoiHanh == ngay &&
                        c.TrangThai != "Đã hủy" &&
                        c.TuyenXe.BenXe.TenBenXe == diemDi &&
                        c.TuyenXe.BenXe1.TenBenXe == diemDen
                    )
                    .Select(c => new
                    {
                        MaChuyen = c.MaChuyen,
                        TenTuyen = c.TuyenXe.TenTuyen,
                        NgayDi = c.NgayKhoiHanh,
                        GioDi = c.GioKhoiHanh,
                        GioDen = c.GioDenDuKien,
                        GiaVe = c.GiaVe
                    })
                    .OrderBy(c => c.GioDi)
                    .ToList();

                dgvVe.Rows.Clear();

                int stt = 1;

                foreach (var chuyen in dsChuyen)
                {
                    int rowIndex = dgvVe.Rows.Add(
                        stt++,
                        chuyen.TenTuyen,
                        chuyen.NgayDi.ToString("dd/MM/yyyy"),
                        chuyen.GioDi.ToString(@"hh\:mm"),
                        chuyen.GioDen.HasValue
                            ? chuyen.GioDen.Value.ToString(@"hh\:mm")
                            : "",
                        chuyen.GiaVe.ToString("N0") + " đ"
                    );

                    dgvVe.Rows[rowIndex].Tag = chuyen.MaChuyen;
                }

                if (dsChuyen.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy chuyến xe phù hợp!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboNoiDi.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn nơi đi!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboNoiDi.Focus();
                return;
            }

            if (cboNoiDen.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn nơi đến!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboNoiDen.Focus();
                return;
            }

            if (cboNoiDi.Text == cboNoiDen.Text)
            {
                MessageBox.Show(
                    "Nơi đi và nơi đến không được giống nhau!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Lấy thẳng dữ liệu từ ComboBox
            string noiDi = cboNoiDi.Text.Trim();
            string noiDen = cboNoiDen.Text.Trim();
            DateTime ngayDi = dtpNgayDi.Value.Date;
            string soLuongVe = cboSoLuongVe.Text;

            // Mở form tra cứu
            FrmTraCuuVe frm = new FrmTraCuuVe(
                noiDi,
                noiDen,
                ngayDi,
                soLuongVe);

            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnXoaLoc_Click(object sender, EventArgs e)
        {
            // Xóa các ô tìm kiếm
            cboNoiDi.SelectedIndex = -1;
            cboNoiDi.Text = "";

            cboNoiDen.SelectedIndex = -1;
            cboNoiDen.Text = "";

            cboSoLuongVe.SelectedIndex = -1;
            cboSoLuongVe.Text = "";

            // Đưa ngày về hôm nay
            dtpNgayDi.Value = DateTime.Today;

            // Xóa thông tin tìm kiếm cũ
            noiDi = "";
            noiDen = "";
            ngayDi = DateTime.Today;
            soLuongVe = "";

            // Hiện lại tất cả chuyến hôm nay
            HienThiChuyenHomNay();
        }

        private void dgvVe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvVe.Rows[e.RowIndex].Cells[1].Value == null)
                return;

            string tenTuyen = dgvVe.Rows[e.RowIndex]
                .Cells[1].Value.ToString();

            string ngay = dgvVe.Rows[e.RowIndex]
                .Cells[2].Value.ToString();

            string gioDi = dgvVe.Rows[e.RowIndex]
                .Cells[3].Value.ToString();

            MessageBox.Show(
                "Tuyến: " + tenTuyen +
                "\nNgày đi: " + ngay +
                "\nGiờ khởi hành: " + gioDi,
                "Thông tin chuyến",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void dgvVe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvVe.Rows[e.RowIndex];

            if (row.Tag == null)
            {
                MessageBox.Show("Không xác định được chuyến xe!");
                return;
            }

            string maChuyen = row.Tag.ToString();

            string tenTuyen = row.Cells[1].Value.ToString();
            DateTime ngayDi = DateTime.Parse(row.Cells[2].Value.ToString());

            string[] tachTuyen = tenTuyen.Split('→');

            if (tachTuyen.Length != 2)
            {
                MessageBox.Show("Không xác định được tuyến xe!");
                return;
            }

            string noiDi = tachTuyen[0].Trim();
            string noiDen = tachTuyen[1].Trim();

            FrmDatVe frm = new FrmDatVe(
                "Bến xe " + noiDi,
                "Bến xe " + noiDen,
                ngayDi
            );

            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
