using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using static ArmourySystem.PackageConstants;

namespace ArmourySystem
{
    public static class ExcelUserStore
    {
        private readonly static string FilePath = "users.xlsx";
        private readonly static string ExcelPassword = "password";
        private readonly static bool encrypt = false; // Set to true if you want to encrypt the Excel file

        public static bool Initialize()
        {
            ExcelPackage.License.SetNonCommercialOrganization(PackageAuthor);

            if (!File.Exists(FilePath))
            {
                using (var package = new ExcelPackage(new FileInfo(FilePath)))
                {
                    var sheet = package.Workbook.Worksheets.Add("Users");

                    // Set headers for the user data
                    sheet.Cells[1, 1].Value = "Username";
                    sheet.Cells[1, 2].Value = "PasswordHash";
                    sheet.Cells[1, 3].Value = "Salt";
                    sheet.Cells[1, 4].Value = "FailedAttempts";
                    sheet.Cells[1, 5].Value = "LockoutUntil";
                    sheet.Cells[1, 6].Value = "Role";

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
                package.Encryption.Algorithm = EncryptionAlgorithm.AES256;
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

        public static void AddUser(string username, string password, string role)
        {
            if (UserExists(username)) throw new Exception("User already exists.");

            string salt = CryptoHelper.GenerateSalt();
            string hash = CryptoHelper.HashPasswordWithSalt(password, salt);

            using (var package = new ExcelPackage(new FileInfo(FilePath), ExcelPassword))
            {
                var sheet = package.Workbook.Worksheets["Users"];
                int row = sheet.Dimension.End.Row + 1;

                sheet.Cells[row, 1].Value = username;
                sheet.Cells[row, 2].Value = hash;
                sheet.Cells[row, 3].Value = salt;
                sheet.Cells[row, 4].Value = 0;
                sheet.Cells[row, 5].Value = null;
                sheet.Cells[row, 6].Value = role;

                SaveEncrypted(package);
            }
        }

        public static bool UserExists(string username)
        {
            using (var package = new ExcelPackage(new FileInfo(FilePath), ExcelPassword))
            {
                var sheet = package.Workbook.Worksheets["Users"];
                var range = sheet.Cells[2, 1, sheet.Dimension.End.Row, 1];
                return range.Any(cell => cell.GetValue<string>() == username);
            }
        }

        public static User GetUser(string username)
        {
            using (var package = new ExcelPackage(new FileInfo(FilePath), ExcelPassword))
            {
                var sheet = package.Workbook.Worksheets["Users"];
                int rowCount = sheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (sheet.Cells[row, 1].GetValue<string>() == username)
                    {
                        DateTime? lockoutUntil = null;
                        if (DateTime.TryParse(sheet.Cells[row, 5].Text, out DateTime dt))
                        {
                            lockoutUntil = dt;
                        }

                        return new User
                        {
                            Username = username,
                            PasswordHash = sheet.Cells[row, 2].Text,
                            Salt = sheet.Cells[row, 3].Text,
                            FailedAttempts = sheet.Cells[row, 4].GetValue<int>(),
                            LockoutUntil = lockoutUntil,
                            Role = sheet.Cells[row, 6].Text ?? "User" // Default to "User" if role is not set
                        };
                    }
                }

                return null;
            }
        }

        public static void UpdateUser(User user)
        {
            using (var package = new ExcelPackage(new FileInfo(FilePath), ExcelPassword))
            {
                var sheet = package.Workbook.Worksheets["Users"];
                int rowCount = sheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (sheet.Cells[row, 1].GetValue<string>() == user.Username)
                    {
                        sheet.Cells[row, 4].Value = user.FailedAttempts;
                        sheet.Cells[row, 5].Value = user.LockoutUntil?.ToString("o");
                        package.Save();
                        break;
                    }
                }
            }
        }
    }

}
