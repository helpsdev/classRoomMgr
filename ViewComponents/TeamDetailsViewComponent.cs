﻿using ClassRoomManager.Models;
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
        public IClassRoomManagerData ClassRoomManagerData { get; }
        public TeamDetailsViewComponent(IClassRoomManagerData classRoomManagerData)
        {
            ClassRoomManagerData = classRoomManagerData;
        }

        public IViewComponentResult Invoke(int teamId)
        {
            var items = GetTeamDetails(teamId);
            return View(items);
        }

        private Team GetTeamDetails(int teamId)
        {
            return ClassRoomManagerData.GetTeamById(teamId);
        }
    }
}
