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
    public partial class FrmBenXe : Form
    {
        QuanLyVeXeModel db = new QuanLyVeXeModel();
        private bool dangThem = false;
        public FrmBenXe()
        {
            InitializeComponent();

            LoadTrangThai();
            LoadDanhSachBenXe();
        }

        private void LoadTrangThai()
        {
            // ComboBox tìm kiếm
            cboTrangThaiTim.Items.Clear();
            cboTrangThaiTim.Items.Add("Tất cả");
            cboTrangThaiTim.Items.Add("Đang hoạt động");
            cboTrangThaiTim.Items.Add("Ngừng hoạt động");
            cboTrangThaiTim.SelectedIndex = 0;

            // ComboBox thông tin chi tiết
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Đang hoạt động");
            guna2ComboBox1.Items.Add("Ngừng hoạt động");
            guna2ComboBox1.SelectedIndex = 0;
        }

        // =========================
        // LOAD DANH SÁCH BẾN XE
        // =========================
        private void LoadDanhSachBenXe()
        {
            var ds = db.BenXes
                       .OrderBy(x => x.MaBenXe)
                       .ToList();

            dgvBenXe.Rows.Clear();

            int stt = 1;

            foreach (var b in ds)
            {
                dgvBenXe.Rows.Add(
                    stt++,
                    b.MaBenXe,
                    b.TenBenXe,
                    b.DiaChi,
                    b.TrangThai
                );
            }
        }


        private void btnBenXe_Click(object sender, EventArgs e)
        {

        }

        private void dgvBenXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dangThem = false;

            DataGridViewRow row = dgvBenXe.Rows[e.RowIndex];

            txtMaBenXe.Text = row.Cells[1].Value?.ToString();
            txtTenBeXe.Text = row.Cells[2].Value?.ToString();
            txtDiaChi.Text = row.Cells[3].Value?.ToString();
            guna2ComboBox1.Text = row.Cells[4].Value?.ToString();

            txtMaBenXe.Enabled = true;
            txtMaBenXe.ReadOnly = true;

            txtTenBeXe.Enabled = true;
            txtDiaChi.Enabled = true;
            guna2ComboBox1.Enabled = true;

            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maBenXe = txtMaBenXe.Text.Trim();
            string tenBenXe = txtTenBeXe.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string trangThai = guna2ComboBox1.Text.Trim();

            // Kiểm tra nhập đủ
            if (string.IsNullOrWhiteSpace(maBenXe) ||
                string.IsNullOrWhiteSpace(tenBenXe) ||
                string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(trangThai))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin bến xe!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // =========================
            // THÊM BẾN XE
            // =========================
            if (dangThem)
            {
                // Kiểm tra mã đã tồn tại
                bool tonTai = db.BenXes.Any(x => x.MaBenXe == maBenXe);

                if (tonTai)
                {
                    MessageBox.Show(
                        "Mã bến xe " + maBenXe + " đã tồn tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMaBenXe.Focus();
                    return;
                }

                BenXe benXeMoi = new BenXe();

                benXeMoi.MaBenXe = maBenXe;
                benXeMoi.TenBenXe = tenBenXe;
                benXeMoi.DiaChi = diaChi;
                benXeMoi.TrangThai = trangThai;

                db.BenXes.Add(benXeMoi);
                db.SaveChanges();

                MessageBox.Show(
                    "Thêm bến xe thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dangThem = false;

                LoadDanhSachBenXe();

                txtMaBenXe.ReadOnly = true;
            }
            // =========================
            // CẬP NHẬT BẾN XE
            // =========================
            else
            {
                var benXe = db.BenXes
                    .FirstOrDefault(x => x.MaBenXe == maBenXe);

                if (benXe == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy bến xe!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                benXe.TenBenXe = tenBenXe;
                benXe.DiaChi = diaChi;
                benXe.TrangThai = trangThai;

                db.SaveChanges();

                MessageBox.Show(
                    "Cập nhật bến xe thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadDanhSachBenXe();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maBenXe = txtMaBenXe.Text.Trim();

            if (string.IsNullOrWhiteSpace(maBenXe))
            {
                MessageBox.Show(
                    "Vui lòng chọn bến xe cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa bến xe " + maBenXe + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            var benXe = db.BenXes
                .FirstOrDefault(x => x.MaBenXe == maBenXe);

            if (benXe == null)
            {
                MessageBox.Show(
                    "Không tìm thấy bến xe!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // Kiểm tra bến xe đã được sử dụng trong tuyến xe chưa
            bool dangSuDung = db.TuyenXes.Any(x =>
                x.MaBenXeDi == maBenXe ||
                x.MaBenXeDen == maBenXe);

            if (dangSuDung)
            {
                MessageBox.Show(
                    "Không thể xóa bến xe này vì đang được sử dụng trong tuyến xe!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            db.BenXes.Remove(benXe);
            db.SaveChanges();

            MessageBox.Show(
                "Xóa bến xe thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LoadDanhSachBenXe();

            txtMaBenXe.Clear();
            txtTenBeXe.Clear();
            txtDiaChi.Clear();
            guna2ComboBox1.SelectedIndex = 0;

            txtMaBenXe.ReadOnly = false;
        }

        private void btnThemBenXe_Click(object sender, EventArgs e)
        {
            dangThem = true;

            txtMaBenXe.Clear();
            txtTenBeXe.Clear();
            txtDiaChi.Clear();

            guna2ComboBox1.SelectedIndex = 0;

            txtMaBenXe.Enabled = true;
            txtMaBenXe.ReadOnly = false;

            txtTenBeXe.Enabled = true;
            txtTenBeXe.ReadOnly = false;

            txtDiaChi.Enabled = true;
            txtDiaChi.ReadOnly = false;

            guna2ComboBox1.Enabled = true;

            btnLuu.Enabled = true;
            btnXoa.Enabled = true;

            txtMaBenXe.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            string trangThai = cboTrangThaiTim.Text.Trim();

            var query = db.BenXes.AsQueryable();

            // Tìm theo mã hoặc tên bến xe
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(x =>
                    x.MaBenXe.Contains(tuKhoa) ||
                    x.TenBenXe.Contains(tuKhoa));
            }

            // Lọc theo trạng thái
            if (!string.IsNullOrWhiteSpace(trangThai) &&
                trangThai != "Tất cả")
            {
                query = query.Where(x => x.TrangThai == trangThai);
            }

            var ds = query
                .OrderBy(x => x.MaBenXe)
                .ToList();

            dgvBenXe.Rows.Clear();

            int stt = 1;

            foreach (var b in ds)
            {
                dgvBenXe.Rows.Add(
                    stt++,
                    b.MaBenXe,
                    b.TenBenXe,
                    b.DiaChi,
                    b.TrangThai
                );
            }
        }
    }
}
