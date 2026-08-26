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
    public partial class FrmQuenMatKhau : Form
    {
        public FrmQuenMatKhau()
        {
            InitializeComponent();
            txtMatKhauMoi.PasswordChar = '*';
            txtXacNhanMatKhau.PasswordChar = '*';
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtDangNhap.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhanMatKhau = txtXacNhanMatKhau.Text;
            // Kiểm tra tên đăng nhập
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }
            // Kiểm tra mật khẩu mới
            if (string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }
            // Kiểm tra xác nhận mật khẩu
            if (string.IsNullOrEmpty(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }
            // Kiểm tra 2 mật khẩu
            if (matKhauMoi != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return;
            }
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // Tìm tài khoản theo tên đăng nhập
                    var taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap);
                    // Không tìm thấy tài khoản
                    if (taiKhoan == null)
                    {
                        MessageBox.Show("Tên đăng nhập không tồn tại!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        txtDangNhap.Focus();
                        return;
                    }
                    // Chỉ cho khách hàng đổi mật khẩu
                    if (taiKhoan.Quyen != "Khách hàng")
                    {
                        MessageBox.Show("Chức năng này chỉ dành cho khách hàng!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    // Cập nhật mật khẩu mới
                    taiKhoan.MatKhau = matKhauMoi;
                    // Lưu xuống SQL Server
                    db.SaveChanges();
                    MessageBox.Show("Đặt lại mật khẩu thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    // Đóng Form quên mật khẩu
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại!\n\n" + ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
