using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace ArmourySystem
{
    public class FilterHelper
    {
        private readonly DataTable _dataTable;
        private readonly string[] _filterColumns;
        private readonly ComboBox[] _filters;
        private readonly CheckBox _chkOut;
        private readonly DataGridView _dataGridView;
        private readonly DataGridView _dataGridViewResults;
        private readonly TextBox _txtSearch;

        public FilterHelper(
            DataTable dataTable,
            string[] filterColumns,
            ComboBox[] filters,
            CheckBox chkOut,
            DataGridView dataGridView,
            DataGridView dataGridViewResults = null,
            TextBox txtSearch = null)
        {
            _dataTable = dataTable;
            _filterColumns = filterColumns;
            _filters = filters;
            _chkOut = chkOut;
            _dataGridView = dataGridView;
            _dataGridViewResults = dataGridViewResults;
            _txtSearch = txtSearch;
        }

        public void PopulateAllFilters()
        {
            for (int i = 0; i < _filters.Length; i++)
            {
                string column = _filterColumns[i];
                var uniqueValues = _dataTable.AsEnumerable()
                    .Select(row => row.Field<string>(column))
                    .Where(val => !string.IsNullOrEmpty(val))
                    .Distinct()
                    .OrderBy(val => val)
                    .ToList();

                uniqueValues.Insert(0, "All");
                _filters[i].DataSource = new BindingSource(uniqueValues, null);
            }
        }

        public void ApplyCombinedFilters()
        {
            DataView view = new DataView(_dataTable);
            List<string> conditions = new List<string>();

            for (int i = 0; i < _filters.Length; i++)
            {
                string value = _filters[i].SelectedItem?.ToString();
                string column = _filterColumns[i];

                if (!string.IsNullOrEmpty(value) && value != "All")
                {
                    string safeValue = value.Replace("'", "''");
                    conditions.Add($"[{column}] = '{safeValue}'");
                }
            }

            if (_chkOut != null && _chkOut.CheckState != CheckState.Indeterminate)
            {
                string boolValue = _chkOut.CheckState == CheckState.Checked ? "true" : "false";
                conditions.Add($"[Out] = {boolValue}");
            }

            view.RowFilter = string.Join(" AND ", conditions);
            _dataGridView.DataSource = view;
        }

        public void HookFilterEvents()
        {
            for (int i = 0; i < _filters.Length; i++)
            {
                _filters[i].SelectedIndexChanged += (s, e) => ApplyCombinedFilters();
            }

            if (_chkOut != null)
                _chkOut.CheckStateChanged += (s, e) => ApplyCombinedFilters();
        }

        public void ClearAllFilters()
        {
            for (int i = 0; i < _filters.Length; i++)
            {
                _filters[i].SelectedItem = "All";
            }

            if (_chkOut != null)
                _chkOut.CheckState = CheckState.Indeterminate;

            ApplyCombinedFilters();

            if (_dataGridViewResults != null)
                _dataGridViewResults.Visible = false;

            _txtSearch?.Clear();
        }

    }
}
