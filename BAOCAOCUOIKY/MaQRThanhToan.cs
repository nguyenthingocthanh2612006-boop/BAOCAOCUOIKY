using System;
using QRCoder;
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
    public partial class FrmMaQRThanhToan : Form
    {
        private decimal tongTien;
        public FrmMaQRThanhToan(decimal tongTien)
        {
            InitializeComponent();
            this.tongTien = tongTien;
            // Cho phép bấm vào mã QR
            picQR.Cursor = Cursors.Hand;
        }

        private void picQR_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Xác nhận thanh toán?\n\n" +
                "Số tiền: " + tongTien.ToString("N0") + "đ",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Thanh toán thành công!\n\n" +
                    "Số tiền: " + tongTien.ToString("N0") + "đ",
                    "Thanh toán",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đã hủy thanh toán.","Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
