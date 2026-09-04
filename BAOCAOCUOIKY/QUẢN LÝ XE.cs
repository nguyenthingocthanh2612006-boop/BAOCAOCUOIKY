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
    public partial class FrmQuanLyXe : Form
    {
        private bool dangThemXe = false;
        public FrmQuanLyXe()
        {
            InitializeComponent();
            LoadDanhSachXe();
            ChonMenu(btnXe);

            cboTrangThaiTT.Items.Clear();
            cboTrangThaiTT.Items.Add("Đang hoạt động");
            cboTrangThaiTT.Items.Add("Không hoạt động");

            cboTrangThaiTT.SelectedIndex = -1;

            txtMaXeTT.ReadOnly = true;
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

            btn.FillColor = Color.FromArgb(35, 85, 180);
        }

        private void LoadDanhSachXe()
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    var ds = db.Xes
                        .Select(x => new
                        {
                            x.MaXe,
                            x.BienSo,
                            x.HangXe,
                            x.NamSanXuat,
                            x.MauXe,
                            x.TrangThai
                        })
                        .ToList();

                    dgvXe.AutoGenerateColumns = false;
                    dgvXe.DataSource = ds;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách xe: " + ex.Message);
            }
        }

        private void FrmQuanLyXe_Load(object sender, EventArgs e)
        {
            LoadTrangThai();
            cboTrangThaiTK.Items.Clear();

            cboTrangThaiTK.Items.Add("Đang hoạt động");
            cboTrangThaiTK.Items.Add("Bảo trì");
            cboTrangThaiTK.Items.Add("Ngừng hoạt động");

            cboTrangThaiTK.SelectedIndex = -1;


            DateTime now = DateTime.Now;

            if (now.DayOfWeek == DayOfWeek.Sunday)
                lblNgay.Text = $"Chủ Nhật, {now:dd/MM/yyyy}";
            else
                lblNgay.Text = $"Thứ {(int)now.DayOfWeek + 1}, {now:dd/MM/yyyy}";

            lblGio.Text = now.ToString("HH:mm:ss");

            lblAdmin.Text = "ADMIN";

            LoadDanhSachXe();
        }

        private void LoadTrangThai()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var dsTrangThai = db.Xes
                    .Select(x => x.TrangThai)
                    .Distinct()
                    .ToList();

                cboTrangThaiTK.Items.Clear();

                foreach (var item in dsTrangThai)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        cboTrangThaiTK.Items.Add(item);
                    }
                }

                cboTrangThaiTK.SelectedIndex = -1;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnltrangchu_Click(object sender, EventArgs e)
        {

        }

        private void pnlNoiDung_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachXe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvXe.Rows[e.RowIndex];

            txtMaXeTT.Text = row.Cells[1].Value?.ToString();
            txtBienSoTT.Text = row.Cells[2].Value?.ToString();
            txtHangXeTT.Text = row.Cells[3].Value?.ToString();
            txtNamSXTT.Text = row.Cells[4].Value?.ToString();
            txtMauXeTT.Text = row.Cells[5].Value?.ToString();
            cboTrangThaiTT.Text = row.Cells[6].Value?.ToString();
        }

        private void pnlTimKiem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void txtBienSo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cboLoaiXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cboSoCho_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtBienSoTK.Clear();
            txtHangXeTK.Clear();
            cboTrangThaiTK.SelectedIndex = -1;

            LoadDanhSachXe();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    string bienSo = txtBienSoTK.Text.Trim();
                    string hangXe = txtHangXeTK.Text.Trim();
                    string trangThai = cboTrangThaiTK.Text.Trim();

                    var query = db.Xes.AsQueryable();

                    // Tìm theo biển số
                    if (!string.IsNullOrEmpty(bienSo))
                    {
                        query = query.Where(x => x.BienSo.Contains(bienSo));
                    }

                    // Tìm theo hãng xe
                    if (!string.IsNullOrEmpty(hangXe))
                    {
                        query = query.Where(x => x.HangXe.Contains(hangXe));
                    }

                    // Tìm theo trạng thái
                    if (!string.IsNullOrEmpty(trangThai))
                    {
                        query = query.Where(x => x.TrangThai == trangThai);
                    }

                    var ds = query
                        .Select(x => new
                        {
                            x.MaXe,
                            x.BienSo,
                            x.HangXe,
                            x.NamSanXuat,
                            x.MauXe,
                            x.TrangThai
                        })
                        .ToList();

                    dgvXe.DataSource = ds;

                    // Đánh lại STT
                    for (int i = 0; i < dgvXe.Rows.Count; i++)
                    {
                        dgvXe.Rows[i].Cells["colSTT"].Value = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pnlDanhSach_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnThemXe_Click(object sender, EventArgs e)
        {
            dangThemXe = true;

            txtMaXeTT.Clear();
            txtBienSoTT.Clear();
            txtHangXeTT.Clear();
            txtNamSXTT.Clear();
            txtMauXeTT.Clear();

            cboTrangThaiTT.SelectedIndex = -1;

            // Khi thêm mới thì cho nhập mã xe
            txtMaXeTT.ReadOnly = false;

            txtMaXeTT.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void lblDanhSach_Click(object sender, EventArgs e)
        {

        }

        private void lblThongTinChiTiet_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaXe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaXeTT.Text))
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!");
                return;
            }

            string maXe = txtMaXeTT.Text.Trim();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn chuyển xe " + maXe + " thành Không hoạt động?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    var xe = db.Xes.FirstOrDefault(x => x.MaXe == maXe);

                    if (xe == null)
                    {
                        MessageBox.Show("Không tìm thấy xe!");
                        return;
                    }

                    // Chuyển trạng thái
                    xe.TrangThai = "Không hoạt động";

                    db.SaveChanges();
                }

                MessageBox.Show("Đã chuyển xe thành Không hoạt động!");

                // Load lại danh sách
                LoadDanhSachXe();

                // Hiển thị trạng thái mới bên thông tin chi tiết
                cboTrangThaiTT.Text = "Không hoạt động";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật trạng thái!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // =========================
                // KIỂM TRA DỮ LIỆU
                // =========================

                if (string.IsNullOrWhiteSpace(txtMaXeTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã xe!");
                    txtMaXeTT.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBienSoTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập biển số!");
                    txtBienSoTT.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHangXeTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập hãng xe!");
                    txtHangXeTT.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNamSXTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập năm sản xuất!");
                    txtNamSXTT.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMauXeTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập màu xe!");
                    txtMauXeTT.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cboTrangThaiTT.Text))
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!");
                    cboTrangThaiTT.Focus();
                    return;
                }

                int namSX;

                if (!int.TryParse(txtNamSXTT.Text.Trim(), out namSX))
                {
                    MessageBox.Show("Năm sản xuất phải là số!");
                    txtNamSXTT.Focus();
                    return;
                }


                // =========================
                // KẾT NỐI DATABASE
                // =========================

                using (var db = new QuanLyVeXeModel())
                {
                    string maXe = txtMaXeTT.Text.Trim();


                    // ==========================================
                    // TRƯỜNG HỢP 1: ĐANG THÊM XE
                    // ==========================================

                    if (dangThemXe)
                    {
                        // Kiểm tra mã xe đã tồn tại
                        var xeTonTai = db.Xes
                            .FirstOrDefault(x => x.MaXe == maXe);

                        if (xeTonTai != null)
                        {
                            MessageBox.Show(
                                "Mã xe " + maXe + " đã tồn tại!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            txtMaXeTT.Focus();
                            return;
                        }

                        // Tạo xe mới
                        Xe xeMoi = new Xe();

                        xeMoi.MaXe = maXe;
                        xeMoi.BienSo = txtBienSoTT.Text.Trim();
                        xeMoi.HangXe = txtHangXeTT.Text.Trim();
                        xeMoi.NamSanXuat = namSX;
                        xeMoi.MauXe = txtMauXeTT.Text.Trim();
                        xeMoi.TrangThai = cboTrangThaiTT.Text.Trim();

                        // Thêm vào database
                        db.Xes.Add(xeMoi);

                        db.SaveChanges();

                        MessageBox.Show(
                            "Thêm xe thành công!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }


                    // ==========================================
                    // TRƯỜNG HỢP 2: ĐANG SỬA XE
                    // ==========================================

                    else
                    {
                        // Tìm xe theo mã
                        var xe = db.Xes
                            .FirstOrDefault(x => x.MaXe == maXe);

                        if (xe == null)
                        {
                            MessageBox.Show(
                                "Không tìm thấy xe cần cập nhật!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        // Cập nhật thông tin
                        xe.BienSo = txtBienSoTT.Text.Trim();
                        xe.HangXe = txtHangXeTT.Text.Trim();
                        xe.NamSanXuat = namSX;
                        xe.MauXe = txtMauXeTT.Text.Trim();
                        xe.TrangThai = cboTrangThaiTT.Text.Trim();

                        db.SaveChanges();

                        MessageBox.Show(
                            "Cập nhật xe thành công!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }


                // =========================
                // SAU KHI LƯU
                // =========================

                dangThemXe = false;

                txtMaXeTT.ReadOnly = true;

                LoadDanhSachXe();

                // Xóa ô thông tin
                txtMaXeTT.Clear();
                txtBienSoTT.Clear();
                txtHangXeTT.Clear();
                txtNamSXTT.Clear();
                txtMauXeTT.Clear();

                cboTrangThaiTT.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void btnInThongTin_Click(object sender, EventArgs e)
        {

        }

        private void lblNgay_Click(object sender, EventArgs e)
        {

        }

        private void lblGio_Click(object sender, EventArgs e)
        {

        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {

        }

        private void picXe_Click(object sender, EventArgs e)
        {

        }

        private void dgvXe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 &&
        dgvXe.Columns[e.ColumnIndex].Name == "colSTT")
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FrmTrangChuAdmin frm = new FrmTrangChuAdmin();
            frm.Show();
            this.Hide();
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
            ChonMenu(btnXe);
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
            FrmThongKe frm = new FrmThongKe();
            frm.Show();
            this.Hide();
        }
    }
}
