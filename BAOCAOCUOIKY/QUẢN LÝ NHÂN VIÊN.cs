using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAOCAOCUOIKY.Models;

namespace BAOCAOCUOIKY
{
    public partial class FrmNhanVien : Form
    {
        private Timer timer;
        private string maNhanVienDangChon = ""; 
        QuanLyVeXeModel db = new QuanLyVeXeModel();
        public FrmNhanVien()
        {
            InitializeComponent();
            ChonMenu(btnNhanVien);

            this.Load += FrmNhanVien_Load;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadComboBox();
            XoaChiTiet();

            // Hiển thị ngày giờ ngay khi mở form
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

        private void LoadNhanVien()
        {
            dgvDanhSachNhanVien.Rows.Clear();

            var danhSach = db.NhanViens
                .OrderBy(x => x.MaNV)
                .ToList();

            int stt = 1;

            foreach (var nv in danhSach)
            {
                int dong = dgvDanhSachNhanVien.Rows.Add();

                dgvDanhSachNhanVien.Rows[dong].Cells["stt"].Value = stt++;
                dgvDanhSachNhanVien.Rows[dong].Cells["manhanvien"].Value = nv.MaNV;
                dgvDanhSachNhanVien.Rows[dong].Cells["hovaten"].Value = nv.HoTen;

                dgvDanhSachNhanVien.Rows[dong].Cells["ngaysinh"].Value =
                    nv.NgaySinh.HasValue
                    ? nv.NgaySinh.Value.ToString("dd/MM/yyyy")
                    : "";

                dgvDanhSachNhanVien.Rows[dong].Cells["diachi"].Value =
                    nv.DiaChi;

                dgvDanhSachNhanVien.Rows[dong].Cells["sodienthoai"].Value =
                    nv.SoDienThoai;

                dgvDanhSachNhanVien.Rows[dong].Cells["gioitinh"].Value =
                    nv.GioiTinh;

                dgvDanhSachNhanVien.Rows[dong].Cells["email"].Value =
                    nv.Email;

                dgvDanhSachNhanVien.Rows[dong].Cells["chucvu"].Value =
                    nv.ChucVu;

                dgvDanhSachNhanVien.Rows[dong].Cells["trangthai"].Value =
                    nv.TrangThai;
            }
        }

        private void LoadComboBox()
        {
            txtSoDienThoaiTK.Text.Trim();
            cboChucVu.Items.Clear();
            cboTrangThai.Items.Clear();

            var dsSDT = db.NhanViens
                .Select(x => x.SoDienThoai)
                .Distinct()
                .ToList();

            var dsChucVu = db.NhanViens
                .Select(x => x.ChucVu)
                .Distinct()
                .ToList();

            foreach (var cv in dsChucVu)
            {
                if (!string.IsNullOrEmpty(cv))
                    cboChucVu.Items.Add(cv);
            }

            cboTrangThai.Items.Add("Đang Hoạt Động");
            cboTrangThai.Items.Add("Nghỉ Việc");
            cboTrangThai.Items.Add("Tạm Nghỉ");

            txtSoDienThoaiTK.Clear();
            cboChucVu.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
        }

        private void XoaChiTiet()
        {
            maNhanVienDangChon = "";

            txtMaNVTT.Text = "";
            txtHoTenTT.Text = "";

            dtpNgaySinhTT.Value = DateTime.Now;

            cboGioiTinhTT.SelectedIndex = -1;
            cboGioiTinhTT.Text = "";

            txtSDTTT.Text = "";
            txtDiaChiTT.Text = "";
            txtEmailTT.Text = "";

            cboChucVuTT.SelectedIndex = -1;
            cboChucVuTT.Text = "";

            cboTrangThaiTT.SelectedIndex = -1;
            cboTrangThaiTT.Text = "";
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string hoTen = txtHoTen.Text.Trim();
                string soDienThoai = txtSoDienThoaiTK.Text.Trim();
                string chucVu = cboChucVu.Text.Trim();
                string trangThai = cboTrangThai.Text.Trim();

                var query = db.NhanViens.AsQueryable();

                // Tìm theo họ tên
                if (!string.IsNullOrEmpty(hoTen))
                {
                    query = query.Where(x =>
                        x.HoTen.Contains(hoTen));
                }

                // Tìm theo số điện thoại
                if (!string.IsNullOrEmpty(soDienThoai))
                {
                    query = query.Where(x =>
                        x.SoDienThoai == soDienThoai);
                }

                // Tìm theo chức vụ
                if (!string.IsNullOrEmpty(chucVu))
                {
                    query = query.Where(x =>
                        x.ChucVu == chucVu);
                }

                // Tìm theo trạng thái
                if (!string.IsNullOrEmpty(trangThai))
                {
                    query = query.Where(x =>
                        x.TrangThai == trangThai);
                }

                // Tìm theo ngày sinh
                DateTime ngaySinh;

                if (DateTime.TryParse(txtNgaySinh.Text, out ngaySinh))
                {
                    DateTime ngayDau = ngaySinh.Date;
                    DateTime ngayCuoi = ngayDau.AddDays(1);

                    query = query.Where(x =>
                        x.NgaySinh >= ngayDau &&
                        x.NgaySinh < ngayCuoi);
                }

                var ketQua = query
                    .OrderBy(x => x.MaNV)
                    .ToList();

                dgvDanhSachNhanVien.Rows.Clear();

                int stt = 1;

                foreach (var nv in ketQua)
                {
                    int dong = dgvDanhSachNhanVien.Rows.Add();

                    dgvDanhSachNhanVien.Rows[dong].Cells["stt"].Value = stt++;

                    dgvDanhSachNhanVien.Rows[dong].Cells["manhanvien"].Value =
                        nv.MaNV;

                    dgvDanhSachNhanVien.Rows[dong].Cells["hovaten"].Value =
                        nv.HoTen;

                    dgvDanhSachNhanVien.Rows[dong].Cells["ngaysinh"].Value =
                        nv.NgaySinh.HasValue
                        ? nv.NgaySinh.Value.ToString("dd/MM/yyyy")
                        : "";

                    dgvDanhSachNhanVien.Rows[dong].Cells["diachi"].Value =
                        nv.DiaChi;

                    dgvDanhSachNhanVien.Rows[dong].Cells["sodienthoai"].Value =
                        nv.SoDienThoai;

                    dgvDanhSachNhanVien.Rows[dong].Cells["gioitinh"].Value =
                        nv.GioiTinh;

                    dgvDanhSachNhanVien.Rows[dong].Cells["email"].Value =
                        nv.Email;

                    dgvDanhSachNhanVien.Rows[dong].Cells["chucvu"].Value =
                        nv.ChucVu;

                    dgvDanhSachNhanVien.Rows[dong].Cells["trangthai"].Value =
                        nv.TrangThai;
                }

                if (ketQua.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy nhân viên!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm:\n" + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();

            txtSoDienThoaiTK.Clear();

            cboChucVu.SelectedIndex = -1;

            cboTrangThai.SelectedIndex = -1;

            txtNgaySinh.Clear();

            dgvDanhSachNhanVien.Rows.Clear();

            LoadNhanVien();

            XoaChiTiet();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maNhanVienDangChon))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var nv = db.NhanViens
                .FirstOrDefault(x => x.MaNV == maNhanVienDangChon);

            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên:\n\n"
                + nv.HoTen + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                db.NhanViens.Remove(nv);
                db.SaveChanges();

                MessageBox.Show(
                    "Xóa nhân viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadNhanVien();
                LoadComboBox();
                XoaChiTiet();
            }
        }

