using OfficeOpenXml;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ArmourySystem
{
    public static class ExcelWeaponStore
    {
        private readonly static string FilePath = "weapons.xlsx";
        private readonly static string SheetName = "Weapons";
        private readonly static string ExcelPassword = "password";
        private readonly static bool encrypt = false; // Set to true if you want to encrypt the Excel file

        public static bool Initialize()
        {
            ExcelPackage.License.SetNonCommercialOrganization("Steve Haworth");

            if (!File.Exists(FilePath))
            {
                using (var package = new ExcelPackage(new FileInfo(FilePath)))
                {
                    var sheet = package.Workbook.Worksheets.Add(SheetName);

                    // Set headers for the user data
                    sheet.Cells[1, 1].Value = "Type";
                    sheet.Cells[1, 2].Value = "Local No";
                    sheet.Cells[1, 3].Value = "Serial No";
                    sheet.Cells[1, 4].Value = "Sight Serial No";
                    sheet.Cells[1, 5].Value = "Out";

                    SaveEncrypted(package);
                    return false; // File was created, so it was empty
                }
            }
            else
            {
                if (!encrypt) // if the file exists but we do not want encryption open it and save it without encryption
                {
                    using (var package = new ExcelPackage(new FileInfo(FilePath), ExcelPassword))
                    {
                        SaveEncrypted(package);
                    }
                }

                return true; // File already exists, so it is not empty
            }
        }

        private static void SaveEncrypted(ExcelPackage package)
        {
            if (encrypt)
            {
                // Enable AES encryption
                package.Encryption.IsEncrypted = true;
                package.Encryption.Password = ExcelPassword;
            }
            else
            {
                // Disable encryption if not needed
                package.Encryption.IsEncrypted = false;
            }

            // Save the package to the file
            package.SaveAs(new FileInfo(FilePath));
        }

        public static DataTable LoadExcelWeaponData()
        {
            var dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(FilePath)))
            {
                var worksheet = package.Workbook.Worksheets[SheetName];
                bool hasHeader = true;

                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // Add headers
                for (int col = 1; col <= colCount; col++)
                {
                    var colName = hasHeader ? worksheet.Cells[1, col].Text : $"Column{col}";
                    if (colName == "Out")  // 👈 Target boolean column
                        dt.Columns.Add(colName, typeof(bool));
                    else
                        dt.Columns.Add(colName);

                }

                int startRow = hasHeader ? 2 : 1;
                for (int row = startRow; row <= rowCount; row++)
                {
                    var newRow = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        var cellText = worksheet.Cells[row, col].Text;

                        // 👇 If column is boolean, parse it
                        if (dt.Columns[col - 1].DataType == typeof(bool))
                        {
                            // Replace the problematic code with the following:
                            newRow[col - 1] = ParseBoolean(cellText);
                        }
                        else
                        {
                            newRow[col - 1] = cellText;
                        }
                    }
                    dt.Rows.Add(newRow);
                }

                return dt;
            }
        }

        public static void SaveExcelWeaponData(DataTable table)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(SheetName);

                    // Write header
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = table.Columns[col].ColumnName;
                    }

                    // Write rows
                    for (int row = 0; row < table.Rows.Count; row++)
                    {
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            var value = table.Rows[row][col];

                            // Convert bool to string for Excel 
                            if (value is bool b)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = b ? "TRUE" : "FALSE";
                            }
                            else
                            {
                                worksheet.Cells[row + 2, col + 1].Value = value?.ToString();
                            }
                        }
                    }

                    SaveEncrypted(package);

                    MessageBox.Show("Weapon data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving no weapon data:\r\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // helper method for stored boolean values
        private static bool ParseBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.ToLower(CultureInfo.InvariantCulture);
            return value == "true" || value == "yes" || value == "1";
        }
    }

}
