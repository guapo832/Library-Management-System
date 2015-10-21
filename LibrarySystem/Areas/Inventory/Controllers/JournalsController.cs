using LibrarySystem.Areas.Inventory.Models;
using LibrarySystem.Areas.Members.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Areas.Inventory.Controllers
{
    public class JournalsController : Controller
    {
        public ActionResult Details(int id)
        {

            return View();
        }
        public ActionResult Create(int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            return View();

        }
        [HttpPost]
        public ActionResult Create(int memberID, FormCollection coll)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            if (!member.IsLibrarian) throw new Exception("You do not have permissions to this page");
            var journals = Utilities.Inventory.GetAllInventory(Session, Utilities.Inventory.InventoryTypes.Journals);
            var journal = new Journal(Utilities.Inventory.getNextID(journals)) { Author = coll["Author"], Title = coll["Title"], JournalIssues = new List<LibrarySystem.Areas.Inventory.Models.Journal.Issue>() };

            journals.Add(journal);
            Dictionary<String, List<InventoryItemBase>> inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
            inventory[Utilities.Inventory.InventoryTypes.Journals.ToString()] = journals;
            Session["Inventory"] = inventory;
            return RedirectToAction(Utilities.Members.MemberType.Staff.ToString(), "Default", new { mid = memberID });
        }
        public ActionResult Update(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var journal = (Journal)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Journals);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            return View(journal);
        }
        [HttpPost]
        public ActionResult Update(int memberID, FormCollection coll)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            if (!member.IsLibrarian) throw new Exception("You do not have permissions to this page");
            var journals = Utilities.Inventory.GetAllInventory(Session, Utilities.Inventory.InventoryTypes.Journals);
            var journal = (Journal)Utilities.Inventory.getInventoryItem(Session, int.Parse(coll["id"]), Utilities.Inventory.InventoryTypes.Journals);
            journal.Author = coll["Author"];
            journal.Title = coll["Title"];

            Dictionary<String, List<InventoryItemBase>> inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
            inventory["Journals"] = journals;
            Session["Inventory"] = inventory;
            return RedirectToAction("Staff", "Default", new { mid = memberID });
        }
        public ActionResult Delete(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var journal = (Journal)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Journals);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            var journals = Utilities.Inventory.GetAllInventory(Session, Utilities.Inventory.InventoryTypes.Journals);
            journals.Remove(journal);
            return RedirectToAction("Staff", "Default", new { mid = memberID });

        }
        public ActionResult CreateIssue(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var journal = (Journal)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Journals);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            int nextid = 1;
            if(journal.JournalIssues.Count>0)
                nextid = journal.JournalIssues.OrderBy(o => o.GetId()).Last().GetId() + 1;
            var issue = new LibrarySystem.Areas.Inventory.Models.Journal.Issue(nextid, journal,true);
            journal.JournalIssues.Add(issue);
            return RedirectToAction("Staff", "Default", new { mid = memberID });

        }
        public ActionResult DeleteIssue(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var journal = (Journal)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Journals);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            var issue = journal.JournalIssues.First(s => s.IsBorrowed() == false);
            journal.JournalIssues.Remove(issue);
            
            return RedirectToAction("Staff", "Default", new { mid = memberID });

        }
        //
        // GET: /Books/BorrowIssue/5
        public ActionResult BorrowIssue(int JournalID, int MemberID)
        {
            var journal = (Journal)Utilities.Inventory.getInventoryItem(Session, JournalID, Utilities.Inventory.InventoryTypes.Journals);
            var memberlist = Utilities.Members.GetAllMembers(Session);
            LibraryMemberBase member = (LibraryMemberBase)memberlist.Single(s => s.ID == MemberID);
            if (member != null)
                if (journal != null)
                {
                    IBorrowable issue = journal.getIssue();
                    member.Borrow(issue);
                }
            //member/staff/details?mid=#
            try
            {
                var staffmember = (StaffMember)Utilities.Members.getMember(Session, MemberID, Utilities.Members.MemberType.Staff);
                return RedirectToAction("Details", "Staff", new { area = "Members", mid = MemberID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", "Member", new { area = "Members", mid = MemberID });
            }


        }

        

    }
}
