using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibrarySystem
{
    public abstract class InventoryItemBase
    {
        public int ID {get; set;}
         public InventoryItemBase(int id)
        {
            this.ID = id;
        }
        [Display(Name = "Title")]
        public String Title { get; set; }
        [Display(Name = "Author")]
        public String Author { get; set; }
       // internal int GetId() { return id; }

    }
}