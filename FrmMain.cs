using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace ArmourySystem
{
    public partial class FrmMain : Form
    {
        private DataTable excelTable;

        private readonly string[] filterColumns;
        private readonly ComboBox[] filters;
        private bool isAdmin = false; // This should be set based on your validation logic
        private bool dataChanged = false; // Track if data has changed

        public FrmMain()
        {
            InitializeComponent();
            LoadFormSettings();
            
            this.btnAddUser.Enabled = false; // Disable Add User button initially
            this.btnLoadData.Enabled = false; // Disable button initially
            this.btnSaveData.Enabled = false; // Disable button initially
            this.btnPrint.Enabled = false; // Disable button initially
            this.btnPrintPreview.Enabled = false; // Disable button initially
            this.btnFind.Enabled = false; // Disable button initially
            this.btnReports.Enabled = false; // Disable button initially

            bool exists = ExcelUserStore.Initialize(); // Ensure user store is initialized
            ExcelWeaponStore.Initialize(); // Ensure weapon store is initialized
            filterColumns = new[] { "Type", "Group", "Op" };
            filters = new[] { cboWeapon, cboGroup, cboOp };

            if (!exists)
            {
                BtnAddUser_Click(null, null);
            }
        }

        private void LoadFormSettings()
        {
            if (Properties.Settings.Default.LastSize.Width > 0)
            {
                this.Size = Properties.Settings.Default.LastSize;
            }

            if (Properties.Settings.Default.LastLocation.X > 0 ||
                Properties.Settings.Default.LastLocation.Y > 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Properties.Settings.Default.LastLocation;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastSize = this.Size;
            Properties.Settings.Default.LastLocation = this.Location;
            Properties.Settings.Default.Save();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            FrmUserDetails loginForm = new FrmUserDetails("LogIn");
            bool validate; // This should be set based on your validation logic

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                string user = loginForm.Username;
                string pass = loginForm.Password;
                User storedUser = null;

                // Validate login
                validate = false; // Set this based on your actual validation logic below
                isAdmin = false; // Set this based on your actual validation logic below

                if (ExcelUserStore.UserExists(user))
                {
                    storedUser = ExcelUserStore.GetUser(user);

                    if (storedUser != null)
                    {
                        if (CryptoHelper.VerifyPassword(pass, storedUser.PasswordHash, storedUser.Salt)) { validate = true; }
                        if (storedUser.Role == "Admin") { isAdmin = true; }
                    }
                }

                if (validate == true) // Assuming Validate is a boolean that checks if the login is successful
                {
                    if (isAdmin == true)
                    {
                        this.btnAddUser.Enabled = true; // Enable Add User button
                        this.btnSaveData.Enabled = true; // Enable Save Data button
                    }

                    this.Text = this.Text + " >> Logged in as: " + storedUser.Username + "  role: " + storedUser.Role.ToUpper();
                    this.btnLogin.Visible = false;

                    this.btnLoadData.Enabled = true; // Enable Load Data button
                    this.btnPrintPreview.Enabled = true; // Enable Print Preview button
                    this.btnPrint.Enabled = true; // Enable Print button
                    this.btnFind.Enabled = true; // Enable Find button
                    this.btnReports.Enabled = true; // Enable Reports button
                }
                else
                {
                    MessageBox.Show("Login failed. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Application.Exit(); // or do nothing
            }
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            FrmUserDetails loginForm = new FrmUserDetails("AddUser");

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                string user = loginForm.Username;
                string pass = loginForm.Password;
                string role = "User"; // Default role

                if (loginForm.IsAdmin)
                {
                    role = "Admin";
                }

                try
                {
                    // Method to add the user to your user store
                    ExcelUserStore.AddUser(user, pass, role);
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // do nothing
            }

        }

        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                // Method to read the weapon data from the excel workbook
                excelTable = ExcelWeaponStore.LoadExcelWeaponData();
                dataGridView.DataSource = excelTable.DefaultView;

                dataChanged = false; // Reset the data changed flag as we have reloaded the data

                // If the user is not an admin, we can disable certain functionalities
                if (!isAdmin)
                {
                    dataGridView.Columns["Type"].ReadOnly = true;
                    dataGridView.Columns["Group"].ReadOnly = true;
                    dataGridView.Columns["Op"].ReadOnly = true;
                    dataGridView.Columns["Local No"].ReadOnly = true;
                    dataGridView.Columns["Serial No"].ReadOnly = true;
                    dataGridView.Columns["Sight Serial No"].ReadOnly = true;
                }

                dataGridView.Columns["Type"].Frozen = true;
                dataGridView.Columns["Group"].Frozen = true;
                dataGridView.Columns["Op"].Frozen = true;
                dataGridView.Columns["Local No"].Frozen = true;
                dataGridView.Columns["Serial No"].Frozen = true;
                dataGridView.Columns["Sight Serial No"].Frozen = true;
                dataGridView.Columns["Out"].Frozen = true;

                PopulateAllFilters();
                HookFilterEvents();

                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading weapon data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (dataChanged)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes. Do you want to save before exiting?",
                                  "Unsaved Changes",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    BtnSaveData_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return; // Cancel the exit
                }
            }

            this.Close();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            RepositionButtons();
        }

        private void BtnSaveData_Click(object sender, EventArgs e)
        {
            // All filters are done on a temp table, excelTable remains a full copy of the data
            try
            {
                // Method to add the weapon data to the excel workbook
                ExcelWeaponStore.SaveExcelWeaponData(excelTable);
                dataChanged = false; // Reset the data changed flag after saving
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving weapon data:\r\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                DataView currentView = (DataView)dataGridView.DataSource;
                DataTable filteredTable = currentView.ToTable();
                PrintHelper printForm = new PrintHelper(filteredTable);
                printForm.Preview_Pages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing no weapon data:\r\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataView currentView = (DataView)dataGridView.DataSource;
                DataTable filteredTable = currentView.ToTable();
                PrintHelper printForm = new PrintHelper(filteredTable);
                printForm.Print_Pages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing no weapon data:\r\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
        // Test new method in seperate class
        private void PopulateAllFilters()
        {
            filterHelper = new FilterHelper(excelTable, filterColumns, filters, chkOut, dataGridView, dataGridViewResults, txtSearch);
            filterHelper.PopulateAllFilters();
        }

        // Test new method in seperate class
        private void ApplyCombinedFilters()
        {
            filterHelper.ApplyCombinedFilters();
        }
        
        // Test new method in seperate class
        private void HookFilterEvents()
        {
            filterHelper.HookFilterEvents();
        }
        // Test new method in seperate class
        private void BtnClearAllFilters_Click(object sender, EventArgs e)
        {
            filterHelper.ClearAllFilters();
        }

        */
        // Old method to populate all filters
        private void PopulateAllFilters()
        {
            for (int i = 0; i < filters.Length; i++)
            {
                      string column = filterColumns[i];
                      var uniqueValues = excelTable.AsEnumerable()
                          .Select(row => row.Field<string>(column))
                          .Where(val => !string.IsNullOrEmpty(val))
                          .Distinct()
                          .OrderBy(val => val)
                          .ToList();

                      uniqueValues.Insert(0, "All");
                      filters[i].DataSource = new BindingSource(uniqueValues, null);
            }
        }

        private void ApplyCombinedFilters()
        {
            DataView view = new DataView(excelTable);
            List<string> conditions = new List<string>();

            for (int i = 0; i < filters.Length; i++)
            {
                string value = filters[i].SelectedItem?.ToString();
                string column = filterColumns[i];

                if (!string.IsNullOrEmpty(value) && value != "All")
                {
                    string safeValue = value.Replace("'", "''");
                    conditions.Add($"[{column}] = '{safeValue}'");
                }
            }

            // Enabled (CheckBox)
            if (chkOut.CheckState != CheckState.Indeterminate)
            {
                string boolValue = chkOut.CheckState == CheckState.Checked ? "true" : "false";
                conditions.Add($"[Out] = {boolValue}");
            }

            view.RowFilter = string.Join(" AND ", conditions);
            dataGridView.DataSource = view;
        }

        private void HookFilterEvents()
        {
            for (int i = 0; i < filters.Length; i++)
            {
                filters[i].SelectedIndexChanged += (s, e) => ApplyCombinedFilters();
            }

            chkOut.CheckStateChanged += (s, e) => ApplyCombinedFilters();
        }

        private void BtnClearAllFilters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < filters.Length; i++)
            {
                filters[i].SelectedItem = "All";
            }

            chkOut.CheckState = CheckState.Indeterminate;
            ApplyCombinedFilters();
            txtSearch.Clear(); // Clear search box
        }
        

        private void DataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (isAdmin)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?",
                                  "Confirm Delete",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true; // Cancels deletion
                    return;
                }

                // Handle deletion only if confirmed
                if (dataGridView.DataSource is DataView && e.Row.DataBoundItem is DataRowView drv)
                {
                    drv.Row.Delete();  // Remove from DataView/DataTable
                    dataChanged = true; // Set flag to indicate data has changed        
                    e.Cancel = true;   // Cancel default grid behavior — prevents double-delete
                }
                else
                {
                    // For unbound grid — let it delete normally
                    dataChanged = true; // Set flag to indicate data has changed        
                    e.Cancel = false;
                }
            }
            else
            {
                MessageBox.Show($"Error row deletion is an Admin function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;   // Cancel default grid behavior — prevents deletion
            }

        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Check if the cell value has changed
                if (dataGridView.IsCurrentCellDirty)
                {
                    dataChanged = true; // Set flag to indicate data has changed
                    dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit); // Commit the edit
                }
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text; //.Trim();

                if (string.IsNullOrEmpty(searchText))
                {
                    MessageBox.Show("Please enter search text in the Find box below", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create a filtered table
                DataTable resultTable = excelTable.Clone(); // same structure

                foreach (DataRow row in excelTable.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        if (item != null && item.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            resultTable.ImportRow(row);
                            break; // match found in one cell, move to next row
                        }
                    }
                }

                if (resultTable.Rows.Count == 0)
                {
                    MessageBox.Show("No search matches found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FrmFindResults resultsForm = new FrmFindResults(resultTable, dataGridView);
                    resultsForm.ShowDialog();

                }

            } catch (Exception ex)
            {
                MessageBox.Show($"Error searching no weapon data:\r\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent beep
                e.Handled = true;

                // Then show the dialog
                BtnFind_Click(sender, e); // Trigger search on Enter key
            }
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            FrmReports reportsForm = new FrmReports();
            reportsForm.ShowDialog();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Focused)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                BtnFind_Click(null, null); 
            }
        }
    }
}
