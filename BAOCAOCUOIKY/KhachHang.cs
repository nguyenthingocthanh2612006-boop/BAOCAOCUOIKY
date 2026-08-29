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
    public partial class FrmTrangChuKhachHang : Form
    {
        public FrmTrangChuKhachHang()
        {
            InitializeComponent();
            lblTrangChu.ForeColor = Color.FromArgb(0, 102, 204);
            lblTrangChu.Font = new Font(lblTrangChu.Font, FontStyle.Bold);
        }

        private void FrmTrangChuKhachHang_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = ThongTinDangNhap.TenDangNhap;
            cboNoiDi.Items.Clear();
            cboNoiDen.Items.Clear();
            string[] diaDiem =
            {
                "Cao Lãnh",
                "Sa Đéc",
                "Hồng Ngự",
                "Lai Vung",
                "Lấp Vò",
                "Tam Nông"
            };
            cboNoiDi.Items.AddRange(diaDiem);
            cboNoiDen.Items.AddRange(diaDiem);
            cboNoiDi.SelectedIndex = -1;
            cboNoiDen.SelectedIndex = -1;
            // Chọn mặc định 1 vé
            cboSoLuongVe.Items.Clear();

            for (int i = 1; i <= 29; i++)
            {
                cboSoLuongVe.Items.Add(i + " vé");
            }
            cboSoLuongVe.SelectedIndex = 0;
            // Ngày đi
            dtpNgayDi.MinDate = DateTime.Today;
            dtpNgayDi.Value = DateTime.Today;
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
            FrmDatVe frm = new FrmDatVe();
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
            FrmTraCuuVe frm = new FrmTraCuuVe();
            this.Hide();
            frm.ShowDialog();
            this.Show();
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (cboNoiDi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nơi đi!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cboNoiDi.Focus();
                return;
            }

            if (cboNoiDen.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nơi đến!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cboNoiDen.Focus();
                return;
            }

            if (cboNoiDi.Text == cboNoiDen.Text)
            {
                MessageBox.Show("Nơi đi và nơi đến không được giống nhau!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            // Lấy thông tin người dùng đã chọn
            string noiDi = cboNoiDi.Text;
            string noiDen = cboNoiDen.Text;
            DateTime ngayDi = dtpNgayDi.Value;
            string soLuongVe = cboSoLuongVe.Text;
            // Mở form Tra Cứu Vé và truyền thông tin qua
            FrmTraCuuVe frm = new FrmTraCuuVe (
                noiDi,
                noiDen,
                ngayDi,
                soLuongVe
            );
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnDatVe1_Click(object sender, EventArgs e)
        {
            FrmDatVe frm = new FrmDatVe ();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnDatVe2_Click(object sender, EventArgs e)
        {
            FrmDatVe frm = new FrmDatVe();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnDatVe3_Click(object sender, EventArgs e)
        {
            FrmDatVe frm = new FrmDatVe();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnDatVe4_Click(object sender, EventArgs e)
        {
            FrmDatVe frm = new FrmDatVe();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnDatVe5_Click(object sender, EventArgs e)
        {
            FrmDatVe frm = new FrmDatVe();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
