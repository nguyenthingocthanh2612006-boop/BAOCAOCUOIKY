namespace BAOCAOCUOIKY
{
    partial class FrmXacNhanMa
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
            this.lblTieuDe = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblThongBao = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMaXacNhan = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnGuiLaiMa = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnQuayLai = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(292, 24);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(300, 37);
            this.lblTieuDe.TabIndex = 27;
            this.lblTieuDe.Text = "XÁC NHẬN MÃ";
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = false;
            this.lblThongBao.BackColor = System.Drawing.Color.Transparent;
            this.lblThongBao.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Location = new System.Drawing.Point(101, 122);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(537, 28);
            this.lblThongBao.TabIndex = 28;
            this.lblThongBao.Text = "Mã xác nhận đã được gửi đến Email của bạn";
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = false;
            this.lblMa.BackColor = System.Drawing.Color.Transparent;
            this.lblMa.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMa.Location = new System.Drawing.Point(101, 188);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(200, 28);
            this.lblMa.TabIndex = 34;
            this.lblMa.Text = "Mã xác nhận";
            // 
            // txtMaXacNhan
            // 
            this.txtMaXacNhan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaXacNhan.DefaultText = "";
            this.txtMaXacNhan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaXacNhan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaXacNhan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaXacNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaXacNhan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaXacNhan.Location = new System.Drawing.Point(254, 177);
            this.txtMaXacNhan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaXacNhan.Name = "txtMaXacNhan";
            this.txtMaXacNhan.PlaceholderText = "";
            this.txtMaXacNhan.SelectedText = "";
            this.txtMaXacNhan.Size = new System.Drawing.Size(527, 48);
            this.txtMaXacNhan.TabIndex = 35;
            this.txtMaXacNhan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaXacNhan_KeyPress);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(92, 300);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(200, 45);
            this.btnXacNhan.TabIndex = 36;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnGuiLaiMa
            // 
            this.btnGuiLaiMa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiLaiMa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiLaiMa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuiLaiMa.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuiLaiMa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuiLaiMa.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiLaiMa.ForeColor = System.Drawing.Color.White;
            this.btnGuiLaiMa.Location = new System.Drawing.Point(340, 300);
            this.btnGuiLaiMa.Name = "btnGuiLaiMa";
            this.btnGuiLaiMa.Size = new System.Drawing.Size(200, 45);
            this.btnGuiLaiMa.TabIndex = 37;
            this.btnGuiLaiMa.Text = "Gửi lại mã";
            this.btnGuiLaiMa.Click += new System.EventHandler(this.btnGuiLaiMa_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuayLai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuayLai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuayLai.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuayLai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuayLai.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Location = new System.Drawing.Point(590, 300);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(200, 45);
            this.btnQuayLai.TabIndex = 38;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // FrmXacNhanMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 427);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnGuiLaiMa);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtMaXacNhan);
            this.Controls.Add(this.lblMa);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "FrmXacNhanMa";
            this.Text = "Xác Nhận Mã";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTieuDe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThongBao;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMa;
        private Guna.UI2.WinForms.Guna2TextBox txtMaXacNhan;
        private Guna.UI2.WinForms.Guna2GradientButton btnXacNhan;
        private Guna.UI2.WinForms.Guna2GradientButton btnGuiLaiMa;
        private Guna.UI2.WinForms.Guna2GradientButton btnQuayLai;
    }
}