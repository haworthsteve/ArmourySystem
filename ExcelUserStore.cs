using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using static ArmourySystem.PackageConstants;

namespace ArmourySystem
{
    public static class ExcelUserStore
    {
        public static bool Initialize()
        {
            ExcelPackage.License.SetNonCommercialOrganization(PackageAuthor);

            if (!File.Exists(UserFilePath))
            {
                using (var package = new ExcelPackage(new FileInfo(UserFilePath)))
                {
                    var sheet = package.Workbook.Worksheets.Add(WorksheetUsers);

                    // Set headers for the user data
                    var headers = GetAllUserHeaderNames();
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
                if (!UserEncrypt) // if the file exists but we do not want encryption open it and save it without encryption
                {
                    using (var package = new ExcelPackage(new FileInfo(UserFilePath), UserExcelPassword))
                    {
                        SaveEncrypted(package);
                    }
                }

                return true; // File already exists, so it is not empty
            }
        }

        private static void SaveEncrypted(ExcelPackage package)
        {
            if (UserEncrypt)
            {
                // Enable AES encryption
                package.Encryption.IsEncrypted = true;
                package.Encryption.Algorithm = EncryptionAlgorithm.AES256;
                package.Encryption.Password = UserExcelPassword;
            }
            else
            {
                // Disable encryption if not needed
                package.Encryption.IsEncrypted = false;
            }

            // Save the package to the file
            package.SaveAs(new FileInfo(UserFilePath));
        }

        public static void AddUser(string username, string password, string role)
        {
            if (UserExists(username)) throw new Exception("User already exists.");

            string salt = CryptoHelper.GenerateSalt();
            string hash = CryptoHelper.HashPasswordWithSalt(password, salt);

            using (var package = new ExcelPackage(new FileInfo(UserFilePath), UserExcelPassword))
            {
                var sheet = package.Workbook.Worksheets[WorksheetUsers];
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
            using (var package = new ExcelPackage(new FileInfo(UserFilePath), UserExcelPassword))
            {
                var sheet = package.Workbook.Worksheets[WorksheetUsers];
                var range = sheet.Cells[2, 1, sheet.Dimension.End.Row, 1];
                return range.Any(cell => cell.GetValue<string>() == username);
            }
        }

        public static User GetUser(string username)
        {
            using (var package = new ExcelPackage(new FileInfo(UserFilePath), UserExcelPassword))
            {
                var sheet = package.Workbook.Worksheets[WorksheetUsers];
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
                            Role = sheet.Cells[row, 6].Text ?? DefaultRole // Default to "User" if role is not set
                        };
                    }
                }

                return null;
            }
        }

        public static void UpdateUser(User user)
        {
            using (var package = new ExcelPackage(new FileInfo(UserFilePath), UserExcelPassword))
            {
                var sheet = package.Workbook.Worksheets[WorksheetUsers];
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
