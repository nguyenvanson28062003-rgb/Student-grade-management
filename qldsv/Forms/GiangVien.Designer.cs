namespace qldsv
{
    partial class GiangVien
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            label1 = new Label();
            button9 = new Button();
            button5 = new Button();
            button2 = new Button();
            button4 = new Button();
            button7 = new Button();
            button1 = new Button();
            button3 = new Button();
            button6 = new Button();
            textBox2 = new TextBox();
            label2 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            button10 = new Button();
            dataGridView1 = new DataGridView();
            cotMGV = new DataGridViewTextBoxColumn();
            cotHT = new DataGridViewTextBoxColumn();
            cotHV = new DataGridViewTextBoxColumn();
            cotCN = new DataGridViewTextBoxColumn();
            cotKhoa = new DataGridViewTextBoxColumn();
            cotEm = new DataGridViewTextBoxColumn();
            cotSDT = new DataGridViewTextBoxColumn();
            cotTT = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(165, 9);
            label1.Name = "label1";
            label1.Size = new Size(475, 52);
            label1.TabIndex = 15;
            label1.Text = "Quản lí giảng viên";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button9
            // 
            button9.Location = new Point(12, 77);
            button9.Name = "button9";
            button9.Size = new Size(112, 43);
            button9.TabIndex = 16;
            button9.Text = "Thêm mới";
            button9.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(146, 77);
            button5.Name = "button5";
            button5.Size = new Size(112, 43);
            button5.TabIndex = 17;
            button5.Text = "Sửa";
            button5.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(280, 77);
            button2.Name = "button2";
            button2.Size = new Size(112, 43);
            button2.TabIndex = 18;
            button2.Text = "Xóa";
            button2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(548, 77);
            button4.Name = "button4";
            button4.Size = new Size(112, 43);
            button4.TabIndex = 19;
            button4.Text = "Xem hồ sơ";
            button4.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(414, 77);
            button7.Name = "button7";
            button7.Size = new Size(112, 43);
            button7.TabIndex = 20;
            button7.Text = "Export";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button1
            // 
            button1.Location = new Point(682, 77);
            button1.Name = "button1";
            button1.Size = new Size(112, 43);
            button1.TabIndex = 21;
            button1.Text = "Phân công";
            button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(682, 141);
            button3.Name = "button3";
            button3.Size = new Size(112, 43);
            button3.TabIndex = 22;
            button3.Text = "Quay về MN";
            button3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(548, 141);
            button6.Name = "button6";
            button6.Size = new Size(112, 43);
            button6.TabIndex = 23;
            button6.Text = "Thoát";
            button6.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(149, 156);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(278, 28);
            textBox2.TabIndex = 24;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(12, 156);
            label2.Name = "label2";
            label2.Size = new Size(131, 34);
            label2.TabIndex = 25;
            label2.Text = "Họ Tên";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.IntegralHeight = false;
            comboBox2.Items.AddRange(new object[] { "Công Nghệ Thông Tin", "", "Khoa Học Máy Tính", "", "Kỹ Thuật Phần Mềm", "", "Hệ Thống Thông Tin", "", "An Toàn Thông Tin", "", "Mạng Máy Tính & Truyền Thông", "", "Điện - Điện Tử", "", "Cơ Khí", "", "Xây Dựng", "", "Kiến Trúc", "", "Kinh Tế" });
            comboBox2.Location = new Point(149, 201);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(224, 28);
            comboBox2.TabIndex = 29;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "Cử Nhân (CN)", "", "Kỹ Sư (KS)", "", "Thạc Sĩ (ThS)", "", "Tiến Sĩ (TS)", "", "Phó Giáo Sư (PGS)", "", "Giáo Sư (GS)" });
            comboBox1.Location = new Point(510, 205);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(224, 28);
            comboBox1.TabIndex = 30;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(12, 201);
            label3.Name = "label3";
            label3.Size = new Size(131, 34);
            label3.TabIndex = 31;
            label3.Text = "Khoa";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Times New Roman", 12F);
            label4.Location = new Point(389, 201);
            label4.Name = "label4";
            label4.Size = new Size(115, 34);
            label4.TabIndex = 32;
            label4.Text = "Học vị";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // button10
            // 
            button10.FlatStyle = FlatStyle.System;
            button10.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button10.Location = new Point(740, 197);
            button10.Name = "button10";
            button10.Size = new Size(44, 38);
            button10.TabIndex = 34;
            button10.Text = "🔍";
            button10.TextAlign = ContentAlignment.BottomCenter;
            button10.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMGV, cotHT, cotHV, cotCN, cotKhoa, cotEm, cotSDT, cotTT });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.GridColor = Color.Gainsboro;
            dataGridView1.Location = new Point(12, 239);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 203);
            dataGridView1.TabIndex = 35;
            // 
            // cotMGV
            // 
            cotMGV.HeaderText = "MGV";
            cotMGV.MinimumWidth = 6;
            cotMGV.Name = "cotMGV";
            // 
            // cotHT
            // 
            cotHT.HeaderText = "Họ Tên";
            cotHT.MinimumWidth = 6;
            cotHT.Name = "cotHT";
            // 
            // cotHV
            // 
            cotHV.HeaderText = "Học Vị";
            cotHV.MinimumWidth = 6;
            cotHV.Name = "cotHV";
            // 
            // cotCN
            // 
            cotCN.HeaderText = "Chuyên Ngành";
            cotCN.MinimumWidth = 6;
            cotCN.Name = "cotCN";
            // 
            // cotKhoa
            // 
            cotKhoa.HeaderText = "Khoa";
            cotKhoa.MinimumWidth = 6;
            cotKhoa.Name = "cotKhoa";
            // 
            // cotEm
            // 
            cotEm.HeaderText = "Email";
            cotEm.MinimumWidth = 6;
            cotEm.Name = "cotEm";
            // 
            // cotSDT
            // 
            cotSDT.HeaderText = "SĐT";
            cotSDT.MinimumWidth = 6;
            cotSDT.Name = "cotSDT";
            // 
            // cotTT
            // 
            cotTT.HeaderText = "Trạng Thái";
            cotTT.MinimumWidth = 6;
            cotTT.Name = "cotTT";
            // 
            // GiangVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button10);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(comboBox1);
            Controls.Add(comboBox2);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(button6);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(button7);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button5);
            Controls.Add(button9);
            Controls.Add(label1);
            Name = "GiangVien";
            Text = "GiangVien";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button9;
        private Button button5;
        private Button button2;
        private Button button4;
        private Button button7;
        private Button button1;
        private Button button3;
        private Button button6;
        private TextBox textBox2;
        private Label label2;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label3;
        private Label label4;
        private Button button10;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMGV;
        private DataGridViewTextBoxColumn cotHT;
        private DataGridViewTextBoxColumn cotHV;
        private DataGridViewTextBoxColumn cotCN;
        private DataGridViewTextBoxColumn cotKhoa;
        private DataGridViewTextBoxColumn cotEm;
        private DataGridViewTextBoxColumn cotSDT;
        private DataGridViewTextBoxColumn cotTT;
    }
}