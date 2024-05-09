using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceCompany.DbStuff.Models.Enums
{
    public enum MemberRoleEnum
    {
        SuperAdmin = 1,
        Executor = 2,
        Manager = 3,
        Admin = 4,
        User = 5,
        ProjectManager = 6,
        BimManager = 7,
        CEO = 8,
    }
}
