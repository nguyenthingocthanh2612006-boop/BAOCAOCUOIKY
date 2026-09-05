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
    public partial class FrmChuyenXe : Form
    {
        private bool dangThem = false;
        private Timer timer;
        public FrmChuyenXe()
        {
            InitializeComponent();

            LoadTuyenXe();
            LoadTrangThai();
            LoadDanhSachChuyenXe();
            XoaTrang();
            ChonMenu(btnChuyenXe);

        }

        private void LoadTuyenXe()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var ds = db.TuyenXes
                    .Where(x => x.TrangThai == "Đang hoạt động")
                    .OrderBy(x => x.TenTuyen)
                    .ToList();

                // Combo tìm kiếm
                cboTuyenXe.DataSource = ds.ToList();
                cboTuyenXe.DisplayMember = "TenTuyen";
                cboTuyenXe.ValueMember = "MaTuyen";
                cboTuyenXe.SelectedIndex = -1;
            }
        }

        private void LoadTrangThai()
        {
            // Trạng thái tìm kiếm
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Chưa khởi hành");
            cboTrangThai.Items.Add("Đang chạy");
            cboTrangThai.Items.Add("Đã hoàn thành");
            cboTrangThai.Items.Add("Đã hủy");

            cboTrangThai.SelectedIndex = 0;


            // Trạng thái chi tiết
            cboTrangThaiCT.Items.Clear();
            cboTrangThaiCT.Items.Add("Chưa khởi hành");
            cboTrangThaiCT.Items.Add("Đang chạy");
            cboTrangThaiCT.Items.Add("Đã hoàn thành");
            cboTrangThaiCT.Items.Add("Đã hủy");

            cboTrangThaiCT.SelectedIndex = 0;
        }

        private void LoadDanhSachChuyenXe()
        {
            using (var db = new QuanLyVeXeModel())
            {
                var ds = (
                    from c in db.ChuyenXes
                    join t in db.TuyenXes
                        on c.MaTuyen equals t.MaTuyen
                    select new
                    {
                        c.MaChuyen,
                        TenTuyen = t.TenTuyen,
                        c.MaXe,
                        c.MaTX,
                        c.NgayKhoiHanh,
                        c.GioKhoiHanh,
                        c.GioDenDuKien,
                        c.GiaVe,
                        c.TrangThai
                    }
                ).ToList();

                dgvDanhSachChuyenXe.Rows.Clear();

                int stt = 1;

                foreach (var c in ds)
                {
                    dgvDanhSachChuyenXe.Rows.Add(
                        stt++,
                        c.MaChuyen,
                        c.TenTuyen,
                        c.MaXe,
                        c.MaTX,
                        c.NgayKhoiHanh.ToString("dd/MM/yyyy"),
                        c.GioKhoiHanh.ToString(@"hh\:mm"),
                        c.GiaVe.ToString("N0"),
                        c.TrangThai
                    );
                }
            }
        }

        private void XoaTrang()
        {
            dangThem = false;

            txtMaChuyenCT.Clear();

            txtTuyenXeCT.Clear();

            txtXeCT.Clear();
            txtTaiXeCT.Clear();

            dtpNgayDiCT.Value = DateTime.Today;

            txtGioDiCT.Clear();
            txtGioDenCT.Clear();
            txtGiaVeCT.Clear();

            if (cboTrangThaiCT.Items.Count > 0)
                cboTrangThaiCT.SelectedIndex = 0;
        }
        private void dgvDanhSachChuyenXe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string maChuyen = dgvDanhSachChuyenXe
                .Rows[e.RowIndex]
                .Cells["machuyen"]
                .Value?.ToString();

            if (string.IsNullOrWhiteSpace(maChuyen))
                return;

            LoadThongTinChuyenXe(maChuyen);
        }

        private void LoadThongTinChuyenXe(string maChuyen)
        {
            using (var db = new QuanLyVeXeModel())
            {
                var chuyen = db.ChuyenXes
                    .FirstOrDefault(x => x.MaChuyen == maChuyen);

                if (chuyen == null)
                {
                    MessageBox.Show("Không tìm thấy chuyến xe!");
                    return;
                }

                // Mã chuyến
                txtMaChuyenCT.Text = chuyen.MaChuyen;

                // Tuyến xe
                txtTuyenXeCT.Text = chuyen.MaTuyen;

                // Xe
                txtXeCT.Text = chuyen.MaXe;

                // Tài xế
                txtTaiXeCT.Text = chuyen.MaTX;

                // Ngày đi
                dtpNgayDiCT.Value = chuyen.NgayKhoiHanh;

                // Giờ đi
                txtGioDiCT.Text =
                    chuyen.GioKhoiHanh.ToString(@"hh\:mm");

                // Giờ đến
                txtGioDenCT.Text = chuyen.GioDenDuKien?.ToString(@"hh\:mm") ?? "";

                // Giá vé
                txtGiaVeCT.Text =
                    chuyen.GiaVe.ToString("N0");

                // Trạng thái
                cboTrangThaiCT.SelectedItem =
                    chuyen.TrangThai;
            }
        }

        private void btnThemChuyen_Click(object sender, EventArgs e)
        {
            dangThem = true;

            // Xóa dữ liệu cũ
            txtMaChuyenCT.Clear();
            txtTuyenXeCT.Clear();
            txtXeCT.Clear();
            txtTaiXeCT.Clear();
            dtpNgayDiCT.Value = DateTime.Today;
            txtGioDiCT.Clear();
            txtGioDenCT.Clear();
            txtGiaVeCT.Clear();

            if (cboTrangThaiCT.Items.Count > 0)
                cboTrangThaiCT.SelectedIndex = 0;

            // Cho nhập mã chuyến
            txtMaChuyenCT.ReadOnly = false;

            // TỰ ĐỘNG NHẢY VÀO Ô MÃ CHUYẾN
            txtMaChuyenCT.Focus();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // ==============================
            // KIỂM TRA DỮ LIỆU
            // ==============================

            string maChuyen = txtMaChuyenCT.Text.Trim();

            if (string.IsNullOrEmpty(maChuyen))
            {
                MessageBox.Show("Vui lòng nhập mã chuyến!");
                txtMaChuyenCT.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTuyenXeCT.Text))
            {
                MessageBox.Show("Vui lòng nhập tuyến xe!");
                txtTuyenXeCT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtXeCT.Text))
            {
                MessageBox.Show("Vui lòng nhập mã xe!");
                txtXeCT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTaiXeCT.Text))
            {
                MessageBox.Show("Vui lòng nhập mã tài xế!");
                txtTaiXeCT.Focus();
                return;
            }

            TimeSpan gioDi;

            if (!TimeSpan.TryParse(txtGioDiCT.Text.Trim(), out gioDi))
            {
                MessageBox.Show("Giờ đi không hợp lệ! Ví dụ: 07:00");
                txtGioDiCT.Focus();
                return;
            }

            TimeSpan gioDen;

            if (!TimeSpan.TryParse(txtGioDenCT.Text.Trim(), out gioDen))
            {
                MessageBox.Show("Giờ đến không hợp lệ! Ví dụ: 08:00");
                txtGioDenCT.Focus();
                return;
            }

            if (gioDen <= gioDi)
            {
                MessageBox.Show("Giờ đến phải lớn hơn giờ đi!");
                return;
            }

            decimal giaVe;

            string giaVeText = txtGiaVeCT.Text
                .Replace(".", "")
                .Replace(",", "")
                .Trim();

            if (!decimal.TryParse(giaVeText, out giaVe) || giaVe < 0)
            {
                MessageBox.Show("Giá vé không hợp lệ!");
                txtGiaVeCT.Focus();
                return;
            }

            string tenTuyen = txtTuyenXeCT.Text.Trim();
            string maXe = txtXeCT.Text.Trim();
            string maTX = txtTaiXeCT.Text.Trim();
            string trangThai = cboTrangThaiCT.Text.Trim();

            using (var db = new QuanLyVeXeModel())
            {
                // Tìm mã tuyến từ tên tuyến
                var tuyen = db.TuyenXes
                    .FirstOrDefault(x => x.TenTuyen == tenTuyen);

                if (tuyen == null)
                {
                    MessageBox.Show(
                        "Tuyến xe không tồn tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTuyenXeCT.Focus();
                    return;
                }

                string maTuyen = tuyen.MaTuyen;
                // =================================================
                // TRƯỜNG HỢP 1: ĐANG THÊM
                // =================================================

                if (dangThem)
                {
                    // ==========================================
                    // TRƯỜNG HỢP 1: THÊM CHUYẾN MỚI
                    // ==========================================

                    bool daTonTai = db.ChuyenXes
                        .Any(x => x.MaChuyen == maChuyen);

                    if (daTonTai)
                    {
                        MessageBox.Show(
                            "Mã chuyến đã tồn tại!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        txtMaChuyenCT.Focus();
                        return;
                    }

                    var chuyenMoi = new ChuyenXe
                    {
                        MaChuyen = maChuyen,
                        MaTuyen = maTuyen,
                        MaXe = maXe,
                        MaTX = maTX,
                        NgayKhoiHanh = dtpNgayDiCT.Value.Date,
                        GioKhoiHanh = gioDi,
                        GioDenDuKien = gioDen,
                        GiaVe = giaVe,

                        // Không hiển thị Số Ghế trên giao diện
                        // nhưng CSDL vẫn cần SoLuongVe
                        SoLuongVe = 29,

                        TrangThai = trangThai
                    };

                    db.ChuyenXes.Add(chuyenMoi);
                    db.SaveChanges();

                    MessageBox.Show(
                        "Thêm chuyến xe thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    dangThem = false;
                }
                else
                {
                    // ==========================================
                    // TRƯỜNG HỢP 2: SỬA CHUYẾN
                    // ==========================================

                    var chuyen = db.ChuyenXes
                        .FirstOrDefault(x => x.MaChuyen == maChuyen);

                    if (chuyen == null)
                    {
                        MessageBox.Show(
                            "Không tìm thấy chuyến xe!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    chuyen.MaTuyen = maTuyen;
                    chuyen.MaXe = maXe;
                    chuyen.MaTX = maTX;
                    chuyen.NgayKhoiHanh = dtpNgayDiCT.Value.Date;
                    chuyen.GioKhoiHanh = gioDi;
                    chuyen.GioDenDuKien = gioDen;
                    chuyen.GiaVe = giaVe;
                    chuyen.TrangThai = trangThai;

                    db.SaveChanges();

                    MessageBox.Show(
                        "Cập nhật chuyến xe thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            // Load lại bảng
            LoadDanhSachChuyenXe();

            // Xóa form
            XoaTrang();
        }

        private void btnXoaChuyen_Click(object sender, EventArgs e)
        {
            string maChuyen = txtMaChuyenCT.Text.Trim();

            if (string.IsNullOrEmpty(maChuyen))
            {
                MessageBox.Show(
                    "Vui lòng chọn chuyến xe cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa chuyến " + maChuyen + " không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            using (var db = new QuanLyVeXeModel())
            {
                var chuyen = db.ChuyenXes
                    .FirstOrDefault(x => x.MaChuyen == maChuyen);

                if (chuyen == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy chuyến xe!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Kiểm tra chuyến đã có vé chưa
                bool daCoVe = db.VeXes
                    .Any(v => v.MaChuyen == maChuyen);

                if (daCoVe)
                {
                    // Đã có vé thì không xóa thật
                    chuyen.TrangThai = "Đã hủy";

                    db.SaveChanges();

                    MessageBox.Show(
                        "Chuyến xe đã có vé nên không thể xóa.\n" +
                        "Hệ thống đã chuyển chuyến sang trạng thái Đã hủy.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Chưa có vé thì xóa thật
                    db.ChuyenXes.Remove(chuyen);
                    db.SaveChanges();

                    MessageBox.Show(
                        "Xóa chuyến xe thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            LoadDanhSachChuyenXe();
            XoaTrang();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var db = new QuanLyVeXeModel())
            {
                string maTuyen = "";

                if (cboTuyenXe.SelectedValue != null)
                    maTuyen = cboTuyenXe.SelectedValue.ToString();

                string taiXe = txtTaiXe.Text.Trim();
                string gioDiText = txtGioDi.Text.Trim();
                string gioDenText = txtGioDen.Text.Trim();
                string trangThai = cboTrangThai.Text.Trim();

                DateTime ngayDi = dtpNgayDi.Value.Date;

                // ==========================================
                // QUERY ENTITY CHUYẾN XE
                // ==========================================

                var query = db.ChuyenXes.AsQueryable();


                // Tuyến xe
                if (!string.IsNullOrEmpty(maTuyen))
                {
                    query = query.Where(c =>
                        c.MaTuyen == maTuyen);
                }


                // Tài xế
                if (!string.IsNullOrEmpty(taiXe))
                {
                    query = query.Where(c =>
                        c.MaTX.Contains(taiXe));
                }


                // Ngày đi
                query = query.Where(c =>
                    c.NgayKhoiHanh == ngayDi);


                // Giờ đi
                if (!string.IsNullOrEmpty(gioDiText))
                {
                    TimeSpan gioDi;

                    if (!TimeSpan.TryParse(gioDiText, out gioDi))
                    {
                        MessageBox.Show(
                            "Giờ đi không hợp lệ! Ví dụ: 07:00");
                        txtGioDi.Focus();
                        return;
                    }

                    query = query.Where(c =>
                        c.GioKhoiHanh == gioDi);
                }


                // Giờ đến
                if (!string.IsNullOrEmpty(gioDenText))
                {
                    TimeSpan gioDen;

                    if (!TimeSpan.TryParse(gioDenText, out gioDen))
                    {
                        MessageBox.Show(
                            "Giờ đến không hợp lệ! Ví dụ: 08:00");
                        txtGioDen.Focus();
                        return;
                    }

                    query = query.Where(c =>
                        c.GioDenDuKien == gioDen);
                }


                // Trạng thái
                if (!string.IsNullOrEmpty(trangThai) &&
                    trangThai != "Tất cả")
                {
                    query = query.Where(c =>
                        c.TrangThai == trangThai);
                }


                // ==========================================
                // LẤY DỮ LIỆU
                // ==========================================

                var ds =
                    from c in query
                    join t in db.TuyenXes
                        on c.MaTuyen equals t.MaTuyen
                    select new
                    {
                        c.MaChuyen,
                        TenTuyen = t.TenTuyen,
                        c.MaXe,
                        c.MaTX,
                        c.NgayKhoiHanh,
                        c.GioKhoiHanh,
                        c.GioDenDuKien,
                        c.GiaVe,
                        c.TrangThai
                    };

                var ketQua = ds
                    .OrderBy(x => x.NgayKhoiHanh)
                    .ThenBy(x => x.GioKhoiHanh)
                    .ToList();


                // ==========================================
                // ĐỔ LÊN DATAGRIDVIEW
                // ==========================================

                dgvDanhSachChuyenXe.Rows.Clear();

                int stt = 1;

                foreach (var c in ketQua)
                {
                    dgvDanhSachChuyenXe.Rows.Add(
                        stt++,
                        c.MaChuyen,
                        c.TenTuyen,
                        c.MaXe,
                        c.MaTX,
                        c.NgayKhoiHanh.ToString("dd/MM/yyyy"),
                        c.GioKhoiHanh.ToString(@"hh\:mm"),
                        c.GiaVe.ToString("N0"),
                        c.TrangThai
                    );
                }

                MessageBox.Show(
                    "Tìm thấy " + ketQua.Count + " chuyến xe.",
                    "Kết quả",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa bộ lọc
            cboTuyenXe.SelectedIndex = -1;  

            txtTaiXe.Clear();
            txtGioDi.Clear();
            txtGioDen.Clear();

            dtpNgayDi.Value = DateTime.Today;

            cboTrangThai.SelectedIndex = 0;

            // Load lại toàn bộ chuyến
            LoadDanhSachChuyenXe();
        }

        private void FrmChuyenXe_Load(object sender, EventArgs e)
        {

            CapNhatNgayGio();

            // Tạo đồng hồ cập nhật mỗi giây
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            btnChuyenXe.FillColor = Color.FromArgb(35, 85, 180);
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
            // Màu các nút bình thường
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

        private void btnTuyenXe_Click(object sender, EventArgs e)
        {
            FrmTuyenXe frm = new FrmTuyenXe();

            frm.Show();

            this.Hide();
        }

        private void btnChuyenXe_Click(object sender, EventArgs e)
        {
            ChonMenu(btnChuyenXe);
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
            FrmThongKe frm = new FrmThongKe();
            frm.Show();
            this.Hide();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FrmTrangChuAdmin frm = new FrmTrangChuAdmin();
            frm.Show();
            this.Hide();
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
