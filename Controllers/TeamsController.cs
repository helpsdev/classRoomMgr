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
    [Route("teams")]
    public class TeamsController : Controller
    {
        public ITeamData TeamData { get; }
        public IActivityData ActivityData { get; }
        public IGroupData GroupData { get; }
        public IStudentData StudentData { get; }

        public TeamsController(ITeamData teamData, IActivityData activityData, IGroupData groupData, IStudentData studentData)
        {
            TeamData = teamData;
            ActivityData = activityData;
            GroupData = groupData;
            StudentData = studentData;
        }

        [Route("list/{groupId:int}", Name = "TeamsList")]
        public IActionResult List(int groupId)
        {
            var teamListVewModel = new TeamListViewModel
            {
                Teams = TeamData.GetTeamsByGroupId(groupId),
                Activities = ActivityData.GetAllActivities(),
                ActivitiesAssigned = ActivityData.GetActivitiesAssignedByGroupId(groupId),
                StudentClassDayList = StudentData.GetStudentClassDaysByDate(DateTimeOffset.Now.Date)

            };
            return View(teamListVewModel);
        }

        [Route("list/{groupId:int}", Name = "TeamListPost")]
        [HttpPost]
        public IActionResult List(int groupId, int activityId)
        {

            if (activityId > 0 && groupId > 0)
            {
                try
                {
                    GroupData.AddActivity(groupId, activityId);
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                }
                
            }

            return RedirectToRoute("TeamsList", new { groupId});
        }

        [Route("details/{groupId:int}", Name = "TeamDetailsPost")]
        [HttpPost]
        public IActionResult Details(int groupId, TeamListViewModel updatedTeamListViewModel)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    
                }
                catch (Exception ex)
                {
                    return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                }

            }

            return RedirectToRoute("TeamsList", new { groupId});
        }
    }
}