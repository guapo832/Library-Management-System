using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;

namespace LibrarySystem
{
    /// <summary>
    /// anything that has a borrowable items may inherit from this class.
    /// </summary>
    [Serializable()]
    public abstract class InventoryItemBase
    {
        public int ID {get; set;}
         public InventoryItemBase(int id)
        {
            this.ID = id;
        }
        [Display(Name = "Title")]
        [Required(ErrorMessage="You must provide a title")]
        public String Title { get; set; }
        [Display(Name = "Author")]
        [Required(ErrorMessage = "You must provide a Author")]
        public String Author { get; set; }
       // internal int GetId() { return id; }

    }
}