using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace GodsRevenge
{
    public static class Extensions
    {
        public static string UpperFirstLetter(this string message)
        {
            string returnvalue = char.ToUpper(message.ElementAt(0)) + message.Substring(1);
            return message;
        }
        public static void RemoveItemFromInventory(this Item item)
        {
            item.stack = 0;
            item.name = "";
            item.netID = 0;
        }
    }
}
