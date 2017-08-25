using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebOrpalPhotoPort.Code
{
    public class CommnonCollections
    {
        static public string[] Roles => new string[] { Properties.Resources.SimpleUser, Properties.Resources.Admin };
        static public string[] Statuses => new string[] { Properties.Resources.Active, Properties.Resources.Banned };

        static public IEnumerable<SelectListItem> GetCollection(string[] names, int selIndex)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            for (int index = 0; index < names.Length; index++)
            {
                items.Add(new SelectListItem { Text = names[index], Value = index.ToString(), Selected = (index == selIndex) });
            }

            return items;
        }
    }
}