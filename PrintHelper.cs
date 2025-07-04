using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using static ArmourySystem.PackageConstants;

namespace ArmourySystem
{
    internal class PrintHelper
    {
        private DataTable DataTable { get; set; }

        private readonly PrintDocument printDoc = new PrintDocument();
        private int currentRow = 0;
        private int currentPage = 0;
        private readonly int colPadding = 2;
        private readonly int rowPadding = 5;

        public PrintHelper(DataTable dataTable)
        {
            DataTable = dataTable;
        }

        // Gets all the unique values from a printout for a given column. Used for header details
        private string GetValue(string aColumn)
        {

            string returnValue = "";
            List<string> values = new List<string>();

            values = DataTable.AsEnumerable()
                    .Select(row => row.Field<string>(aColumn))
                    .Where(val => !string.IsNullOrEmpty(val))
                    .Distinct()
                    .OrderBy(val => val)
                    .ToList();

            foreach (string aValue in values)
            {
                returnValue += aValue + " - ";
            }

            if (returnValue.EndsWith(" - "))
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 3);
            }

            return returnValue;
        }

        public void Preview_Pages()
        {
            // Check if DataTable is null or empty before printing
            if (DataTable == null || DataTable.Rows.Count == 0)
            {
                MessageBox.Show("No data to print.");
                return;
            }

            // Set up the print preview document
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.PrintPage -= PrintDoc_PrintPage; // remove any duplicate handlers
            printDoc.PrintPage += PrintDoc_PrintPage;
            currentRow = 0; // reset row tracker
            currentPage = 0; // reset page tracker

            // Create a PrintPreviewDialog to show the print preview
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = printDoc;
                preview.Icon = SystemIcons.Application;
                preview.Text = "Print Preview - Transactions of all Weapons";
                preview.Width = 1000;  // optional, for better display
                preview.Height = 800;
                DisablePrintButton(preview); // disable print button in preview 

                preview.ShowDialog();  // user cannot choose to print from toolbar as it's disabled
            }
        }

        public void Print_Pages()
        {
            // Check if DataTable is null or empty before printing
            if (DataTable == null || DataTable.Rows.Count == 0)
            {
                MessageBox.Show("No data to print.");
                return;
            }

            // Set up the print document
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.Color = false;
            printDoc.PrintPage -= PrintDoc_PrintPage; // remove any duplicate handlers
            printDoc.PrintPage += PrintDoc_PrintPage;
            currentRow = 0; // reset row tracker
            currentPage = 0; // reset page tracker

            // Create a PrintDialog to allow user to select printer and options
            using (PrintDialog printDialog = new PrintDialog())
            {
                printDialog.Document = printDoc;
                printDialog.AllowSomePages = true;
                printDialog.AllowSelection = true;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            ;
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = 30;
            int y = 30;
            int rowHeight = 30;
            int hdrRowHeight = 40;
            currentPage++; // increase page number

            // Calculate total pages based on the number of rows
            int totalRows = DataTable.Rows.Count;
            int rowsPerPage = ((e.MarginBounds.Height - rowHeight) / rowHeight) - 2; // exclude header and remove 2 rows for the footer
            int totalPages = (int)Math.Ceiling((double)totalRows / rowsPerPage);

            Font bodyFont = new Font("Arial", 9, FontStyle.Regular);
            Font bodyHeaderFont = new Font("Arial", 9, FontStyle.Bold);
            Brush brush = Brushes.Black;
            Pen pen = Pens.Black;


            // Page Header printing
            Font pageHeaderFontB = new Font("Arial", 16, FontStyle.Bold);
            Font pageHeaderFont = new Font("Arial", 12, FontStyle.Regular);
            string headerText;

            // Document Title
            headerText = "Transactions of all Weapons";
            Rectangle headerRect = new Rectangle(x, y, 400, 50);
            e.Graphics.DrawString(headerText, pageHeaderFontB, brush, headerRect);

            // Print out the Types from column "Type"
            headerText = GetValue(GetWeaponName(WeaponHeader.Type));
            headerRect = new Rectangle(x, y + 50, 400, 80);
            e.Graphics.DrawString(headerText, pageHeaderFontB, brush, headerRect);

            // Print out the Groups from column "Group"
            headerText = GetValue(GetWeaponName(WeaponHeader.Group));
            headerRect = new Rectangle(x, y + 80, 400, 110);
            e.Graphics.DrawString(headerText, pageHeaderFont, brush, headerRect);

            // Print out the Ops from column "Op"
            headerText = GetValue(GetWeaponName(WeaponHeader.Op));
            headerRect = new Rectangle(x, y + 100, 400, 130);
            e.Graphics.DrawString(headerText, pageHeaderFont, brush, headerRect);

            // Armoury Stamp Box
            headerRect = new Rectangle(x + 450, y, 200, 150);
            e.Graphics.DrawRectangle(pen, headerRect);

            // Document Reference
            headerText = "RN S 3016A";
            SizeF textSize = e.Graphics.MeasureString(headerText, pageHeaderFontB);
            float xPos = e.MarginBounds.Right - textSize.Width;
            float yPos = y;
            e.Graphics.DrawString(headerText, pageHeaderFontB, brush, xPos, yPos);

            // Document Sub Reference 1
            headerText = "(Revised 11/20)";
            textSize = e.Graphics.MeasureString(headerText, pageHeaderFont);
            xPos = e.MarginBounds.Right - textSize.Width;
            e.Graphics.DrawString(headerText, pageHeaderFont, brush, xPos, yPos + 30);

            // Document Sub Reference 2
            headerText = "Part 4";
            textSize = e.Graphics.MeasureString(headerText, pageHeaderFontB);
            xPos = e.MarginBounds.Right - textSize.Width;
            e.Graphics.DrawString(headerText, pageHeaderFontB, brush, xPos, yPos + 60);

            y += 160; // Move y position after header
            int colPos = 0;

            // Add the footer with page number of total pages
            string footerText = $"Page {currentPage} of {totalPages}";
            textSize = e.Graphics.MeasureString(footerText, pageHeaderFontB);
            xPos = e.MarginBounds.Right - textSize.Width;
            yPos = e.MarginBounds.Bottom - textSize.Height;
            e.Graphics.DrawString(footerText, pageHeaderFontB, brush, xPos, yPos + 80);


            // Create an array to store col widths and set any not to print as -1
            int[] colWidths = new int[DataTable.Columns.Count];

            // set so we skip the columns in rows below
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.Type))] = -1;              // Type column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.Group))] = -1;             // Group column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.Op))] = -1;                // Op column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.ReceiptDate))] = -1;       // Receipt Date column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.ReceiptVoucher))] = -1;    // Receipt Voucher column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.IssueDate))] = -1;         // Issue Date column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.IssueVoucher))] = -1;      // Issue Voucher column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.PermIssue))] = -1;         // Perm Issue column
            colWidths[DataTable.Columns.IndexOf(GetWeaponName(WeaponHeader.PermName))] = -1;          // Perm Name column

            // Draw table header once per page and calculate column widths
            for (int col = 0; col < DataTable.Columns.Count; col++)
            {
                // Skip the "Out" column and any columns that are not needed
                if (DataTable.Columns[col].ColumnName != GetWeaponName(WeaponHeader.Out))
                {
                    // This creates a blank space column set to 0 but fixed with will be 10 pixels
                    if (DataTable.Columns[col].ColumnName == GetWeaponName(WeaponHeader.Dash))
                    {
                        colWidths[col] = 0;
                        colPos += 10;
                    }
                    // Calculate the width of the column based on its content and print text in an enclosing box
                    else
                    {
                        // Ensure column width is not set to skip. Values will be 0 for wanted columns, -1 for skipped columns
                        if (colWidths[col] >= 0)
                        {
                            int colPrintWidth = CalculateColumnAutoWidth(e.Graphics, DataTable, col, bodyFont, colPadding * 2);
                            Rectangle rect = new Rectangle(x + colPos, y, colPrintWidth, hdrRowHeight);
                            Rectangle rectStr = new Rectangle(x + colPos + colPadding, y + rowPadding, colPrintWidth - colPadding, rowHeight + rowPadding);
                            e.Graphics.FillRectangle(Brushes.LightGray, rect);
                            e.Graphics.DrawRectangle(pen, rect);
                            e.Graphics.DrawString(DataTable.Columns[col].ColumnName, bodyHeaderFont, brush, rectStr);
                            colWidths[col] = colPrintWidth;
                            colPos += colPrintWidth;
                        }
                    }
                }
                else
                {
                    // set so we skip in rows below
                    colWidths[col] = -1;
                }
            }

            y += rowHeight + 10;


            int rowsPrinted = 0;
            // Print the rows of the DataTable
            while (currentRow < DataTable.Rows.Count && rowsPrinted < rowsPerPage)
            {
                colPos = 0;
                DataRow row = DataTable.Rows[currentRow];

                for (int col = 0; col < DataTable.Columns.Count; col++)
                {
                    // standard column printing
                    if (colWidths[col] > 0)
                    {
                        Rectangle rect = new Rectangle(x + colPos, y, colWidths[col], rowHeight);
                        Rectangle rectStr = new Rectangle(x + colPos + colPadding, y + rowPadding, colWidths[col] - colPadding, rowHeight + rowPadding);
                        e.Graphics.DrawRectangle(pen, rect);
                        // Skip any added print formatter strings
                        if (row[col]?.ToString().Contains("XXXXXXXX") != true)
                        {
                            e.Graphics.DrawString(row[col]?.ToString(), bodyFont, brush, rectStr);
                        }
                        colPos += colWidths[col];
                    }
                    // This is to provide a space of 10 pixels between column groups
                    else if (colWidths[col] == 0)
                    {
                        colPos += 10;
                    }
                    // This is to skip columns that are not needed
                    else
                    {
                        // skip this column
                    }
                }


                y += rowHeight;
                currentRow++;
                rowsPrinted++;
            }

            // If more rows remain, request another page
            e.HasMorePages = currentRow < DataTable.Rows.Count;
        }

        int CalculateColumnAutoWidth(Graphics g, DataTable table, int columnIndex, Font font, int padding = 10)
        {
            if (table == null || table.Columns.Count <= columnIndex)
                return 0;

            string headerText = table.Columns[columnIndex].ColumnName;
            float maxWidth = g.MeasureString(headerText, font).Width - padding;

            // Check each row in the specified column to find the maximum width
            foreach (DataRow row in table.Rows)
            {
                var cellText = row[columnIndex]?.ToString() ?? "";
                float cellWidth = g.MeasureString(cellText, font).Width;
                if (cellWidth > maxWidth)
                    maxWidth = cellWidth;
            }

            return (int)(maxWidth + padding); // Add some padding for spacing
        }

        private void DisablePrintButton(PrintPreviewDialog preview)
        {
            foreach (Control c in preview.Controls)
            {
                if (c is ToolStrip toolStrip)
                {
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        if (item.ToolTipText == "Print")
                        {
                            item.Enabled = false;
                            item.Visible = false; // Hide the print button
                        }
                    }
                }
            }
        }

    }
}

