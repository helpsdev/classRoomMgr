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
        public INoteData NoteData { get; }
        public IStudentData StudentData { get; }

        public NotesController(INoteData noteData, IStudentData studentData)
        {
            NoteData = noteData;
            StudentData = studentData;
        }

        [Route("edit/{noteId:int}", Name = "EditNoteForm")]
        public IActionResult EditNote(int noteId)
        {
            return View(NoteData.GetNoteById(noteId));
        }

        [Route("edit/{noteId:int}", Name = "EditNote")]
        [HttpPost]
        public IActionResult EditNote(Note note)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    NoteData.UpdateNote(note);
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
                ? NoteData.GetAllNotes()
                : NoteData.GetNotesByGroupId(groupId);

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
                StudentData.GetStudentsByGroupId(groupId),
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
                    NoteData.AddNote(note);
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