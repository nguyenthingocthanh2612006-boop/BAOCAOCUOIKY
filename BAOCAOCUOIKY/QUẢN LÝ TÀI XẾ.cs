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
using ClosedXML.Excel;
using System.IO;

namespace BAOCAOCUOIKY
{
    public partial class FrmTaiXe : Form
    {
        private Timer timer;
        private bool dangThemTaiXe = false;
        private QuanLyVeXeModel db = new QuanLyVeXeModel();
        private string maTXDangChon = "";
        public FrmTaiXe()
        {
            InitializeComponent();

            LoadDanhSachTaiXe();

            cboGioiTinhTT.Items.Clear();
            cboGioiTinhTT.Items.Add("Nam");
            cboGioiTinhTT.Items.Add("Nữ");

            cboTrangThaiTT.Items.Clear();
            cboTrangThaiTT.Items.Add("Đang hoạt động");
            cboTrangThaiTT.Items.Add("Tạm nghỉ");
            cboTrangThaiTT.Items.Add("Nghỉ việc");
        }

        private void LoadDanhSachTaiXe()
        {
            var danhSach = db.TaiXes
                .Select(x => new
                {
                    x.MaTX,
                    x.HoTen,
                    x.SoDienThoai,
                    GiayPhepLaiXe = x.SoBangLai,
                    x.NgayCap,
                    x.NgayHetHan,
                    x.TrangThai
                })
                .ToList();

            dgvDanhSachTaiXe.Rows.Clear();

            int stt = 1;

            foreach (var tx in danhSach)
            {
                dgvDanhSachTaiXe.Rows.Add(
                    stt++,
                    tx.MaTX,
                    tx.HoTen,
                    tx.SoDienThoai,
                    tx.GiayPhepLaiXe,
                    tx.NgayCap?.ToString("dd/MM/yyyy"),
                    tx.NgayHetHan?.ToString("dd/MM/yyyy"),
                    tx.TrangThai
                );
            }
        }
        private void btnXoaTaiXe_Click(object sender, EventArgs e)
        {
            string maTX = txtMaTXTT.Text.Trim();

            if (string.IsNullOrEmpty(maTX))
            {
                MessageBox.Show("Vui lòng chọn tài xế!");
                return;
            }

            var tx = db.TaiXes.FirstOrDefault(x => x.MaTX == maTX);

            if (tx == null)
            {
                MessageBox.Show("Không tìm thấy tài xế!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn ngừng hoạt động tài xế này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                tx.TrangThai = "Không hoạt động";

                db.SaveChanges();

                MessageBox.Show("Đã chuyển tài xế sang trạng thái Không hoạt động!");

                LoadDanhSachTaiXe();
                XoaThongTinChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void XoaThongTinChiTiet()
        {
            txtMaTXTT.Clear();
            txtHoTenTT.Clear();
            txtSDTTT.Clear();
            txtGPLXTT.Clear();
            txtDiaChiTT.Clear();

            cboGioiTinhTT.SelectedIndex = -1;
            cboTrangThaiTT.SelectedIndex = -1;

            dtpNgaySinhTT.Value = DateTime.Today;
            dtpNgayCapTT.Value = DateTime.Today;
            dtpNgayHetHanTT.Value = DateTime.Today;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // =========================
                // CHẾ ĐỘ THÊM TÀI XẾ
                // =========================
                if (dangThemTaiXe)
                {
                    // Kiểm tra dữ liệu
                    if (string.IsNullOrWhiteSpace(txtMaTXTT.Text) ||
                        string.IsNullOrWhiteSpace(txtHoTenTT.Text) ||
                        string.IsNullOrWhiteSpace(txtSDTTT.Text) ||
                        string.IsNullOrWhiteSpace(txtGPLXTT.Text))
                    {
                        MessageBox.Show(
                            "Vui lòng nhập đầy đủ thông tin tài xế!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    // Kiểm tra mã tài xế đã tồn tại chưa
                    string maTX = txtMaTXTT.Text.Trim();

                    bool tonTai = db.TaiXes.Any(x => x.MaTX == maTX);

                    if (tonTai)
                    {
                        MessageBox.Show(
                            "Mã tài xế đã tồn tại!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    // Tạo tài xế mới
                    TaiXe tx = new TaiXe();

                    tx.MaTX = txtMaTXTT.Text.Trim();
                    tx.HoTen = txtHoTenTT.Text.Trim();
                    tx.NgaySinh = dtpNgaySinhTT.Value;
                    tx.GioiTinh = cboGioiTinhTT.Text;
                    tx.SoDienThoai = txtSDTTT.Text.Trim();
                    tx.SoBangLai = txtGPLXTT.Text.Trim();
                    tx.HangBang = "E";
                    tx.NgayCap = dtpNgayCapTT.Value;
                    tx.NgayHetHan = dtpNgayHetHanTT.Value;
                    tx.DiaChi = txtDiaChiTT.Text.Trim();
                    tx.TrangThai = cboTrangThaiTT.Text;

                    db.TaiXes.Add(tx);
                    db.SaveChanges();

                    MessageBox.Show(
                        "Thêm tài xế thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Chuyển về chế độ bình thường
                    dangThemTaiXe = false;

                    // Hiện lại danh sách
                    LoadDanhSachTaiXe();

                    // Chọn tài xế vừa thêm nếu muốn
                    LoadDanhSachTaiXe();
                }
                else
                {
                    // =========================
                    // CHẾ ĐỘ CẬP NHẬT
                    // =========================

                    string maTX = txtMaTXTT.Text.Trim();

                    var tx = db.TaiXes.FirstOrDefault(x => x.MaTX == maTX);

                    if (tx == null)
                    {
                        MessageBox.Show("Không tìm thấy tài xế!");
                        return;
                    }

                    tx.HoTen = txtHoTenTT.Text.Trim();
                    tx.NgaySinh = dtpNgaySinhTT.Value;
                    tx.GioiTinh = cboGioiTinhTT.Text;
                    tx.SoDienThoai = txtSDTTT.Text.Trim();
                    tx.SoBangLai = txtGPLXTT.Text.Trim();
                    tx.NgayCap = dtpNgayCapTT.Value;
                    tx.NgayHetHan = dtpNgayHetHanTT.Value;
                    tx.DiaChi = txtDiaChiTT.Text.Trim();
                    tx.TrangThai = cboTrangThaiTT.Text;

                    db.SaveChanges();

                    MessageBox.Show(
                        "Cập nhật tài xế thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadDanhSachTaiXe();
                }
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

        private void lblThongTinChiTiet_Click(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachTaiXe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string maTX = dgvDanhSachTaiXe.Rows[e.RowIndex]
                .Cells["mataixe"].Value?.ToString();

            if (string.IsNullOrEmpty(maTX))
                return;

            TaiXe tx = db.TaiXes
                .FirstOrDefault(x => x.MaTX == maTX);

            if (tx == null)
                return;

            maTXDangChon = tx.MaTX;

            // Thông tin chi tiết
            txtMaTXTT.Text = tx.MaTX;
            txtHoTenTT.Text = tx.HoTen;
            txtSDTTT.Text = tx.SoDienThoai;
            txtGPLXTT.Text = tx.SoBangLai;
            txtDiaChiTT.Text = tx.DiaChi;

            // Giới tính
            cboGioiTinhTT.SelectedItem = tx.GioiTinh;

            // Trạng thái
            cboTrangThaiTT.SelectedItem = tx.TrangThai;

            // Ngày sinh
            if (tx.NgaySinh.HasValue)
                dtpNgaySinhTT.Value = tx.NgaySinh.Value;
            else
                dtpNgaySinhTT.Value = DateTime.Today;

            // Ngày cấp
            if (tx.NgayCap.HasValue)
                dtpNgayCapTT.Value = tx.NgayCap.Value;
            else
                dtpNgayCapTT.Value = DateTime.Today;

            // Ngày hết hạn
            if (tx.NgayHetHan.HasValue)
                dtpNgayHetHanTT.Value = tx.NgayHetHan.Value;
            else
                dtpNgayHetHanTT.Value = DateTime.Today;
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

      

        private void btnThemTaiXe_Click(object sender, EventArgs e)
        {
            dangThemTaiXe = true;

            // Xóa thông tin cũ
            txtMaTXTT.Clear();
            txtHoTenTT.Clear();
            txtSDTTT.Clear();
            txtDiaChiTT.Clear();
            txtGPLXTT.Clear();

            cboGioiTinhTT.SelectedIndex = -1;
            cboTrangThaiTT.SelectedIndex = -1;

            // Đưa ngày về hiện tại
            dtpNgaySinhTT.Value = DateTime.Now;
            dtpNgayCapTT.Value = DateTime.Now;
            dtpNgayHetHanTT.Value = DateTime.Now;

            // Cho nhập mã tài xế
            txtMaTXTT.Enabled = true;

            // Đưa con trỏ vào mã tài xế
            txtMaTXTT.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtGPLX.Clear();

            cboTrangThai.SelectedIndex = -1;

            LoadDanhSachTaiXe();
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string hoTen = txtHoTen.Text.Trim();
                string soDienThoai = txtSoDienThoai.Text.Trim();
                string soBangLai = txtGPLX.Text.Trim();
                string trangThai = cboTrangThai.Text.Trim();

                var query = db.TaiXes.AsQueryable();

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
                        x.SoDienThoai.Contains(soDienThoai));
                }

                // Tìm theo giấy phép lái xe
                if (!string.IsNullOrEmpty(soBangLai))
                {
                    query = query.Where(x =>
                        x.SoBangLai.Contains(soBangLai));
                }

                // Tìm theo trạng thái
                if (!string.IsNullOrEmpty(trangThai))
                {
                    query = query.Where(x =>
                        x.TrangThai == trangThai);
                }

                var danhSach = query.ToList();

                // Xóa dữ liệu cũ trên bảng
                dgvDanhSachTaiXe.Rows.Clear();

                // Đổ kết quả tìm kiếm vào bảng
                int stt = 1;

                foreach (var tx in danhSach)
                {
                    dgvDanhSachTaiXe.Rows.Add(
                        stt++,
                        tx.MaTX,
                        tx.HoTen,
                        tx.SoDienThoai,
                        tx.SoBangLai,
                        tx.NgayCap?.ToString("dd/MM/yyyy"),
                        tx.NgayHetHan?.ToString("dd/MM/yyyy"),
                        tx.TrangThai
                    );
                }

                // Không tìm thấy
                if (danhSach.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy tài xế phù hợp!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void FrmTaiXe_Load(object sender, EventArgs e)
        {
            // Hiển thị ngày, giờ và tài khoản
            CapNhatNgayGio();

            // Tạo Timer
            timer = new Timer();
            timer.Interval = 1000; // 1 giây
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

            // Thứ + ngày
            lblNgay.Text = now.ToString("dddd, dd/MM/yyyy");

            // Giờ
            lblGio.Text = now.ToString("HH:mm:ss");

            // Tài khoản
            lblAdmin.Text = "ADMIN";
        }
    }
}
