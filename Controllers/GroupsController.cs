using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    [Route("groups")]
    public class GroupsController : Controller
    {
        public IGroupData GroupData { get; }
        public IStudentData StudentData { get; }
        public ITeamData TeamData { get; }

        public GroupsController(IGroupData groupData, IStudentData studentData, ITeamData teamData)
        {
            GroupData = groupData;
            StudentData = studentData;
            TeamData = teamData;
        }

        [Route("list/{groupId:int}", Name = "GroupList")]
        public IActionResult List(int groupId)
        {
            return View(new ListViewModel
            {
                Students = StudentData.GetStudentsByGroupId(groupId),
                Teams = TeamData.GetTeamsByGroupId(groupId),
                GroupId = groupId
            });
        }
        [Route("list/{groupId:int}", Name = "GroupListPost")]
        [HttpPost]
        public IActionResult List(Team team, int groupId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TeamData.AddTeam(team);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("error", ex.Message);
                }

            }
            return RedirectToAction("List", new { groupid = groupId });
        }
    }
}