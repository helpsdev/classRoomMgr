using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    public class AdminController : Controller
    {
        public IPeriodData PeriodsData { get; }
        public IActivityData ActivityData { get; }

        public AdminController(IPeriodData periodsData, IActivityData activityData)
        {
            PeriodsData = periodsData;
            ActivityData = activityData;
        }

        public IActionResult Index()
        {
            return View();
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
