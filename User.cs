using System;
using static ArmourySystem.PackageConstants;

namespace ArmourySystem
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime? LockoutUntil { get; set; }

        private string _role = DefaultRole; // Default role is "User"  
        public string Role
        {
            get => _role;
            set => _role = value;
        }
    }

}
