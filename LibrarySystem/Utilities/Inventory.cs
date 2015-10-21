using LibrarySystem.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Utilities
{
    internal class Inventory
    {
        internal enum InventoryTypes
        {
            Journals,
            Books
        }
        internal static List<InventoryItemBase> GetAllInventory(HttpSessionStateBase Session,InventoryTypes inventorytype)
        {
            Dictionary<String, List<InventoryItemBase>> Inventory = Session["Inventory"] as Dictionary<String, List<InventoryItemBase>>;
            List<InventoryItemBase> list = Inventory[inventorytype.ToString()];
            return list;
        }

        

        internal static int getNextID(List<InventoryItemBase> items)
        {
            if (items.Count == 0) return 1;
            InventoryItemBase itm = items.OrderBy(o => o.ID).Last();
            return itm.ID + 1;
        }

        internal static InventoryItemBase getInventoryItem(HttpSessionStateBase Session, int ID, InventoryTypes inventorytype)
        {
            var list = Utilities.Inventory.GetAllInventory(Session,inventorytype);
            var item = list.Single(s => s.ID == ID) as InventoryItemBase;
            return item;
        }

       
    }
}