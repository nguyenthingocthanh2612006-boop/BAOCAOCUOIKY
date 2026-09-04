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
    public partial class FrmTuyenXe : Form
    {
        private Timer timer;
        private bool dangThem = false;
        public FrmTuyenXe()
        {
            InitializeComponent();

            LoadBenXe();
            LoadTrangThai();
            LoadDanhSachTuyen();
            ChonMenu(btnTuyenXe);
        }

        private void LoadBenXe()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var dsBenXe = db.BenXes
                    .Where(x => x.TrangThai == "Đang hoạt động")
                    .OrderBy(x => x.TenBenXe)
                    .ToList();

                // Combo tìm kiếm
                cboTuDiem.DataSource = dsBenXe.ToList();
                cboTuDiem.DisplayMember = "TenBenXe";
                cboTuDiem.ValueMember = "MaBenXe";

                cboDenDiem.DataSource = dsBenXe.ToList();
                cboDenDiem.DisplayMember = "TenBenXe";
                cboDenDiem.ValueMember = "MaBenXe";

                // Combo thông tin tuyến
                cboTuDiemCT.DataSource = dsBenXe.ToList();
                cboTuDiemCT.DisplayMember = "TenBenXe";
                cboTuDiemCT.ValueMember = "MaBenXe";

                cboDenDiemCT.DataSource = dsBenXe.ToList();
                cboDenDiemCT.DisplayMember = "TenBenXe";
                cboDenDiemCT.ValueMember = "MaBenXe";

                cboTuDiem.SelectedIndex = -1;
                cboDenDiem.SelectedIndex = -1;
            }
        }


        private void LoadTrangThai()
        {
            cboTrangThaiTT.Items.Clear();
            cboTrangThaiTT.Items.Add("Đang hoạt động");
            cboTrangThaiTT.Items.Add("Ngừng hoạt động");
            cboTrangThaiTT.SelectedIndex = 0;

            cboTrangThaiTim.Items.Clear();
            cboTrangThaiTim.Items.Add("Tất cả");
            cboTrangThaiTim.Items.Add("Đang hoạt động");
            cboTrangThaiTim.Items.Add("Ngừng hoạt động");
            cboTrangThaiTim.SelectedIndex = 0;
        }



        private void LoadDanhSachTuyen()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var ds = (
                    from t in db.TuyenXes
                    join b1 in db.BenXes
                        on t.MaBenXeDi equals b1.MaBenXe
                    join b2 in db.BenXes
                        on t.MaBenXeDen equals b2.MaBenXe
                    select new
                    {
                        t.MaTuyen,
                        t.TenTuyen,
                        MaBenXeDi = b1.MaBenXe,
                        MaBenXeDen = b2.MaBenXe,
                        TuDiem = b1.TenBenXe,
                        DenDiem = b2.TenBenXe,
                        t.KhoangCach,
                        t.ThoiGianDuKien,
                        t.GiaVeCoBan,
                        t.TrangThai
                    }
                );

                // ===== TỪ ĐIỂM =====
                if (cboTuDiem.SelectedValue != null)
                {
                    string maTu = cboTuDiem.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(maTu))
                    {
                        ds = ds.Where(x => x.MaBenXeDi == maTu);
                    }
                }

                // ===== ĐẾN ĐIỂM =====
                if (cboDenDiem.SelectedValue != null)
                {
                    string maDen = cboDenDiem.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(maDen))
                    {
                        ds = ds.Where(x => x.MaBenXeDen == maDen);
                    }
                }

                // ===== TRẠNG THÁI =====
                string trangThai = cboTrangThaiTim.Text;

                if (!string.IsNullOrEmpty(trangThai) &&
                    trangThai != "Tất cả")
                {
                    ds = ds.Where(x => x.TrangThai == trangThai);
                }

                // ===== MÃ HOẶC TÊN TUYẾN =====
                string tuKhoa = txtTimKiem.Text.Trim();

                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    ds = ds.Where(x =>
                        x.MaTuyen.Contains(tuKhoa) ||
                        x.TenTuyen.Contains(tuKhoa));
                }

                var ketQua = ds
                    .OrderBy(x => x.MaTuyen)
                    .ToList();

                // ===== ĐỔ LẠI BẢNG =====
                dgvTuyenXe.Rows.Clear();

                int stt = 1;

                foreach (var t in ketQua)
                {
                    dgvTuyenXe.Rows.Add(
                        stt++,
                        t.MaTuyen,
                        t.TenTuyen,
                        t.TuDiem,
                        t.DenDiem,
                        t.KhoangCach,
                        t.ThoiGianDuKien,
                        t.GiaVeCoBan.ToString("N0"),
                        t.TrangThai
                    );
                }
            }
        }

        private void dgvTuyenXe_CellContentClick(object sender,DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string maTuyen = dgvTuyenXe.Rows[e.RowIndex]
                .Cells["MaTuyen"].Value?.ToString();

            if (string.IsNullOrEmpty(maTuyen))
                return;

            string tenCot = dgvTuyenXe.Columns[e.ColumnIndex].Name;

            // ===== SỬA =====
            if (tenCot == "Sua")
            {
                MoChinhSua();
                LoadThongTinTuyen(maTuyen);
                return;
            }

            // ===== XÓA =====
            if (tenCot == "Xoa")
            {
                XoaTuyen(maTuyen);
                return;
            }

            // Bấm dòng bình thường
            MoChinhSua();
            LoadThongTinTuyen(maTuyen);
        }

        private void MoChinhSua()
        {
            pnlThongTinTuyen.Enabled = true;

            txtMaTuyen.Enabled = true;
            txtMaTuyen.ReadOnly = true;

            txtTenTuyen.Enabled = true;
            txtTenTuyen.ReadOnly = false;

            txtKhoangCach.Enabled = true;
            txtKhoangCach.ReadOnly = false;

            txtThoiGian.Enabled = true;
            txtThoiGian.ReadOnly = false;

            txtGiaVe.Enabled = true;
            txtGiaVe.ReadOnly = false;

            cboTuDiemCT.Enabled = true;
            cboDenDiemCT.Enabled = true;
            cboTrangThaiTT.Enabled = true;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            dangThem = false;
        }

        private void MoThem()
        {
            dangThem = true;

            // BẬT KHUNG CHA
            pnlThongTinTuyen.Enabled = true;

            // Cho nhập tất cả
            txtMaTuyen.Enabled = true;
            txtMaTuyen.ReadOnly = false;

            txtTenTuyen.Enabled = true;
            txtTenTuyen.ReadOnly = false;

            txtKhoangCach.Enabled = true;
            txtKhoangCach.ReadOnly = false;

            txtThoiGian.Enabled = true;
            txtThoiGian.ReadOnly = false;

            txtGiaVe.Enabled = true;
            txtGiaVe.ReadOnly = false;

            cboTuDiemCT.Enabled = true;
            cboDenDiemCT.Enabled = true;
            cboTrangThaiTT.Enabled = true;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void LoadThongTinTuyen(string maTuyen)
        {
            using (var db = new QuanLyVeXeModel())
            {
                var t = db.TuyenXes
                    .FirstOrDefault(x => x.MaTuyen == maTuyen);

                if (t == null)
                    return;

                txtMaTuyen.Text = t.MaTuyen;
                txtTenTuyen.Text = t.TenTuyen;

                cboTuDiemCT.SelectedValue = t.MaBenXeDi;
                cboDenDiemCT.SelectedValue = t.MaBenXeDen;

                txtKhoangCach.Text =
                    t.KhoangCach?.ToString() ?? "";

                txtThoiGian.Text =
                    t.ThoiGianDuKien?.ToString() ?? "";

                txtGiaVe.Text =
                    t.GiaVeCoBan.ToString();

                cboTrangThaiTT.SelectedItem = t.TrangThai;
            }
        }

        private void XoaTuyen(string maTuyen)
        {
            DialogResult hoi = MessageBox.Show(
                "Bạn có chắc muốn xóa tuyến " + maTuyen + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (hoi != DialogResult.Yes)
                return;

            using (var db = new QuanLyVeXeModel())
            {
                var tuyen = db.TuyenXes
                    .FirstOrDefault(x => x.MaTuyen == maTuyen);

                if (tuyen == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy tuyến!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Kiểm tra tuyến có chuyến xe đang sử dụng không
                bool coChuyenXe = db.ChuyenXes
                    .Any(c => c.MaTuyen == maTuyen);

                if (coChuyenXe)
                {
                    // Không xóa thật
                    tuyen.TrangThai = "Ngừng hoạt động";

                    db.SaveChanges();

                    MessageBox.Show(
                        "Tuyến này đang được sử dụng bởi chuyến xe.\n" +
                        "Đã chuyển tuyến sang trạng thái Ngừng hoạt động.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Không có chuyến → xóa thật
                    db.TuyenXes.Remove(tuyen);
                    db.SaveChanges();

                    MessageBox.Show(
                        "Đã xóa tuyến xe!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            LoadDanhSachTuyen();

            txtMaTuyen.Clear();
            txtTenTuyen.Clear();
            txtKhoangCach.Clear();
            txtThoiGian.Clear();
            txtGiaVe.Clear();

            cboTuDiemCT.SelectedIndex = -1;
            cboDenDiemCT.SelectedIndex = -1;
            cboTrangThaiTT.SelectedIndex = -1;

            dgvTuyenXe.ClearSelection();
        }

        private void XoaTrang()
        {
            txtMaTuyen.Clear();
            txtTenTuyen.Clear();
            txtKhoangCach.Clear();
            txtThoiGian.Clear();
            txtGiaVe.Clear();

            if (cboTuDiemCT.Items.Count > 0)
                cboTuDiemCT.SelectedIndex = 0;

            if (cboDenDiemCT.Items.Count > 0)
                cboDenDiemCT.SelectedIndex = 0;

            if (cboTrangThaiTT.Items.Count > 0)
                cboTrangThaiTT.SelectedIndex = 0;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            dangThem = false;

            txtMaTuyen.Clear();
            txtTenTuyen.Clear();
            txtKhoangCach.Clear();
            txtThoiGian.Clear();
            txtGiaVe.Clear();

            cboTuDiemCT.SelectedIndex = -1;
            cboDenDiemCT.SelectedIndex = -1;
            cboTrangThaiTT.SelectedIndex = -1;

            txtMaTuyen.ReadOnly = true;
            txtTenTuyen.ReadOnly = true;
            txtKhoangCach.ReadOnly = true;
            txtThoiGian.ReadOnly = true;
            txtGiaVe.ReadOnly = true;

            cboTuDiemCT.Enabled = false;
            cboDenDiemCT.Enabled = false;
            cboTrangThaiTT.Enabled = false;

            btnLuu.Enabled = false;

            dgvTuyenXe.ClearSelection();

            pnlThongTinTuyen.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // =========================
            // KIỂM TRA DỮ LIỆU
            // =========================
            string maTuyen = txtMaTuyen.Text.Trim();
            string tenTuyen = txtTenTuyen.Text.Trim();

            if (string.IsNullOrWhiteSpace(maTuyen))
            {
                MessageBox.Show("Vui lòng nhập mã tuyến!");
                txtMaTuyen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tenTuyen))
            {
                MessageBox.Show("Vui lòng nhập tên tuyến!");
                txtTenTuyen.Focus();
                return;
            }

            if (cboTuDiemCT.SelectedValue == null ||
                cboDenDiemCT.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bến đi và bến đến!");
                return;
            }

            string maBenDi = cboTuDiemCT.SelectedValue.ToString();
            string maBenDen = cboDenDiemCT.SelectedValue.ToString();

            if (maBenDi == maBenDen)
            {
                MessageBox.Show("Bến đi và bến đến không được trùng nhau!");
                return;
            }

            if (!double.TryParse(txtKhoangCach.Text.Trim(), out double khoangCach))
            {
                MessageBox.Show("Khoảng cách không hợp lệ!");
                txtKhoangCach.Focus();
                return;
            }

            if (!int.TryParse(txtThoiGian.Text.Trim(), out int thoiGian))
            {
                MessageBox.Show("Thời gian không hợp lệ!");
                txtThoiGian.Focus();
                return;
            }

            if (!decimal.TryParse(txtGiaVe.Text.Trim(), out decimal giaVe))
            {
                MessageBox.Show("Giá vé không hợp lệ!");
                txtGiaVe.Focus();
                return;
            }

            string trangThai = cboTrangThaiTT.SelectedItem?.ToString();

            using (var db = new QuanLyVeXeModel())
            {
                // =========================
                // THÊM TUYẾN MỚI
                // =========================
                if (dangThem)
                {
                    bool daTonTai = db.TuyenXes
                        .Any(x => x.MaTuyen == maTuyen);

                    if (daTonTai)
                    {
                        MessageBox.Show("Mã tuyến đã tồn tại!");
                        txtMaTuyen.Focus();
                        return;
                    }

                    TuyenXe tuyenMoi = new TuyenXe
                    {
                        MaTuyen = maTuyen,
                        TenTuyen = tenTuyen,
                        MaBenXeDi = maBenDi,
                        MaBenXeDen = maBenDen,
                        KhoangCach = khoangCach,
                        ThoiGianDuKien = thoiGian,
                        GiaVeCoBan = giaVe,
                        TrangThai = trangThai
                    };

                    db.TuyenXes.Add(tuyenMoi);
                    db.SaveChanges();

                    MessageBox.Show("Thêm tuyến xe thành công!");
                }
                // =========================
                // SỬA TUYẾN
                // =========================
                else
                {
                    var t = db.TuyenXes
                        .FirstOrDefault(x => x.MaTuyen == maTuyen);

                    if (t == null)
                    {
                        MessageBox.Show("Không tìm thấy tuyến để sửa!");
                        return;
                    }

                    t.TenTuyen = tenTuyen;
                    t.MaBenXeDi = maBenDi;
                    t.MaBenXeDen = maBenDen;
                    t.KhoangCach = khoangCach;
                    t.ThoiGianDuKien = thoiGian;
                    t.GiaVeCoBan = giaVe;
                    t.TrangThai = trangThai;

                    db.SaveChanges();

                    MessageBox.Show("Cập nhật tuyến thành công!");
                }
            }

            // =========================
            // SAU KHI LƯU
            // =========================
            dangThem = false;

            LoadDanhSachTuyen();

            XoaTrang();

            // Khóa lại phần thông tin
            txtMaTuyen.ReadOnly = true;
            txtTenTuyen.ReadOnly = true;
            txtKhoangCach.ReadOnly = true;
            txtThoiGian.ReadOnly = true;
            txtGiaVe.ReadOnly = true;

            cboTuDiemCT.Enabled = false;
            cboDenDiemCT.Enabled = false;
            cboTrangThaiTT.Enabled = false;

            btnLuu.Enabled = false;

            dgvTuyenXe.ClearSelection();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachTuyen();
        }

        private void btnThemTuyen_Click(object sender, EventArgs e)
        {
            MoThem();

            txtMaTuyen.Clear();
            txtTenTuyen.Clear();
            txtKhoangCach.Clear();
            txtThoiGian.Clear();
            txtGiaVe.Clear();

            if (cboTuDiemCT.Items.Count > 0)
                cboTuDiemCT.SelectedIndex = 0;

            if (cboDenDiemCT.Items.Count > 1)
                cboDenDiemCT.SelectedIndex = 1;

            if (cboTrangThaiTT.Items.Count > 0)
                cboTrangThaiTT.SelectedIndex = 0;

            dgvTuyenXe.ClearSelection();

            txtMaTuyen.Focus();
        }

        private void FrmTuyenXe_Load(object sender, EventArgs e)
        {
            CapNhatNgayGio();

            // Tạo đồng hồ cập nhật mỗi giây
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
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

        private void ChonMenu(Guna.UI2.WinForms.Guna2Button btn)
        {
            btnTrangChu.FillColor = Color.FromArgb(70, 130, 220);
            btnQuanLyVe.FillColor = Color.FromArgb(70, 130, 220);
            btnChuyenXe.FillColor = Color.FromArgb(70, 130, 220);
            btnTuyenXe.FillColor = Color.FromArgb(70, 130, 220);
            btnQuanLyXeXe.FillColor = Color.FromArgb(70, 130, 220);
            btnTaiXe.FillColor = Color.FromArgb(70, 130, 220);
            btnNhanVien.FillColor = Color.FromArgb(70, 130, 220);
            btnThongKe.FillColor = Color.FromArgb(70, 130, 220);

            btn.FillColor = Color.FromArgb(35, 85, 180);
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

        private void btnQuanLyXeXe_Click(object sender, EventArgs e)
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
            FrmThongKe frm = new FrmThongKe();
            frm.Show();
            this.Hide();
        }

        private void btnTuyenXe_Click(object sender, EventArgs e)
        {
            ChonMenu(btnTuyenXe);
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
