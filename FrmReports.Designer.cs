namespace ArmourySystem
{
    partial class FrmReports
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClearAllFilters = new System.Windows.Forms.Button();
            this.chkOut = new System.Windows.Forms.CheckBox();
            this.lblOp = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cboOp = new System.Windows.Forms.ComboBox();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.cboWeapon = new System.Windows.Forms.ComboBox();
            this.btnOODMuster = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(12, 403);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 35);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnClearAllFilters
            // 
            this.btnClearAllFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAllFilters.Location = new System.Drawing.Point(648, 6);
            this.btnClearAllFilters.Name = "btnClearAllFilters";
            this.btnClearAllFilters.Size = new System.Drawing.Size(74, 31);
            this.btnClearAllFilters.TabIndex = 24;
            this.btnClearAllFilters.Text = "Clear All";
            this.btnClearAllFilters.UseVisualStyleBackColor = true;
            // 
            // chkOut
            // 
            this.chkOut.AutoSize = true;
            this.chkOut.Checked = true;
            this.chkOut.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkOut.Location = new System.Drawing.Point(611, 14);
            this.chkOut.Name = "chkOut";
            this.chkOut.Size = new System.Drawing.Size(43, 17);
            this.chkOut.TabIndex = 23;
            this.chkOut.Text = "Out";
            this.chkOut.ThreeState = true;
            this.chkOut.UseVisualStyleBackColor = true;
            // 
            // lblOp
            // 
            this.lblOp.AutoSize = true;
            this.lblOp.Location = new System.Drawing.Point(465, 15);
            this.lblOp.Name = "lblOp";
            this.lblOp.Size = new System.Drawing.Size(24, 13);
            this.lblOp.TabIndex = 22;
            this.lblOp.Text = "Op:";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(331, 15);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(39, 13);
            this.lblGroup.TabIndex = 21;
            this.lblGroup.Text = "Group:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(199, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 20;
            this.lblType.Text = "Type:";
            // 
            // cboOp
            // 
            this.cboOp.AllowDrop = true;
            this.cboOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOp.FormattingEnabled = true;
            this.cboOp.Location = new System.Drawing.Point(495, 12);
            this.cboOp.Name = "cboOp";
            this.cboOp.Size = new System.Drawing.Size(110, 21);
            this.cboOp.TabIndex = 19;
            // 
            // cboGroup
            // 
            this.cboGroup.AllowDrop = true;
            this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Location = new System.Drawing.Point(376, 12);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(83, 21);
            this.cboGroup.TabIndex = 18;
            // 
            // cboWeapon
            // 
            this.cboWeapon.AllowDrop = true;
            this.cboWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeapon.FormattingEnabled = true;
            this.cboWeapon.Location = new System.Drawing.Point(239, 12);
            this.cboWeapon.Name = "cboWeapon";
            this.cboWeapon.Size = new System.Drawing.Size(86, 21);
            this.cboWeapon.TabIndex = 17;
            // 
            // btnOODMuster
            // 
            this.btnOODMuster.Location = new System.Drawing.Point(12, 43);
            this.btnOODMuster.Name = "btnOODMuster";
            this.btnOODMuster.Size = new System.Drawing.Size(100, 35);
            this.btnOODMuster.TabIndex = 25;
            this.btnOODMuster.Text = "OOD Muster";
            this.btnOODMuster.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(128, 43);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(594, 395);
            this.dataGridView.TabIndex = 26;
            // 
            // FrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnOODMuster);
            this.Controls.Add(this.btnClearAllFilters);
            this.Controls.Add(this.chkOut);
            this.Controls.Add(this.lblOp);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cboOp);
            this.Controls.Add(this.cboGroup);
            this.Controls.Add(this.cboWeapon);
            this.Controls.Add(this.btnExit);
            this.Name = "FrmReports";
            this.Text = "Armoury Reports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReports_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClearAllFilters;
        private System.Windows.Forms.CheckBox chkOut;
        private System.Windows.Forms.Label lblOp;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboOp;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.ComboBox cboWeapon;
        private System.Windows.Forms.Button btnOODMuster;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}