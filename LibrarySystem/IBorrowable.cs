using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public interface IBorrowable
    {
        int GetId();
        void Borrow();
        void Return();
        InventoryItemBase GetItem();
        bool IsBorrowed();
        bool IsAvailable();
    }
}
