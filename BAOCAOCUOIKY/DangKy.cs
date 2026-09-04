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
    public partial class FrmDangKy : Form
    {
        public FrmDangKy()
        {
            InitializeComponent();
            txtMatKhau.PasswordChar = '*';
            txtXacNhanMatKhau.PasswordChar = '*';
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string xacNhanMatKhau = txtXacNhanMatKhau.Text;
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Kiểm tra tên đăng nhập
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            //Kiểm tra số điện thoại
            if (string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }
            if (soDienThoai.Length != 10 || !soDienThoai.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            // Kiểm tra email
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (string.IsNullOrEmpty(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }
            if (matKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có tối thiểu 6 ký tự!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }
            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }
            try
            {
                using (var db = new QuanLyVeXeModel())
                {
                    // Kiểm tra tên đăng nhập đã tồn tại
                    bool daTonTai = db.TaiKhoans.Any(x => x.TenDangNhap == tenDangNhap);
                    if (daTonTai)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        txtDangNhap.Focus();
                        return;
                    }

                    // Kiểm tra số điện thoại đã tồn tại
                    bool sdtDaTonTai = db.TaiKhoans.Any(x => x.SoDienThoai == soDienThoai);
                    if (sdtDaTonTai)
                    {
                        MessageBox.Show("Số điện thoại này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoDienThoai.Focus();
                        return;
                    }

                    // Kiểm tra Email đã tồn tại
                    bool emailDaTonTai = db.TaiKhoans.Any(x => x.Email == email);
                    if (emailDaTonTai)
                    {
                        MessageBox.Show("Email này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Focus();
                        return;
                    }

                    // Tạo mã tài khoản
                    int soThuTu = db.TaiKhoans.Count() + 1;
                    string maTK = "TK" + soThuTu.ToString("D3");
                    
                    // Tạo tài khoản khách hàng
                    TaiKhoan taiKhoan = new TaiKhoan
                    {
                        MaTK = maTK,
                        MaNV = null,
                        TenDangNhap = tenDangNhap,
                        SoDienThoai = soDienThoai,
                        Email = email,
                        MatKhau = matKhau,
                        Quyen = "Khách hàng",
                        TrangThai = "Đang hoạt động"
                    };

                    // Lưu vào SQL
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    MessageBox.Show("Đăng ký tài khoản thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng ký thất bại!\n\n" + ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
