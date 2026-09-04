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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAOCAOCUOIKY
{
    public partial class FrmXacNhanMa : Form
    {
        // Mã xác nhận, eamil
        private string maXacNhan;
        private string email;

        public FrmXacNhanMa(string ma, string emailNguoiDung)
        {
            // Nhận mã và email từ Form Quên Mật Khẩu
            InitializeComponent();
            maXacNhan = ma;
            email = emailNguoiDung;
            lblThongBao.Text = "Mã xác nhận đã được gửi đến Email: " + email;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string maNhap = txtMaXacNhan.Text.Trim();
            if (string.IsNullOrEmpty(maNhap))
            {
                MessageBox.Show("Vui lòng nhập mã xác nhận!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtMaXacNhan.Focus();
                return;
            }

            // Chỉ cho nhập đúng 6 số
            if (maNhap.Length != 6 || !maNhap.All(char.IsDigit))
            {
                MessageBox.Show("Mã xác nhận phải gồm đúng 6 số!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtMaXacNhan.Clear();
                txtMaXacNhan.Focus();
                return;
            }

            // Kiểm tra mã
            if (maNhap == maXacNhan)
            {
                MessageBox.Show("Xác nhận mã thành công!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã xác nhận không đúng!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtMaXacNhan.Clear();
                txtMaXacNhan.Focus();
            }
        }
        private void txtMaXacNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
       
        private void btnGuiLaiMa_Click(object sender, EventArgs e)
        {
            // Tạo mã OTP mới
            Random random = new Random();
            maXacNhan = random.Next(100000, 1000000).ToString();
            try
            {
                MailMessage mail = new MailMessage();
                MessageBox.Show("Mã xác nhận mới của bạn là: " + maXacNhan,"Mã xác nhận",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtMaXacNhan.Clear();
                txtMaXacNhan.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể gửi lại mã OTP!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
