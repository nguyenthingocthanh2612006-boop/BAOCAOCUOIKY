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
        private bool dangThem = false;
        public FrmTuyenXe()
        {
            InitializeComponent();

            LoadBenXe();
            LoadTrangThai();
            LoadDanhSachTuyen();

           
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

        private void dgvTuyenXe_CellContentClick(
     object sender,
     DataGridViewCellEventArgs e)
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

            if (hoi == DialogResult.No)
                return;

            using (var db = new QuanLyVeXeModel())
            {
                var t = db.TuyenXes
                    .FirstOrDefault(x => x.MaTuyen == maTuyen);

                if (t == null)
                {
                    MessageBox.Show("Không tìm thấy tuyến!");
                    return;
                }

                db.TuyenXes.Remove(t);
                db.SaveChanges();
            }

            MessageBox.Show("Đã xóa tuyến xe!");

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

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
