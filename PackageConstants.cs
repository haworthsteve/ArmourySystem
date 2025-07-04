using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public enum Header
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

        public static readonly Dictionary<Header, string> HeaderNames = new Dictionary<Header, string>
            {
                { Header.Type, "Type" },
                { Header.Group, "Group" },
                { Header.Op, "Op" },
                { Header.LocalNo, "Local No" },
                { Header.SerialNo, "Serial No" },
                { Header.SightSerialNo, "Sight Serial No" },
                { Header.Out, "Out" },
                { Header.PermIssue, "Perm Issue" },
                { Header.PermName, "Perm Name" },
                { Header.DateOfIssue, "Date of Issue" },
                { Header.RankAndName, "Rank and Name" },
                { Header.ServiceNumber, "Service Number" },
                { Header.Signature, "Signature" },
                { Header.DestinationOrLocation, "Destination / Location" },
                { Header.Dash, "-" },
                { Header.DateOfReturn, "Date of Return" },
                { Header.NameOfRecipient, "Name of Recipient" },
                { Header.SignatureOfRecipient, "Signature of Recipient" },
                { Header.ReceiptDate, "Receipt Date" },
                { Header.ReceiptVoucher, "Receipt Voucher" },
                { Header.IssueDate, "Issue Date" },
                { Header.IssueVoucher, "Issue Voucher" }
            };

        public static string GetHeaderName(Header header) => HeaderNames[header];

        public static string[] GetAllHeaderNames() => HeaderNames.Values.ToArray();
    }

}

