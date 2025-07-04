using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ArmourySystem
{
    public class FilterHelper
    {
        private readonly DataTable _excelTable;
        private readonly DataGridView _dataGridView;
        private readonly ComboBox[] _filters;
        private readonly string[] _filterColumns;
        private readonly CheckBox _chkOut;
        private readonly TextBox _txtSearch;

        public FilterHelper(
              DataTable excelTable,
              DataGridView dataGridView,
              ComboBox[] filters,
              string[] filterColumns,
              CheckBox chkOut = null,
              TextBox txtSearch = null)
        {
            _excelTable = excelTable ?? throw new ArgumentNullException(nameof(excelTable));
            _dataGridView = dataGridView ?? throw new ArgumentNullException(nameof(dataGridView));
            _filters = filters ?? throw new ArgumentNullException(nameof(filters));
            _filterColumns = filterColumns ?? throw new ArgumentNullException(nameof(filterColumns));
            _chkOut = chkOut;
            _txtSearch = txtSearch;
        }

        public void PopulateAllFilters()
        {
            for (int i = 0; i < _filters.Length; i++)
            {
                string column = _filterColumns[i];
                var uniqueValues = _excelTable.AsEnumerable()
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
            DataView view = new DataView(_excelTable);
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
            foreach (var filter in _filters)
            {
                filter.SelectedIndexChanged += (s, e) => ApplyCombinedFilters();
            }

            if (_chkOut != null)
            {
                _chkOut.CheckStateChanged += (s, e) => ApplyCombinedFilters();
            }
        }

        public void ClearAllFilters()
        {
            foreach (var filter in _filters)
            {
                filter.SelectedItem = "All";
            }

            if (_chkOut != null)
                _chkOut.CheckState = CheckState.Indeterminate;

            _txtSearch?.Clear();

            ApplyCombinedFilters();
        }
    }
}
