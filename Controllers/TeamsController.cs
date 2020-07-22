using System;
using System.Collections.Generic;
using System.Linq;
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

        public TeamsController(ITeamData teamData, IActivityData activityData)
        {
            TeamData = teamData;
            ActivityData = activityData;
        }

        [Route("list/{groupId:int}", Name = "TeamsList")]
        public IActionResult List(int groupId)
        {
            
            return View(new TeamListViewModel 
            {
                Teams = TeamData.GetTeamsByGroupId(groupId),
                Activities = ActivityData.GetAllActivities()
            });
        }
    }
}