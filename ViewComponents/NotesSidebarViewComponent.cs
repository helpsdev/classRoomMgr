using ClassRoomManager.Models;
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

        public IViewComponentResult Invoke(int? groupId, int? studentId)
        {
            IEnumerable<Note> notesList = new List<Note>();
            if (groupId.HasValue)
            {
                notesList = NoteData.GetNotesByGroupId(groupId.Value);
            }
            else if (studentId.HasValue)
            {
                notesList = NoteData.GetNotesByStudentId(studentId.Value);
            }
            
            return View(notesList);
        }
    }
}
