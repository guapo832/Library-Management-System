using LibrarySystem.Areas.Members.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Utilities
{
    internal class Members
    {
        internal enum MemberType
        {
            Staff,
            Member
        }
        internal static StaffMember getMember(HttpSessionStateBase Session ,int memberID,MemberType membertype)
        {
            Dictionary<String, List<LibraryMemberBase>> librarymembers = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            List<LibraryMemberBase> staffmemberlist = librarymembers[membertype.ToString()];
            StaffMember member = (StaffMember)staffmemberlist.Single(s => s.ID == memberID);
            return member;
        }

        internal static List<LibraryMemberBase> GetAllMembers(HttpSessionStateBase Session)
        {
            Dictionary<String, List<LibraryMemberBase>> librarymembers = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            List<LibraryMemberBase> memberlist = new List<LibraryMemberBase>();
            memberlist.AddRange(librarymembers[MemberType.Staff.ToString()]);
            memberlist.AddRange(librarymembers[MemberType.Member.ToString()]);
            return memberlist;
        }
    }
}