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
    public partial class FrmTraCuuVe : Form
    {
        public FrmTraCuuVe()
        {
            InitializeComponent();

            // Tra cứu vé sáng
            lblTraCuuVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTraCuuVe.Font = new Font(
                lblTraCuuVe.Font,
                FontStyle.Bold
            );
        }
        public FrmTraCuuVe(
            string noiDi,
            string noiDen,
            DateTime ngayDi,
            string soLuongVe)
        {
            InitializeComponent();
            // Nhận thông tin từ trang chủ
            cboNoiDi.Text = noiDi;
            cboNoiDen.Text = noiDen;
            dtpNgayDi.Value = ngayDi;
            cboSoLuongVe.Text = soLuongVe;
            lblTraCuuVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Bold);
            
        }

        private void lblTrangChu_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.FromArgb(0, 102, 204);
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Bold);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
            // MỞ FORM TRANG CHỦ
            FrmTrangChuKhachHang frm = new FrmTrangChuKhachHang();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void lblDatVe_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Bold);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
            // MỞ FORM ĐẶT VÉ
            FrmDatVe frm = new FrmDatVe("", "", DateTime.Today);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void lblTraCuuVe_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Bold);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
        }

        private void lblTuyenXe_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.FromArgb(0, 102, 204);
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Bold);
            lblHoaDon.ForeColor = Color.Black;
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Regular);
        }

        private void lblHoaDon_Click(object sender, EventArgs e)
        {
            //MÀU CHO CÁC NÚT
            lblTrangChu.ForeColor = Color.Black;
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Regular);
            lblDatVe.ForeColor = Color.Black;
            lblDatVe.Font = new Font(lblDatVe.Font, FontStyle.Regular);
            lblTraCuuVe.ForeColor = Color.Black;
            lblTraCuuVe.Font = new Font(lblTraCuuVe.Font, FontStyle.Regular);
            lblTuyenXe.ForeColor = Color.Black;
            lblTuyenXe.Font = new Font(lblTuyenXe.Font, FontStyle.Regular);
            lblHoaDon.ForeColor = Color.FromArgb(0, 102, 204);
            lblHoaDon.Font = new Font(lblHoaDon.Font, FontStyle.Bold);
        }

        private void FrmTraCuuVe_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;
        }

    }
}