        private void btnXoaNhanVien_Click(object sender, EventArgs e)
        {
            btnXoa_Click(sender, e);
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra mã nhân viên
                if (string.IsNullOrWhiteSpace(txtMaNVTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên!");
                    txtMaNVTT.Focus();
                    return;
                }

                // Kiểm tra họ tên
                if (string.IsNullOrWhiteSpace(txtHoTenTT.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ và tên!");
                    txtHoTenTT.Focus();
                    return;
                }

                // Kiểm tra giới tính
                if (string.IsNullOrWhiteSpace(cboGioiTinhTT.Text))
                {
                    MessageBox.Show("Vui lòng chọn giới tính!");
                    return;
                }

                // Kiểm tra chức vụ
                if (string.IsNullOrWhiteSpace(cboChucVuTT.Text))
                {
                    MessageBox.Show("Vui lòng chọn chức vụ!");
                    return;
                }

                // Kiểm tra trạng thái
                if (string.IsNullOrWhiteSpace(cboTrangThaiTT.Text))
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!");
                    return;
                }

                string maNV = txtMaNVTT.Text.Trim();

                // Kiểm tra mã nhân viên đã tồn tại
                var kt = db.NhanViens
                    .FirstOrDefault(x => x.MaNV == maNV);

                if (kt != null)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại!");
                    txtMaNVTT.Focus();
                    return;
                }

                // Tạo nhân viên mới
                NhanVien nv = new NhanVien();

                nv.MaNV = txtMaNVTT.Text.Trim();
                nv.HoTen = txtHoTenTT.Text.Trim();
                nv.NgaySinh = dtpNgaySinhTT.Value;
                nv.GioiTinh = cboGioiTinhTT.Text.Trim();
                nv.SoDienThoai = txtSDTTT.Text.Trim();
                nv.Email = txtEmailTT.Text.Trim();
                nv.DiaChi = txtDiaChiTT.Text.Trim();
                nv.ChucVu = cboChucVuTT.Text.Trim();
                nv.TrangThai = cboTrangThaiTT.Text.Trim();

                // Thêm vào database
                db.NhanViens.Add(nv);
                db.SaveChanges();

                MessageBox.Show(
                    "Thêm nhân viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Load lại danh sách
                LoadNhanVien();

                // Xóa dữ liệu nhập
                XoaChiTiet();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string loi = "";

                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        loi += error.PropertyName
                            + ": "
                            + error.ErrorMessage
                            + "\n";
                    }
                }

