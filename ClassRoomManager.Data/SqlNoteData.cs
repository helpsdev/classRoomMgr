using ClassRoomManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class SqlNoteData : INoteData
    {
        public ClassRoomManagerContext ClassRoomManagerDbContext { get; set; }
        public SqlNoteData(ClassRoomManagerContext classRoomManagerContext)
        {
            ClassRoomManagerDbContext = classRoomManagerContext;
        }
        public int AddNote(Note note)
        {
            note.CreationDate =
            note.ModificationDate = DateTimeOffset.Now;
            ClassRoomManagerDbContext.Notes.Add(note);
            return ClassRoomManagerDbContext.SaveChanges();
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return ClassRoomManagerDbContext.Notes
                .Include(n => n.Period)
                .ToList();
        }

        public Note GetNoteById(int noteId)
        {
            return ClassRoomManagerDbContext.Notes
                .Where(n => n.NoteId == noteId)
                .Include(n => n.Student)
                .FirstOrDefault();
        }

        public IEnumerable<Note> GetNotesByGroupId(int groupId)
        {
            return ClassRoomManagerDbContext.Notes
                .Include(n => n.Period)
                .Include(n => n.Student)
                    .ThenInclude(s => s.Group)
                .Where(n => n.Student.Group.GroupId == groupId)
                .ToList();
        }

        public void UpdateNote(Note note)
        {
            ClassRoomManagerDbContext.Notes
                .Update(note);
            ClassRoomManagerDbContext.SaveChanges();
        }

        public IEnumerable<Note> GetNotesByStudentId(int studentId)
        {
            return ClassRoomManagerDbContext.Notes
                .Include(n => n.Student)
                    .ThenInclude(s => s.Group)
                .Where(n => n.StudentId == studentId)
                .ToList();
        }
    }
}
