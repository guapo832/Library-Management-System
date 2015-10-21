using LibrarySystem.Areas.Members.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Areas.Members.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Members/Member/

        public ActionResult Index()
        {
            Dictionary<String, List<LibraryMemberBase>> members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            List<LibraryMemberBase> libmembers = members["Member"];
            return View(libmembers);
        }


        public ActionResult Details(int mid)
        {
            Dictionary<String, List<LibraryMemberBase>> members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            LibraryMemberBase member = members["Member"].Single(s => s.ID == mid);
            return View(member);
        }

        public ActionResult ReturnItem(int memberid, int itemID)
        {
            Dictionary<String, List<LibraryMemberBase>> members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            LibraryMemberBase member = members["Member"].Single(s => s.ID == memberid);
            IBorrowable itemToReturn = (IBorrowable)member.BorrowedItems.Single(s => s.GetId() == itemID);
            member.Return(itemToReturn);
            return RedirectToAction("Details", new { mid = memberid });
        }
    }
}
