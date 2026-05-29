namespace quản_lí_điểm_sinh_viên
{
    partial class NhapDiem
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            dataGridView1 = new DataGridView();
            cotMSV = new DataGridViewTextBoxColumn();
            cotHoTen = new DataGridViewTextBoxColumn();
            cotChuyenCan = new DataGridViewTextBoxColumn();
            cotGuaKy = new DataGridViewTextBoxColumn();
            cotCuoiKy = new DataGridViewTextBoxColumn();
            cotDTB = new DataGridViewTextBoxColumn();
            cotGPA = new DataGridViewTextBoxColumn();
            cotXepLoai = new DataGridViewTextBoxColumn();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 140);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin lớp học phần";
            //
            // label1 – prefix "Mã lớp HP:"
            //
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(8, 30);
            label1.Name = "label1";
            label1.Size = new Size(110, 30);
            label1.TabIndex = 14;
            label1.Text = "Mã lớp HP:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label7 – value MaLHP (set in code)
            //
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(122, 30);
            label7.Name = "label7";
            label7.Size = new Size(240, 30);
            label7.TabIndex = 19;
            label7.Text = "";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label3 – prefix "Môn học:"
            //
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(8, 72);
            label3.Name = "label3";
            label3.Size = new Size(110, 30);
            label3.TabIndex = 15;
            label3.Text = "Môn học:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label8 – value TenMH (set in code)
            //
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(122, 72);
            label8.Name = "label8";
            label8.Size = new Size(240, 30);
            label8.TabIndex = 20;
            label8.Text = "";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label4 – prefix "Giảng viên:"
            //
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(400, 30);
            label4.Name = "label4";
            label4.Size = new Size(110, 30);
            label4.TabIndex = 16;
            label4.Text = "Giảng viên:";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label9 – value HoTenGV (set in code)
            //
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(514, 30);
            label9.Name = "label9";
            label9.Size = new Size(252, 30);
            label9.TabIndex = 21;
            label9.Text = "";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label6 – prefix "Học kỳ:"
            //
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label6.Location = new Point(400, 72);
            label6.Name = "label6";
            label6.Size = new Size(110, 30);
            label6.TabIndex = 18;
            label6.Text = "Học kỳ:";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label10 – value TenHK (set in code)
            //
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(514, 72);
            label10.Name = "label10";
            label10.Size = new Size(252, 30);
            label10.TabIndex = 22;
            label10.Text = "";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label5 – ẩn (không dùng)
            //
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(0, 0);
            label5.TabIndex = 17;
            label5.Text = "";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(12, 171);
            label2.Name = "label2";
            label2.Size = new Size(776, 29);
            label2.TabIndex = 14;
            label2.Text = "💡 Điểm quá trình 40% (CC=10% + GK=30%) + Cuối kỳ 60%";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Location = new Point(146, 204);
            button1.Name = "button1";
            button1.Size = new Size(112, 43);
            button1.TabIndex = 15;
            button1.Text = "Import Excel";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(12, 204);
            button2.Name = "button2";
            button2.Size = new Size(112, 43);
            button2.TabIndex = 16;
            button2.Text = "Lưu điểm";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(414, 204);
            button3.Name = "button3";
            button3.Size = new Size(112, 43);
            button3.TabIndex = 17;
            button3.Text = "Chốt điểm";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(280, 204);
            button4.Name = "button4";
            button4.Size = new Size(112, 43);
            button4.TabIndex = 18;
            button4.Text = "Export";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(548, 204);
            button5.Name = "button5";
            button5.Size = new Size(112, 43);
            button5.TabIndex = 19;
            button5.Text = "Thoát";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(682, 204);
            button6.Name = "button6";
            button6.Size = new Size(112, 43);
            button6.TabIndex = 20;
            button6.Text = "Quay lại Menu";
            button6.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMSV, cotHoTen, cotChuyenCan, cotGuaKy, cotCuoiKy, cotDTB, cotGPA, cotXepLoai });
            dataGridView1.Location = new Point(12, 253);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 196);
            dataGridView1.TabIndex = 21;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // cotMSV
            // 
            cotMSV.HeaderText = "MSV";
            cotMSV.MinimumWidth = 6;
            cotMSV.Name = "cotMSV";
            // 
            // cotHoTen
            // 
            cotHoTen.HeaderText = "Họ Tên";
            cotHoTen.MinimumWidth = 6;
            cotHoTen.Name = "cotHoTen";
            // 
            // cotChuyenCan
            // 
            cotChuyenCan.HeaderText = "Chuyên cần";
            cotChuyenCan.MinimumWidth = 6;
            cotChuyenCan.Name = "cotChuyenCan";
            // 
            // cotGuaKy
            // 
            cotGuaKy.HeaderText = "Giữa Kỳ";
            cotGuaKy.MinimumWidth = 6;
            cotGuaKy.Name = "cotGuaKy";
            // 
            // cotCuoiKy
            // 
            cotCuoiKy.HeaderText = "Cuối Kỳ";
            cotCuoiKy.MinimumWidth = 6;
            cotCuoiKy.Name = "cotCuoiKy";
            // 
            // cotDTB
            // 
            cotDTB.HeaderText = "DTB";
            cotDTB.MinimumWidth = 6;
            cotDTB.Name = "cotDTB";
            cotDTB.ReadOnly = true;
            // 
            // cotGPA
            // 
            cotGPA.HeaderText = "GPA";
            cotGPA.MinimumWidth = 6;
            cotGPA.Name = "cotGPA";
            cotGPA.ReadOnly = true;
            // 
            // cotXepLoai
            // 
            cotXepLoai.HeaderText = "Xếp Loại";
            cotXepLoai.MinimumWidth = 6;
            cotXepLoai.Name = "cotXepLoai";
            cotXepLoai.ReadOnly = true;
            // 
            // NhapDiem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Name = "NhapDiem";
            Text = "NhapDiem";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMSV;
        private DataGridViewTextBoxColumn cotHoTen;
        private DataGridViewTextBoxColumn cotChuyenCan;
        private DataGridViewTextBoxColumn cotGuaKy;
        private DataGridViewTextBoxColumn cotCuoiKy;
        private DataGridViewTextBoxColumn cotDTB;
        private DataGridViewTextBoxColumn cotGPA;
        private DataGridViewTextBoxColumn cotXepLoai;
        private Label label4;
        private Label label3;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label8;
        private Label label7;
        private Label label10;
        private Label label9;
    }
}