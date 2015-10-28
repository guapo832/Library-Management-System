using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Areas.Members.Models;

namespace LibrarySystem.Areas.Members.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Members/Staff/

        public ActionResult Index()
        {
            Dictionary<String, List<LibraryMemberBase>> members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            List<LibraryMemberBase> staffmembers = members["Staff"];
          
            return View(staffmembers);
        }

        public ActionResult Details(int mid)
        {
            Dictionary<String, List<LibraryMemberBase>> members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            LibraryMemberBase member = members["Staff"].Single(s => s.ID == mid);
            return View(member);
        }

        public ActionResult ReturnItem(int memberid, int itemID)
        {
            Dictionary<String, List<LibraryMemberBase>> members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            LibraryMemberBase member = members["Staff"].Single(s => s.ID == memberid);
            IBorrowable itemToReturn = (IBorrowable)member.BorrowedItems.First(s=>s.GetItem().ID == itemID);
            member.Return(itemToReturn);
            return RedirectToAction("Details", new { mid = memberid });
        }

    }
}
