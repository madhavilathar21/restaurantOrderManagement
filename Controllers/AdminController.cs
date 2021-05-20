using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bhukkhad.Models;
using System.Data.Entity;

namespace Bhukkhad.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminHome()
        {
            BhukkadEntities SendtoPV = new BhukkadEntities();
            var bookingdetails = SendtoPV.BookingMasters.ToList();
            return View(bookingdetails);
        }

        public ActionResult AdminUpdateBooking()
        {
            BhukkadEntities SendToPV = new BhukkadEntities();
            var bookingdetails = SendToPV.BookingMasters.ToList();
            return View(bookingdetails);
        }

        [HttpGet]
        public ActionResult AdminEditBokking(int BookId)
        {
            BhukkadEntities EditPV = new BhukkadEntities();
            var BDetails = EditPV.BookingMasters.Where(q => q.BookId == BookId).FirstOrDefault();
            TempData["myPasswd"] = BDetails.Password.ToString();
            ViewData["myId"]=BDetails.BookId.ToString();
            return View(BDetails);
        }


        [HttpPost]
        public ActionResult AdminEditBooking(BookingMaster bookingmaster)
        {
            BhukkadEntities editbooking = new BhukkadEntities();
            editbooking.Entry(bookingmaster).State = EntityState.Modified;
            bookingmaster.Password = TempData["myPasswd"].ToString();
            editbooking.SaveChanges();
            return RedirectToAction("AdminUpdateBooking");
        }

        public JsonResult DeleteItem(int BookId)
        {
            try
            {
                BhukkadEntities DeleteTable = new BhukkadEntities();
                var BookingDetails = DeleteTable.BookingMasters.Where(q => q.BookId == BookId).FirstOrDefault();
                DeleteTable.BookingMasters.Remove(BookingDetails);
                DeleteTable.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AdminBookme(BookingMaster bookingmaster)
        {
            BhukkadEntities bookingTable = new BhukkadEntities();
            bookingTable.BookingMasters.Add(bookingmaster);
            bookingTable.SaveChanges();
            return RedirectToAction("AdminUpdateBooking");
        }

         public ActionResult AdminLogin()
        {
            return View();
        }

         public ActionResult AdminAboutUs()
        {
            return View();
        }

         public ActionResult AdminChangePassword()
        {
            return View();
        }

         public ActionResult AdminContactUs()
        {
            return View();
        }

         public ActionResult AdminHelp()
        {
            return View();
        }

         public ActionResult AdminShowMe()
        {
            return View();
        }
    }
    }
 