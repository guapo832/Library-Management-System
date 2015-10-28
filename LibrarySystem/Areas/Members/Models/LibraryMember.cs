using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystem.Areas.Inventory.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace LibrarySystem.Areas.Members.Models
{
    [Serializable()]
    public class LibraryMember: LibraryMemberBase
    {
        internal LibraryMember(String membername):base(membername){}
        public const int MAX_BORROW = 5;

        protected override void PickUpBorrowedItem(IBorrowable itemToAdd)
        {
            if (!itemToAdd.GetType().Equals(typeof(LibrarySystem.Areas.Inventory.Models.Book.Copy))) throw new Exception("You must be staff to borrow this item");

            if (borrowedItems.Count < MAX_BORROW)
            {
                borrowedItems.Add(itemToAdd);
            }
            else
            {
                throw new Exception("Borrow Limit Reached");
            }
        }
        protected override void DropOffReturnedItem(int iborrowableID,  int inventoryID)
        {
            var itemtoremove = borrowedItems.First(s => s.GetId() == iborrowableID && s.GetItem().ID == inventoryID);
            if (itemtoremove == null) throw new Exception("This item doesn't appear to be checked out ");
            borrowedItems.Remove(itemtoremove);
                //IBorrowable itemToReturn = (IBorrowable) borrowedItems.first(s=>s.GetId() == id);
                //if(itemToReturn==null) throw new Exception("You do not have this item checked out");
                //borrowedItems.Remove(itemToReturn);
           
        }
    }
}