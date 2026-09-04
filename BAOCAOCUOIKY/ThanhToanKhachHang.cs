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
    public partial class FrmThanhToan : Form
    {
        private string noiDi;
        private string noiDen;
        private string gioDi;
        private string gioDen;
        private DateTime ngayDi;
        private string bienSo;
        private string ghe;
        private int soLuong;
        private decimal giaVe;
        private decimal tongTien;
            public FrmThanhToan(
                string noiDi,
                string noiDen,
                string gioDi,
                string gioDen,
                DateTime ngayDi,
                string bienSo,
                string ghe,
                int soLuong,
                decimal giaVe,
                decimal tongTien)
        {
            InitializeComponent();

            this.noiDi = noiDi;
            this.noiDen = noiDen;
            this.gioDi = gioDi;
            this.gioDen = gioDen;
            this.ngayDi = ngayDi;
            this.bienSo = bienSo;
            this.ghe = ghe;
            this.soLuong = soLuong;
            this.giaVe = giaVe;
            this.tongTien = tongTien;

            HienThiThongTin();
        }
        private void HienThiThongTin()
        {
            lblTuyen7.Text = noiDi + " → " + noiDen;
            lblChuyen1.Text = gioDi + " → " + gioDen;
            lblXe4.Text = "29 chỗ";
            lblBienSo1.Text = bienSo;
            lblNgayDi2.Text = ngayDi.ToString("dd-MM-yyyy");
            lblGheDaChon1.Text = ghe;

            // Giá 1 vé
            lblGia5.Text = giaVe.ToString("N0") + "đ";

            // Số lượng vé
            lblSoLuong3.Text = soLuong + " vé";

            // Tạm tính
            decimal tamTinh = giaVe * soLuong;

            // Phí dịch vụ = Tổng tiền - Tạm tính
            decimal phiDichVu = tongTien - tamTinh;

            lblTien.Text = tamTinh.ToString("N0") + "đ";
            lblPhiDichVu.Text = phiDichVu.ToString("N0") + "đ";
            lblTongTien.Text = tongTien.ToString("N0") + "đ";
        }
        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            // Lấy thông tin khách hàng đang đăng nhập
            using (var db = new QuanLyVeXeModel())
            {
                string tenDangNhap = ThongTinDangNhap.TenDangNhap;

                var taiKhoan = db.TaiKhoans
                    .FirstOrDefault(x => x.TenDangNhap == tenDangNhap);

                if (taiKhoan != null)
                {
                    txtDangNhap.Text = taiKhoan.TenDangNhap;
                    txtSoDienThoai.Text = taiKhoan.SoDienThoai;
                    txtEmail.Text = taiKhoan.Email;
                }
            }
        }

        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            // Chưa chọn phương thức
            if (!rdoTienMat.Checked && !rdoChuyenKhoan.Checked)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            // Chọn tiền mặt
            if (rdoTienMat.Checked)
            {
                MessageBox.Show("Vui lòng thanh toán tiền mặt khi lên xe.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Chọn chuyển khoản
            if (rdoChuyenKhoan.Checked)
            {
                FrmMaQRThanhToan frm = new FrmMaQRThanhToan(tongTien);
                frm.ShowDialog();
            }
        }
    }
}
