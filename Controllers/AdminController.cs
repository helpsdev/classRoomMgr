using System;
using System.Net;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    public class AdminController : Controller
    {
        public IPeriodData PeriodData { get; }
        public IActivityData ActivityData { get; }

        public AdminController(IPeriodData periodData, IActivityData activityData)
        {
            PeriodData = periodData;
            ActivityData = activityData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PeriodsList()
        {
            return View(PeriodData.GetAllPeriods());
        }

        public IActionResult CreatePeriod()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePeriod(Period period)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PeriodData.AddPeriod(period);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(period);
        }

        public IActionResult ActivitiesList()
        {
            return View(ActivityData.GetAllActivities());
        }

        public IActionResult CreateActivity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ActivityData.AddActivity(activity);
                }
                catch (Exception)
                {
                    return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                }
            }
            return RedirectToAction("List");
        }
    }
}
