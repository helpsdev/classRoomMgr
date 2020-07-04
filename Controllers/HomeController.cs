using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    public class HomeController : Controller
    {
        public IClassRoomManagerData ClassRoomManagerData { get; }
        public HomeController(IClassRoomManagerData classRoomManagerData)
        {
            ClassRoomManagerData = classRoomManagerData;
        }

        [Route("/", Name = "Home")]
        public IActionResult Home()
        {
            return View(ClassRoomManagerData.GetAllGroups());
        }
    }
}