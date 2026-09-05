namespace BAOCAOCUOIKY
{
    partial class FrmMaQRThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaQRThanhToan));
            this.lblChuyenxe = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.picQR = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.SuspendLayout();
            // 
            // lblChuyenxe
            // 
            this.lblChuyenxe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblChuyenxe.AutoSize = false;
            this.lblChuyenxe.BackColor = System.Drawing.Color.Transparent;
            this.lblChuyenxe.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuyenxe.Location = new System.Drawing.Point(49, 21);
            this.lblChuyenxe.Name = "lblChuyenxe";
            this.lblChuyenxe.Size = new System.Drawing.Size(342, 40);
            this.lblChuyenxe.TabIndex = 63;
            this.lblChuyenxe.Text = "Thanh toán bằng chuyển khoản";
            // 
            // picQR
            // 
            this.picQR.Image = ((System.Drawing.Image)(resources.GetObject("picQR.Image")));
            this.picQR.ImageRotate = 0F;
            this.picQR.Location = new System.Drawing.Point(59, 67);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(300, 307);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.TabIndex = 64;
            this.picQR.TabStop = false;
            this.picQR.Click += new System.EventHandler(this.picQR_Click);
            // 
            // FrmMaQRThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 436);
            this.Controls.Add(this.picQR);
            this.Controls.Add(this.lblChuyenxe);
            this.Name = "FrmMaQRThanhToan";
            this.Text = "Mã QR";
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblChuyenxe;
        private Guna.UI2.WinForms.Guna2PictureBox picQR;
    }
}