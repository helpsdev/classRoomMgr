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
        public IClassRoomManagerData ClassRoomManagerData { get; }
        public GroupsController(IClassRoomManagerData classRoomManagerData)
        {
            ClassRoomManagerData = classRoomManagerData;
        }

        [Route("list/{groupId:int}", Name = "GroupList")]
        public IActionResult List(int groupId)
        {
            return View(new ListViewModel
            {
                Students = ClassRoomManagerData.GetStudentsByGroupId(groupId),
                Teams = ClassRoomManagerData.GetTeamsByGroupId(groupId),
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
                    ClassRoomManagerData.AddTeam(team);
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