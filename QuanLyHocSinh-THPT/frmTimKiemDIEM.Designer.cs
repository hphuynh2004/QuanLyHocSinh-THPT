namespace QuanLyHocSinh_THPT
{
    partial class frmTimKiemDIEM
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
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblTimkiem = new System.Windows.Forms.Label();
            this.btnTimkiem = new System.Windows.Forms.Button();
            this.gridKetQua = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Location = new System.Drawing.Point(136, 52);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(241, 20);
            this.txtTuKhoa.TabIndex = 1;
            // 
            // lblTimkiem
            // 
            this.lblTimkiem.AutoSize = true;
            this.lblTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimkiem.Location = new System.Drawing.Point(133, 25);
            this.lblTimkiem.Name = "lblTimkiem";
            this.lblTimkiem.Size = new System.Drawing.Size(257, 15);
            this.lblTimkiem.TabIndex = 0;
            this.lblTimkiem.Text = "Nhập tên hoặc mã học sinh cần tra cứu";
            this.lblTimkiem.Click += new System.EventHandler(this.lblTimkiem_Click);
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimkiem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTimkiem.Location = new System.Drawing.Point(136, 78);
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(241, 32);
            this.btnTimkiem.TabIndex = 2;
            this.btnTimkiem.Text = "Tra cứu";
            this.btnTimkiem.UseVisualStyleBackColor = false;
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click);
            // 
            // gridKetQua
            // 
            this.gridKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKetQua.Location = new System.Drawing.Point(50, 116);
            this.gridKetQua.Name = "gridKetQua";
            this.gridKetQua.Size = new System.Drawing.Size(434, 150);
            this.gridKetQua.TabIndex = 3;
            this.gridKetQua.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridKetQua_CellFormatting_1);
            // 
            // frmTimKiemDIEM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 316);
            this.Controls.Add(this.gridKetQua);
            this.Controls.Add(this.btnTimkiem);
            this.Controls.Add(this.txtTuKhoa);
            this.Controls.Add(this.lblTimkiem);
            this.Name = "frmTimKiemDIEM";
            this.Text = "frmTimKiemDIEM";
            this.Load += new System.EventHandler(this.frmTimKiemDIEM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridKetQua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Label lblTimkiem;
        private System.Windows.Forms.Button btnTimkiem;
        private System.Windows.Forms.DataGridView gridKetQua;
    }
}