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
        }
    }
}
