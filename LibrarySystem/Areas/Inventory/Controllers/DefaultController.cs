using LibrarySystem.Areas.Members.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Areas.Inventory.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Inventory/Home/

        public ActionResult Staff(int mid)
        {
            Dictionary<String, List<InventoryItemBase>> Inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
            Dictionary<String, List<LibraryMemberBase>> Members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            StaffMember member = Members["Staff"].Single(s => s.ID == mid) as StaffMember;
            if (member.BorrowedItems.Count >= StaffMember.MAX_BORROW)
                ViewData["ShowBorrow"] = false;
            else
                ViewData["ShowBorrow"] = true;
            ViewData["MemberID"] = mid;
            ViewData["IsLibrarian"] = member.IsLibrarian;
            return View(Inventory);
        }

        public ActionResult Member(int mid)
        {
            Dictionary<String, List<InventoryItemBase>> Inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
            Dictionary<String, List<LibraryMemberBase>> Members = Session["LibraryMembers"] as Dictionary<String, List<LibraryMemberBase>>;
            LibraryMember member = Members["Member"].Single(s => s.ID == mid) as LibraryMember;
            if (member.BorrowedItems.Count >= LibraryMember.MAX_BORROW)
                ViewData["ShowBorrow"] = false;
            else
                ViewData["ShowBorrow"] = true;
            ViewData["MemberID"] = mid;
            return View(Inventory);
        }

    }
}
