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
            // Kiểm tra xác nhận mật khẩu
            if (string.IsNullOrEmpty(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
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
                    // Tạo mã tài khoản
                    int soThuTu = db.TaiKhoans.Count() + 1;
                    string maTK = "TK" + soThuTu.ToString("D3");
                    // Tạo tài khoản khách hàng
                    TaiKhoan taiKhoan = new TaiKhoan
                    {
                        MaTK = maTK,
                        MaNV = null,
                        TenDangNhap = tenDangNhap,
                        MatKhau = matKhau,
                        Quyen = "Khách hàng",
                        TrangThai = "Đang hoạt động"
                    };
                    // Lưu vào SQL
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    MessageBox.Show("Đăng ký tài khoản thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    // Xóa dữ liệu sau khi đăng ký
                    txtDangNhap.Clear();
                    txtMatKhau.Clear();
                    txtXacNhanMatKhau.Clear();
                    txtDangNhap.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng ký thất bại!\n\n" + ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
