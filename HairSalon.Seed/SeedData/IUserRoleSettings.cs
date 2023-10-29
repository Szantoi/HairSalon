using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Seed.SeedData
{
    public interface IUserRoleSettings
    {
        public string AdminRoleName { get; }

        public string AdminPasword { get; }
        public string AdminUserName { get; }
        public string AdminEmail { get; }
        public string AdminFullName { get; }

        public string AszisztensRoleName { get; }

        public string AszisztensPasword { get; }
        public string AszisztensUserName { get; }
        public string AszisztensEmail { get; }
        public string AszisztensFullName { get; }

        public string PrivilegedUserRoleName { get; }

        public string PrivilegedUserPasword { get; }
        public string PrivilegedUserUserName { get; }
        public string PrivilegedUserEmail { get; }
        public string PrivilegedUserFullName { get; }
    }
}
