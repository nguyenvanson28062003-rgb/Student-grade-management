namespace qldsv
{
    partial class SinhVien
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
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            label1 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            dataGridView1 = new DataGridView();
            cotMSV = new DataGridViewTextBoxColumn();
            cotHT = new DataGridViewTextBoxColumn();
            cotNS = new DataGridViewTextBoxColumn();
            cotL = new DataGridViewTextBoxColumn();
            cotKhoa = new DataGridViewTextBoxColumn();
            cotGPA = new DataGridViewTextBoxColumn();
            cotTT = new DataGridViewTextBoxColumn();
            button10 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(435, 136);
            button1.Name = "button1";
            button1.Size = new Size(112, 43);
            button1.TabIndex = 5;
            button1.Text = "Quay về MN";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(353, 72);
            button2.Name = "button2";
            button2.Size = new Size(112, 43);
            button2.TabIndex = 6;
            button2.Text = "Xóa";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(658, 72);
            button3.Name = "button3";
            button3.Size = new Size(112, 43);
            button3.TabIndex = 7;
            button3.Text = "Xem điểm";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(511, 72);
            button4.Name = "button4";
            button4.Size = new Size(112, 43);
            button4.TabIndex = 8;
            button4.Text = "Xem hồ sơ";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(194, 72);
            button5.Name = "button5";
            button5.Size = new Size(112, 43);
            button5.TabIndex = 9;
            button5.Text = "Sửa";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(587, 136);
            button6.Name = "button6";
            button6.Size = new Size(112, 43);
            button6.TabIndex = 10;
            button6.Text = "Thoát";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(275, 136);
            button7.Name = "button7";
            button7.Size = new Size(112, 43);
            button7.TabIndex = 11;
            button7.Text = "Export";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(112, 136);
            button8.Name = "button8";
            button8.Size = new Size(112, 43);
            button8.TabIndex = 12;
            button8.Text = "Import Excel";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(27, 72);
            button9.Name = "button9";
            button9.Size = new Size(112, 43);
            button9.TabIndex = 13;
            button9.Text = "Thêm mới";
            button9.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(148, 9);
            label1.Name = "label1";
            label1.Size = new Size(475, 52);
            label1.TabIndex = 14;
            label1.Text = "Quản lí sinh viên";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(145, 191);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(204, 28);
            textBox2.TabIndex = 15;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(8, 185);
            label2.Name = "label2";
            label2.Size = new Size(131, 34);
            label2.TabIndex = 24;
            label2.Text = "MSV/Họ Tên";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(391, 185);
            label3.Name = "label3";
            label3.Size = new Size(131, 34);
            label3.TabIndex = 26;
            label3.Text = "Khoa";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "Cấu trúc dữ liệu và giải thuật", "Lập trình hướng đối tượng", "Kĩ thuật lập trình", "Mạng máy tính", "Trí tuệ nhân tạo", "Web 1" });
            comboBox1.Location = new Point(528, 191);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(204, 28);
            comboBox1.TabIndex = 27;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.IntegralHeight = false;
            comboBox2.Items.AddRange(new object[] { "524CNT", "524QTK", "524DTV", "523YCT" });
            comboBox2.Location = new Point(145, 225);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(204, 28);
            comboBox2.TabIndex = 28;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.IntegralHeight = false;
            comboBox3.Items.AddRange(new object[] { "Cấu trúc dữ liệu và giải thuật", "Lập trình hướng đối tượng", "Kĩ thuật lập trình", "Mạng máy tính", "Trí tuệ nhân tạo", "Web 1" });
            comboBox3.Location = new Point(528, 225);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(204, 28);
            comboBox3.TabIndex = 29;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Times New Roman", 12F);
            label4.Location = new Point(391, 219);
            label4.Name = "label4";
            label4.Size = new Size(131, 34);
            label4.TabIndex = 30;
            label4.Text = "Tình Trạng";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.Font = new Font("Times New Roman", 12F);
            label5.Location = new Point(8, 219);
            label5.Name = "label5";
            label5.Size = new Size(131, 34);
            label5.TabIndex = 31;
            label5.Text = "Lớp";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMSV, cotHT, cotNS, cotL, cotKhoa, cotGPA, cotTT });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.GridColor = Color.Gainsboro;
            dataGridView1.Location = new Point(12, 269);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 203);
            dataGridView1.TabIndex = 32;
            // 
            // cotMSV
            // 
            cotMSV.HeaderText = "MSV";
            cotMSV.MinimumWidth = 6;
            cotMSV.Name = "cotMSV";
            // 
            // cotHT
            // 
            cotHT.HeaderText = "Họ Tên";
            cotHT.MinimumWidth = 6;
            cotHT.Name = "cotHT";
            // 
            // cotNS
            // 
            cotNS.HeaderText = "Ngày Sinh";
            cotNS.MinimumWidth = 6;
            cotNS.Name = "cotNS";
            // 
            // cotL
            // 
            cotL.HeaderText = "Lớp";
            cotL.MinimumWidth = 6;
            cotL.Name = "cotL";
            // 
            // cotKhoa
            // 
            cotKhoa.HeaderText = "Khoa";
            cotKhoa.MinimumWidth = 6;
            cotKhoa.Name = "cotKhoa";
            // 
            // cotGPA
            // 
            cotGPA.HeaderText = "GPA";
            cotGPA.MinimumWidth = 6;
            cotGPA.Name = "cotGPA";
            // 
            // cotTT
            // 
            cotTT.HeaderText = "Tình Trạng";
            cotTT.MinimumWidth = 6;
            cotTT.Name = "cotTT";
            // 
            // button10
            // 
            button10.FlatStyle = FlatStyle.System;
            button10.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button10.Location = new Point(744, 191);
            button10.Name = "button10";
            button10.Size = new Size(44, 38);
            button10.TabIndex = 33;
            button10.Text = "🔍";
            button10.TextAlign = ContentAlignment.BottomCenter;
            button10.UseVisualStyleBackColor = true;
            // 
            // SinhVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 484);
            Controls.Add(button10);
            Controls.Add(dataGridView1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "SinhVien";
            Text = "SinhVien";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Label label4;
        private Label label5;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMSV;
        private DataGridViewTextBoxColumn cotHT;
        private DataGridViewTextBoxColumn cotNS;
        private DataGridViewTextBoxColumn cotL;
        private DataGridViewTextBoxColumn cotKhoa;
        private DataGridViewTextBoxColumn cotGPA;
        private DataGridViewTextBoxColumn cotTT;
        private Button button10;
    }
}