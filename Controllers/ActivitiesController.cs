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
    public class ActivitiesController : Controller
    {
        public IActivityData ActivityData { get; }
        public ActivitiesController(IActivityData activityData)
        {
            ActivityData = activityData;
        }

        public IActionResult List()
        {
            return View(ActivityData.GetAllActivities());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Activity activity)
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