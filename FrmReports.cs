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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static ArmourySystem.PackageConstants;

namespace ArmourySystem
{
    public partial class FrmReports : Form
    {
        private readonly FilterHelper filterHelper;
        private readonly string[] filterColumns;
        private readonly ComboBox[] filters;
        private readonly DataTable excelTable;

        public FrmReports(DataTable aDataTable, DataGridView orgDataGridView)
        {
            try
            {
                InitializeComponent();
                LoadFormSettings();
                filterColumns = new[] { GetWeaponName(WeaponHeader.Type), GetWeaponName(WeaponHeader.Group), GetWeaponName(WeaponHeader.Op) };
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

        private void BtnClearAllFilters_Click(object sender, EventArgs e)
        {
            filterHelper.ClearAllFilters();
        }

        private void DataGridViewReports_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Prevent beep
        }

        public class PermOutSummary
        {
            public string RecipientName { get; set; }
            public int Count { get; set; }
        }

        public class WeaponSummary
        {
            public string Type { get; set; }
            public int TotalCount { get; set; }
            public int OutCount { get; set; }
            public List<PermOutSummary> PermOutRecipients { get; set; }
        }

        
        private void BtnOODMuster_Click(object sender, EventArgs e)
        {
            var weaponSummaries = excelTable.AsEnumerable()
                .Where(row => !string.IsNullOrWhiteSpace(row.Field<string>(GetWeaponName(WeaponHeader.Type))))
                .GroupBy(row => row.Field<string>(GetWeaponName(WeaponHeader.Type)))
                .Select(group =>
            {
                var type = group.Key;
                var totalCount = group.Count();
                var outCount = group.Count(r => r.Field<bool>(GetWeaponName(WeaponHeader.Out)));

            // Find all PERM outs for this type
            var permOuts = group
                .Where(r => r.Field<bool>(GetWeaponName(WeaponHeader.Out)) == true &&
                            r.Field<bool>(GetWeaponName(WeaponHeader.PermIssue)) == true)
                .GroupBy(r => r.Field<string>(GetWeaponName(WeaponHeader.PermName))?.Trim())
                .Select(g => new PermOutSummary
                {
                    RecipientName = string.IsNullOrWhiteSpace(g.Key) ? "(No Recipient)" : g.Key,
                    Count = g.Count()
                })
                .OrderBy(p => p.RecipientName)
                .ToList();

                return new WeaponSummary
                {
                    Type = type,
                    TotalCount = totalCount,
                    OutCount = outCount,
                    PermOutRecipients = permOuts
                 };
            })
            .OrderByDescending(ws => ws.TotalCount)
            .ToList();

            // TODO: Formulate the printed page

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Weapon Type Summary:");
            sb.AppendLine("------------------------");

            foreach (var weapon in weaponSummaries)
            {
                string permRecipientsStr = "";
                
                foreach (PermOutSummary permRecipient in weapon.PermOutRecipients)
                {
                    permRecipientsStr += permRecipient.RecipientName + "(" + permRecipient.Count + ")  "; 
                }

                if (permRecipientsStr.Length == 0)
                {
                    sb.AppendLine($"{weapon.Type}: Count = {weapon.TotalCount}, Out = {weapon.OutCount}");
                }
                else
                {
                    sb.AppendLine($"{weapon.Type}: Count = {weapon.TotalCount}, Out = {weapon.OutCount}, PERM ISSUE - {permRecipientsStr}");
                }
            }

            MessageBox.Show(sb.ToString(), "Weapon Type Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}