using Inventory_Management_Web_Application.Models;
using Inventory_Management_Web_Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class _SecurityFilter : ActionFilterAttribute
    {
        InventoryContext db = new InventoryContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            if (HttpContext.Current.Session["Kullanici"] == null)
            {
                if ((controllerName != "Kullanici" || controllerName == "Kullanici") && actionName != "Login")
                {
                    filterContext.Result = new RedirectResult("/Kullanici/Login");
                    return;
                }
            }
            else
            {
                //Personel p = (Personel)HttpContext.Current.Session["Kullanici"];
                //MenuControl k = new MenuControl();
                //k.menuler = db.Menu.ToList();
                //k.roller = db.MenuRol.Where(x => x.RolID == p.RolID).ToList();
                //bool yetki = false;
                //foreach (Menu item in k.menuler)
                //{
                //    if (actionName == item.Action)
                //    {
                //        foreach (MenuRol rol in k.roller)
                //        {
                //            if (item.ID == rol.MenuID)
                //            {
                //                yetki = true;
                //            }
                //        }
                //    }
                //}
                //if (yetki !=true)
                //{
                //    filterContext.Result = new RedirectResult("/Admin/YetkiBulunamadi");
                //    return;
                //}
                //else
                //{
                //    base.OnActionExecuting(filterContext);
                //}

            }
            base.OnActionExecuting(filterContext);
        }
    }
}