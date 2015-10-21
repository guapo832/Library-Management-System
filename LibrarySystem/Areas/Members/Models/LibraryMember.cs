using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystem.Areas.Inventory.Models;

namespace LibrarySystem.Areas.Members.Models
{
    public class LibraryMember: LibraryMemberBase
    {
        internal LibraryMember(String membername):base(membername){}
        internal const int MAX_BORROW = 5;

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
        protected override void DropOffReturnedItem(int id)
        {
                IBorrowable itemToReturn = (IBorrowable) borrowedItems.Single(s=>s.GetId() == id);
                if(itemToReturn==null) throw new Exception("You do not have this item checked out");
                borrowedItems.Remove(itemToReturn);
           
        }
    }
}