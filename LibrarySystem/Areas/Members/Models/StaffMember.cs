using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibrarySystem.Areas.Inventory.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace LibrarySystem.Areas.Members.Models
{
    [Serializable()]
    public class StaffMember : LibraryMember
    {
        public bool IsLibrarian { get; set; }
        internal StaffMember(string MemberName) : base(MemberName) 
        {
            IsLibrarian = false;
        }
        public const int MAX_BORROW = 10;  
        

       protected override void PickUpBorrowedItem(IBorrowable itemToBorrow)
        {
            if (borrowedItems.Count < MAX_BORROW)
                borrowedItems.Add((IBorrowable)itemToBorrow);
            else
            {
                throw new Exception("Borrow Limit Reached");
            }
        }
      
    }
}