                MessageBox.Show(
                    loi,
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void dgvDanhSachNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row = dgvDanhSachNhanVien.Rows[e.RowIndex];

                if (row.Cells["manhanvien"].Value == null)
                    return;

                // Mã nhân viên là STRING
                string maNV = row.Cells["manhanvien"].Value.ToString();

                maNhanVienDangChon = maNV;

                // Tìm nhân viên trong Entity Framework
                var nv = db.NhanViens
                           .FirstOrDefault(x => x.MaNV == maNV);

                if (nv == null)
                    return;

                // Đổ dữ liệu sang bên phải
                txtMaNVTT.Text = nv.MaNV;
                txtHoTenTT.Text = nv.HoTen;

                if (nv.NgaySinh.HasValue)
                    dtpNgaySinhTT.Value = nv.NgaySinh.Value;

                // Guna2ComboBox
                cboGioiTinhTT.Text = nv.GioiTinh;
                cboChucVuTT.Text = nv.ChucVu;
                cboTrangThaiTT.Text = nv.TrangThai;

                txtSDTTT.Text = nv.SoDienThoai;
                txtDiaChiTT.Text = nv.DiaChi;
                txtEmailTT.Text = nv.Email;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi chọn nhân viên: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCapNhatTT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maNhanVienDangChon))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật!");
                return;
            }

            try
            {
                var nv = db.NhanViens
                           .FirstOrDefault(x => x.MaNV == maNhanVienDangChon);

                if (nv == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên!");
                    return;
                }

                // Cập nhật thông tin
                nv.HoTen = txtHoTenTT.Text.Trim();
                nv.NgaySinh = dtpNgaySinhTT.Value;

                nv.GioiTinh = cboGioiTinhTT.Text.Trim();

                nv.SoDienThoai = txtSDTTT.Text.Trim();
                nv.DiaChi = txtDiaChiTT.Text.Trim();
                nv.Email = txtEmailTT.Text.Trim();

                nv.ChucVu = cboChucVuTT.Text.Trim();
                nv.TrangThai = cboTrangThaiTT.Text.Trim();

                // Lưu Entity Framework
                db.SaveChanges();

                MessageBox.Show(
                    "Cập nhật nhân viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Load lại danh sách
                LoadNhanVien();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string loi = "";

                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        loi += error.PropertyName
                             + ": "
                             + error.ErrorMessage
                             + "\n";
                    }
                }

                MessageBox.Show(
                    loi,
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

            // Nút đang chọn
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

        private void btnTuyenXe_Click(object sender, EventArgs e)
        {
            FrmTuyenXe frm = new FrmTuyenXe();
            frm.Show();
            this.Hide();
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
            ChonMenu(btnNhanVien);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            FrmThongKe frm = new FrmThongKe();
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
