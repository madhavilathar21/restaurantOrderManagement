using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Bhukkhad.Models;

namespace Bhukkhad.Controllers
{
    public class EaterController : Controller
    {
        // GET: Eater
        public ActionResult EaterHome()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EaterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EaterLogin(FormCollection frm)
        {
            var myEmail = Convert.ToString(frm["Email"]);
            var myPasswd = Convert.ToString(frm["Passwd"]);
            TempData["myPasswd"] = myPasswd;
            BhukkadEntities FromTable = new BhukkadEntities();
            var RdFound = FromTable.BookingMasters.Where(cust => cust.Email == myEmail && cust.Password == myPasswd) //lambda expression
            .FirstOrDefault();

            if (RdFound != null)
            {
                ViewData["ErrMsg"] = "Login Successfully";
                Session["BookId"] = RdFound.BookId;
                Session["Name"] = RdFound.Name;
                return RedirectToAction("EaterUpdateProfile");
            }
            else
            {
                TempData["ErrMsg"] = "Invalid Email or Password";
                return View();
            }
        }


        public ActionResult EaterAboutUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EaterBookMe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EaterBookMe(BookingMaster bookingmaster)
        {
            BhukkadEntities bookingTable = new BhukkadEntities();
            bookingTable.BookingMasters.Add(bookingmaster);
            bookingTable.SaveChanges();
            return RedirectToAction("EaterLogin");
            
        }

        public JsonResult CheckEmail(string Email)
        {
            BhukkadEntities Entityfile = new BhukkadEntities();
            var chkEmail = Entityfile.BookingMasters.Where(q => q.Email == Email).Count();
            if(chkEmail!= 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EaterChangePassword()
        {
            return View();
        }

        public JsonResult CheckLoginJson(string Email, string Password, string NPassword)
        {
            BhukkadEntities FromTable = new BhukkadEntities();
            BookingMaster bm = new BookingMaster();
            bm = FromTable.BookingMasters.Where(cust => cust.Email == Email && cust.Password == Password).FirstOrDefault();

            if(bm != null)
            {
                bm.Password = Convert.ToString(NPassword);
                FromTable.Entry(bm).State = EntityState.Modified;
                FromTable.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Invalid Email-Id or Password", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EaterUpdateProfile()
        {
            if(Session["BookId"] == null)
            {
                return RedirectToAction("EaterLogin", "Eater");
            }
            var id = Convert.ToInt32(Session["BookId"]);
            var name = Convert.ToString(Session["Name"]);
            BhukkadEntities updateProfile = new BhukkadEntities();
            var BookingDetails = updateProfile.BookingMasters.Where(u => u.BookId == id).FirstOrDefault();
            return View(BookingDetails);
            

            return View();
        
        }

        [HttpPost]
        public ActionResult EaterUpdateProfile(BookingMaster bookingmaster)
        {
            BhukkadEntities updateProfile = new BhukkadEntities();
            updateProfile.Entry(bookingmaster).State = EntityState.Modified;
            bookingmaster.Password = TempData["myPasswd"].ToString();
            updateProfile.SaveChanges();
            return RedirectToAction("EaterShowBooking");
        }

        public ActionResult EaterShowBooking()
        {
            if(Session["Name"]!=null)
            {
                ViewData["Name"] = Convert.ToString(Session["Nmae"]);
            
            }
            BhukkadEntities SendtoPV = new BhukkadEntities();
            var bookingdetails = SendtoPV.BookingMasters.ToList();
            return View(bookingdetails);
        }

        public ActionResult EaterContactUs()
        {
            return View();
        }

        public ActionResult EaterHelp()
        {
            return View();
        }
    }
}