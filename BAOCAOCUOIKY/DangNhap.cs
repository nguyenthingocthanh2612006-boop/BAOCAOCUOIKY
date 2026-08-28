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
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            // Kiểm tra bỏ trống
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // Tìm tài khoản trong SQL Server
                    var taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhau);
                    // Không tìm thấy
                    if (taiKhoan == null)
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMatKhau.Clear();
                        txtMatKhau.Focus();
                        return;
                    }
                    // LƯU THÔNG TIN NGƯỜI ĐANG ĐĂNG NHẬP
                    ThongTinDangNhap.TenDangNhap = taiKhoan.TenDangNhap;
                    ThongTinDangNhap.Quyen = taiKhoan.Quyen;
                    // Kiểm tra trạng thái tài khoản
                    if (taiKhoan.TrangThai != "Đang hoạt động")
                    {
                        MessageBox.Show("Tài khoản đang bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // PHÂN QUYỀN NHÂN VIÊN
                    else if (taiKhoan.Quyen == "Nhân viên")
                    {
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Mở trang chủ nhân viên
                        FrmTrangChuNhanVien frm = new FrmTrangChuNhanVien();
                        frm.Show();
                        // Ẩn Form đăng nhập
                        this.Hide();
                    }
                    // PHÂN QUYỀN KHÁCH HÀNG
                    else if (taiKhoan.Quyen == "Khách hàng")
                    {
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmTrangChuKhachHang frm = new FrmTrangChuKhachHang ();
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu!\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblQuenMatKhau_Click(object sender, EventArgs e)
        {
            FrmQuenMatKhau frm = new FrmQuenMatKhau();
            frm.ShowDialog();
        }

        private void lblDangKy_Click(object sender, EventArgs e)
        {
            FrmDangKy frm = new FrmDangKy();
            frm.ShowDialog();
        }
    }
}
