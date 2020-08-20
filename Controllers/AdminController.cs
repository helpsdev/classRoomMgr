using System;
using System.Linq;
using System.Net;
using System.Text;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        #region Periods
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

        public IActionResult EditPeriod(int periodId, bool? saveErrors = false)
        {
            try
            {
                var period = PeriodData.GetPeriodById(periodId);

                if (saveErrors.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Hubo un error al guardar la información. Intenta de nuevo, si el problema persiste llama al administrador del sitio.";
                }

                return View(period);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public IActionResult EditPeriod(Period period)
        {
            try
            {
                PeriodData.UpdatePeriod(period);
                
                return View(period);
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(EditPeriod), new {period.PeriodId, saveErrors = true });
            }
        }

        public IActionResult DeletePeriod(int periodId, bool? saveErrors = false)
        {
            try
            {
                var period = PeriodData.GetPeriodById(periodId);

                if (saveErrors.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Hubo un error al guardar la información. Intenta de nuevo, si el problema persiste llama al administrador del sitio.";
                }

                return View(period);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult DeletePeriod(int periodId)
        {
            try
            {
                var periodToDelete = PeriodData.GetPeriodById(periodId);
                if (periodToDelete == null)
                {
                    return NotFound();
                }
                PeriodData.DeletePeriod(periodToDelete);
                return RedirectToAction(nameof(PeriodsList));
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(DeletePeriod), new { periodId, saveErrors = true });
            }
        }

        #endregion

        #region Activities
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(activity);
        }

        public IActionResult EditActivity(int activityId)
        {
            try
            {
                var activity = ActivityData.GetActivityById(activityId);
                return View(activity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

    }
}
