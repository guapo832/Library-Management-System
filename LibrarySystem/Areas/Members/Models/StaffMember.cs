using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystem.Areas.Inventory.Models;

namespace LibrarySystem.Areas.Members.Models
{
    public class StaffMember : LibraryMember
    {
        public bool IsLibrarian { get; set; }
        internal StaffMember(string MemberName) : base(MemberName) 
        {
            IsLibrarian = false;
        }
        internal const int MAX_BORROW = 10;  
        

       protected override void PickUpBorrowedItem(IBorrowable itemToBorrow)
        {
            if (borrowedItems.Count < MAX_BORROW)
                borrowedItems.Add((IBorrowable)itemToBorrow);
            else
            {
                throw new Exception("Borrow Limit Reached");
            }
        }
       protected override void DropOffReturnedItem(int id)
       {
           
                //borrowedItems.Remove(itemToRemove.GetId());
           var itemtoremove = borrowedItems.Single(s=>s.GetId() == id );
           if(itemtoremove==null) throw new Exception("This item doesn't appear to be checked out ");
           borrowedItems.Remove(itemtoremove);
       }
    }
}