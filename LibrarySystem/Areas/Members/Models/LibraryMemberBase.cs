using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;

namespace LibrarySystem.Areas.Members.Models
{
    [Serializable()]
    public abstract class LibraryMemberBase
    {
        #region Constructors
        internal LibraryMemberBase(string membername) { 
            this.MemberName = membername;
            this.borrowedItems = new List<IBorrowable>();
        }
        #endregion
        /// <summary>
        /// Used to enumerate currently checked out items for this member
        /// </summary>
        internal List<IBorrowable> borrowedItems;
        internal void Borrow(IBorrowable itemToBorrow)
        {
                itemToBorrow.Borrow();
                PickUpBorrowedItem(itemToBorrow);
        }
        internal void Return(IBorrowable itemToReturn)
        {
            itemToReturn.Return();
            DropOffReturnedItem(itemToReturn.GetId(), itemToReturn.GetItem().ID);
        }
        public List<IBorrowable> BorrowedItems
        {
            get { return borrowedItems; }
        }
        public int ID { get; set; }
        public String MemberName { get; set; }
        
       
        abstract protected void PickUpBorrowedItem(IBorrowable itemToBorrow);
        abstract protected void DropOffReturnedItem(int iborrowableID,int inventoryID);
        
    }
}