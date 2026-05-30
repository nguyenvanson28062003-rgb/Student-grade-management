namespace quản_lí_điểm_sinh_viên
{
    partial class BangDiemcs
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
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            cotMMH = new DataGridViewTextBoxColumn();
            cotTenMH = new DataGridViewTextBoxColumn();
            cotTC = new DataGridViewTextBoxColumn();
            cotCC = new DataGridViewTextBoxColumn();
            cotGK = new DataGridViewTextBoxColumn();
            cotCK = new DataGridViewTextBoxColumn();
            cotDTB = new DataGridViewTextBoxColumn();
            cotGPA = new DataGridViewTextBoxColumn();
            cotXL = new DataGridViewTextBoxColumn();
            cotKQ = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 129);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin sinh viên";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.Font = new Font("Times New Roman", 12F);
            label4.Location = new Point(418, 83);
            label4.Name = "label4";
            label4.Size = new Size(352, 34);
            label4.TabIndex = 18;
            label4.Text = "Khoa";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Times New Roman", 12F);
            label3.Location = new Point(418, 23);
            label3.Name = "label3";
            label3.Size = new Size(352, 34);
            label3.TabIndex = 17;
            label3.Text = "Lớp";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(0, 83);
            label2.Name = "label2";
            label2.Size = new Size(352, 34);
            label2.TabIndex = 16;
            label2.Text = "Họ Tên";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F);
            label1.Location = new Point(0, 23);
            label1.Name = "label1";
            label1.Size = new Size(352, 34);
            label1.TabIndex = 15;
            label1.Text = "MSV";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.Font = new Font("Times New Roman", 12F);
            label5.Location = new Point(12, 145);
            label5.Name = "label5";
            label5.Size = new Size(98, 28);
            label5.TabIndex = 5;
            label5.Text = "Học Kỳ";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Items.AddRange(new object[] { "HK1 - Năm 1", "HK2 - Năm 1", "HK1 - Năm 2", "HK2 - Năm 2", "HK1 - Năm 3", "HK2 - Năm 3", "HK1 - Năm 4", "HK2 - Năm 4", "Học kỳ hè" });
            comboBox1.Location = new Point(130, 145);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(234, 28);
            comboBox1.TabIndex = 18;
            // 
            // button1
            // 
            button1.Location = new Point(384, 145);
            button1.Name = "button1";
            button1.Size = new Size(112, 43);
            button1.TabIndex = 19;
            button1.Text = "Xuất điểm Excel";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(512, 145);
            button2.Name = "button2";
            button2.Size = new Size(112, 43);
            button2.TabIndex = 20;
            button2.Text = "Lịch sử điểm";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(643, 145);
            button3.Name = "button3";
            button3.Size = new Size(112, 43);
            button3.TabIndex = 21;
            button3.Text = "Quay lại Menu";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotMMH, cotTenMH, cotTC, cotCC, cotGK, cotCK, cotDTB, cotGPA, cotXL, cotKQ });
            dataGridView1.Location = new Point(12, 194);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 244);
            dataGridView1.TabIndex = 22;
            // 
            // cotMMH
            // 
            cotMMH.HeaderText = "Mã Môn Học";
            cotMMH.MinimumWidth = 6;
            cotMMH.Name = "cotMMH";
            // 
            // cotTenMH
            // 
            cotTenMH.HeaderText = "Tên Môn Học";
            cotTenMH.MinimumWidth = 6;
            cotTenMH.Name = "cotTenMH";
            // 
            // cotTC
            // 
            cotTC.HeaderText = "Tín Chỉ";
            cotTC.MinimumWidth = 6;
            cotTC.Name = "cotTC";
            // 
            // cotCC
            // 
            cotCC.HeaderText = "CC";
            cotCC.MinimumWidth = 6;
            cotCC.Name = "cotCC";
            // 
            // cotGK
            // 
            cotGK.HeaderText = "Gk";
            cotGK.MinimumWidth = 6;
            cotGK.Name = "cotGK";
            // 
            // cotCK
            // 
            cotCK.HeaderText = "CK";
            cotCK.MinimumWidth = 6;
            cotCK.Name = "cotCK";
            // 
            // cotDTB
            // 
            cotDTB.HeaderText = "DTB";
            cotDTB.MinimumWidth = 6;
            cotDTB.Name = "cotDTB";
            // 
            // cotGPA
            // 
            cotGPA.HeaderText = "GPA";
            cotGPA.MinimumWidth = 6;
            cotGPA.Name = "cotGPA";
            // 
            // cotXL
            // 
            cotXL.HeaderText = "Xếp Loại";
            cotXL.MinimumWidth = 6;
            cotXL.Name = "cotXL";
            // 
            // cotKQ
            // 
            cotKQ.HeaderText = "Kết Quả";
            cotKQ.MinimumWidth = 6;
            cotKQ.Name = "cotKQ";
            // 
            // BangDiemcs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(groupBox1);
            Name = "BangDiemcs";
            Text = "BangDiemcs";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label5;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotMMH;
        private DataGridViewTextBoxColumn cotTenMH;
        private DataGridViewTextBoxColumn cotTC;
        private DataGridViewTextBoxColumn cotCC;
        private DataGridViewTextBoxColumn cotGK;
        private DataGridViewTextBoxColumn cotCK;
        private DataGridViewTextBoxColumn cotDTB;
        private DataGridViewTextBoxColumn cotGPA;
        private DataGridViewTextBoxColumn cotXL;
        private DataGridViewTextBoxColumn cotKQ;
    }
}
