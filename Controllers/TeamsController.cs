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

        public TeamsController(ITeamData teamData, IActivityData activityData, IGroupData groupData)
        {
            TeamData = teamData;
            ActivityData = activityData;
            GroupData = groupData;
        }

        [Route("list/{groupId:int}", Name = "TeamsList")]
        public IActionResult List(int groupId)
        {
            
            return View(new TeamListViewModel 
            {
                Teams = TeamData.GetTeamsByGroupId(groupId),
                Activities = ActivityData.GetAllActivities(),
                ActivitiesAssigned = ActivityData.GetActivitiesAssignedByGroupId(groupId)
            });
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

            return View(new TeamListViewModel
            {
                Teams = TeamData.GetTeamsByGroupId(groupId),
                Activities = ActivityData.GetAllActivities()
            });
        }
    }
}