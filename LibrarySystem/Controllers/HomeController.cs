using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Areas.Members.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
            return View();
        }
      
        public ActionResult LogOut()
        {
            var inventoryFilepath = Server.MapPath("~/App_Data/inventory.txt");
            var memberfilepath = Server.MapPath("~/App_Data/members.txt");
            Dictionary<string, List<InventoryItemBase>> inventory = Session["Inventory"] as Dictionary<string, List<InventoryItemBase>>;
            Dictionary<string, List<LibraryMemberBase>> LibraryMembers = Session["LibraryMembers"] as Dictionary<string, List<LibraryMemberBase>>;
            Utilities.Serializer.serialize(inventoryFilepath, inventory);
            Utilities.Serializer.serialize(memberfilepath, LibraryMembers);
           return RedirectToAction("Index");
        }
        
    }
}
