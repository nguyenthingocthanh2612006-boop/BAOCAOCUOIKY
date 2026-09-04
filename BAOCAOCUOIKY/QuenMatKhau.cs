using BAOCAOCUOIKY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAOCAOCUOIKY
{
    public partial class FrmQuenMatKhau : Form
    {
        private string maXacNhan;
        private string emailNguoiDung;
        private bool daXacNhanMa = false;

        public FrmQuenMatKhau()
        {
            InitializeComponent();
            txtMatKhauMoi.PasswordChar = '*';
            txtXacNhanMatKhau.PasswordChar = '*';
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // CHƯA XÁC NHẬN OTP THÌ KHÔNG ĐƯỢC ĐỔI MẬT KHẨU
            if (!daXacNhanMa)
            {
                MessageBox.Show("Bạn phải gửi và xác nhận mã trước khi đổi mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có tối thiểu 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnGuiMa_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtDangNhap.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Kiểm tra tên đăng nhập
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }

            // Kiểm tra số điện thoại
            if (string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }
            if (soDienThoai.Length != 10 || !soDienThoai.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            // Kiểm tra Email
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // Kiểm tra cả Tên đăng nhập + SĐT + Email
                    var taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap && x.SoDienThoai == soDienThoai && x.Email == email);

                    // Không tìm thấy tài khoản
                    if (taiKhoan == null)
                    {
                        MessageBox.Show("Thông tin tài khoản không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Chỉ cho khách hàng khôi phục
                    if (taiKhoan.Quyen != "Khách hàng")
                    {
                        MessageBox.Show("Chức năng này chỉ dành cho khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // TẠO MÃ XÁC NHẬN 6 SỐ
                    Random random = new Random();
                    maXacNhan = random.Next(100000, 1000000).ToString();

                    // Lấy Email trong database
                    emailNguoiDung = taiKhoan.Email;

                    // Hiển thị mã OTP để kiểm tra
                    MessageBox.Show("Mã xác nhận của bạn là: " + maXacNhan,"Mã xác nhận",MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // MỞ FORM NHẬP MÃ
                    FrmXacNhanMa frm = new FrmXacNhanMa(maXacNhan, emailNguoiDung);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        daXacNhanMa = true;
                        MessageBox.Show("Xác nhận mã thành công! Bây giờ bạn có thể đổi mật khẩu.","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi mã xác nhận!\n\n" + ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
             // Chỉ cho nhập số và phím điều khiển (Backspace, Delete...)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
