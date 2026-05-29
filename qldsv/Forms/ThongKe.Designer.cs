namespace quản_lí_điểm_sinh_viên
{
    partial class ThongKe
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
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnDangnhap = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            panel7 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            groupBox2 = new GroupBox();
            dataGridView1 = new DataGridView();
            cotMon = new DataGridViewTextBoxColumn();
            cotDat = new DataGridViewTextBoxColumn();
            cotRot = new DataGridViewTextBoxColumn();
            cotDTB = new DataGridViewTextBoxColumn();
            label13 = new Label();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "Công nghệ thông tin", "Điện tử viễn thông", "Ngôn ngữ Anh", "Du lịch", "Ngôn ngữ Trung", "Quan hệ công chúng" });
            comboBox1.Location = new Point(199, 108);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(543, 28);
            comboBox1.TabIndex = 18;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Sinh Viên", "Giảng Viên", "Admin" });
            comboBox2.Location = new Point(199, 74);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(543, 28);
            comboBox2.TabIndex = 19;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(163, 9);
            label1.Name = "label1";
            label1.Size = new Size(475, 43);
            label1.TabIndex = 20;
            label1.Text = "Báo cáo thống kê";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(33, 68);
            label2.Name = "label2";
            label2.Size = new Size(140, 34);
            label2.TabIndex = 21;
            label2.Text = "Học Kỳ";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(33, 108);
            label3.Name = "label3";
            label3.Size = new Size(140, 34);
            label3.TabIndex = 22;
            label3.Text = "Khoa";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDangnhap
            // 
            btnDangnhap.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangnhap.Location = new Point(33, 145);
            btnDangnhap.Name = "btnDangnhap";
            btnDangnhap.Size = new Size(179, 43);
            btnDangnhap.TabIndex = 23;
            btnDangnhap.Text = "Xem thống kê";
            btnDangnhap.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(218, 145);
            button1.Name = "button1";
            button1.Size = new Size(179, 43);
            button1.TabIndex = 24;
            button1.Text = "Xuất báo cáo";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(403, 145);
            button2.Name = "button2";
            button2.Size = new Size(179, 43);
            button2.TabIndex = 25;
            button2.Text = "Quay lại Menu";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(588, 145);
            button3.Name = "button3";
            button3.Size = new Size(179, 43);
            button3.TabIndex = 26;
            button3.Text = "Thoát";
            button3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(4, 194);
            panel1.Name = "panel1";
            panel1.Size = new Size(789, 63);
            panel1.TabIndex = 27;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.Font = new Font("Times New Roman", 12F);
            label7.Location = new Point(623, 13);
            label7.Name = "label7";
            label7.Size = new Size(161, 37);
            label7.TabIndex = 7;
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.Font = new Font("Times New Roman", 12F);
            label6.Location = new Point(418, 13);
            label6.Name = "label6";
            label6.Size = new Size(161, 37);
            label6.TabIndex = 6;
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.Font = new Font("Times New Roman", 12F);
            label5.Location = new Point(213, 13);
            label5.Name = "label5";
            label5.Size = new Size(161, 37);
            label5.TabIndex = 5;
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Times New Roman", 12F);
            label4.Location = new Point(8, 13);
            label4.Name = "label4";
            label4.Size = new Size(161, 37);
            label4.TabIndex = 4;
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label19);
            groupBox1.Controls.Add(label18);
            groupBox1.Controls.Add(label17);
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(panel7);
            groupBox1.Controls.Add(panel6);
            groupBox1.Controls.Add(panel5);
            groupBox1.Controls.Add(panel4);
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Location = new Point(12, 263);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(385, 182);
            groupBox1.TabIndex = 28;
            groupBox1.TabStop = false;
            groupBox1.Text = "Phân bố điểm theo xếp loại";
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label19.Font = new Font("Times New Roman", 10.2F);
            label19.Location = new Point(329, 149);
            label19.Name = "label19";
            label19.Size = new Size(50, 16);
            label19.TabIndex = 32;
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label18.Font = new Font("Times New Roman", 10.2F);
            label18.Location = new Point(329, 126);
            label18.Name = "label18";
            label18.Size = new Size(50, 16);
            label18.TabIndex = 31;
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label17.Font = new Font("Times New Roman", 10.2F);
            label17.Location = new Point(329, 104);
            label17.Name = "label17";
            label17.Size = new Size(50, 16);
            label17.TabIndex = 30;
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label16.Font = new Font("Times New Roman", 10.2F);
            label16.Location = new Point(329, 52);
            label16.Name = "label16";
            label16.Size = new Size(50, 16);
            label16.TabIndex = 29;
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label15.Font = new Font("Times New Roman", 10.2F);
            label15.Location = new Point(329, 78);
            label15.Name = "label15";
            label15.Size = new Size(50, 16);
            label15.TabIndex = 28;
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label14.Font = new Font("Times New Roman", 10.2F);
            label14.Location = new Point(329, 26);
            label14.Name = "label14";
            label14.Size = new Size(50, 16);
            label14.TabIndex = 27;
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Red;
            panel7.Location = new Point(78, 149);
            panel7.Name = "panel7";
            panel7.Size = new Size(246, 16);
            panel7.TabIndex = 26;
            // 
            // panel6
            // 
            panel6.BackColor = Color.DarkKhaki;
            panel6.Location = new Point(78, 122);
            panel6.Name = "panel6";
            panel6.Size = new Size(246, 16);
            panel6.TabIndex = 25;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Khaki;
            panel5.Location = new Point(78, 100);
            panel5.Name = "panel5";
            panel5.Size = new Size(246, 16);
            panel5.TabIndex = 24;
            // 
            // panel4
            // 
            panel4.BackColor = Color.DarkBlue;
            panel4.Location = new Point(78, 78);
            panel4.Name = "panel4";
            panel4.Size = new Size(246, 16);
            panel4.TabIndex = 23;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.HotTrack;
            panel3.Location = new Point(78, 48);
            panel3.Name = "panel3";
            panel3.Size = new Size(246, 16);
            panel3.TabIndex = 22;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Green;
            panel2.Location = new Point(78, 26);
            panel2.Name = "panel2";
            panel2.Size = new Size(246, 16);
            panel2.TabIndex = 21;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label12.Font = new Font("Times New Roman", 10.2F);
            label12.Location = new Point(0, 120);
            label12.Name = "label12";
            label12.Size = new Size(52, 29);
            label12.TabIndex = 20;
            label12.Text = "C";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label11.Font = new Font("Times New Roman", 10.2F);
            label11.Location = new Point(6, 94);
            label11.Name = "label11";
            label11.Size = new Size(52, 26);
            label11.TabIndex = 19;
            label11.Text = "C+";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label10.Font = new Font("Times New Roman", 10.2F);
            label10.Location = new Point(0, 68);
            label10.Name = "label10";
            label10.Size = new Size(52, 26);
            label10.TabIndex = 18;
            label10.Text = "B";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.Font = new Font("Times New Roman", 10.2F);
            label9.Location = new Point(6, 42);
            label9.Name = "label9";
            label9.Size = new Size(52, 26);
            label9.TabIndex = 17;
            label9.Text = "B+";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.Font = new Font("Times New Roman", 10.2F);
            label8.Location = new Point(0, 16);
            label8.Name = "label8";
            label8.Size = new Size(58, 26);
            label8.TabIndex = 16;
            label8.Text = "A";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Location = new Point(408, 263);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(385, 175);
            groupBox2.TabIndex = 29;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tỉ lệ Đạt/Rớt theo môn";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMon, cotDat, cotRot, cotDTB });
            dataGridView1.Location = new Point(6, 26);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(374, 149);
            dataGridView1.TabIndex = 0;
            // 
            // cotMon
            // 
            cotMon.HeaderText = "Môn";
            cotMon.MinimumWidth = 6;
            cotMon.Name = "cotMon";
            // 
            // cotDat
            // 
            cotDat.HeaderText = "Đạt";
            cotDat.MinimumWidth = 6;
            cotDat.Name = "cotDat";
            // 
            // cotRot
            // 
            cotRot.HeaderText = "Rớt";
            cotRot.MinimumWidth = 6;
            cotRot.Name = "cotRot";
            // 
            // cotDTB
            // 
            cotDTB.HeaderText = "DTB";
            cotDTB.MinimumWidth = 6;
            cotDTB.Name = "cotDTB";
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label13.Font = new Font("Times New Roman", 10.2F);
            label13.Location = new Point(18, 412);
            label13.Name = "label13";
            label13.Size = new Size(46, 16);
            label13.TabIndex = 21;
            label13.Text = "D/F";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ThongKe
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label13);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnDangnhap);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Name = "ThongKe";
            Text = "ThongKe";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnDangnhap;
        private Button button1;
        private Button button2;
        private Button button3;
        private Panel panel1;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label13;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Panel panel7;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMon;
        private DataGridViewTextBoxColumn cotDat;
        private DataGridViewTextBoxColumn cotRot;
        private DataGridViewTextBoxColumn cotDTB;
    }
}