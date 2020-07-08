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
        public INoteData NoteData { get; }
        public NotesSidebarViewComponent(INoteData noteData)
        {
            NoteData = noteData;
        }

        public IViewComponentResult Invoke(int groupId)
        {
            return View(NoteData.GetNotesByGroupId(groupId));
        }
    }
}
