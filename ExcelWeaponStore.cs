using OfficeOpenXml;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using static ArmourySystem.PackageConstants;

namespace ArmourySystem
{
    public static class ExcelWeaponStore
    {

        public static bool Initialize()
        {
            ExcelPackage.License.SetNonCommercialOrganization(PackageAuthor);

            if (!File.Exists(WeaponFilePath))
            {
                using (var package = new ExcelPackage(new FileInfo(WeaponFilePath)))
                {
                    var sheet = package.Workbook.Worksheets.Add(WeaponSheetName);

                    // Set headers for the weapon data
                    var headers = GetAllWeaponHeaderNames();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        sheet.Cells[1, i + 1].Value = headers[i];
                    }

                    SaveEncrypted(package);
                    return false; // File was created, so it was empty
                }
            }
            else
            {
                if (!WeaponEncrypt) // if the file exists but we do not want encryption open it and save it without encryption
                {
                    using (var package = new ExcelPackage(new FileInfo(WeaponFilePath), WeaponExcelPassword))
                    {
                        SaveEncrypted(package);
                    }
                }

                return true; // File already exists, so it is not empty
            }
        }

        private static void SaveEncrypted(ExcelPackage package)
        {
            if (WeaponEncrypt)
            {
                // Enable AES encryption
                package.Encryption.IsEncrypted = true;
                package.Encryption.Password = WeaponExcelPassword;
            }
            else
            {
                // Disable encryption if not needed
                package.Encryption.IsEncrypted = false;
            }

            // Save the package to the file
            package.SaveAs(new FileInfo(WeaponFilePath));
        }

        public static DataTable LoadExcelWeaponData()
        {
            var dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(WeaponFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[WeaponSheetName];
                bool hasHeader = true;

                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // Add headers
                for (int col = 1; col <= colCount; col++)
                {
                    var colName = hasHeader ? worksheet.Cells[1, col].Text : $"Column{col}";
                    if ((colName == GetWeaponName(WeaponHeader.Out)) || (colName == GetWeaponName(WeaponHeader.PermIssue)))  // comparison using GetHeaderName
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

                        // If column is boolean, parse it
                        if (dt.Columns[col - 1].DataType == typeof(bool))
                        {
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
                    var worksheet = package.Workbook.Worksheets.Add(WeaponSheetName);

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

                            // Handle specific columns
                            if (table.Columns[col].ColumnName == GetWeaponName(WeaponHeader.Signature))
                            {
                                worksheet.Cells[row + 2, col + 1].Value = "XXXXXXXXXXXXXX"; // Set Signature box spreader value
                                continue;
                            }

                            // Convert bool to string for Excel 
                            if (value is bool b)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = b ? strTrue : strFalse;
                            }
                            else
                            {
                                // Handle DBNull values for the "Out" column
                                if (((table.Columns[col].ColumnName == GetWeaponName(WeaponHeader.Out)) && (value is DBNull)) | 
                                    ((table.Columns[col].ColumnName == GetWeaponName(WeaponHeader.PermIssue)) && (value is DBNull)))
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = strFalse; // Default to false if DBNull
                                }
                                else 
                                {
                                    // For other types, just convert to string
                                    worksheet.Cells[row + 2, col + 1].Value = value?.ToString();
                                }
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
