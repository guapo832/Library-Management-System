using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using LibrarySystem.Areas.Inventory.Models;
using LibrarySystem.Areas.Members.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LibrarySystem
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        private string inventoryFilepath = "";
        private string memberfilepath = "";
       

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
           

            



        }

        
        /// <summary>
        /// if no file exists we will build a new data source and use that.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, List<InventoryItemBase>> initializeInventoryData()
        {
            List<InventoryItemBase> journals = new List<InventoryItemBase>();
            List<InventoryItemBase> books = new List<InventoryItemBase>();

            books.Add(new Book(1) { Author = "J.K Rawling", Title = "Test Book 1" });
            List<LibrarySystem.Areas.Inventory.Models.Book.Copy> bookCopies = ((Book)books[0]).BookCopies;
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(1, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(2, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(3, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(4, (Book)books[0], true));
            bookCopies.Add(new LibrarySystem.Areas.Inventory.Models.Book.Copy(5, (Book)books[0], true));
            ((Book)books[0]).BookCopies = bookCopies;

            books.Add(new Book(2) { Author = "J.K Rawling", Title = "My Mind is Numb!" });
            books.Add(new Book(3) { Author = "J.K Rawling", Title = "Test Book 1" });
            books.Add(new Book(4) { Author = "J.K Rawling", Title = "My Mind is Numb!" });

            journals.Add(new Journal(5) { Author = "Thomas Blevins", Title = "How to Drive a Car" });
            journals.Add(new Journal(6) { Author = "Frank Yerby", Title = "Autumn Flavors" });
            journals.Add(new Journal(7) { Author = "Author1", Title = "Some Journal 1" });
            journals.Add(new Journal(8) { Author = "Cat Stevens", Title = "Psycho In America" });
            journals.Add(new Journal(9) { Author = "John Lennon", Title = "Its all about cars" });
            journals.Add(new Journal(10) { Author = "Humphre Nelson", Title = "Real People" });
            Dictionary<String, List<InventoryItemBase>> inventory = new Dictionary<String, List<InventoryItemBase>>();
            inventory.Add("Journals", journals);
            inventory.Add("Books", books);
            return inventory;
        }

        /// <summary>
        /// if no data file exists we'll build one in memory
        /// </summary>
        /// <returns></returns>
        private Dictionary<String, List<LibraryMemberBase>> initializeMemberData()
        {
            Dictionary<String, List<LibraryMemberBase>> LibraryMembers = new Dictionary<string, List<LibraryMemberBase>>(); LibraryMembers = new Dictionary<string, List<LibraryMemberBase>>();
            LibraryMembers.Add("Staff", new List<LibraryMemberBase>());
            LibraryMembers["Staff"].Add(new StaffMember("Uday Chakraborty") { ID = 1, IsLibrarian = true });
            LibraryMembers["Staff"].Add(new StaffMember("Don Moxle") { ID = 2 });
            LibraryMembers["Staff"].Add(new StaffMember("John Doe") { ID = 3 });


            LibraryMembers.Add("Member", new List<LibraryMemberBase>());
            LibraryMembers["Member"].Add(new LibraryMember("Gary Yerby") { ID = 7 });
            LibraryMembers["Member"].Add(new LibraryMember("Jayaprakash Linganmeneni") { ID = 8 });
            LibraryMembers["Member"].Add(new LibraryMember("Krishna Phanse") { ID = 9 });
            LibraryMembers["Member"].Add(new LibraryMember("Daniel Vomund") { ID = 10 });
            LibraryMembers["Member"].Add(new LibraryMember("Robert Brockschmidt") { ID = 11 });
            LibraryMembers["Member"].Add(new LibraryMember("Suzan Schmidt") { ID = 12 });
            return LibraryMembers;
        }

        /// <summary>
        /// get data from file
        /// </summary>
        /// <returns></returns>
        private Dictionary<String , List<InventoryItemBase>> getInventoryData(){
            // Dictionary<String, List<InventoryItemBase>> inventory =  new Dictionary<String,List<InventoryItemBase>>();

            Dictionary<string, List<InventoryItemBase>> objects = null;
            using (FileStream fs = new FileStream(inventoryFilepath, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                objects = (Dictionary<string, List<InventoryItemBase>>)bin.Deserialize(fs);
                fs.Close();
            }

            return objects;
        }

        /// <summary>
        /// gets data from file.
        /// </summary>
        /// <returns></returns>
        private Dictionary<String, List<LibraryMemberBase>>  getMemberData(){
            // Dictionary<String, List<InventoryItemBase>> inventory =  new Dictionary<String,List<InventoryItemBase>>();

            Dictionary<string, List<LibraryMemberBase>> objects = null;
            using (FileStream fs = new FileStream(memberfilepath, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                objects = (Dictionary<string, List<LibraryMemberBase>>)bin.Deserialize(fs);
                fs.Close();
            }

            return objects;
        }

        /// <summary>
        /// initialize the data to be used on the site.
        /// </summary>
        protected void Session_Start()
        {
            inventoryFilepath= Server.MapPath("~/App_Data/inventory.txt");
            memberfilepath = Server.MapPath("~/App_Data/members.txt");
            Dictionary<String, List<InventoryItemBase>> inventory =  new Dictionary<String,List<InventoryItemBase>>();
            Dictionary<String, List<LibraryMemberBase>> LibraryMembers = new Dictionary<string, List<LibraryMemberBase>>();
            inventory = (!File.Exists(inventoryFilepath)) ? (Dictionary<string, List<InventoryItemBase>>)initializeInventoryData() : (Dictionary<string, List<InventoryItemBase>>)getInventoryData();
            LibraryMembers = (!File.Exists(memberfilepath)) ? (Dictionary<string, List<LibraryMemberBase>>)initializeMemberData() : (Dictionary<string, List<LibraryMemberBase>>)getMemberData();  
            
           HttpContext.Current.Session.Add("Inventory", inventory);
            HttpContext.Current.Session.Add("LibraryMembers",LibraryMembers);
           

        }

        
      

    }
}