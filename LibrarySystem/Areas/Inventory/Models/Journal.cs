using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Areas.Inventory.Models
{
    [Serializable]
    public class Journal: InventoryItemBase
    {
        public Journal(int id) : base(id) {
            JournalIssues = new List<Issue>();
        }
       

        private int numIssuesBorrowed = 0;
        public List<Issue> JournalIssues { get; set; }

        public int NumberIssuesBorrowed
        {
            get { return numIssuesBorrowed; }
        }
        internal void BorrowIssue()
        {
            numIssuesBorrowed++;
        }
        internal void ReturnIssue()
        {
            numIssuesBorrowed--;
        }
        internal IBorrowable getIssue()
        {
            Issue issue = null;
            try
            {
                issue = JournalIssues.First(s => s.IsAvailable() == true && s.IsBorrowed() == false);
            }
            catch (System.InvalidOperationException ex)
            {
                Exception sf = ex;
                
            }
            return issue;
        }
       


        public int GetId() { return base.ID; }
        public InventoryItemBase GetItem() { return this; }
        

        [Serializable]
        public class Issue : IBorrowable
        {
            #region Private Vars
            private bool isBorrowed;
            private bool isAvailable;
            private int id;
            private Journal journal;
            #endregion
            #region Constructors
            public Issue(int id, Journal journal) : this(id, journal, false) { }
            public Issue(int id, Journal journal, bool isAvailable)
            {
                this.isAvailable = isAvailable;
                this.isBorrowed = false;
                this.journal = journal;
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
                journal.BorrowIssue();
                this.isAvailable = false;
                this.isBorrowed = true;
            }
            public void Return()
            {
                this.isBorrowed = false;
                this.isAvailable = true;
                this.journal.ReturnIssue();

            }
       
            public InventoryItemBase GetItem() { return this.journal; }
            public int GetId() { return this.id; }
            public bool IsBorrowed() { return isBorrowed; }
            public bool IsAvailable() { return isAvailable; }
            #endregion

        }

    }
}