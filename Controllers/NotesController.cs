using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    [Route("notes")]
    public class NotesController : Controller
    {
        public IClassRoomManagerData ClassRoomManagerData { get; }
        public NotesController(IClassRoomManagerData classRoomManagerData)
        {
            ClassRoomManagerData = classRoomManagerData;
        }

        [Route("edit/{noteId:int}", Name = "EditNoteForm")]
        public IActionResult EditNote(int noteId)
        {
            return View(ClassRoomManagerData.GetNoteById(noteId));
        }

        [Route("edit/{noteId:int}", Name = "EditNote")]
        [HttpPost]
        public IActionResult EditNote(Note note)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClassRoomManagerData.UpdateNote(note);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View(note);
        }

        [Route("{groupId?}", Name = "Notes")]
        public IActionResult Notes(int groupId)
        {
            IEnumerable<Note> notes = groupId == 0
                ? ClassRoomManagerData.GetAllNotes()
                : ClassRoomManagerData.GetNotesByGroupId(groupId);

            var notesViewModel = new NotesViewModel()
            {
                Notes = notes,
                GroupId = groupId
            };

            return View(notesViewModel);
        }
        [Route("create/{groupId:int}", Name = "CreateNoteForm")]
        public IActionResult Create(int groupId)
        {
            var t = new CreateNoteViewModel(
                ClassRoomManagerData.GetStudentsByGroupId(groupId),
                groupId
            );
            return View(t);
        }
        [HttpPost]
        [Route("create/{groupId:int}", Name = "CreateNote")]
        public IActionResult Create(Note note, int groupId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClassRoomManagerData.AddNote(note);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("error", ex.Message);
                }

            }
            return RedirectToAction("Notes", new { groupid = groupId });
        }
    }
}