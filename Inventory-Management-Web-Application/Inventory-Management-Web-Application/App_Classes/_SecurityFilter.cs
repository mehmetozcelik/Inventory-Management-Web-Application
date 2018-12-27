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
            string metot = filterContext.HttpContext.Request.RequestType;
            if (HttpContext.Current.Session["Kullanici"] == null)
            {
                if ((controllerName != "Kullanici" || controllerName == "Kullanici") && actionName != "Login")
                {
                    filterContext.Result = new RedirectResult("/Kullanici/Login");
                }
            }
            else
            {
                Personel p = (Personel)HttpContext.Current.Session["Kullanici"];
                if (controllerName=="Admin" && actionName=="Index" )
                {                    
                    return;
                }
                else
                {
                    if (actionName == "MenuGetir")
                    {
                        return;
                    }

                    if (metot == "POST")
                    {
                        //IslemErisim currentIslem = db.IslemErisim.Where(x => x.Action == actionName && x.Controller == controllerName).SingleOrDefault();
                        //if (currentIslem != null)
                        //{
                        //    ErisimRol rol = db.ErisimRol.Where(x => x.RolID == p.RolID && x.ErisimID == currentIslem.ID).SingleOrDefault();
                        //    if (rol == null)
                        //    {
                        //        if (actionName == "YetkiBulunamadi")
                        //        {
                        //            return;
                        //        }
                        //        filterContext.Result = new RedirectResult("/Admin/YetkiBulunamadi");
                        //    }
                        //    else
                        //    {
                        //        if (controllerName != currentIslem.Controller && actionName != currentIslem.Action)
                        //        {

                        //            filterContext.Result = new RedirectResult("/" + currentIslem.Controller + "/" + currentIslem.Action);
                        //        }
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                        //    return;
                        //}
                    }
                    else
                    {

                        Menu currentMenu = db.Menu.Where(x => x.Action == actionName && x.Controller == controllerName).SingleOrDefault();
                        if (currentMenu != null)
                        {
                            MenuRol rol = db.MenuRol.Where(x => x.RolID == p.RolID && x.MenuID == currentMenu.ID).SingleOrDefault();
                            if (rol == null)
                            {
                                if (actionName == "YetkiBulunamadi")
                                {
                                    return;
                                }
                                filterContext.Result = new RedirectResult("/Admin/YetkiBulunamadi");
                            }
                            else
                            {
                                if (controllerName != currentMenu.Controller && actionName != currentMenu.Action)
                                {

                                    filterContext.Result = new RedirectResult("/" + currentMenu.Controller + "/" + currentMenu.Action);
                                }
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                   
                }                            

            }
            base.OnActionExecuting(filterContext);
        }
    }
}