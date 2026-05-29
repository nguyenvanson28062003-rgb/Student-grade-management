namespace quản_lí_điểm_sinh_viên
{
    partial class DangNhap
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            panel1 = new Panel();
            btnDangnhap = new Button();
            lblQuen = new Label();
            lblDangki = new Label();
            lbllogin = new Label();
            txtPass = new TextBox();
            txtUser = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDangnhap);
            panel1.Controls.Add(lblQuen);
            panel1.Controls.Add(lblDangki);
            panel1.Controls.Add(lbllogin);
            panel1.Controls.Add(txtPass);
            panel1.Controls.Add(txtUser);
            panel1.Location = new Point(120, 45);
            panel1.Name = "panel1";
            panel1.Size = new Size(573, 359);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnDangnhap
            // 
            btnDangnhap.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangnhap.Location = new Point(195, 258);
            btnDangnhap.Name = "btnDangnhap";
            btnDangnhap.Size = new Size(179, 58);
            btnDangnhap.TabIndex = 5;
            btnDangnhap.Text = "Đăng nhập";
            btnDangnhap.UseVisualStyleBackColor = true;
            btnDangnhap.Click += btnDangnhap_Click;
            // 
            // lblQuen
            // 
            lblQuen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblQuen.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQuen.Location = new Point(370, 205);
            lblQuen.Name = "lblQuen";
            lblQuen.Size = new Size(141, 36);
            lblQuen.TabIndex = 4;
            lblQuen.Text = "Quên mật khẩu";
            lblQuen.TextAlign = ContentAlignment.MiddleCenter;
            lblQuen.Click += lblQuen_Click;
            // 
            // lblDangki
            // 
            lblDangki.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblDangki.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDangki.ForeColor = SystemColors.ControlText;
            lblDangki.Location = new Point(82, 205);
            lblDangki.Name = "lblDangki";
            lblDangki.Size = new Size(107, 36);
            lblDangki.TabIndex = 3;
            lblDangki.Text = "Đăng kí";
            lblDangki.TextAlign = ContentAlignment.MiddleCenter;
            lblDangki.Click += lblDangki_Click;
            // 
            // lbllogin
            // 
            lbllogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbllogin.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbllogin.Location = new Point(126, 13);
            lbllogin.Name = "lbllogin";
            lbllogin.Size = new Size(336, 73);
            lbllogin.TabIndex = 2;
            lbllogin.Text = "LOGIN";
            lbllogin.TextAlign = ContentAlignment.MiddleCenter;
            lbllogin.Click += lbllogin_Click;
            // 
            // txtPass
            // 
            txtPass.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPass.Location = new Point(104, 143);
            txtPass.Multiline = true;
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(380, 48);
            txtPass.TabIndex = 1;
            txtPass.TextAlign = HorizontalAlignment.Center;
            txtPass.TextChanged += textBox2_TextChanged;
            // 
            // txtUser
            // 
            txtUser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtUser.Location = new Point(104, 89);
            txtUser.Multiline = true;
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(380, 48);
            txtUser.TabIndex = 0;
            txtUser.TextAlign = HorizontalAlignment.Center;
            txtUser.TextChanged += txtUser_TextChanged;
            // 
            // DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtUser;
        private TextBox txtPass;
        private Label lblDangki;
        private Label lbllogin;
        private Button btnDangnhap;
        private Label lblQuen;
    }
}
