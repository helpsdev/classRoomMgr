using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.ViewComponents
{
    public class TeamDetailsViewComponent : ViewComponent
    {
        public ITeamData TeamData { get; }
        public TeamDetailsViewComponent(ITeamData teamData)
        {
            TeamData = teamData;
        }

        public IViewComponentResult Invoke(int teamId)
        {
            var team = TeamData.GetTeamById(teamId);
            return View(team);
        }
    }
}
