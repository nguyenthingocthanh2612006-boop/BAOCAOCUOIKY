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
        private string maChuyenDangChon = "";
        private string maXeDangChon = "";
        private decimal giaVeDangChon = 0;
        private List<string> gheDangChon = new List<string>();
        public FrmDatVe(string noiDi, string noiDen, DateTime ngayDi)
        {
            InitializeComponent();

            for (int i = 1; i <= 29; i++)
            {
                var btn = pnlXe.Controls["btnGhe" + i]
                    as Guna.UI2.WinForms.Guna2GradientButton;

                if (btn != null)
                {
                    btn.Enabled = false;
                    btn.Click += btnGhe_Click;
                }
            }

            LoadBenXe();

            cboNoiDi.Text = noiDi;
            cboNoiDen.Text = noiDen;
            dtpNgayDi.Value = ngayDi;
            lblDatVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Bold);
        }

        private void LoadBenXe()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var dsBenXe = db.BenXes
                    .Where(x => x.TrangThai == "Đang hoạt động")
                    .OrderBy(x => x.TenBenXe)
                    .ToList();

                cboNoiDi.DataSource = dsBenXe.ToList();
                cboNoiDi.DisplayMember = "TenBenXe";
                cboNoiDi.ValueMember = "MaBenXe";

                cboNoiDen.DataSource = dsBenXe.ToList();
                cboNoiDen.DisplayMember = "TenBenXe";
                cboNoiDen.ValueMember = "MaBenXe";
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
            // MỞ FORM TRA CỨU VÉ
            FrmTraCuuVe frm = new FrmTraCuuVe();
            this.Hide();
            frm.ShowDialog();
            this.Show();
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

        private void FrmDatVe_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (cboNoiDi.SelectedValue == null ||
       cboNoiDen.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn nơi đi và nơi đến!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string maBenDi = cboNoiDi.SelectedValue.ToString();
            string maBenDen = cboNoiDen.SelectedValue.ToString();

            if (maBenDi == maBenDen)
            {
                MessageBox.Show(
                    "Nơi đi và nơi đến không được giống nhau!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DateTime ngayDi = dtpNgayDi.Value.Date;

            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    var ds = (
                        from c in db.ChuyenXes
                        join t in db.TuyenXes
                            on c.MaTuyen equals t.MaTuyen
                        where t.MaBenXeDi == maBenDi
                           && t.MaBenXeDen == maBenDen
                           && c.NgayKhoiHanh == ngayDi
                           && c.TrangThai == "Chưa khởi hành"
                        orderby c.GioKhoiHanh
                        select c
                    ).ToList();

                    dgvDanhSachChuyen.Rows.Clear();

                    foreach (var c in ds)
                    {
                        dgvDanhSachChuyen.Rows.Add(
                            c.GioKhoiHanh.ToString(@"hh\:mm"),
                            c.GioDenDuKien.HasValue
                                ? c.GioDenDuKien.Value.ToString(@"hh\:mm")
                                : "",
                            cboNoiDi.Text,
                            cboNoiDen.Text,
                            c.MaXe,
                            c.GiaVe.ToString("N0")
                        );
                    }

                    if (ds.Count == 0)
                    {
                        MessageBox.Show(
                            "Không có chuyến xe phù hợp trong ngày này.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tìm chuyến xe:\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachChuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string bienSo = dgvDanhSachChuyen
                .Rows[e.RowIndex]
                .Cells["colBienSo"]
                .Value?.ToString();

            string gioDi = dgvDanhSachChuyen
                .Rows[e.RowIndex]
                .Cells["colGioDi"]
                .Value?.ToString();

            if (string.IsNullOrEmpty(bienSo) ||
                string.IsNullOrEmpty(gioDi))
                return;

            DateTime ngayDi = dtpNgayDi.Value.Date;

            using (var db = new QuanLyVeXeModel())
            {
                // Lấy các chuyến theo xe và ngày trước
                var danhSach = db.ChuyenXes
                    .Where(c =>
                        c.MaXe == bienSo &&
                        c.NgayKhoiHanh == ngayDi)
                    .ToList();

                // Sau khi lấy về C# mới đổi TimeSpan thành chuỗi để so sánh
                var chuyen = danhSach.FirstOrDefault(c =>
                    c.GioKhoiHanh.ToString(@"hh\:mm") == gioDi);

                if (chuyen == null)
                {
                    MessageBox.Show("Không tìm thấy chuyến xe!");
                    return;
                }

                maChuyenDangChon = chuyen.MaChuyen;
                maXeDangChon = chuyen.MaXe;
                giaVeDangChon = chuyen.GiaVe;

                gheDangChon.Clear();

                lblTuyen5.Text =
                    cboNoiDi.Text + " → " + cboNoiDen.Text;

                lblChuyenxe.Text =
                    chuyen.MaChuyen;

                lblXe3.Text =
                    chuyen.MaXe;

                lblNgayDi1.Text =
                    chuyen.NgayKhoiHanh.ToString("dd/MM/yyyy");

                lblNgayDi2.Text =
                    chuyen.GioKhoiHanh.ToString(@"hh\:mm");

                lblGia3.Text =
                    chuyen.GiaVe.ToString("N0") + "đ";

                lblGia5.Text =
                    chuyen.GiaVe.ToString("N0") + "đ";

                LoadTrangThaiGhe();
                CapNhatThongTinGhe();
            }
        }

        private void LoadTrangThaiGhe()
        {
            if (string.IsNullOrEmpty(maChuyenDangChon))
                return;

            // Mở tất cả 29 ghế trước
            for (int i = 1; i <= 29; i++)
            {
                var btn = pnlXe.Controls["btnGhe" + i]
                    as Guna.UI2.WinForms.Guna2GradientButton;

                if (btn != null)
                {
                    btn.Enabled = true;
                    btn.FillColor = Color.FromArgb(94, 148, 255);
                    btn.FillColor2 = Color.FromArgb(231, 76, 156);
                }
            }

            using (var db = new QuanLyVeXeModel())
            {
                // Những ghế đã có vé trong chuyến
                var gheDaDat = (
                    from v in db.VeXes
                    join g in db.Ghes
                        on v.MaGhe equals g.MaGhe
                    where v.MaChuyen == maChuyenDangChon
                       && v.TrangThai != "Đã hủy"
                       && g.MaXe == maXeDangChon
                    select g.SoGhe
                ).ToList();

                foreach (var soGhe in gheDaDat)
                {
                    var btn = pnlXe.Controls["btnGhe" + soGhe]
                        as Guna.UI2.WinForms.Guna2GradientButton;

                    if (btn != null)
                    {
                        btn.Enabled = false;
                        btn.FillColor = Color.LightGray;
                        btn.FillColor2 = Color.LightGray;
                    }
                }
            }
        }


        private void btnGhe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maChuyenDangChon))
            {
                MessageBox.Show("Vui lòng chọn chuyến xe trước!");
                return;
            }

            var btn = sender
                as Guna.UI2.WinForms.Guna2GradientButton;

            if (btn == null)
                return;

            string maGhe = "G" + btn.Text;

            // Đang chọn → bỏ chọn
            if (gheDangChon.Contains(maGhe))
            {
                gheDangChon.Remove(maGhe);

                btn.FillColor = Color.FromArgb(94, 148, 255);
                btn.FillColor2 = Color.FromArgb(231, 76, 156);
            }
            else
            {
                int soLuong = 1;

                if (!string.IsNullOrWhiteSpace(cboSoLuongVe.Text))
                {
                    string so =
                        cboSoLuongVe.Text.Replace(" vé", "").Trim();

                    int.TryParse(so, out soLuong);

                    if (soLuong <= 0)
                        soLuong = 1;
                }

                if (gheDangChon.Count >= soLuong)
                {
                    MessageBox.Show(
                        "Bạn chỉ được chọn " +
                        soLuong + " ghế!");
                    return;
                }

                gheDangChon.Add(maGhe);

                btn.FillColor = Color.LightPink;
                btn.FillColor2 = Color.LightPink;
            }

            CapNhatThongTinGhe();
        }

        private void CapNhatThongTinGhe()
        {
            int soLuong = gheDangChon.Count;

            decimal tongTien =
                soLuong * giaVeDangChon;

            lblGheDaChon1.Text =
                soLuong.ToString();

            lblSoLuong3.Text =
                soLuong + " vé";

            lblTien.Text =
                tongTien.ToString("N0") + "đ";

            lblTongTien.Text =
                tongTien.ToString("N0") + "đ";

            btnThanhToan.Enabled =
                !string.IsNullOrEmpty(maChuyenDangChon)
                && soLuong > 0;
        }

        private void cboSoLuongVe_SelectedIndexChanged(object sender, EventArgs e)
        {
            gheDangChon.Clear();

            LoadTrangThaiGhe();

            CapNhatThongTinGhe();
        }
    }
}
