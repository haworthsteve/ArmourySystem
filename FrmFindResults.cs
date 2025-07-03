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
    public partial class FrmFindResults : Form
    {
        public FrmFindResults()
        {
            InitializeComponent();
        }

        public FrmFindResults(DataTable aResultsTable, DataGridView orgDataGridView) : this()
        {
            dataGridViewResults.DataSource = aResultsTable;

            for (int i = 0; i < orgDataGridView.Columns.Count; i++)
            {
                DataGridViewColumn sourceCol = orgDataGridView.Columns[i];
                DataGridViewColumn targetCol = dataGridViewResults.Columns[i];

                targetCol.Width = sourceCol.Width;
                targetCol.Frozen = sourceCol.Frozen;
            }

            // Lock the dataGridViewResults functionality to prevent editing/deleting rows
            dataGridViewResults.ReadOnly = true;                  // Prevent editing
            dataGridViewResults.AllowUserToAddRows = false;       // Disable new row creation
            dataGridViewResults.AllowUserToDeleteRows = false;    // Disable row deletion
            dataGridViewResults.AllowUserToResizeColumns = false; // Prevent resizing columns
            dataGridViewResults.AllowUserToResizeRows = false;    // Prevent resizing rows
            dataGridViewResults.MultiSelect = false;              // Optional: single row select only
            dataGridViewResults.ClearSelection();                 // Optional: deselect everything

            // Set selection to prevent showing selection colors
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewResults.DefaultCellStyle.SelectionBackColor = dataGridViewResults.DefaultCellStyle.BackColor;
            dataGridViewResults.DefaultCellStyle.SelectionForeColor = dataGridViewResults.DefaultCellStyle.ForeColor;

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmFindResults_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true; // Prevent beep
            e.Handled = true;
        }
    }
}
