using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Areas.Inventory.Models
{
    [Serializable]
    public class Book : InventoryItemBase
    {
       public Book(int id):base(id)
       {
           BookCopies = new List<Copy>();
       }

        private int numCopiesBorrowed = 0;
        public List<Copy> BookCopies { get; set; }
        public int NumberCopiesBorrowed
        {
            get { return numCopiesBorrowed; }
        }
        internal void BorrowCopy()
        {
            numCopiesBorrowed++;
        }
        internal void ReturnCopy()
        {
            numCopiesBorrowed--;
        }
        internal IBorrowable getCopy()
        {
            Copy copy = null;
            try
            {
                copy = BookCopies.First(s => s.IsAvailable() ==true && s.IsBorrowed() == false);
            }
            catch (System.InvalidOperationException ex)
            {
                Exception sf = ex;      
               //no copies available
            }
            return copy;
        }

        public int GetId(){return base.ID;}

        [Serializable]
        public class Copy : IBorrowable
        {
            #region Private Vars
            private bool isBorrowed;
            private bool isAvailable;
            private int id;
            private Book book;
            #endregion
            #region Constructors
            public Copy(int id, Book book) : this(id, book, false) { }
            public Copy(int id, Book book, bool isAvailable)
            {
                this.isAvailable = isAvailable;
                this.isBorrowed = false;
                this.book = book;
                this.id = id;
            }


            #endregion
            #region internal


            #endregion
            #region Interface members
            public void Borrow()
            {
                if (isBorrowed)
                    throw new Exception("This copy has already been borrowed.");
                book.BorrowCopy();
                this.isAvailable = false;
                this.isBorrowed = true;
            }
            public void Return()
            {
                this.isBorrowed = false;
                this.isAvailable = true;
                this.book.ReturnCopy();
                //testing

            }
            public void Browse()
            {
                if (isBorrowed)
                    throw new Exception("This copy has already been borrowed.");

                this.isAvailable = false;


            }
            public InventoryItemBase GetItem() { return this.book; }
            public int GetId() { return this.id; }
            public bool IsBorrowed() { return isBorrowed; }
            public bool IsAvailable() { return isAvailable; }
            #endregion

        }
    }

    
}