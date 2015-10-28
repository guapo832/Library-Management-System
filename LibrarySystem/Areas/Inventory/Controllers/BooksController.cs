using LibrarySystem.Areas.Inventory.Models;
using LibrarySystem.Areas.Members.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Areas.Inventory.Controllers
{
    public class BooksController : Controller
    {
        //
        // GET: /Books/

        public ActionResult Index()
        {
            var booklist = Session["bookDB"] as List<Book>;
            return View(booklist);
        }

        //
        // GET: /Books/Details/5

        public ActionResult Details(int id)
        {

            return View();
        }
        public ActionResult Create(int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");

            //int ID = getNextJournalID();
            //Journal journal = new Journal(ID) { Author = coll["Author"], Title = coll["Author"] };
            return View();

        }
        [HttpPost]
        public ActionResult Create(int memberID, FormCollection coll)
        {
            if (ModelState.IsValid)
            {
                var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
                if (!member.IsLibrarian) throw new Exception("You do not have permissions to this page");
                var books = Utilities.Inventory.GetAllInventory(Session, Utilities.Inventory.InventoryTypes.Books);
                var book = new Book(Utilities.Inventory.getNextID(books)) { Author = coll["Author"], Title = coll["Title"], BookCopies = new List<LibrarySystem.Areas.Inventory.Models.Book.Copy>() };

                books.Add(book);
                Dictionary<String, List<InventoryItemBase>> inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
                inventory[Utilities.Inventory.InventoryTypes.Books.ToString()] = books;
                Session["Inventory"] = inventory;
                return RedirectToAction(Utilities.Members.MemberType.Staff.ToString(), "Default", new { mid = memberID });
            }

            return View(memberID);
        }
        public ActionResult Update(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session,memberID,Utilities.Members.MemberType.Staff);
            var book = (Book)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Books);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            return View(book);
        }
        [HttpPost]
        public ActionResult Update(int memberID, FormCollection coll)
        {
            var member = Utilities.Members.getMember(Session,memberID,Utilities.Members.MemberType.Staff);
            if (!member.IsLibrarian) throw new Exception("You do not have permissions to this page");
            var books = Utilities.Inventory.GetAllInventory(Session, Utilities.Inventory.InventoryTypes.Books);
            var book = (Book)Utilities.Inventory.getInventoryItem(Session, int.Parse(coll["id"]), Utilities.Inventory.InventoryTypes.Books);
            book.Author = coll["Author"];
            book.Title = coll["Title"];

            Dictionary<String, List<InventoryItemBase>> inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
            inventory["Books"] = books;
            Session["Inventory"] = inventory;
            return RedirectToAction("Staff", "Default", new { mid = memberID });
        }
        public ActionResult Delete(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var book = (Book)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Books);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            var books = Utilities.Inventory.GetAllInventory(Session, Utilities.Inventory.InventoryTypes.Books);
            books.Remove(book);
            //int ID = getNextJournalID();
            //Journal journal = new Journal(ID) { Author = coll["Author"], Title = coll["Author"] };
            return RedirectToAction("Staff", "Default", new { mid = memberID });

        }
        public ActionResult CreateCopy(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var book = (Book)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Books);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            int nextid = 1;
            if(book.BookCopies.Count>0)
                nextid = book.BookCopies.OrderBy(o => o.GetId()).Last().GetId() + 1;
            var copy = new LibrarySystem.Areas.Inventory.Models.Book.Copy(nextid, book,true);
            book.BookCopies.Add(copy);
            return RedirectToAction("Staff", "Default", new { mid = memberID });

        }
        public ActionResult DeleteCopy(int id, int memberID)
        {
            var member = Utilities.Members.getMember(Session, memberID, Utilities.Members.MemberType.Staff);
            var book = (Book)Utilities.Inventory.getInventoryItem(Session, id, Utilities.Inventory.InventoryTypes.Books);
            if (!member.IsLibrarian) throw new Exception("You do not have access to this page");
            var copy = book.BookCopies.First(s => s.IsBorrowed() == false);
            book.BookCopies.Remove(copy);
            //int ID = getNextJournalID();
            //Journal journal = new Journal(ID) { Author = coll["Author"], Title = coll["Author"] };
            return RedirectToAction("Staff", "Default", new { mid = memberID });

        }
        //
        // GET: /Books/BorrowCopy/5
        public ActionResult BorrowCopy(int BookID, int MemberID)
        {
            var book = (Book)Utilities.Inventory.getInventoryItem(Session, BookID, Utilities.Inventory.InventoryTypes.Books);
            var memberlist = Utilities.Members.GetAllMembers(Session);
            LibraryMemberBase member = (LibraryMemberBase) memberlist.Single(s => s.ID == MemberID);
            if (member != null)
                if (book != null)
                {
                    IBorrowable copy = book.getCopy();
                    member.Borrow(copy);
                }
            //member/staff/details?mid=#
            try
            {
                var staffmember =(StaffMember) Utilities.Members.getMember(Session, MemberID, Utilities.Members.MemberType.Staff);
                return RedirectToAction("Details", "Staff", new { area = "Members", mid = MemberID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", "Member", new { area = "Members", mid = MemberID });
            }
            
           
        }

         

    }
}
