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

        
    }
}