using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;

namespace ArmourySystem
{
    internal class PackageConstants
    {
        public const string PackageName = "ArmourySystem";
        public const string PackageVersion = "1.0.0";
        public const string PackageDescription = "A system for managing and tracking armoury items.";
        public const string PackageAuthor = "Steve Haworth";
        public const string PackageLicense = "MIT";
        public const string PackageRepositoryUrl = ""; // URL to the repository, e.g., GitHub

        // Excel USER file constants
        public readonly static string WorksheetUsers = "Users";
        public readonly static string DefaultRole = "User";
        public readonly static string UserFilePath = "users.xlsx";
        public readonly static string UserExcelPassword = "password";
        public readonly static bool UserEncrypt = false; // Set to true if you want to encrypt the User Excel file 

        // Excel WEAPON file constants
        public readonly static string WeaponFilePath = "weapons.xlsx";
        public readonly static string WeaponSheetName = "Weapons";
        public readonly static string WeaponExcelPassword = "password";
        public readonly static bool WeaponEncrypt = false; // Set to true if you want to encrypt the Weapon Excel file

        public readonly static string strTrue = "TRUE";
        public readonly static string strFalse = "FALSE";


        public enum UserHeader
        {
            Username,
            PasswordHash,
            Salt,
            FailedAttempts,
            LockoutUntil,
            Role
        }
        
        public static readonly Dictionary<UserHeader, string> UserHeaderNames = new Dictionary<UserHeader, string>
            {
                { UserHeader.Username, "Username" },
                { UserHeader.PasswordHash, "Password Hash" },
                { UserHeader.Salt, "Salt" },
                { UserHeader.FailedAttempts, "Failed Attempts" },
                { UserHeader.LockoutUntil, "Lockout Until" },
                { UserHeader.Role, "Role" }
            };
        public static string GetUserName(UserHeader header) => UserHeaderNames[header];
        public static string[] GetAllUserHeaderNames() => UserHeaderNames.Values.ToArray();

        public enum WeaponHeader
        {
            Type,
            Group,
            Op,
            LocalNo,
            SerialNo,
            SightSerialNo,
            Out,
            PermIssue,
            PermName,
            DateOfIssue,
            RankAndName,
            ServiceNumber,
            Signature,
            DestinationOrLocation,
            Dash,
            DateOfReturn,
            NameOfRecipient,
            SignatureOfRecipient,
            ReceiptDate,
            ReceiptVoucher,
            IssueDate,
            IssueVoucher
        }

        public static readonly Dictionary<WeaponHeader, string> WeaponHeaderNames = new Dictionary<WeaponHeader, string>
            {
                { WeaponHeader.Type, "Type" },
                { WeaponHeader.Group, "Group" },
                { WeaponHeader.Op, "Op" },
                { WeaponHeader.LocalNo, "Local No" },
                { WeaponHeader.SerialNo, "Serial No" },
                { WeaponHeader.SightSerialNo, "Sight Serial No" },
                { WeaponHeader.Out, "Out" },
                { WeaponHeader.PermIssue, "Perm Issue" },
                { WeaponHeader.PermName, "Perm Name" },
                { WeaponHeader.DateOfIssue, "Date of Issue" },
                { WeaponHeader.RankAndName, "Rank and Name" },
                { WeaponHeader.ServiceNumber, "Service Number" },
                { WeaponHeader.Signature, "Signature" },
                { WeaponHeader.DestinationOrLocation, "Destination / Location" },
                { WeaponHeader.Dash, "-" },
                { WeaponHeader.DateOfReturn, "Date of Return" },
                { WeaponHeader.NameOfRecipient, "Name of Recipient" },
                { WeaponHeader.SignatureOfRecipient, "Signature of Recipient" },
                { WeaponHeader.ReceiptDate, "Receipt Date" },
                { WeaponHeader.ReceiptVoucher, "Receipt Voucher" },
                { WeaponHeader.IssueDate, "Issue Date" },
                { WeaponHeader.IssueVoucher, "Issue Voucher" }
            };

        public static string GetWeaponName(WeaponHeader header) => WeaponHeaderNames[header];

        public static string[] GetAllWeaponHeaderNames() => WeaponHeaderNames.Values.ToArray();
    }

}

