using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmourySystem
{
    public partial class FrmReports : Form
    {
        private FilterHelper filterHelper;
        private readonly string[] filterColumns;
        private readonly ComboBox[] filters;
        private readonly DataTable excelTable;

        public FrmReports(DataTable aDataTable, DataGridView orgDataGridView)
        {
            try
            {
                InitializeComponent();
                LoadFormSettings();
                filterColumns = new[] { "Type", "Group", "Op" };
                filters = new[] { cboWeapon, cboGroup, cboOp };

                excelTable = aDataTable;
                dataGridViewReports.DataSource = aDataTable.DefaultView;

                filterHelper = new FilterHelper(
                    excelTable,                     // loaded full DataTable
                    dataGridViewReports,            // Main display grid
                    filters,
                    filterColumns,
                    chkOut                          // Optional: In/Out checkbox
                );

                filterHelper.PopulateAllFilters();
                filterHelper.HookFilterEvents();
                filterHelper.ApplyCombinedFilters();


                for (int i = 0; i < orgDataGridView.Columns.Count; i++)
                {
                    DataGridViewColumn sourceCol = orgDataGridView.Columns[i];
                    DataGridViewColumn targetCol = dataGridViewReports.Columns[i];

                    targetCol.Width = sourceCol.Width;
                    targetCol.Frozen = sourceCol.Frozen;
                }

                // Lock the dataGridViewResults functionality to prevent editing/deleting rows
                dataGridViewReports.ReadOnly = true;                  // Prevent editing
                dataGridViewReports.AllowUserToAddRows = false;       // Disable new row creation
                dataGridViewReports.AllowUserToDeleteRows = false;    // Disable row deletion
                dataGridViewReports.AllowUserToResizeColumns = false; // Prevent resizing columns
                dataGridViewReports.AllowUserToResizeRows = false;    // Prevent resizing rows
                dataGridViewReports.MultiSelect = false;              // Optional: single row select only
                dataGridViewReports.ClearSelection();                 // Optional: deselect everything

                // Set selection to prevent showing selection colors
                dataGridViewReports.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dataGridViewReports.DefaultCellStyle.SelectionBackColor = dataGridViewReports.DefaultCellStyle.BackColor;
                dataGridViewReports.DefaultCellStyle.SelectionForeColor = dataGridViewReports.DefaultCellStyle.ForeColor;

                // Remove the left Hand row headers
                dataGridViewReports.RowHeadersVisible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading no weapon data:\r\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadFormSettings()
        {
            if (Properties.Settings.Default.ResultsLastSize.Width > 0)
            {
                this.Size = Properties.Settings.Default.ResultsLastSize;
            }

            if (Properties.Settings.Default.ResultsLastLocation.X > 0 ||
                Properties.Settings.Default.ResultsLastLocation.Y > 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Properties.Settings.Default.ResultsLastLocation;
            }

        }

        private void FrmReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ResultsLastSize = this.Size;
            Properties.Settings.Default.ResultsLastLocation = this.Location;
            Properties.Settings.Default.Save();
        }

        private void btnClearAllFilters_Click(object sender, EventArgs e)
        {
            filterHelper.ClearAllFilters();
        }

        private void dataGridViewReports_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Prevent beep
        }
    }
}