namespace quản_lí_điểm_sinh_viên
{
    partial class QLTK
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
            label1 = new Label();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            cotName = new DataGridViewTextBoxColumn();
            cotRole = new DataGridViewTextBoxColumn();
            cotStatus = new DataGridViewTextBoxColumn();
            cotCreationdate = new DataGridViewTextBoxColumn();
            cotLastLogin = new DataGridViewTextBoxColumn();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            textBox2 = new TextBox();
            label2 = new Label();
            button7 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(137, 9);
            label1.Name = "label1";
            label1.Size = new Size(475, 52);
            label1.TabIndex = 1;
            label1.Text = "Quản lí tài khoản đăng nhập";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Location = new Point(24, 72);
            button1.Name = "button1";
            button1.Size = new Size(112, 43);
            button1.TabIndex = 3;
            button1.Text = "Thêm TK";
            button1.UseVisualStyleBackColor = true;
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cotName, cotRole, cotStatus, cotCreationdate, cotLastLogin });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.GridColor = Color.Gainsboro;
            dataGridView1.Location = new Point(12, 174);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 264);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // cotName
            // 
            cotName.HeaderText = "Username";
            cotName.MinimumWidth = 6;
            cotName.Name = "cotName";
            // 
            // cotRole
            // 
            cotRole.HeaderText = "Role";
            cotRole.MinimumWidth = 6;
            cotRole.Name = "cotRole";
            // 
            // cotStatus
            // 
            cotStatus.HeaderText = "Status";
            cotStatus.MinimumWidth = 6;
            cotStatus.Name = "cotStatus";
            // 
            // cotCreationdate
            // 
            cotCreationdate.HeaderText = "Creation date";
            cotCreationdate.MinimumWidth = 6;
            cotCreationdate.Name = "cotCreationdate";
            // 
            // cotLastLogin
            // 
            cotLastLogin.HeaderText = "Last login";
            cotLastLogin.MinimumWidth = 6;
            cotLastLogin.Name = "cotLastLogin";
            // 
            // button2
            // 
            button2.Location = new Point(665, 72);
            button2.Name = "button2";
            button2.Size = new Size(112, 43);
            button2.TabIndex = 5;
            button2.Text = "Thoát";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(154, 72);
            button3.Name = "button3";
            button3.Size = new Size(112, 43);
            button3.TabIndex = 6;
            button3.Text = "Xóa TK";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(284, 72);
            button4.Name = "button4";
            button4.Size = new Size(112, 43);
            button4.TabIndex = 7;
            button4.Text = "Sửa TK";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(538, 72);
            button5.Name = "button5";
            button5.Size = new Size(112, 43);
            button5.TabIndex = 8;
            button5.Text = "Quay lại Menu";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(411, 72);
            button6.Name = "button6";
            button6.Size = new Size(112, 43);
            button6.TabIndex = 9;
            button6.Text = "Tìm kiếm";
            button6.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(154, 138);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(261, 28);
            textBox2.TabIndex = 10;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Times New Roman", 12F);
            label2.Location = new Point(36, 134);
            label2.Name = "label2";
            label2.Size = new Size(140, 34);
            label2.TabIndex = 13;
            label2.Text = "Username";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button7
            // 
            button7.FlatStyle = FlatStyle.System;
            button7.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(421, 139);
            button7.Name = "button7";
            button7.Size = new Size(30, 29);
            button7.TabIndex = 16;
            button7.Text = "↻";
            button7.TextAlign = ContentAlignment.BottomCenter;
            button7.UseVisualStyleBackColor = true;
            // 
            // QLTK
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button7);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "QLTK";
            Text = "QLTK";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn cotName;
        private DataGridViewTextBoxColumn cotRole;
        private DataGridViewTextBoxColumn cotStatus;
        private DataGridViewTextBoxColumn cotCreationdate;
        private DataGridViewTextBoxColumn cotLastLogin;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private TextBox textBox2;
        private Label label2;
        private Button button7;
    }
}