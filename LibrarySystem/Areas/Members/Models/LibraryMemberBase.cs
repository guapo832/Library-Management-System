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
        /// <summary>
        /// in order to borrow a borrowable item this method must exist.
        /// </summary>
        /// <param name="itemToBorrow"></param>
        internal void Borrow(IBorrowable itemToBorrow)
        {
                itemToBorrow.Borrow();
                PickUpBorrowedItem(itemToBorrow);
        }

        /// <summary>
        /// in order to return a borrowable item this method must be implemented.
        /// </summary>
        /// <param name="itemToReturn"></param>
        internal void Return(IBorrowable itemToReturn)
        {
            itemToReturn.Return();
            DropOffReturnedItem(itemToReturn.GetId(), itemToReturn.GetItem().ID);
        }

        /// <summary>
        /// the copies or issues that are borrowed.
        /// </summary>
        public List<IBorrowable> BorrowedItems
        {
            get { return borrowedItems; }
        }
        public int ID { get; set; }
        public String MemberName { get; set; }
        
       /// <summary>
       /// inheriters of this class must implement the following methods.
       /// </summary>
       /// <param name="itemToBorrow"></param>
        abstract protected void PickUpBorrowedItem(IBorrowable itemToBorrow);
        abstract protected void DropOffReturnedItem(int iborrowableID,int inventoryID);
        
    }
}