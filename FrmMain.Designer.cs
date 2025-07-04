using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArmourySystem
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private const int btnWidth = 100;
        private const int btnHeight = 40;
        private const int btnBottomOffset = 12;
        private const int btnLeft = 12;
        private const int btnSpacing = 12;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cboWeapon = new System.Windows.Forms.ComboBox();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.cboOp = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblOp = new System.Windows.Forms.Label();
            this.chkOut = new System.Windows.Forms.CheckBox();
            this.btnClearAllFilters = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblFindText = new System.Windows.Forms.Label();
            this.btnReports = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogin.Location = new System.Drawing.Point(12, 58);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 40);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddUser.Location = new System.Drawing.Point(12, 104);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 40);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.Click += new System.EventHandler(this.BtnAddUser_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadData.Location = new System.Drawing.Point(12, 150);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(100, 40);
            this.btnLoadData.TabIndex = 3;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.Click += new System.EventHandler(this.BtnLoadData_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(12, 407);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(132, 46);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(596, 371);
            this.dataGridView.TabIndex = 5;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            this.dataGridView.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserAddedRow);
            this.dataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridView_UserDeletingRow);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveData.Location = new System.Drawing.Point(12, 196);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(100, 40);
            this.btnSaveData.TabIndex = 4;
            this.btnSaveData.Text = "Save Data";
            this.btnSaveData.Click += new System.EventHandler(this.BtnSaveData_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(12, 288);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 40);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // cboWeapon
            // 
            this.cboWeapon.AllowDrop = true;
            this.cboWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeapon.FormattingEnabled = true;
            this.cboWeapon.Location = new System.Drawing.Point(179, 12);
            this.cboWeapon.Name = "cboWeapon";
            this.cboWeapon.Size = new System.Drawing.Size(86, 21);
            this.cboWeapon.TabIndex = 7;
            // 
            // cboGroup
            // 
            this.cboGroup.AllowDrop = true;
            this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Location = new System.Drawing.Point(316, 12);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(83, 21);
            this.cboGroup.TabIndex = 8;
            // 
            // cboOp
            // 
            this.cboOp.AllowDrop = true;
            this.cboOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOp.FormattingEnabled = true;
            this.cboOp.Location = new System.Drawing.Point(435, 12);
            this.cboOp.Name = "cboOp";
            this.cboOp.Size = new System.Drawing.Size(110, 21);
            this.cboOp.TabIndex = 9;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(139, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 10;
            this.lblType.Text = "Type:";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(271, 15);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(39, 13);
            this.lblGroup.TabIndex = 11;
            this.lblGroup.Text = "Group:";
            // 
            // lblOp
            // 
            this.lblOp.AutoSize = true;
            this.lblOp.Location = new System.Drawing.Point(405, 15);
            this.lblOp.Name = "lblOp";
            this.lblOp.Size = new System.Drawing.Size(24, 13);
            this.lblOp.TabIndex = 12;
            this.lblOp.Text = "Op:";
            // 
            // chkOut
            // 
            this.chkOut.AutoSize = true;
            this.chkOut.Checked = true;
            this.chkOut.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkOut.Location = new System.Drawing.Point(551, 14);
            this.chkOut.Name = "chkOut";
            this.chkOut.Size = new System.Drawing.Size(43, 17);
            this.chkOut.TabIndex = 15;
            this.chkOut.Text = "Out";
            this.chkOut.ThreeState = true;
            this.chkOut.UseVisualStyleBackColor = true;
            // 
            // btnClearAllFilters
            // 
            this.btnClearAllFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAllFilters.Location = new System.Drawing.Point(654, 6);
            this.btnClearAllFilters.Name = "btnClearAllFilters";
            this.btnClearAllFilters.Size = new System.Drawing.Size(74, 31);
            this.btnClearAllFilters.TabIndex = 16;
            this.btnClearAllFilters.Text = "Clear All";
            this.btnClearAllFilters.UseVisualStyleBackColor = true;
            this.btnClearAllFilters.Click += new System.EventHandler(this.BtnClearAllFilters_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintPreview.Location = new System.Drawing.Point(12, 242);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(100, 40);
            this.btnPrintPreview.TabIndex = 17;
            this.btnPrintPreview.Text = "Preview";
            this.btnPrintPreview.Click += new System.EventHandler(this.BtnPrintPreview_Click);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(631, 426);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(97, 29);
            this.btnFind.TabIndex = 18;
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(405, 431);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 20);
            this.txtSearch.TabIndex = 20;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // lblFindText
            // 
            this.lblFindText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindText.AutoSize = true;
            this.lblFindText.Location = new System.Drawing.Point(325, 434);
            this.lblFindText.Name = "lblFindText";
            this.lblFindText.Size = new System.Drawing.Size(74, 13);
            this.lblFindText.TabIndex = 21;
            this.lblFindText.Text = "Search String:";
            // 
            // btnReports
            // 
            this.btnReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReports.Location = new System.Drawing.Point(12, 334);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(100, 40);
            this.btnReports.TabIndex = 22;
            this.btnReports.Text = "Reports";
            this.btnReports.Click += new System.EventHandler(this.BtnReports_Click);
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(740, 459);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.lblFindText);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.btnClearAllFilters);
            this.Controls.Add(this.chkOut);
            this.Controls.Add(this.lblOp);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cboOp);
            this.Controls.Add(this.cboGroup);
            this.Controls.Add(this.cboWeapon);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "HMS Collingwood Armoury";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void RepositionButtons()
        {
            this.btnExit.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnExit.Location = new Point(btnLeft, this.ClientSize.Height - (btnHeight + btnBottomOffset));

            this.btnReports.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnReports.Location = new Point(btnLeft, btnExit.Top - (btnSpacing * 2) - btnHeight);

            this.btnPrint.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnPrint.Location = new Point(btnLeft, btnReports.Top - btnSpacing - btnHeight);

            this.btnPrintPreview.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnPrintPreview.Location = new Point(btnLeft, btnPrint.Top - btnSpacing - btnHeight);

            this.btnSaveData.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnSaveData.Location = new Point(btnLeft, btnPrintPreview.Top - btnSpacing - btnHeight);

            this.btnLoadData.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnLoadData.Location = new Point(btnLeft, btnSaveData.Top - btnSpacing - btnHeight);

            this.btnAddUser.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnAddUser.Location = new Point(btnLeft, btnLoadData.Top - btnSpacing - btnHeight);

            this.btnLogin.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.btnLogin.Location = new Point(btnLeft, btnAddUser.Top - btnSpacing - btnHeight);

            this.MinimumSize = new Size((btnWidth * 2) + (btnLeft * 4), btnBottomOffset + (btnHeight * 6) + (btnSpacing * 6));
        }

        #endregion

        private Button btnLogin;
        private Button btnAddUser;
        private Button btnLoadData;
        private Button btnExit;
        private Button btnSaveData;
        private DataGridView dataGridView;
        private Button btnPrint;
        private ComboBox cboWeapon;
        private ComboBox cboGroup;
        private ComboBox cboOp;
        private Label lblType;
        private Label lblGroup;
        private Label lblOp;
        private CheckBox chkOut;
        private Button btnClearAllFilters;
        private Button btnPrintPreview;
        private Button btnFind;
        private TextBox txtSearch;
        private Label lblFindText;
        private Button btnReports;
    }
}

