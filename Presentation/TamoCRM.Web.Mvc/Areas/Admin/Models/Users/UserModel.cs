using System.Collections.Generic;
using TamoCRM.Domain.Branches;
using TamoCRM.Domain.Groups;
using TamoCRM.Domain.UserRole;

namespace TamoCRM.Web.Mvc.Areas.Admin.Models.Users
{
    public class UserModel
    {
        public UserInfo UserInfo { get; set; }

        // Role
        public PostedRole PostedRole { get; set; }
        public IEnumerable<RoleInfo> AllRole { get; set; }
        public IEnumerable<RoleInfo> SelectedRole { get; set; }

        // Branch
        public PostedBranch PostedBranch { get; set; }
        public IEnumerable<BranchInfo> AllBranch { get; set; }
        public IEnumerable<BranchInfo> SelectedBranch { get; set; }

        // Group Consultant
        public PostedGroup PostedGroupConsultant { get; set; }
        public IEnumerable<GroupInfo> AllGroupConsultants { get; set; }
        public IEnumerable<GroupInfo> SelectedGroupConsultants { get; set; }

        // Group Consultant Type
        public PostedGroupType PostedGroupConsultantType { get; set; }
        public IEnumerable<GroupTypeInfo> AllGroupConsultantTypes { get; set; }
        public IEnumerable<GroupTypeInfo> SelectedGroupConsultantTypes { get; set; }

        // Group Collaborator
        public PostedGroup PostedGroupCollaborator { get; set; }
        public IEnumerable<GroupInfo> AllGroupCollaborators { get; set; }
        public IEnumerable<GroupInfo> SelectedGroupCollaborators { get; set; }

        // Group Collaborator Type
        public PostedGroupType PostedGroupCollaboratorType { get; set; }
        public IEnumerable<GroupTypeInfo> AllGroupCollaboratorTypes { get; set; }
        public IEnumerable<GroupTypeInfo> SelectedGroupCollaboratorTypes { get; set; }
    }

    public class PostedRole
    {
        public int[] Id { get; set; }
    }
    public class PostedGroup
    {
        public int[] Id { get; set; }
    }
    public class PostedBranch
    {
        public int[] Id { get; set; }
    }
    public class PostedGroupType
    {
        public int[] Id { get; set; }
    }
}
