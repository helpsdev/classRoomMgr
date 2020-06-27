using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.ViewComponents
{
    public class NotesSidebarViewComponent : ViewComponent
    {
        public IClassRoomManagerData ClassRoomManagerData { get; }
        public NotesSidebarViewComponent(IClassRoomManagerData classRoomManagerData)
        {
            ClassRoomManagerData = classRoomManagerData;
        }

        public IViewComponentResult Invoke(int groupId)
        {
            return View(ClassRoomManagerData.GetNotesByGroupId(groupId));
        }
    }
}
