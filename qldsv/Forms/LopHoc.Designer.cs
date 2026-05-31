namespace qldsv
{
    partial class LopHoc
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            label1 = new Label();
            button7 = new Button();
            button8 = new Button();
            comboBox1 = new ComboBox();
            label5 = new Label();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            cotMaLop = new DataGridViewTextBoxColumn();
            cotMH = new DataGridViewTextBoxColumn();
            cotGV = new DataGridViewTextBoxColumn();
            cotP = new DataGridViewTextBoxColumn();
            cotLH = new DataGridViewTextBoxColumn();
            cotSVDK = new DataGridViewTextBoxColumn();
            cotTT = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            button1.Location = new Point(12, 71);
            button1.Name = "button1";
            button1.Size = new Size(112, 43);
            button1.TabIndex = 4;
            button1.Text = "Tạo lớp HP";
            button1.UseVisualStyleBackColor = true;
            button2.Location = new Point(140, 71);
            button2.Name = "button2";
            button2.Size = new Size(112, 43);
            button2.TabIndex = 5;
            button2.Text = "Sửa";
            button2.UseVisualStyleBackColor = true;
            button3.Location = new Point(271, 71);
            button3.Name = "button3";
            button3.Size = new Size(112, 43);
            button3.TabIndex = 6;
            button3.Text = "Xóa";
            button3.UseVisualStyleBackColor = true;
            button4.Location = new Point(520, 71);
            button4.Name = "button4";
            button4.Size = new Size(112, 43);
            button4.TabIndex = 7;
            button4.Text = "Nhập điểm ";
            button4.UseVisualStyleBackColor = true;
            button5.Location = new Point(398, 71);
            button5.Name = "button5";
            button5.Size = new Size(112, 43);
            button5.TabIndex = 8;
            button5.Text = "Danh sách SV";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            button6.Location = new Point(647, 71);
            button6.Name = "button6";
            button6.Size = new Size(112, 43);
            button6.TabIndex = 9;
            button6.Text = "Export TKB";
            button6.UseVisualStyleBackColor = true;
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(157, 9);
            label1.Name = "label1";
            label1.Size = new Size(475, 52);
            label1.TabIndex = 10;
            label1.Text = "Quản lí lớp học phần";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            button7.Location = new Point(520, 129);
            button7.Name = "button7";
            button7.Size = new Size(112, 43);
            button7.TabIndex = 11;
            button7.Text = "Quay lại Menu";
            button7.UseVisualStyleBackColor = true;
            button8.Location = new Point(647, 129);
            button8.Name = "button8";
            button8.Size = new Size(112, 43);
            button8.TabIndex = 12;
            button8.Text = "Thoát";
            button8.UseVisualStyleBackColor = true;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "HK1 - Năm 1", "HK2 - Năm 1", "HK1 - Năm 2", "HK2 - Năm 2", "HK1 - Năm 3", "HK2 - Năm 3", "HK1 - Năm 4", "HK2 - Năm 4", "Học kỳ hè" });
            comboBox1.Location = new Point(140, 145);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(330, 28);
            comboBox1.TabIndex = 19;
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.Font = new Font("Times New Roman", 12F);
            label5.Location = new Point(12, 144);
            label5.Name = "label5";
            label5.Size = new Size(98, 28);
            label5.TabIndex = 20;
            label5.Text = "Học kỳ";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.IntegralHeight = false;
            comboBox2.Items.AddRange(new object[] { "Cấu trúc dữ liệu và giải thuật", "Lập trình hướng đối tượng", "Kĩ thuật lập trình", "Mạng máy tính", "Trí tuệ nhân tạo", "Web 1" });
            comboBox2.Location = new Point(140, 192);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(204, 28);
            comboBox2.TabIndex = 21;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.IntegralHeight = false;
            comboBox3.Items.AddRange(new object[] { "Nguyễn Văn A", "Nguyễn Văn D", "Trần Thanh T", "Bùi Thị H" });
            comboBox3.Location = new Point(525, 192);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(234, 28);
            comboBox3.TabIndex = 22;
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(12, 192);
            label2.Name = "label2";
            label2.Size = new Size(98, 28);
            label2.TabIndex = 23;
            label2.Text = "Môn học";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(412, 191);
            label3.Name = "label3";
            label3.Size = new Size(98, 28);
            label3.TabIndex = 24;
            label3.Text = "Giảng viên";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMaLop, cotMH, cotGV, cotP, cotLH, cotSVDK, cotTT });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.GridColor = Color.Gainsboro;
            dataGridView1.Location = new Point(12, 226);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 220);
            dataGridView1.TabIndex = 25;
            cotMaLop.HeaderText = "Mã Lớp";
            cotMaLop.MinimumWidth = 6;
            cotMaLop.Name = "cotMaLop";
            cotMH.HeaderText = "Môn Học";
            cotMH.MinimumWidth = 6;
            cotMH.Name = "cotMH";
            cotGV.HeaderText = "GV phụ trách";
            cotGV.MinimumWidth = 6;
            cotGV.Name = "cotGV";
            cotP.HeaderText = "Phòng";
            cotP.MinimumWidth = 6;
            cotP.Name = "cotP";
            cotLH.HeaderText = "Lịch Học";
            cotLH.MinimumWidth = 6;
            cotLH.Name = "cotLH";
            cotSVDK.HeaderText = "SV Đăng Ký";
            cotSVDK.MinimumWidth = 6;
            cotSVDK.Name = "cotSVDK";
            cotTT.HeaderText = "Trạng Thái";
            cotTT.MinimumWidth = 6;
            cotTT.Name = "cotTT";
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(label1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "LopHoc";
            Text = "LopHoc";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label label1;
        private Button button7;
        private Button button8;
        private ComboBox comboBox1;
        private Label label5;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Label label2;
        private Label label3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMaLop;
        private DataGridViewTextBoxColumn cotMH;
        private DataGridViewTextBoxColumn cotGV;
        private DataGridViewTextBoxColumn cotP;
        private DataGridViewTextBoxColumn cotLH;
        private DataGridViewTextBoxColumn cotSVDK;
        private DataGridViewTextBoxColumn cotTT;
    }
}
