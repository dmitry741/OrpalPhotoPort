﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebOrpalPhotoPort.Code
{
    public class CommnonCollections
    {
        static public IEnumerable<SelectListItem> CollectionStatuses => GetCollectionStatuses(true, false);

        static public IEnumerable<SelectListItem> GetCollectionStatuses(bool bSel0, bool bSel1)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Активный", Value = "0", Selected = bSel0 });
            items.Add(new SelectListItem { Text = "Заблокирован", Value = "1", Selected = bSel1 });

            return items;
        }

        static public IEnumerable<SelectListItem> CollectionRoles => GetCollectionRoles(true, false);

        static public IEnumerable<SelectListItem> GetCollectionRoles(bool bSel0, bool bSel1)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Пользователь", Value = "0", Selected = bSel0 });
            items.Add(new SelectListItem { Text = "Админ", Value = "1", Selected = bSel1 });

            return items;
        }
    }
}