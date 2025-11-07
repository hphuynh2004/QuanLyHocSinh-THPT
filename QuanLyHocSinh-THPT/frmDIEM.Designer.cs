namespace QuanLyHocSinh_THPT
{
    partial class frmDIEM
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
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.lblCONGTHONGTIN = new System.Windows.Forms.Label();
            this.lblPHUTHAI = new System.Windows.Forms.Label();
            this.lblThiCK = new System.Windows.Forms.Label();
            this.lblDiem1tiet = new System.Windows.Forms.Label();
            this.lblDiem15p = new System.Windows.Forms.Label();
            this.lblMaMH = new System.Windows.Forms.Label();
            this.lblMAHS = new System.Windows.Forms.Label();
            this.lblDIEMTHI = new System.Windows.Forms.Label();
            this.txtDiemTB = new System.Windows.Forms.TextBox();
            this.lblDiemTB = new System.Windows.Forms.Label();
            this.cbMaHS = new System.Windows.Forms.ComboBox();
            this.cbMaMH = new System.Windows.Forms.ComboBox();
            this.numDiem15P = new System.Windows.Forms.NumericUpDown();
            this.numDiem1T = new System.Windows.Forms.NumericUpDown();
            this.numDiemCK = new System.Windows.Forms.NumericUpDown();
            this.lblMadiem = new System.Windows.Forms.Label();
            this.txtMadiem = new System.Windows.Forms.TextBox();
            this.gridDIEM = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem15P)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem1T)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemCK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDIEM)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(637, 307);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 31);
            this.btnThoat.TabIndex = 47;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(218, 307);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(108, 31);
            this.btnLuu.TabIndex = 45;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(503, 307);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(108, 31);
            this.btnXoa.TabIndex = 44;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(357, 307);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(108, 31);
            this.btnSua.TabIndex = 43;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(67, 307);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(108, 31);
            this.btnThem.TabIndex = 42;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // lblCONGTHONGTIN
            // 
            this.lblCONGTHONGTIN.AutoSize = true;
            this.lblCONGTHONGTIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCONGTHONGTIN.Location = new System.Drawing.Point(63, 22);
            this.lblCONGTHONGTIN.Name = "lblCONGTHONGTIN";
            this.lblCONGTHONGTIN.Size = new System.Drawing.Size(236, 20);
            this.lblCONGTHONGTIN.TabIndex = 33;
            this.lblCONGTHONGTIN.Text = "CỔNG THÔNG TIN ĐIỆN TỬ";
            // 
            // lblPHUTHAI
            // 
            this.lblPHUTHAI.AutoSize = true;
            this.lblPHUTHAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPHUTHAI.Location = new System.Drawing.Point(72, 53);
            this.lblPHUTHAI.Name = "lblPHUTHAI";
            this.lblPHUTHAI.Size = new System.Drawing.Size(227, 20);
            this.lblPHUTHAI.TabIndex = 32;
            this.lblPHUTHAI.Text = "Trường THPT Chuyên Phú Thái";
            // 
            // lblThiCK
            // 
            this.lblThiCK.AutoSize = true;
            this.lblThiCK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThiCK.Location = new System.Drawing.Point(436, 220);
            this.lblThiCK.Name = "lblThiCK";
            this.lblThiCK.Size = new System.Drawing.Size(79, 18);
            this.lblThiCK.TabIndex = 31;
            this.lblThiCK.Text = "Thi cuối kỳ";
            // 
            // lblDiem1tiet
            // 
            this.lblDiem1tiet.AutoSize = true;
            this.lblDiem1tiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiem1tiet.Location = new System.Drawing.Point(438, 187);
            this.lblDiem1tiet.Name = "lblDiem1tiet";
            this.lblDiem1tiet.Size = new System.Drawing.Size(78, 18);
            this.lblDiem1tiet.TabIndex = 30;
            this.lblDiem1tiet.Text = "Điểm 1 tiết";
            // 
            // lblDiem15p
            // 
            this.lblDiem15p.AutoSize = true;
            this.lblDiem15p.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiem15p.Location = new System.Drawing.Point(438, 153);
            this.lblDiem15p.Name = "lblDiem15p";
            this.lblDiem15p.Size = new System.Drawing.Size(95, 18);
            this.lblDiem15p.TabIndex = 29;
            this.lblDiem15p.Text = "Điểm 15 phút";
            // 
            // lblMaMH
            // 
            this.lblMaMH.AutoSize = true;
            this.lblMaMH.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaMH.Location = new System.Drawing.Point(106, 219);
            this.lblMaMH.Name = "lblMaMH";
            this.lblMaMH.Size = new System.Drawing.Size(92, 18);
            this.lblMaMH.TabIndex = 26;
            this.lblMaMH.Text = "Mã môn học";
            // 
            // lblMAHS
            // 
            this.lblMAHS.AutoSize = true;
            this.lblMAHS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMAHS.Location = new System.Drawing.Point(106, 187);
            this.lblMAHS.Name = "lblMAHS";
            this.lblMAHS.Size = new System.Drawing.Size(89, 18);
            this.lblMAHS.TabIndex = 25;
            this.lblMAHS.Text = "Mã học sinh";
            // 
            // lblDIEMTHI
            // 
            this.lblDIEMTHI.AutoSize = true;
            this.lblDIEMTHI.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIEMTHI.Location = new System.Drawing.Point(307, 100);
            this.lblDIEMTHI.Name = "lblDIEMTHI";
            this.lblDIEMTHI.Size = new System.Drawing.Size(194, 24);
            this.lblDIEMTHI.TabIndex = 24;
            this.lblDIEMTHI.Text = "QUẢN LÝ ĐIỂM THI";
            // 
            // txtDiemTB
            // 
            this.txtDiemTB.Location = new System.Drawing.Point(403, 265);
            this.txtDiemTB.Name = "txtDiemTB";
            this.txtDiemTB.Size = new System.Drawing.Size(120, 20);
            this.txtDiemTB.TabIndex = 49;
            // 
            // lblDiemTB
            // 
            this.lblDiemTB.AutoSize = true;
            this.lblDiemTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemTB.Location = new System.Drawing.Point(271, 267);
            this.lblDiemTB.Name = "lblDiemTB";
            this.lblDiemTB.Size = new System.Drawing.Size(126, 18);
            this.lblDiemTB.TabIndex = 48;
            this.lblDiemTB.Text = "Điểm trung bình";
            // 
            // cbMaHS
            // 
            this.cbMaHS.FormattingEnabled = true;
            this.cbMaHS.Location = new System.Drawing.Point(218, 188);
            this.cbMaHS.Name = "cbMaHS";
            this.cbMaHS.Size = new System.Drawing.Size(121, 21);
            this.cbMaHS.TabIndex = 50;
            // 
            // cbMaMH
            // 
            this.cbMaMH.FormattingEnabled = true;
            this.cbMaMH.Location = new System.Drawing.Point(218, 221);
            this.cbMaMH.Name = "cbMaMH";
            this.cbMaMH.Size = new System.Drawing.Size(121, 21);
            this.cbMaMH.TabIndex = 51;
            // 
            // numDiem15P
            // 
            this.numDiem15P.Location = new System.Drawing.Point(556, 155);
            this.numDiem15P.Name = "numDiem15P";
            this.numDiem15P.Size = new System.Drawing.Size(121, 20);
            this.numDiem15P.TabIndex = 52;
            // 
            // numDiem1T
            // 
            this.numDiem1T.Location = new System.Drawing.Point(557, 188);
            this.numDiem1T.Name = "numDiem1T";
            this.numDiem1T.Size = new System.Drawing.Size(120, 20);
            this.numDiem1T.TabIndex = 53;
            // 
            // numDiemCK
            // 
            this.numDiemCK.Location = new System.Drawing.Point(557, 221);
            this.numDiemCK.Name = "numDiemCK";
            this.numDiemCK.Size = new System.Drawing.Size(120, 20);
            this.numDiemCK.TabIndex = 54;
            // 
            // lblMadiem
            // 
            this.lblMadiem.AutoSize = true;
            this.lblMadiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMadiem.Location = new System.Drawing.Point(106, 153);
            this.lblMadiem.Name = "lblMadiem";
            this.lblMadiem.Size = new System.Drawing.Size(65, 18);
            this.lblMadiem.TabIndex = 55;
            this.lblMadiem.Text = "Mã điểm";
            // 
            // txtMadiem
            // 
            this.txtMadiem.Location = new System.Drawing.Point(218, 151);
            this.txtMadiem.Name = "txtMadiem";
            this.txtMadiem.Size = new System.Drawing.Size(121, 20);
            this.txtMadiem.TabIndex = 56;
            // 
            // gridDIEM
            // 
            this.gridDIEM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDIEM.Location = new System.Drawing.Point(67, 368);
            this.gridDIEM.Name = "gridDIEM";
            this.gridDIEM.Size = new System.Drawing.Size(678, 150);
            this.gridDIEM.TabIndex = 66;
            this.gridDIEM.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDIEM_CellClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(565, 34);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 27);
            this.button1.TabIndex = 74;
            this.button1.Text = "Tìm kiếm nâng cao";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(606, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "theo mã điểm / mã học sinh";
            // 
            // frmDIEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 559);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridDIEM);
            this.Controls.Add(this.txtMadiem);
            this.Controls.Add(this.lblMadiem);
            this.Controls.Add(this.numDiemCK);
            this.Controls.Add(this.numDiem1T);
            this.Controls.Add(this.numDiem15P);
            this.Controls.Add(this.cbMaMH);
            this.Controls.Add(this.cbMaHS);
            this.Controls.Add(this.txtDiemTB);
            this.Controls.Add(this.lblDiemTB);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.lblCONGTHONGTIN);
            this.Controls.Add(this.lblPHUTHAI);
            this.Controls.Add(this.lblThiCK);
            this.Controls.Add(this.lblDiem1tiet);
            this.Controls.Add(this.lblDiem15p);
            this.Controls.Add(this.lblMaMH);
            this.Controls.Add(this.lblMAHS);
            this.Controls.Add(this.lblDIEMTHI);
            this.Name = "frmDIEM";
            this.Text = "frmDIEM";
            this.Load += new System.EventHandler(this.frmDIEM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDiem15P)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem1T)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemCK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDIEM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblCONGTHONGTIN;
        private System.Windows.Forms.Label lblPHUTHAI;
        private System.Windows.Forms.Label lblThiCK;
        private System.Windows.Forms.Label lblDiem1tiet;
        private System.Windows.Forms.Label lblDiem15p;
        private System.Windows.Forms.Label lblMaMH;
        private System.Windows.Forms.Label lblMAHS;
        private System.Windows.Forms.Label lblDIEMTHI;
        private System.Windows.Forms.TextBox txtDiemTB;
        private System.Windows.Forms.Label lblDiemTB;
        private System.Windows.Forms.ComboBox cbMaHS;
        private System.Windows.Forms.ComboBox cbMaMH;
        private System.Windows.Forms.NumericUpDown numDiem15P;
        private System.Windows.Forms.NumericUpDown numDiem1T;
        private System.Windows.Forms.NumericUpDown numDiemCK;
        private System.Windows.Forms.Label lblMadiem;
        private System.Windows.Forms.TextBox txtMadiem;
        private System.Windows.Forms.DataGridView gridDIEM;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}