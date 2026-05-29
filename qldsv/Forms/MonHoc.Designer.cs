namespace qldsv
{
    partial class MonHoc
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
            label1 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            button8 = new Button();
            cotMaMon = new DataGridViewTextBoxColumn();
            cotMH = new DataGridViewTextBoxColumn();
            cotTC = new DataGridViewTextBoxColumn();
            cotL = new DataGridViewTextBoxColumn();
            cotKhoa = new DataGridViewTextBoxColumn();
            cotGV = new DataGridViewTextBoxColumn();
            cotTT = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(130, 82);
            button1.Name = "button1";
            button1.Size = new Size(103, 48);
            button1.TabIndex = 5;
            button1.Text = "Sửa";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(675, 82);
            button2.Name = "button2";
            button2.Size = new Size(103, 48);
            button2.TabIndex = 6;
            button2.Text = "Thoát";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(239, 82);
            button3.Name = "button3";
            button3.Size = new Size(103, 48);
            button3.TabIndex = 7;
            button3.Text = "Xóa";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(457, 82);
            button4.Name = "button4";
            button4.Size = new Size(103, 48);
            button4.TabIndex = 8;
            button4.Text = "Export";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(566, 82);
            button5.Name = "button5";
            button5.Size = new Size(103, 48);
            button5.TabIndex = 9;
            button5.Text = "Quay lại MN";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(348, 82);
            button6.Name = "button6";
            button6.Size = new Size(103, 48);
            button6.TabIndex = 10;
            button6.Text = "Phân công GV";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(21, 82);
            button7.Name = "button7";
            button7.Size = new Size(103, 48);
            button7.TabIndex = 11;
            button7.Text = "Thêm môn";
            button7.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(176, 9);
            label1.Name = "label1";
            label1.Size = new Size(475, 52);
            label1.TabIndex = 12;
            label1.Text = "Quản lí môn học";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.IntegralHeight = false;
            comboBox2.Items.AddRange(new object[] { "Cấu trúc dữ liệu và giải thuật", "Lập trình hướng đối tượng", "Kĩ thuật lập trình", "Mạng máy tính", "Trí tuệ nhân tạo", "Web 1" });
            comboBox2.Location = new Point(155, 137);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(204, 28);
            comboBox2.TabIndex = 22;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "Cấu trúc dữ liệu và giải thuật", "Lập trình hướng đối tượng", "Kĩ thuật lập trình", "Mạng máy tính", "Trí tuệ nhân tạo", "Web 1" });
            comboBox1.Location = new Point(480, 136);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(204, 28);
            comboBox1.TabIndex = 23;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(26, 136);
            label2.Name = "label2";
            label2.Size = new Size(123, 28);
            label2.TabIndex = 24;
            label2.Text = "Mã/Tên môn";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(376, 137);
            label3.Name = "label3";
            label3.Size = new Size(98, 28);
            label3.TabIndex = 25;
            label3.Text = "Khoa";
            label3.TextAlign = ContentAlignment.MiddleCenter;
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMaMon, cotMH, cotTC, cotL, cotKhoa, cotGV, cotTT });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.GridColor = Color.Gainsboro;
            dataGridView1.Location = new Point(12, 180);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 258);
            dataGridView1.TabIndex = 26;
            // 
            // button8
            // 
            button8.FlatStyle = FlatStyle.System;
            button8.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.Location = new Point(702, 136);
            button8.Name = "button8";
            button8.Size = new Size(44, 38);
            button8.TabIndex = 27;
            button8.Text = "🔍";
            button8.TextAlign = ContentAlignment.BottomCenter;
            button8.UseVisualStyleBackColor = true;
            // 
            // cotMaMon
            // 
            cotMaMon.HeaderText = "Mã Môn";
            cotMaMon.MinimumWidth = 6;
            cotMaMon.Name = "cotMaMon";
            // 
            // cotMH
            // 
            cotMH.HeaderText = "Môn Học";
            cotMH.MinimumWidth = 6;
            cotMH.Name = "cotMH";
            // 
            // cotTC
            // 
            cotTC.HeaderText = "Tín Chỉ";
            cotTC.MinimumWidth = 6;
            cotTC.Name = "cotTC";
            // 
            // cotL
            // 
            cotL.HeaderText = "Loại";
            cotL.MinimumWidth = 6;
            cotL.Name = "cotL";
            // 
            // cotKhoa
            // 
            cotKhoa.HeaderText = "Khoa";
            cotKhoa.MinimumWidth = 6;
            cotKhoa.Name = "cotKhoa";
            // 
            // cotGV
            // 
            cotGV.HeaderText = "Giảng Viên";
            cotGV.MinimumWidth = 6;
            cotGV.Name = "cotGV";
            // 
            // cotTT
            // 
            cotTT.HeaderText = "Trạng Thái";
            cotTT.MinimumWidth = 6;
            cotTT.Name = "cotTT";
            // 
            // MonHoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button8);
            Controls.Add(dataGridView1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(comboBox2);
            Controls.Add(label1);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "MonHoc";
            Text = "MonHoc";
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
        private Button button7;
        private Label label1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMaMon;
        private DataGridViewTextBoxColumn cotMH;
        private DataGridViewTextBoxColumn cotTC;
        private DataGridViewTextBoxColumn cotL;
        private DataGridViewTextBoxColumn cotKhoa;
        private DataGridViewTextBoxColumn cotGV;
        private DataGridViewTextBoxColumn cotTT;
        private Button button8;
    }
}