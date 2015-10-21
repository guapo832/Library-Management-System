using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using LibrarySystem.Areas.Inventory.Models;
using LibrarySystem.Areas.Members.Models;

namespace LibrarySystem
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
           

            



        }

        protected void Session_Start()
        {
            Dictionary<String,List<InventoryItemBase>> inventory = new Dictionary<String,List<InventoryItemBase>>();
            List<InventoryItemBase> journals = new List<InventoryItemBase>();
            List<InventoryItemBase> books = new List<InventoryItemBase>();
            //List<LibraryMemberBase> libmembers = new List<LibraryMemberBase>();
         
            books.Add(new Book(1) { Author = "J.K Rawling", Title = "Test Book 1" });
            List<LibrarySystem.Areas.Inventory.Models.Book.Copy> bookCopies = ((Book)books[0]).BookCopies;
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(1,(Book)books[0],true)) ;
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(2, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(3, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(4, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(5, (Book)books[0], true));
           ((Book)books[0]).BookCopies = bookCopies;

            books.Add(new Book(2) { Author = "J.K Rawling", Title = "My Mind is Numb!" });
            //bookCopies = new List<LibrarySystem.Areas.Inventory.Models.Book.Copy>();
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(6, (Book)books[1], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(7, (Book)books[1], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(8, (Book)books[1], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(9, (Book)books[1], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(10, (Book)books[1], true));
           // ((Book)books[1]).BookCopies = bookCopies;

            books.Add(new Book(3) { Author = "J.K Rawling", Title = "Test Book 1" });
            //bookCopies = new List<LibrarySystem.Areas.Inventory.Models.Book.Copy>();
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(11, (Book)books[2], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(12, (Book)books[2], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(13, (Book)books[2], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(14, (Book)books[2], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(15, (Book)books[2], true));
            //((Book)books[2]).BookCopies = bookCopies;

            books.Add(new Book(4) { Author = "J.K Rawling", Title = "My Mind is Numb!" });
            //bookCopies = new List<LibrarySystem.Areas.Inventory.Models.Book.Copy>();
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(16, (Book)books[3], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(17, (Book)books[3], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(18, (Book)books[3], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(19, (Book)books[3], true));
            //bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(20, (Book)books[3], true));
            //((Book)books[3]).BookCopies = bookCopies;
            
            journals.Add(new Journal(1){ Author="Thomas Blevins", Title="How to Drive a Car"});
            journals.Add(new Journal(2){ Author="Frank Yerby", Title="Autumn Flavors"});
            journals.Add(new Journal(3) { Author = "Author1", Title = "Some Journal 1" });
            journals.Add(new Journal(4) { Author = "Cat Stevens", Title = "Psycho In America" });
            journals.Add(new Journal(5) { Author = "John Lennon", Title = "Its all about cars" });
            journals.Add(new Journal(6) { Author = "Humphre Nelson", Title = "Real People" });


            inventory.Add("Journals", journals);
            inventory.Add("Books", books);

            Dictionary<String, List<LibraryMemberBase>> LibraryMembers = new Dictionary<string, List<LibraryMemberBase>>();
            LibraryMembers.Add("Staff", new List<LibraryMemberBase>());
            LibraryMembers["Staff"].Add(new StaffMember("Uday Chakraborty") { ID = 1, IsLibrarian=true});
            LibraryMembers["Staff"].Add(new StaffMember("Don Moxle") { ID = 2 });
            LibraryMembers["Staff"].Add(new StaffMember("John Doe") { ID = 3 });
            
            
            LibraryMembers.Add("Member", new List<LibraryMemberBase>());
            LibraryMembers["Member"].Add(new LibraryMember("Gary Yerby") { ID = 7 });
            LibraryMembers["Member"].Add(new LibraryMember("Jayaprakash Linganmeneni") { ID = 8 });
            LibraryMembers["Member"].Add(new LibraryMember("Krishna Phanse") { ID = 9 });
            LibraryMembers["Member"].Add(new LibraryMember("Daniel Vomund") { ID = 10 });
            LibraryMembers["Member"].Add(new LibraryMember("Robert Brockschmidt") { ID = 11 });
            LibraryMembers["Member"].Add(new LibraryMember("Suzan Schmidt") { ID = 12 });




            HttpContext.Current.Session.Add("Inventory", inventory);
            HttpContext.Current.Session.Add("LibraryMembers",LibraryMembers);
           

        }
    }
}