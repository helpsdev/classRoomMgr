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

        [Route("/")]
        public IActionResult Home()
        {
            return View(ClassRoomManagerData.GetAllGroups());
        }

        [Route("groups/{groupId:int}")]
        public IActionResult List(int groupId)
        {
            return View(new ListViewModel 
            {
                Students = ClassRoomManagerData.GetStudentsByGroupId(groupId),
                Teams = ClassRoomManagerData.GetTeamsByGroupId(groupId)
            });
        }
        [Route("groups/{groupId:int}")]
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

        [Route("notes/{groupId?}")]
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
        [Route("notes/create/{groupId:int}", Name = "CreateNoteForm")]
        public IActionResult Create(int groupId)
        {
            var t = new CreateNoteViewModel(
                ClassRoomManagerData.GetStudentsByGroupId(groupId),
                groupId
            );
            return View(t);
        }
        [HttpPost]
        [Route("notes/create/{groupId:int}", Name = "CreateNote")]
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